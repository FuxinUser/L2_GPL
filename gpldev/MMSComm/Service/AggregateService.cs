using Akka.IO;
using Core.Define;
using Core.Help.DumpRawDataHelp;
using Core.Util;
using DataMod.MMS;
using DataModel.Common;
using DataModel.MES;
using MMSComm.Config;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MMSComm.Service
{
    public class AggregateService
    {

        public MMS_ACK_Structure MMSAck { get; set; }

        public MMSTypeAndLengthDic MsgLengthAndTypeDic { get; set; }

        public SendQueue<ByteString> SndQueue { get; set; }

        public MMS_Heartbeat_Structure MMSHeartBeatMsg { get; set; }

        public AppSetting appSetting { get; set; }

        public IDumpRawData DumpRawData { get; set; }

        public AggregateService()
        {

        }

        public void InitMMSHeartBeatMsg()
        {
            MMSHeartBeatMsg = new MMS_Heartbeat_Structure()
            {
                Header = new MMS_Header_Structure()
                {
                    Length = Marshal.SizeOf<MMS_Heartbeat_Structure>().ToString("0000").ToCByteArray(4),
                    Code = "999999".ToCByteArray(6),
                    SendWho = L2SystemDef.SystemIDCode.ToCByteArray(2),
                    RcvWho = MMSSysDef.SysCode.ToCByteArray(2),
                    FuncCode = MMSSysDef.DataCode.HeartMsg.ToCByteArray(1)
                }
            };
            MMSHeartBeatMsg.End = new byte[] { 0x0A };
        }

        public MMS_ACK_Structure GetNowTimeAckMsg(string msgID, string isAccept)
        {
            MMSAck = new MMS_ACK_Structure()
            {
                Header = new MMS_Header_Structure()
                {
                    Code = msgID.ToCByteArray(6),
                    FuncCode = isAccept.ToCByteArray(1),
                    SendWho = L2SystemDef.SystemIDCode.ToCByteArray(2),
                    RcvWho = MMSSysDef.SysCode.ToCByteArray(2),
                    Length = Marshal.SizeOf<MMS_ACK_Structure>().ToString("0000").ToCByteArray(4),
                    Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8),
                    Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6),
                },
                Control = MsgAnalUtil.GetFixedString("", 80).ToCByteArray(80),
                End = new byte[] { 0x0A }
            };

            return MMSAck;
        }


        public MMS_Heartbeat_Structure GetNowHeartBeatMsg()
        {
            MMSHeartBeatMsg.Header.Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8);
            MMSHeartBeatMsg.Header.Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6);
        
            return MMSHeartBeatMsg;
        }

        public byte[] AddEndTag(byte[] data)
        {
            var container = new List<byte>(data);
            container.Add(0x0A);
            return container.ToArray();
        }

        public byte[] RemoveEndTag(byte[] data)
        {
            var container = new List<byte>(data);
            container.Remove(0x0A);
            return container.ToArray();
        }

        public void DumpRcvRawData(byte[] msg)
        {
            try
            {
                if (appSetting.DumpRcvMsgSwitchOn)
                    DumpRawData.DumpMsg(msg, appSetting.RcvMsgFilePath);
            }
            catch
            {
                throw;
            }
        }

        public void DumpSndRawData(byte[] msg)
        {
            try
            {
                if (appSetting.DumpSndMsgSwitchOn)
                    DumpRawData.DumpMsg(msg, appSetting.SndMsgFilePath);
            }
            catch
            {
                throw;
            }
        }

        public void DumpFailRawData(byte[] msg)
        {
            try
            {
                if (appSetting.DumpFailMsgSwitchOn)
                    DumpRawData.DumpMsg(msg, appSetting.FailMsgFilePath);
            }
            catch
            {
                throw;
            }
        }

        public void DumpDebugRawData(byte[] msg)
        {
            try
            {
                if (appSetting.DumpDebugMsgSwitchOn)
                    DumpRawData.DumpMsg(msg, appSetting.DebugFilePath);
            }
            catch
            {
                throw;
            }
        }
    }
}
