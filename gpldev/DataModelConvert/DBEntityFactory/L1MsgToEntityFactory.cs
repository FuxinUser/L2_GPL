using Core.Define;
using Core.Util;
using System;
using System.Linq;
using static DBService.L1Repository.L1L2MsgDBModel;
using static DBService.L1Repository.L2L1MsgDBModel;
using static MsgStruct.L2L1Snd;

namespace MsgConvert.DBTable
{
    public static class L1MsgToEntityFactory
    {

        public static object ConvertL1DBModel(this object message, string msgID)
        {
            object dbModel = null;

            switch (msgID)
            {
                #region 接收
                case PlcSysDef.RcvMsgCode.L1102PDOInfo:
                    dbModel = FromMessage<L1L2_102>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1104ProcessData:
                    dbModel = FromMessage<L1L2_104>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1105TrackMap:
                    dbModel = FromMessage<L1L2_105>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1106CoilWeldData:
                    dbModel = FromMessage<L1L2_106>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1107GrindRecords:
                    dbModel = FromMessage<L1L2_107>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1108DefectData:
                    dbModel = FromMessage<L1L2_108>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1109BeltAccLength:
                    dbModel = FromMessage<L1L2_109>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1110UpdateWt:
                    dbModel = FromMessage<L1L2_110>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1111LineFault:
                    dbModel = FromMessage<L1L2_111>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1112Utility:
                    dbModel = FromMessage<L1L2_112>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1113BeltChange:
                    dbModel = FromMessage<L1L2_113>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1114CoilMount:
                    dbModel = FromMessage<L1L2_114>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1115CoilUnMount:
                    dbModel = FromMessage<L1L2_115>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1116CoilWeight:
                    dbModel = FromMessage<L1L2_116>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1117CoilSplit:
                    dbModel = FromMessage<L1L2_117>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1118EntryStartCondition:
                    dbModel = FromMessage<L1L2_118>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1119EntryTakeOverStart:
                    dbModel = FromMessage<L1L2_119>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1120EntryTakeOverEndEntry:
                    dbModel = FromMessage<L1L2_120>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1121DeliveryStartCondition:
                    dbModel = FromMessage<L1L2_121>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1122DeliveryTakeOverStartDelivery:
                    dbModel = FromMessage<L1L2_122>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1123DeliveryTakeOverEnd:
                    dbModel = FromMessage<L1L2_123>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1124StripBrakeSignal:
                    dbModel = FromMessage<L1L2_124>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1125ShareCutData:
                    dbModel = FromMessage<L1L2_125>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1126CoilUmountPOR:
                    dbModel = FromMessage<L1L2_126>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1127CoilIDModifyReply:
                    dbModel = FromMessage<L1L2_127>(message);
                    break;

                #endregion

                #region 發送
                case PlcSysDef.SndMsgCode.L1202PDI:
                    dbModel = FromMessage<L2L1_202>(message);
                    break;
                case PlcSysDef.SndMsgCode.L1203EntryScnResult:
                    dbModel = FromMessage<L2L1_203>(message);
                    break;
                case PlcSysDef.SndMsgCode.L1204Preset:
                    dbModel = FromMessage<L2L1_204>(message);
                    break;
                case PlcSysDef.SndMsgCode.L1205Preset:
                    dbModel = FromMessage<L2L1_205>(message);
                    break;
                case PlcSysDef.SndMsgCode.L1206BeltInfoMsg:
                    dbModel = FromMessage<L2L1_206>(message);
                    break;
                case PlcSysDef.SndMsgCode.L1207EntryTakeOverStartCMD:
                    dbModel = FromMessage<L2L1_207>(message);
                    break;
                case PlcSysDef.SndMsgCode.L1208DelveryToCMD:
                    dbModel = FromMessage<L2L1_208>(message);
                    break;
                case PlcSysDef.SndMsgCode.L1209DeliveryScnResult:
                    dbModel = FromMessage<L2L1_209>(message);
                    break;
                case PlcSysDef.SndMsgCode.L1210DelCoil:
                    dbModel = FromMessage<L2L1_210>(message);
                    break;
                case PlcSysDef.SndMsgCode.L1211ModifyCoilID:
                    dbModel = FromMessage<L2L1_211>(message);
                    break;
                    #endregion
            };

            return dbModel;
        }

        public static object ConvertL1MsgModel(this object message, string msgID)
        {
            object msgModel = null;

            switch (msgID)
            {              
                #region 發送
                case PlcSysDef.SndMsgCode.L1202PDI:
                    msgModel = FromDBModel<Msg_202_PDI_TM1>(message);
                    break;
                case PlcSysDef.SndMsgCode.L1203EntryScnResult:
                    msgModel = FromDBModel<Msg_203_PDI_TM2>(message);
                    break;
                case PlcSysDef.SndMsgCode.L1204Preset:
                    msgModel = FromDBModel<Msg_204_PDI_TM3>(message);
                    break;
                case PlcSysDef.SndMsgCode.L1205Preset:
                    msgModel = FromDBModel<Msg_205_PDI_TM3_2>(message);
                    break;
                case PlcSysDef.SndMsgCode.L1206BeltInfoMsg:
                    msgModel = FromDBModel<Msg_206_Belt_Info>(message);
                    break;
                case PlcSysDef.SndMsgCode.L1207EntryTakeOverStartCMD:
                    msgModel = FromDBModel<Msg_207_Entry_Take_Over_Start>(message);
                    break;
                case PlcSysDef.SndMsgCode.L1208DelveryToCMD:
                    msgModel = FromDBModel<Msg_208_Delivery_Take_Over_Start_CM>(message);
                    break;
                case PlcSysDef.SndMsgCode.L1209DeliveryScnResult:
                    msgModel = FromDBModel<Msg_209_Delivery_BC_Confirm>(message);
                    break;
                case PlcSysDef.SndMsgCode.L1210DelCoil:
                    msgModel = FromDBModel<Msg_210_Del_CoilID>(message);
                    break;
                case PlcSysDef.SndMsgCode.L1211ModifyCoilID:
                    msgModel = FromDBModel<Msg_211_Modify_Coil_ID>(message);
                    break;
                    #endregion
            };

            return msgModel;
        }

        /// <summary>
        /// 報文格式轉換成Tbl , 當作儲存Log之用 (速度平均29ns)
        /// </summary>
        public static T FromMessage<T>(object msgStruct, bool containsDateTime = true)
        {
            //產生ORM實例:
            T orm = Activator.CreateInstance<T>();

            var msgKV = msgStruct.GetType().GetFields().ToDictionary(x => x.Name, x => x.GetValue(msgStruct));
            var ormProps = orm.GetType().GetProperties();

            foreach (var property in ormProps)
            {

                try
                {
                    if (msgKV.ContainsKey(property.Name))
                    {
                        var value = msgKV[property.Name];

                        if (value.GetType() == typeof(char[]) && property.PropertyType == typeof(string))
                        {
                            property.SetValue(orm, new string(value as char[]));
                        }

                        else if (value.GetType() == typeof(byte[]))
                        {

                            var byteData = value as byte[];
                            property.SetValue(orm, byteData.ToStr());
                        }

                        else
                        {
                            property.SetValue(orm, value);
                        }
                    }
                }catch(Exception e)
                {

                } 
               
            }


            if (containsDateTime)
            {
                var createTime = orm.GetType().GetProperty("CreateTime");
                if (createTime == null) createTime = orm.GetType().GetProperty("CreateTime");
                if (null != createTime)
                {
                    createTime.SetValue(orm, DateTime.Now);
                }

                var dateTime = orm.GetType().GetProperty("DateTime");
                var dateTimeVal = msgStruct.GetType().GetProperty("DateTime").GetValue(msgStruct);
                dateTime.SetValue(orm, dateTimeVal);
            }
            return orm;
        }

        /// <summary>
        /// Tbl轉報文格式
        /// </summary>
        public static T FromDBModel<T>(object msgStruct)
        {
            //產生ORM實例:
            T orm = Activator.CreateInstance<T>();

            var msgKV = msgStruct.GetType().GetProperties().ToDictionary(x => x.Name, x => x.GetValue(msgStruct));
            var ormFields = orm.GetType().GetFields();

            foreach (var field in ormFields)
            {
                if (msgKV.ContainsKey(field.Name))
                {
                    var value = msgKV[field.Name];

                    if (value.GetType() == typeof(char[]) && field.FieldType == typeof(string))
                    {
                        field.SetValue(orm, new string(value as char[]));
                    }

                    else if (value.GetType() == typeof(byte[]))
                    {

                        var byteData = value as byte[];
                        field.SetValue(orm, byteData.ToStr());
                    }

                    else if(value.GetType() == typeof(string))
                    {
                        var str = value as string;
                        var sizeConst = str.Count();

                        foreach (var NA1 in field.CustomAttributes.ElementAt(0).NamedArguments)
                        {
                            if (NA1.MemberName == "SizeConst")
                            {
                                sizeConst = Convert.ToInt32(NA1.TypedValue.Value);
                                break;
                            }
                        }
                        field.SetValue(orm, str.ToCByteArray(sizeConst));
                    }

                    else
                    {
                        field.SetValue(orm, value);
                    }
                }
            }

            return orm;
        }
    }
}
