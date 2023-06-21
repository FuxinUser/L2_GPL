using Core.Define;
using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.CutReocrd
{
    public class CoilCutRecordEntity
    {     
        public class TBL_Coil_CutRecord : BaseRepositoryModel
        {
            [PrimaryKey]
            public string CoilID { get; set; } = "";
            public int CutMode { get; set; } = 0;
            public double CutLength { get; set; } = 0;
            public double DiamRec { get; set; } = 0;
            public double LengthRec { get; set; } = 0;
            public double CalculateWeightRec { get; set; } = 0;
            [PrimaryKey]
            public DateTime CutTime { get; set; }
        }

    }
}
