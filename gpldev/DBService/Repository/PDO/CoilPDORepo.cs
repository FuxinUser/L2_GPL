using DBService.Base;
using System;

namespace DBService.Repository.PDO
{
    public class CoilPDORepo : BaseRepository<PDOEntity.TBL_PDO>
    {
        protected override string TableName => nameof(PDOEntity.TBL_PDO);

        protected override string[] PKName => new string[] { nameof(PDOEntity.TBL_PDO.Out_Coil_ID), nameof(PDOEntity.TBL_PDO.In_Coil_ID) };

        public CoilPDORepo(string connStr) : base(connStr)
        {
        }

        public int UpdateExitCoilIDChecked(string exitCoilNo, string entryCoilIDChecked)
        {
            var dbObj = new
            {
                Coil_Check_Result = entryCoilIDChecked,

            };      
            return DBContext.Update(TableName, dbObj, $"{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)} = '{exitCoilNo}'");
        }

        // 更新PDO毛重
        public int UpdatePDOCoilGrossWeight(string exitCoilNo, float CoilWeight)
        {
            var dbObj = new
            {
                Out_Coil_Gross_WT = CoilWeight,

            };
            return DBContext.Update(TableName, dbObj, $"{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)} = '{exitCoilNo}'");
        }


        // 更新PDO毛重, 淨重
        public int UpdatePDOCoilWt(string planNo,string exitCoilNo, float coilWt, float coilPureWt)
        {
            var dbObj = new
            {
                out_coil_act_WT = coilPureWt,
                Out_Coil_Gross_WT = coilWt
                //Out_Coil_ID = coilPureWt,
                //Out_Coil_Gross_WT = coilWt

            };
            return DBContext.Update(TableName, dbObj, $"{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)} = '{exitCoilNo}' And { nameof(PDOEntity.TBL_PDO.Plan_No)} = '{planNo}'");
        }

        public int UpdateUploadFlag(string planNo,string exitCoilNo, string upload)
        {
            var dbObj = new
            {
                PDO_Uploaded_Flag = upload,
             
            };
            return DBContext.Update(TableName, dbObj, $"{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)} = '{exitCoilNo}' And {nameof(PDOEntity.TBL_PDO.Plan_No)} = '{planNo}'");
        }

    }
}
