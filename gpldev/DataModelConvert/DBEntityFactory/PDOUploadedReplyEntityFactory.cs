using Core.Define;
using Core.Util;
using DBService.Repository;
using DBService.Repository.PDI;
using MsgStruct;
using System;
using System.Security.Cryptography;

namespace MsgConvert.DBTable
{
    public static class PDOUploadedReplyEntityFactory
    {
        public static PDOUploadedReplyEntity.TBL_PDOUploadedReply ToTblPDOUploadedReply(this MMSL2Rcv.Msg_Res_RcvPDO respdoMsg)
        {
            var tblPDOUploadedReply = new PDOUploadedReplyEntity.TBL_PDOUploadedReply
            {
                Out_Coil_ID = respdoMsg.Respon_Coil_No.ToStr(),
                Plan_No = respdoMsg.Respon_Plan_No.ToStr(),
                Succ_Flag = respdoMsg.Success_Flag.ToStr(),
                Err_Msg = respdoMsg.Error_Reason.ToStr(),
            };
            return tblPDOUploadedReply;
        }
    }
}
