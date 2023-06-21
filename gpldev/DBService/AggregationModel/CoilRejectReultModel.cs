using System;
using System.Collections.Generic;
using static DBService.Repository.CoilRejResultEntity;
using static DBService.Repository.DefectData.CoilDefectDataEntity;
using static DBService.Repository.ReturnCoil.ReturnCoilEntity;

namespace DBService.AggregationModel
{
    [Serializable]
    public class CoilRejectReultModel
    {
        public TBL_RetrunCoil_Temp CoilRejectResult { get; set; }

        public IEnumerable<L3L2_TBL_DefectData> CoilDefects { get; set; }

        public CoilRejectReultModel(TBL_RetrunCoil_Temp coilRejectResult, IEnumerable<L3L2_TBL_DefectData> coilDefects)
        {
            CoilRejectResult = coilRejectResult;
            CoilDefects = coilDefects;
        }

    }
}
