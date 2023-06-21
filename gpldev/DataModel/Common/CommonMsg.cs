using DataModel.MES;
using DataModel.WMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.Common
{
    /// <summary>  
    /// 1.Server內部Actor Send給[SndEdit]的Message的通用格式
    /// 2.[RcvEdit] Send給Server內部Actor的Message的通用格式
    /// 其具有不可變特性
    /// </summary>
    [Serializable]
    public class CommonMsg
    {
    
        public string Message_Length { get; }    //報文長度
        public string Message_Id { get; }    //報文ID
        public byte[] Data { get; }     //報文byte[]資料
        public int ReSndCnt { get; set; } = 0;  // 已重發次數
        public bool IsAck { get; }
          

        public CommonMsg(string length = "", string id = "", byte[] bytes = null, bool isAck = false)
        {
            Message_Length = length;
            Message_Id = id;
            Data = bytes;
            IsAck = isAck;
        }
    }
}