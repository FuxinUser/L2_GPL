using AkkaSysBase.Base;
using Controller;
using Controller.MsgPro;
using Core.Define;
using Core.Util;
using DataModel.Common;
using LogSender;
using MSMQ.Core.MSMQ;
using WMSComm.Model;

namespace WMSComm.Actor
{
    public class WMSRcvEdit : BaseActor
    {
        private AggregateService _agService;

        private IMsgProController _msgProService;       // Msg Process Service
        public WMSRcvEdit(AggregateService agService, IMsgProController msgProService, ILog log) : base(log)
        {
            _agService = agService;
            _msgProService = msgProService;

            _msgProService.SetLog(log);


            Receive<CommonMsg>(message => ProRcvMsg(message));
            ReceiveAny(message => RcvObject(message));

        }


        private void ProRcvMsg(CommonMsg msg)
        {

            //todo: WMs處理解碼
          
            _log.D("接收驗證成功報文", "  message.Length=" + msg.Message_Length);
            _log.D("接收驗證成功報文", "  message.Message_ID=" + msg.Message_Id);

            // 存取DB
            _msgProService.CreateMMSWMSMsg("TBL_WMS_ReceiveRecord", msg);

            var decodedMsg = MsgAnalUtil.RawDeserialize(msg.Data, _agService.MsgLengthAndTypeDic.WMSMsgType[msg.Message_Id]);
            if(decodedMsg == null)
            {
                // Dump File

                // if result==null，表示解碼失敗，可能原因:資料欄位長度不一致 etc..
                _log.E("報文反序列化物件失敗", "  Message decode Fail. please chk program.");
                return;
            }

            
            Router(msg.Message_Id, decodedMsg);
        }

        private void Router(string msgID, object message)
        {
            switch (msgID)
            {
                case WMSSysDef.RcvMsgCode.ProDone:

                    _log.D("WMS動作完成訊息", $"入料/出料/退料完成訊息=>{msgID}");
                    MQPoolService.SendToTrk(InfoTrk.WMSActionFinish.Data(message));
                    
                    break;

             
                case WMSSysDef.RcvMsgCode.ProResRequest:

                    _log.D("WMS動作要求回復訊息", $"入料/出料/退料要求回復訊息=>{msgID}");
                    MQPoolService.SendToTrk(InfoTrk.WMSCoilProResRequest.Data(message));

                    break;
            }
        }

        private void RcvObject(object msg)
        {
            _log.E("AThread接收資料-RcvObject", $"無法解析資料! Type:{msg.GetType()} From Sender:{Sender.Path}");
        }
    }
}
