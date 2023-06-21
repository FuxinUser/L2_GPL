using BLL;
using Core.Define;
using Core.Util;
using DBService;
using LogSender;
using MsgConvert.DBTable;
using System;
using static Core.Define.DBParaDef.ConnectionSysDef;

namespace Controller.Sys
{
    public class SysController : ISysController
    {
        //private LogSnd _log;
        private SystemSetLogic _sysLogic;
        private ILog _log;

        public SysController()
        {
            _sysLogic = new SystemSetLogic();
        }

        public void SetLog(ILog log)
        {
            _log = log;
        }

        public void UpdateL1LastAliveTime(string time)
        {
            int updateNum = 0;

            try
            {
                updateNum = _sysLogic.UpdateSysValue(L2SystemDef.GPLGroup, DBParaDef.SysParaL1AliveLastTime, time);
            }
            catch(Exception e)
            {
                _log.E("更新L1 Alive最後時間失敗", TypeConvertUtil.CleanInvalidChar(e.Message));
            }

            if (updateNum > 0)
                _log.I("更新L1 Alive最後時間", $"更新成功，時間為{time}");
            else
                _log.E("更新L1 Alive最後時間", $"更新失敗，時間為{time}");
        }


        public bool UpdateSysValue(string parameterGroup, string parameter, string value)
        {
            try
            {
                _log.I($"更新{parameterGroup}  {parameter} 成功", $"更新Value=>{value}");
                var updateNum = _sysLogic.UpdateSysValue(parameterGroup, parameter, value);
                return updateNum > 0;
            }
            catch (Exception e)
            {
                _log.E($"更新{parameterGroup}  {parameter} 失敗", e.Message.CleanInvalidChar());
                return false;
            }
        }


        public int GetSysProCoilWeight()
        {
            try
            {
                var weight = _sysLogic.GetSysValue(L2SystemDef.GPLGroup, DBParaDef.SysParaCoilWeight);

                return Int32.Parse(weight);

            }
            catch (Exception e)
            {
                _log.E("撈取系統鋼卷成捲重量設定值失敗", TypeConvertUtil.CleanInvalidChar(e.Message));
                return 0;
            }
        }


        public bool UpdateConnectionStatuts(ConnectionType type, string ip, string port, string connectionStatuts)
        {
            //return true;
            var tb = EntityFactory.ToTblConnectionStatusEntity(type, ip, port, connectionStatuts);
            int insertOrUpdateNum = 0;

            try
            {
                insertOrUpdateNum = _sysLogic.UpdateConnectionStatuts(tb);
                _log?.I($"更新連線狀態=>{insertOrUpdateNum > 0}", $"{tb.Connection_From}-{tb.Connection_To} IP:{ip}:{port} Statuts:{connectionStatuts}");


                //var entity = type.ToL2L25L1L2DisConnection(connectionStatuts);
                //insertOrUpdateNum = _sysLogic.CreateL2DisConnectionRepo(entity);
                //_log?.I($"新增L25連線狀態資料庫=>{insertOrUpdateNum > 0}", $"{tb.Connection_From}-{tb.Connection_To} IP:{ip}:{port} Statuts:{connectionStatuts}");

                return insertOrUpdateNum > 0;
            }
            catch (Exception e)
            {
                _log?.E($"更新連線狀態失敗", e.Message.CleanInvalidChar());
                return false;
            }

        }
    }
}
