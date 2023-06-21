using DBService.Base;
using DBService.Repository.ReturnCoil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBService.Repository.ReturnCoilTemp
{
    public class ReturnCoilTempRepo : BaseRepository<ReturnCoilEntity.TBL_RetrunCoil_Temp>
    {

        public ReturnCoilTempRepo(string connStr) : base(connStr)
        {
        }
        protected override string TableName => nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp);

        protected override string[] PKName => new string[] { nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Reject_Coil_No) };


    }
}
