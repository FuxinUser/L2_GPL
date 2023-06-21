using AkkaSysBase.Base;
using Controller;
using Controller.MsgPro;
using Core.Define;
using Core.Util;
using DataModel.Common;
using LogSender;
using MsgStruct;
using MSMQ.Core.MSMQ;

namespace MMSComm.Actor
{
    public class MMSRcvEdit : BaseActor
    {

        private IMsgProController _msgProService;       // Msg Process Service

        public MMSRcvEdit(IMsgProController msgProService, ILog log) : base(log)
        {
            _msgProService = msgProService;
            _msgProService.SetLog(log);
            Receive<CommonMsg>(message => TryFlow(() => ProRcvMsg(message)));
            ReceiveAny(message => RcvObject(message));

        }


        private void ProRcvMsg(CommonMsg message)
        {
            // 存取DB
            _msgProService.CreateMMSWMSMsg("TBL_MMS_ReceiveRecord", message);

            // MMG104:接收過度捲
            if (message.Message_Id.Equals(MMSSysDef.RcvMsgCode.CoilDummyCode))
            {

                var rcvMsg = MsgAnalUtil.RawDeserialize(message.Data,
                                                             typeof(MMSL2Rcv.Msg_Dummy_Coil_List))
                                                             as MMSL2Rcv.Msg_Dummy_Coil_List;
                _log.I($"接收過度捲資料:{rcvMsg.MsgID}", $"【鋼捲】:{rcvMsg.CoilNoID}, 【Code】:{rcvMsg.GradeCode}");             
                MQPoolService.SendToCoil(InfoCoil.SaveDummyCoil.Data(rcvMsg));
                return;
            }

            // MMG101:接收排程電文G1MM03  
            if (message.Message_Id.Contains(MMSSysDef.RcvMsgCode.CoilScheduleCode))
            {

                var rcvMsg = MsgAnalUtil.RawDeserialize(message.Data,
                                                       typeof(MMSL2Rcv.Msg_Coil_Schedule))
                                                       as MMSL2Rcv.Msg_Coil_Schedule;

                _log.I($"鋼捲排程接收:{rcvMsg.MsgID}", $"接收資料{rcvMsg.CoilCount}筆鋼捲排程資料");
                MQPoolService.SendToCoil(InfoCoil.SaveSchedule.Data(rcvMsg));
                return;
            }

            // MMG102:接收PDI
            if (message.Message_Id.Equals(MMSSysDef.RcvMsgCode.CoilPDICode))
            {
                var rcvMsg = MsgAnalUtil.RawDeserialize(message.Data,
                                                            typeof(MMSL2Rcv.Msg_PDI))
                                                            as MMSL2Rcv.Msg_PDI;

                _log.I($"接收鋼捲PDI:{rcvMsg.MsgID}", $"【鋼捲】:{rcvMsg.EntryCoilNo},【計畫】:{rcvMsg.PlanNo}");
                MQPoolService.SendToCoil(InfoCoil.SaveCoilPDI.Data(rcvMsg));
                return;
            }

            // MMG103:作業計畫刪除請求
            if (message.Message_Id.Contains(MMSSysDef.RcvMsgCode.ReqDeletePlanNoCode))
            {
                var rcvMsg = MsgAnalUtil.RawDeserialize(
                                                            message.Data,
                                                            typeof(MMSL2Rcv.Msg_Req_Delete_Schedule_Plan))
                                                            as MMSL2Rcv.Msg_Req_Delete_Schedule_Plan;

                _log.I($"PDI整計畫刪除:{rcvMsg.MsgID}", $"計畫號{rcvMsg.PlanNo}刪除要求");
                MQPoolService.SendToCoil(InfoCoil.DeleteShcedPlanNo.Data(rcvMsg));

                return;
            }

            // MMG106:三級要求PDO
            if (message.Message_Id.Contains(MMSSysDef.RcvMsgCode.ReqProResultCode))
            {
                var rcvMsg = MsgAnalUtil.RawDeserialize(message.Data,
                                                            typeof(MMSL2Rcv.Msg_Product_Result_Request))
                                                            as MMSL2Rcv.Msg_Product_Result_Request;

                _log.I($"要求生產實績(PDO):{rcvMsg.MsgID}", $"要求鋼捲{rcvMsg.CoilNoID}PDO資料");
                MQPoolService.SendToCoil(InfoCoil.ReqPDO.Data(rcvMsg));
                return;
            }

            // MMG107:接收鋼捲刪除/回退回應
            if (message.Message_Id.Contains(MMSSysDef.RcvMsgCode.CoilRejectResultCode))
            {
                var rcvMsg = MsgAnalUtil.RawDeserialize(message.Data,
                                                            typeof(MMSL2Rcv.Msg_Res_For_Coil_Reject_Result))
                                                            as MMSL2Rcv.Msg_Res_For_Coil_Reject_Result;

                _log.I($"鋼捲刪除/回退回應接收:{rcvMsg.MsgID}", $"接收鋼捲{rcvMsg.RequestedCoilNo}回退回應, 處理結果{rcvMsg.ProcessResult}, 原因{rcvMsg.RejectCause}");
                MQPoolService.SendToCoil(InfoCoil.CoilRejectResult.Data(rcvMsg));
                return;
            }

            // MMG108:無鋼捲生產命令回覆
            if (message.Message_Id.Contains(MMSSysDef.RcvMsgCode.ResForNoCoilCode))
            {
                var rcvMsg = MsgAnalUtil.RawDeserialize(message.Data,
                                                            typeof(MMSL2Rcv.Msg_Res_For_No_Coil_Schedule))
                                                            as MMSL2Rcv.Msg_Res_For_No_Coil_Schedule;

                _log.I($"無鋼捲生產命令回覆:{rcvMsg.Code.ToStr()}", $"無鋼捲生產命令回覆:{rcvMsg.Mat_No.ToStr()}");
                MQPoolService.SendToCoil(InfoCoil.ResNoCoil.Data(rcvMsg));
                return;
            }

            // MMG109:無鋼捲PDI回覆
            if (message.Message_Id.Contains(MMSSysDef.RcvMsgCode.ResForNoCoilPDICode))
            {
                var rcvMsg = MsgAnalUtil.RawDeserialize(message.Data,
                                                            typeof(MMSL2Rcv.Msg_Res_For_No_Coil_PDI))
                                                            as MMSL2Rcv.Msg_Res_For_No_Coil_PDI;

                _log.I($"無鋼捲PDI回覆接收:{rcvMsg.Code.ToStr()}", $"無鋼捲PDI回覆接收:{rcvMsg.Mat_No.ToStr()}");
                MQPoolService.SendToCoil(InfoCoil.ResNoPDI.Data(rcvMsg));
                return;
            }


            //MMG110：反饋PDO是否處理成功   added 2023.04.25
            if (message.Message_Id.Contains(MMSSysDef.RcvMsgCode.ResRcvPDO))
            {
                var rcvMsg = MsgAnalUtil.RawDeserialize(message.Data,
                                                             typeof(MMSL2Rcv.Msg_Res_RcvPDO))
                                                             as MMSL2Rcv.Msg_Res_RcvPDO;

                _log.I($"回應是否成功接收PDO訊息:{rcvMsg.Code.ToStr()}", $"接收(1)/拒絕(0):{rcvMsg.Success_Flag.ToStr()}");
                MQPoolService.SendToCoil(InfoCoil.ResponRcvPDO.Data(rcvMsg));
                return;
            }


            // MMG115:套筒静态数据同步
            if (message.Message_Id.Contains(MMSSysDef.RcvMsgCode.SleeveValueSynCode))
            {
                var rcvMsg = MsgAnalUtil.RawDeserialize(message.Data,
                                                            typeof(MMSL2Rcv.Msg_Sleeve_Value_Synchronize))
                                                            as MMSL2Rcv.Msg_Sleeve_Value_Synchronize;

                _log.I($"套筒資料同步訊息:{rcvMsg.Code.ToStr()}", $"CODE:{rcvMsg.SleeveCode.ToStr()}");
                MQPoolService.SendToCoil(InfoCoil.SyncSleeveValue.Data(rcvMsg));
                return;
            }

            // MMG116:垫纸静态数据同步
            if (message.Message_Id.Contains(MMSSysDef.RcvMsgCode.PaperValueSyncCode))
            {
                var rcvMsg = MsgAnalUtil.RawDeserialize(message.Data,
                                                            typeof(MMSL2Rcv.Msg_Paper_Value_Synchronize))
                                                            as MMSL2Rcv.Msg_Paper_Value_Synchronize;

                _log.I($"墊紙資料同步訊息:{rcvMsg.Code.ToStr()}", $"CODE:{rcvMsg.PaperCode.ToStr()}");
                _log.D("墊紙資料同步訊息", JsonUtil.ToJson(rcvMsg));
                MQPoolService.SendToCoil(InfoCoil.SyncPaperValue.Data(rcvMsg));
                return;
            }



            // 過渡捲刪除回應

            if (message.Message_Id.Contains(MMSSysDef.RcvMsgCode.CoilDummyResCode))
            {
                var rcvMsg = MsgAnalUtil.RawDeserialize(message.Data,
                                                            typeof(MMSL2Rcv.Msg_Res_of_Dummy_Coil_List_Req))
                                                            as MMSL2Rcv.Msg_Res_of_Dummy_Coil_List_Req;

                _log.I($"過渡捲刪除回應:{rcvMsg.Code.ToStr()}", $"結果:{rcvMsg.Dummy_Deal_Reult.ToStr()}");
                _log.D("過渡捲刪除回應", JsonUtil.ToJson(rcvMsg));
                MQPoolService.SendToCoil(InfoCoil.InfoDelDummyResult.Data(rcvMsg));
                return;
            }



        }

        private void RcvObject(object msg)
        {
            _log.E("AThread接收資料-RcvObject", $"無法解析資料! Type:{msg.GetType()} From Sender:{Sender.Path}");
        }

    }
}
