using Core.Define;
using DBService.Level25Repository.L2L25_L1L2DisConnection;
using DBService.Repository.ConnectionStatus;
using DBService.Repository.SystemSetting;
using System;
using static DBService.Repository.ConnectionStatus.ConnectionStatusEntity;

namespace BLL
{
    public class SystemSetLogic
    {
        private SystemSettingRepo _systemSettingRepo;
        private ConnectionStatusRepo _connectionStatusRepo;
        private L2L25_L1L2DisConnectionRepo _l2l25_l2DisConnectionRepo;

        public SystemSetLogic()
        {
            _systemSettingRepo = new SystemSettingRepo(DBParaDef.DBConn);
            _connectionStatusRepo = new ConnectionStatusRepo(DBParaDef.DBConn);

            _l2l25_l2DisConnectionRepo = new L2L25_L1L2DisConnectionRepo(DBParaDef.Level2_5DBConn);
        }

       
        public bool VaildSystemAutoInputOn(string parameterGroup, string parameter)
        {
          
            try
            {
                var setting = _systemSettingRepo.Get(new string[] { parameterGroup, parameter });

                return setting.Value.Equals(L2SystemDef.AutoInputOn);
         

            }
            catch
            {
                throw;

            }

           
        }
        public int UpdateSysValue(string parameterGroup, string parameter, string value)
        {

            var sysSet = new SystemSettingEntity.TBL_SystemSetting
            {
                Value = value,
            };
            try
            {
               return  _systemSettingRepo.Update(sysSet, new string[] { parameterGroup, parameter });
            }
            catch
            {
                throw;
            }

        }

        public string GetSysValue(string parameterGroup, string parameter)
        {
            try
            {
                var setting = _systemSettingRepo.Get(new string[] { parameterGroup, parameter });
                return setting.Value;
            }
            catch
            {
                throw;
            }
        }

        public int UpdateConnectionStatuts(TBL_ConnectionStatus entity)
        {
            try
            {
                entity.Create_DateTime = DateTime.Now;
                return _connectionStatusRepo.Update(entity, new string[] { entity.Connection_From, entity.Connection_To });
            }
            catch
            {
                throw;
            }
        }

        public int CreateL2DisConnectionRepo(L2L25_L1L2DisConnection entity)
        {
            try
            {

                return _l2l25_l2DisConnectionRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
    }
}
