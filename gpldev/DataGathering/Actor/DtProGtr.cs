using Akka.Actor;
using AkkaSysBase;
using AkkaSysBase.Base;
using Controller.Coil;
using Controller.DtGtr;
using Core.Define;
using LogSender;
using MSMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Define.L25SysDef;
using static DataModel.HMIServerCom.Msg.SCCommMsg;

namespace DataGathering.Actor
{
    public class DtProGtr : BaseActor
    {
        private IActorRef _selfActor;
        private ICoilController _coilController;
        private IDataGatheringController _dtgtrController;
        private ICancelable _tmrl25Alive;


        public DtProGtr(ISysAkkaManager akkaManager,
                      IDataGatheringController dtgtrController,
                      ICoilController coilController,
                      ILog log) : base(log)
        {
            _selfActor = akkaManager.GetActor(GetType().Name);

            _dtgtrController = dtgtrController;
            _coilController = coilController;

            _dtgtrController.SetLog(log);
            _coilController.SetLog(log);

            StartTmr(1, EventDef.CMDSET.L25Alive, _tmrl25Alive);

            MQPool.GetMQ(nameof(DtProGtr)).Receive(x =>
            {
                var msg = (x as MQPool.MQMessage).Data;
                _selfActor.Tell(msg);
            });


            // 上傳PDO給MMS-紀錄生產資訊
            //Receive<string>(message => TryFlow(() => ProCmd(message)));
            //Receive<CS06_SendMMSPDO>(message => TryFlow(() => CreateProDataToL25(message)));
            Receive<EventDef.CMDSET>(msg => TryFlow(() => ProEventCmd(msg)));
            Receive<CS06_SendMMSPDO>(message => TryFlow(() => CreatePDODataToL25(message)));

            //測試
            //var testmsg = new CS06_SendMMSPDO();
            //testmsg.Coil_ID = "CG220127570000";
            //testmsg.Plan_No = "GP280135";
            //CreatePDODataToL25(testmsg);

        }

        private void CreatePDODataToL25(CS06_SendMMSPDO msg)
        {
            _log.I("新增2.5生產資料", $"新增{msg.Coil_ID},{msg.Plan_No}資料至L25");
            var pdo = _coilController.GetPDOonly(msg.Plan_No,msg.Coil_ID);
            
            //process data
            var processData = _dtgtrController.QueryProcessDatas(pdo.Start_Time, pdo.Finish_Time);
            
            if (processData != null)
            {
             var data = _dtgtrController.CalculateProcessData(processData);
             if (data != null)
             {
                //連續值資料寫入Table
                _log.I("新增L2.5生產資料", "ProcessData");
                var insertOK = _dtgtrController.Create25ProcessCTData(pdo, data);
                if (!insertOK)
                    _log.I("新增L2.5生產資料失敗", "ProcessData");
             }
            }

            //研磨 頭中尾各道次統計
            var GrindData_Total = _dtgtrController.QueryGrindDatas_Total(pdo.In_Coil_ID, msg.Plan_No).ToList().OrderByDescending(x => x.Current_Pass);
            _log.I("撈取頭中尾與各道次研磨生產資料", $"資料筆數Total：{GrindData_Total.Count().ToString()}");
            var HeadPassNum = GrindData_Total.Where(x => x.Current_Senssion.Equals(DBParaDef.GrindRecordSession_H))
                                                   .Select(v => v.Current_Pass).FirstOrDefault();
            var MidPassNum = GrindData_Total.Where(x => x.Current_Senssion.Equals(DBParaDef.GrindRecordSession_M))
                                                   .Select(v => v.Current_Pass).FirstOrDefault();
            var TailPassNum = GrindData_Total.Where(x => x.Current_Senssion.Equals(DBParaDef.GrindRecordSession_T))
                                                   .Select(v => v.Current_Pass).FirstOrDefault();

            //部位+道次数
            if (HeadPassNum > 0)  //头段  Session = 1
            {
                for (int PassNum = 1; PassNum <= HeadPassNum; PassNum++) 
                {
                    var GrindData = _dtgtrController.QueryGrindDatas(pdo.In_Coil_ID, msg.Plan_No, PassNum, 1);
                    //Grind data计算
                    var GData = _dtgtrController.CalculateGrindData(GrindData);
                    if (GData != null)
                    {
                        //連續值資料寫入Table
                        _log.I("新增L2.5生產資料", $"GrindData 部位：头部,道次：{PassNum.ToString()}");
                        _log.I("新增L2.5生產資料", $"GrindData 資料筆數：{GrindData.ToList().Count.ToString()},長度：{GData.TotalLength}");
                        var insertOK = _dtgtrController.Create25GrindData(pdo, GData, PassNum,1);

                        if (!insertOK)
                            _log.I("新增L2.5生產資料失敗", "GrindData");
                    }
                }
            };

            if (MidPassNum > 0)  //中段 Session = 2
            {
                for (int PassNum = 1; PassNum <= MidPassNum; PassNum++)
                {
                    var GrindData = _dtgtrController.QueryGrindDatas(pdo.In_Coil_ID, msg.Plan_No, PassNum, 2);
                    //Grind data计算
                    var GData = _dtgtrController.CalculateGrindData(GrindData);
                    if (GData != null)
                    {
                        //連續值資料寫入Table
                        _log.I("新增L2.5生產資料", $"GrindData 部位：中部,道次：{PassNum.ToString()}");
                        _log.I("新增L2.5生產資料", $"GrindData 資料筆數：{GrindData.ToList().Count.ToString()},長度：{GData.TotalLength}");
                        var insertOK = _dtgtrController.Create25GrindData(pdo, GData, PassNum, 2);

                        if (!insertOK)
                            _log.I("新增L2.5生產資料失敗", "GrindData");
                    }
                }
            };
            if (TailPassNum > 0) //尾段 Session = 3
            {
                for (int PassNum = 1; PassNum <= TailPassNum; PassNum++)
                {
                    var GrindData = _dtgtrController.QueryGrindDatas(pdo.In_Coil_ID, msg.Plan_No, PassNum, 3);
                    //Grind data计算
                    var GData = _dtgtrController.CalculateGrindData(GrindData);
                    if (GData != null)
                    {
                        //連續值資料寫入Table
                        _log.I("新增L2.5生產資料", $"GrindData 部位：尾部,道次：{PassNum.ToString()}");
                        _log.I("新增L2.5生產資料", $"GrindData 資料筆數：{GrindData.ToList().Count.ToString()},長度：{GData.TotalLength}");
                        var insertOK = _dtgtrController.Create25GrindData(pdo, GData, PassNum, 3);

                        if (!insertOK)
                            _log.I("新增L2.5生產資料失敗", "GrindData");
                    }
                }
            };




            ////Grind 研磨數據
            //if (GData != null)
            //{
            //    //連續值資料寫入Table
            //    _log.I("新增L2.5生產資料", "GrindData");
            //    var insertOK = _dtgtrController.Create25GrindData(pdo, GData);

            //    if (!insertOK)
            //        _log.I("新增L2.5生產資料失敗", "GrindData");
            //}

        }
        // Scan觸發Pro
        private void ProEventCmd(EventDef.CMDSET msg)
        {

            switch (msg)
            {
                case EventDef.CMDSET.L25Alive:
                    _dtgtrController.CreateL25Alive();
                    break;

            }
        }


        /// <summary>
        /// 開始Scan
        /// </summary>
        private void StartTmr(int second, object message, ICancelable timer)
        {
            timer?.Cancel();

            var interval = TimeSpan.FromSeconds(second);

            timer = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(interval, interval, Self, message, Self);
        }
    }
}
