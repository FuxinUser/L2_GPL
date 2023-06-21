using System;
using System.Data;
using BLL.Coil;
using Core.Define;
using Core.Util;
using DataMod.PLC;
using DBService;
using DBService.Repository;
using DBService.Repository.Belt;
using DBService.Repository.BeltPatterns;
using DBService.Repository.PDI;
using DBService.Repository.PDO;
using MsgConvert.DBTable;
using MsgStruct;
using System.Collections.Generic;
using System.Linq;
using DBService.Repository.GrindPlanHistory;
using DBService.Repository.GrindPlan;
using static DBService.Repository.PDO.PDOEntity;
using static DBService.Repository.LookupTblPaper.LkUpTablePaperEntity;
using static DBService.Repository.LookupTblSleeve.LkUpTableSleeveEntity;
using static DBService.Repository.MaterialGrade.MaterialGradeEntity;
using static DBService.Repository.BeltPatterns.BeltPatternsEntity;
using static DBService.Repository.PDI.PDIEntity;
using static DBService.Repository.DefectData.CoilDefectDataEntity;
using LogSender;
using System.Threading;
using DBService.Repository.ReturnCoil;
using static DBService.Repository.CoilScheduleEntity;
using static DBService.Repository.CoilMapEntity;
using static MsgStruct.MMSL2Rcv;

/**
 * Author: ICSC Spyua
 * Date : 2019/12/31
 * Desc : Coil API (Controller)
 **/

namespace Controller.Coil
{
    public class CoilController : ICoilController
    {
       
        private CoilProLogic _coilLogic;

        private ILog _log;

        public CoilController()
        {
            _coilLogic = new CoilProLogic();
        }
                   
        public void SetLog(ILog log)
        {
            _log = log;
        }

        #region -- PDI相關 --
        public bool VaildHasPDI(string coilNo)
        {
            try
            {
                var hasPDI = _coilLogic.VaildHasPDI(coilNo);
                _log.I("是否有PDI查詢", $"【鋼卷】: {coilNo}{hasPDI.ToHasStr()}PDI");
                return hasPDI;
            }
            catch (Exception e)
            {
                _log.E("是否有PDI查詢", TypeConvertUtil.CleanInvalidChar(e.Message));
                return false;
            }

        }
        public bool VaildHasPDIandPlanNo(string coilNo, string planNo)
        {
        
            try
            {
                var hasPDI = _coilLogic.VaildHasPDIandPlanNo(coilNo, planNo);
                _log.I("是否有PDI查詢", $"【鋼卷】: {coilNo} 計畫號: {planNo} {hasPDI.ToHasStr()}");
                return hasPDI;
            }
            catch (Exception e)
            {
                _log.E("是否有PDI查詢", TypeConvertUtil.CleanInvalidChar(e.Message));
                return false;
            }

        }
        public bool VaildHasDummy(string coilNo)
        {
            try
            {
                var hasPDI = _coilLogic.VaildHasDummy(coilNo);
                _log.I("是否有Dummy查詢", $"【DummyCoil】: {coilNo}{hasPDI.ToHasStr()}");
                return hasPDI;
            }
            catch (Exception e)
            {
                _log.E("是否有Dummy查詢", TypeConvertUtil.CleanInvalidChar(e.Message));
                return false;
            }

        }

        public int UpdatePDI(string coilNo, MMSL2Rcv.Msg_PDI mmsPDI)
        {
            int updateNum = 0;

            try
            {
                var entity = mmsPDI.ToTblCoilPDI();
                updateNum = _coilLogic.UpdatePDI(coilNo, entity);
                _log.I($"接收鋼捲PDI:{mmsPDI.MsgID}", $"【鋼捲】:{coilNo} 更新PDI{(updateNum>0).ToStr()}");
            }
            catch (Exception e)
            {
                _log.E($"接收鋼捲PDI:{mmsPDI.MsgID}", $"【鋼捲】:{coilNo}更新失敗:" + e.Message.CleanInvalidChar());
            }

            return updateNum;
        }
        public void UpdateDummyPDI(string coilNo, PDOEntity.TBL_PDO pdo)
        {
            int updateNum = 0;
            try
            {
                updateNum = _coilLogic.UpdateDummy(coilNo,pdo);
                _log.I($"更新DummyPDI", $"【鋼捲】:{coilNo} 更新{(updateNum > 0).ToStr()}");
            }
            catch (Exception e)
            {
                _log.E($"更新DummyPDI", $"【鋼捲】:{coilNo}更新失敗:" + e.Message.CleanInvalidChar());
            }

            
        }
        public bool DeleteDummy(string coilNo)
        {
            bool DeleteNum = false;
            try
            {
                 DeleteNum = _coilLogic.DeleteDummy(coilNo);
                _log.I($"刪除Dummy", $"【鋼捲】:{coilNo} 刪除{DeleteNum.ToString()}");
            }
            catch (Exception e)
            {
                _log.E($"刪除Dummy", $"【鋼捲】:{coilNo}更新失敗:" + e.Message.CleanInvalidChar());
            }

            return DeleteNum;
        }

            public int CreatePDI(MMSL2Rcv.Msg_PDI mmsPDI)
        {

            int insertNum = 0;

            try
            {
                var entity = mmsPDI.ToTblCoilPDI();
                insertNum = _coilLogic.CreatePDI(entity);
                _log.I($"接收鋼捲PDI:{mmsPDI.MsgID}", $"【鋼捲】:{mmsPDI.In_Mat_No.ToStr().Trim()}新增PDI{(insertNum > 0).ToStr()}");
            }
            catch (Exception e)
            {
                _log.E($"接收鋼捲PDI:{mmsPDI.MsgID}", $"【鋼捲】:{mmsPDI.In_Mat_No.ToStr().Trim()}新增PDI失敗:"+e.Message.CleanInvalidChar());
            }

            return insertNum;

        }

        public bool CreateL25PDI(TBL_PDI dao)
        {
            dao.VaildObjectNull("msg", "存取L25PDI錯誤");
            try
            {
                var entity = dao.ToL25CoilPDIEntity();
                var insertNum = _coilLogic.CreateL25PDI(entity);
                var insertOK = insertNum > 0;
                _log.I($"新增鋼捲PDI至L25", $"【鋼捲】:{entity.In_Coil_ID.Trim()}新增PDI{(insertOK).ToStr()}");
                return insertOK;
                //return true;

                //insertNum = _coilLogic.CreateL25PDIHis(tb);
                //insertOK = insertNum > 0;
                //_log.I($"新增鋼捲PDI至L25歷史資料庫:{msg.EntryCoilNo}", $"【鋼捲】:{msg.EntryCoilNo.Trim()}新增PDI{(insertOK).ToStr()}");


            }
            catch (Exception e)
            {
                _log.E($"新增鋼捲PDI至L25錯誤:{dao.In_Coil_ID}", e.ToString().CleanInvalidChar());
                return false;
            }
        }


        public string GetPDIPlanNo(string coilNo)
        {
            try
            {
                var planNo = _coilLogic.GetPDIPlanNoByEntryCoilID(coilNo);
                _log.I("撈取PDI的PlanNo", $"撈取{coilNo}PDI Plan No => {planNo != null}");
                return planNo != null ? planNo : "";
            }
            catch (Exception e)
            {
                _log.E("撈取PDI的PlanNo失敗", $"撈取{coilNo}PDI Plan No失敗");
                _log.E("撈取PDI的PlanNo失敗", TypeConvertUtil.CleanInvalidChar(e.Message));
                return "";
            }
        }

       
        public void UpdatePDIScrapedWeight(float coilWeight, string coilNo)
        {
            try
            {
                var updateNum = _coilLogic.UpdatePDIScrapedWeight(coilWeight, coilNo);
                _log.I("存取PDI廢料重量", $"存取{coilNo}廢料重量{coilWeight}=>{updateNum>0}");              
            }
            catch (Exception e)
            {
                _log.E("存取PDI廢料重量", e.Message);
            } 
        }

        public bool UpdateEntryScanCoilInfo(string coilNo, bool entryCoilIDChecked)
        {

            try
            {
                var updateNum = _coilLogic.UpdateEntryScanCoilInfo(coilNo, entryCoilIDChecked);
                _log.I("更新PDI CoilCheck", $"{coilNo} 更新PDI Entry Coil Checked : {entryCoilIDChecked} => {updateNum > 0}");
                return updateNum > 0;

            }
            catch (Exception e)
            {
                _log.E("更新PDI CoilCheck 失敗", TypeConvertUtil.CleanInvalidChar(e.Message));
                return false;
            }
        }

        public PDIEntity.TBL_PDI GetPDI(string coilNo)
        {

            try
            {              
                var pdi = _coilLogic.GetPDI(coilNo);
                _log.I($"撈取PDI", $"撈取鋼卷{coilNo}PDI資料 => {pdi != null}");
                return pdi;
            }
            catch (Exception e)
            {
                _log.E("撈取PDI", e.Message.CleanInvalidChar());
                return null;
            }
        }
        public PDIEntity.TBL_PDI GetPDIOnly(string PlanNo,string InCoilNo)
        {

            try
            {
                var pdi = _coilLogic.GetPDIOnly(PlanNo, InCoilNo);
                _log.I($"撈取PDI", $"撈取鋼卷{PlanNo},{InCoilNo}PDI資料 => {pdi != null}");
                return pdi;
            }
            catch (Exception e)
            {
                _log.E("撈取PDI", e.Message.CleanInvalidChar());
                return null;
            }
        }

        public void UpdatePDIStarTime(string coilIDNo)
        {
            try
            {
                var updateNum = _coilLogic.UpdatePDIStarTime(coilIDNo);
                _log.I("更新鋼捲生產開始時間", $"鋼捲{coilIDNo} 紀錄生產時間 => {updateNum>0}");
            }
            catch (Exception e)
            {
                _log.E("更新鋼捲生產開始時間", TypeConvertUtil.CleanInvalidChar(e.Message));
            };
        }

        public void UpdatePDIFinishTime(string coilIDNo)
        {
            try
            {
                var updateNum = _coilLogic.UpdatePDIFinishTime(coilIDNo);
                _log.I("更新鋼捲PDI結束時間", $"鋼捲{coilIDNo}紀錄生產結束時間:{updateNum > 0}");
            }
            catch (Exception e)
            {
                _log.E("更新鋼捲PDI結束時間", e.Message.CleanInvalidChar());
            };
        }


        public void UpdatePDIEndTime(string entryCoil)
        {
            try
            {
                var updateNum = _coilLogic.UpdatePDIEndTime(entryCoil);
                _log.I("更新鋼捲生產結束時間", $"鋼捲{entryCoil} 紀錄生產結束時間 => {updateNum>0}");
            }
            catch (Exception e)
            {
                _log.E("更新鋼捲生產結束時間", TypeConvertUtil.CleanInvalidChar(e.Message));
            };
        }



        public void UpdateIsInfoWMSDown(bool infoDone, string coilIDNo)
        {
            try
            {
                var updateNum = _coilLogic.UpdateIsInfoWMSDown(infoDone, coilIDNo);
                _log.I("更新PDI已通知WMS狀態旗標", $"鋼捲{coilIDNo} 已通知WMS => {updateNum > 0}");
            }
            catch (Exception e)
            {
                _log.E("更新PDI已通知WMS狀態旗標", TypeConvertUtil.CleanInvalidChar(e.Message));
            };
        }
        #endregion

        #region -- PDO相關 --
        public bool UpdatePDOExCoilIDChecked(string exitCoilNo, string exitCoilIDChecked)
        {
            try
            {
                var updateNum = _coilLogic.UpdateExitCoilIDChecked(exitCoilNo, exitCoilIDChecked);

                if (updateNum > 0)
                    _log.I("更新PDO ECoilCheck成功", $"已更新PDO Exit Coil Checked {exitCoilIDChecked}");
                else
                    _log.E("更新PDO ECoilCheck失敗", $"無此鋼捲{exitCoilNo}PDO");

                return updateNum > 0;
            }
            catch (Exception e)
            {
                _log.E("更新PDO ECoilCheck失敗", TypeConvertUtil.CleanInvalidChar(e.Message));
                return false;

            }



        }

        public TBL_PDO GetPDO(string outMatNo)
        {
            try
            {
                var pdo = _coilLogic.GetPDO(outMatNo);
                _log.I("撈取PDO資料", $"撈取PDO{pdo != null}");
                return pdo;
            }
            catch (Exception e)
            {
                _log.E("撈取PDO資料失敗", TypeConvertUtil.CleanInvalidChar(e.Message));
                return null;
            }
        }
        public TBL_PDO GetPDOonly(string planNo,string out_coil_id)
        {
            try
            {
                var pdo = _coilLogic.GetPDOonly(planNo,out_coil_id);
                _log.I("撈取PDO資料", $"撈取{out_coil_id},{planNo} PDO資訊");
                return pdo;
            }
            catch (Exception e)
            {
                _log.E("撈取PDO資料失敗", TypeConvertUtil.CleanInvalidChar(e.Message));
                return null;
            }

        }



        public TBL_PDO CalculatePDOResult(L1L2Rcv.Msg_102_PDO msg)
        {
            var pdi = new PDIEntity.TBL_PDI();
            var coilPDOPara = new LkUpTableModel.GenPDODataPara();
            var outCoil = msg.CoilIDNo;
            var parentCoil = string.Empty;
            // PDI (以母捲ID查詢)
            try
            {

                pdi = _coilLogic.GetPDIByOutCoil(outCoil);
                parentCoil = _coilLogic.GetSplitParentCoilNo(outCoil);

                //找的到PDI且无分切记录
                if ((pdi != null) && (parentCoil.Equals(string.Empty)))
                {
                    //正常产出钢卷
                    coilPDOPara.FixedWtFlag = MMSSysDef.Cmd.NotUse;  //無分切
                }
                //找的到PDI且有分切记录
                if ((pdi != null) && (parentCoil != string.Empty))
                {
                    //正常产出钢卷
                    coilPDOPara.FixedWtFlag = MMSSysDef.Cmd.NotUse;  //無分切
                }
                //找不到PDI且有分切记录
                if ((pdi == null) && (parentCoil != string.Empty))
                {
                    //分切钢卷产出
                    pdi = _coilLogic.GetPDIByInCoil(parentCoil.Trim());
                    coilPDOPara.FixedWtFlag = MMSSysDef.Cmd.Use;  //有分切
                   
                    var SplitCnt = _coilLogic.GetParentCnt(parentCoil.Trim());
                    if (SplitCnt > 1)
                    {
                        var SplitCoilRec = _coilLogic.GetPreSplitCoilTime(parentCoil.Trim(), SplitCnt);
                        pdi.StarTime = SplitCoilRec.CreateTime;
                    }

                }

            }
            catch (Exception e)
            {
                _log.E("撈取PDI失敗", $"{outCoil} PDO組合失敗," + TypeConvertUtil.CleanInvalidChar(e.ToString()));
                return null;
            }

            // 索取各個重量
            if (pdi != null)
            {
                //墊紙寬度取得
                if (msg.PaperInstalled == PlcSysDef.Cmd.InstallPaper)
                {
                    var coilPaperData = GetPaperWt(pdi.In_Paper_Code);
                    coilPDOPara.Paper_Width = coilPaperData.Paper_Width;
                }


                ////套筒
                //if (msg.SleeveInstallled == PlcSysDef.Cmd.Install_Sleeve)
                //    coilPDOPara.SleeveWt = (int)GetSleeveWt(pdi.Out_Paper_Req_Code);

                ////導帶 0:不使用  1：使用

                //// 頭段
                //if (msg.HeadEndLeader == PlcSysDef.Cmd.Use)
                //    coilPDOPara.HeadLeaderWt = (pdi.Head_Leader_Length  * pdi.Head_Leader_Width /1000 * pdi.Head_Leader_Thickness /1000) * DeviceDef.LeaderDensity /1000;
                //// 尾段
                //if (msg.TailEndLeader == PlcSysDef.Cmd.Use)
                //    coilPDOPara.HeadLeaderWt = (pdi.Tail_Leader_Length *  pdi.Tail_Leader_Width / 1000 * pdi.Tail_Leader_Thickness / 1000) * DeviceDef.LeaderDensity /1000;
            }

            // 出口實際研磨道次
            try
            {
                var grindRecords = _coilLogic.QueryGrindRecords(pdi.In_Coil_ID,pdi.Plan_No).ToList().OrderByDescending(x => x.Current_Pass);

                _log.I("撈取出口實際研磨道次", $"撈取出口實際研磨道次 => {grindRecords.Count() > 0}");

                coilPDOPara.HeadPassNum = grindRecords.Where(x => x.Current_Senssion.Equals(DBParaDef.GrindRecordSession_H))
                                                        .Select(v => v.Current_Pass).FirstOrDefault();

                coilPDOPara.MidPassNum = grindRecords.Where(x => x.Current_Senssion.Equals(DBParaDef.GrindRecordSession_M))
                                                       .Select(v => v.Current_Pass).FirstOrDefault();

                coilPDOPara.TailPassNum = grindRecords.Where(x => x.Current_Senssion.Equals(DBParaDef.GrindRecordSession_T))
                                                       .Select(v => v.Current_Pass).FirstOrDefault();

                //var beltPasses = _coilLogic.GetBeltPasses(pdi.Entry_Mat_No).ToList();
                //_log.Info("撈取出口實際研磨道次", $"撈取出口實際研磨道次 => {beltPasses.Count()>0}");
                //coilCutRecord.HeadPassNum = beltPasses.Where(x => x.Pass_Section.Equals(DBParaDef.HPassSection)).Select(v => v.PassNumber).FirstOrDefault();
                //coilCutRecord.MidPassNum = beltPasses.Where(x => x.Pass_Section.Equals(DBParaDef.MPassSection)).Select(v => v.PassNumber).FirstOrDefault();
                //coilCutRecord.TailPassNum = beltPasses.Where(x => x.Pass_Section.Equals(DBParaDef.TPassSection)).Select(v => v.PassNumber).FirstOrDefault();
            }
            catch (Exception e)
            {
                _log.E("出口實際研磨道次撈取失敗", e.Message);
            }

            //// 撈取分切紀錄
            //try
            //{
            //    var parenCoil = _coilLogic.GetSplitParentCoilNo(outCoil);
            //    _log.I("撈取分切紀錄", $"撈取分切紀錄 母捲ID為{parenCoil}");
            //    coilPDOPara.FixedWtFlag = parenCoil.Equals(string.Empty) ? MMSSysDef.Cmd.NotUse : MMSSysDef.Cmd.Use;
            //}
            //catch(Exception e)
            //{
            //    _log.E("撈取分切紀錄失敗", e.Message);
            //}

            // PDO (索取PDO)
            //var pdo = _coilLogic.GetPDO(outCoil);

            //if (pdo == null)
            //{
            //    _log.Error("撈取PDO失敗", $"無此{outCoil}PDO資料!");
            //    return null;
            //}

            var pdo = new TBL_PDO();

            // Load Defect Data
            try
            {
                var defectDatas = _coilLogic.QueryDefect(outCoil, 10);
                pdo.LoadDefectData(defectDatas);
            }
            catch(Exception e)
            {
                _log.E("Defect Data處理錯誤", $"{e.Message}");
            }

            // 表面經度代碼處理
            try
            {
                // 取得TBL_GrindRecords最後一筆 GR6的號數 Paritcle_No
                var paritcleNo = _coilLogic.GetGR6LastRecordsParticleNo(outCoil);  
                var grindLevel = _coilLogic.GetGrindLevel(pdi.Surface_Accuracy_Acture);
              
                if (msg.ActualUncoilDirection == PlcSysDef.Cmd.UnCoilUp)
                {
                    // 上開下收:磨外表面 收卷:外表面變內表面-翻面處理

                    var proGrindParitcleCode = paritcleNo != 0 ? _coilLogic.ParitcleCode(paritcleNo) : grindLevel.OuterGrade;     //實際研磨面研磨等級
                    /** 
                     * 上開下收             
                     * PDI: 外表FP  內表FP -->使用砂帶號數<100  --> 外表#80 內表FP -->收卷完成變成 外表FP 內表#80 --> 查詢新的SufraceCode                  
                     **/
                    var newSurfaceAccuCode = _coilLogic.GetGrindLevel(grindLevel.InnerGrade, proGrindParitcleCode);
                    coilPDOPara.NewSurfaceAccuCode = newSurfaceAccuCode!=null? newSurfaceAccuCode.Code:string.Empty;

                    coilPDOPara.Pre_Grinding_Surface = pdi.Pre_Grinding_Surface.Equals("O") ? "I" : "O";

                    coilPDOPara.Grinding_Count_Out = pdi.Grinding_Count_In;
                    coilPDOPara.Grinding_Count_In = pdi.Grinding_Count_Out+1; // 收卷外表面變內表面
                }
                else
                {
                    // 下開下收 :磨內表面 收卷:無翻面   
                    var proGrindParitcleCode = paritcleNo != 0 ? _coilLogic.ParitcleCode(paritcleNo) : grindLevel.InnerGrade;     //實際研磨面研磨等級
                    /** 
                     * 下開下收             
                     * PDI: 外表FP  內表FP -->使用砂帶號數<100  --> 外表FP 內表#80 -->收卷完成變成 外表FP 內表#80 --> 查詢新的SufraceCode                  
                     **/
                    var newSurfaceAccuCode = _coilLogic.GetGrindLevel(grindLevel.OuterGrade, proGrindParitcleCode);
                    coilPDOPara.NewSurfaceAccuCode = newSurfaceAccuCode != null ? newSurfaceAccuCode.Code : string.Empty;

                    coilPDOPara.Pre_Grinding_Surface = pdi.Pre_Grinding_Surface;

                    coilPDOPara.Grinding_Count_Out = pdi.Grinding_Count_Out;
                    coilPDOPara.Grinding_Count_In = pdi.Grinding_Count_In + 1; // 內表面

                }
                
                
               
            }
            catch (Exception e)
            {
                _log.E("表面精度代碼處理錯誤", $"{e.Message}");
            }

            // 撈取班次
            try
            {
                //var shift = ShiftHelp.GetShift(pdi.EndTime);
                //var shiftDate = pdi.EndTime.Date.ToString("yyyyMMdd");
                //var shiftInfo = _coilLogic.GetWorkSchedule(shift, shiftDate);
                var shiftInfo = _coilLogic.GetScheduleByTime(pdi.EndTime);
                coilPDOPara.Shift = shiftInfo.Shift.ToString();
                coilPDOPara.Team = shiftInfo.Team;

            }
            catch(Exception e)
            {
                _log.E("撈取班次處理錯誤", $"{e.Message}");
            }


            //RollTension 计算
            try
            {
                const float Gn = 9.80665f;  //标准重力
                var lineTension = _coilLogic.GetLineTensionByGradeAndThick(pdi.St_No.Substring(1, 5), pdi.In_Coil_Thick, pdi.In_Coil_Width, "C");
                if (lineTension != null)
                {
                    // 单位张力(kgf/mm2) * 宽(mm) * 厚(mm) * Gn(9.80665) / 1000   =  *** (kN)
                    coilPDOPara.RollTension = (float)Math.Round((double)((lineTension.TRTension * pdi.In_Coil_Thick * pdi.In_Coil_Width * Gn) / 1000), 2);
                    _log.I("撈取单张资料并计算卷曲张力", $"SteelNo:{pdi.St_No.Substring(1, 5)} Thick:{pdi.In_Coil_Thick} mm Width:{pdi.In_Coil_Width} mm 单张:{lineTension.TRTension} kgf/mm2 卷曲张力:{coilPDOPara.RollTension} kN");
                }
                else
                {
                    coilPDOPara.RollTension = 0f;
                    _log.E("無張力機資料", $"SteelNo:{pdi.St_No.Substring(1, 5)} Thick:{pdi.In_Coil_Thick} Width:{pdi.In_Coil_Width} 卷曲张力:{coilPDOPara.RollTension}");
                }


            }
            catch (Exception e)
            {
                _log.E("撈取張力機失敗", $"StNo:{pdi.St_No}  Mat Thick:{pdi.In_Coil_Thick} " + e.Message.CleanInvalidChar());
            }

            var pdoResult = pdo.CombinationPDO(msg, pdi, coilPDOPara, outCoil);
            return pdoResult;

        }

        public TBL_LookupTable_Paper GetPaperWt(string code)
        {
            try
            {
                var paperData = _coilLogic.GetPaperData(code);
                _log.I("撈取墊紙重量", $"撈取{code}-Paper資料");

                return paperData == null ? new TBL_LookupTable_Paper() : paperData;
            }
            catch (Exception e)
            {
                _log.E("撈取墊紙重量錯誤", TypeConvertUtil.CleanInvalidChar(e.Message));
                return new TBL_LookupTable_Paper(); ;
            }
        }

        public float GetSleeveWt(string code)
        {
            try
            {
                var SleeveData = _coilLogic.GetSleeveData(code);
                var wt = SleeveData != null ? SleeveData.Sleeve_Weight : 0;
                _log.I("撈取套筒重量", $"撈取{code}-Paper重量 => {SleeveData != null} 重量為{wt}");
                return wt;
            }
            catch (Exception e)
            {
                _log.E("撈取套筒重量錯誤", TypeConvertUtil.CleanInvalidChar(e.Message));
                return 0;
            }
        }
        public TBL_LookupTable_Sleeve GetSleeveData(string code)
        {
            try
            {
                var SleeveData = _coilLogic.GetSleeveData(code);
                _log.I("撈取套筒資料", $"撈取{code}-Sleeve資料");
                return SleeveData == null ? new TBL_LookupTable_Sleeve() : SleeveData;
            }
            catch (Exception e)
            {
                _log.E("撈取套筒資料", TypeConvertUtil.CleanInvalidChar(e.Message));
                return new TBL_LookupTable_Sleeve(); ;
            }
        }
        public bool InvaildHasPDO(string outCoilNo)
        {
            try
            {
                var hasData = _coilLogic.VaildHasPDO(outCoilNo);
                _log.E($"是否有PDO資料", $"是否有PDO資料 => {hasData}");
                return hasData;
            }
            catch (Exception e)
            {
                _log.E($"是否有PDO資料", TypeConvertUtil.CleanInvalidChar(e.Message));
                return false;
            }
        }

        public bool UpdatePDO(PDOEntity.TBL_PDO pdo)
        {

            try
            {
                var updateNum = _coilLogic.UpdatePDO(pdo);
                _log.I($"更新鋼卷{pdo.Out_Coil_ID}PDO資料", $"鋼卷{pdo.Out_Coil_ID}更新PDO => {updateNum > 0}");
                return updateNum > 0;

            }
            catch (Exception e)
            {
                _log.E($"新增鋼卷{pdo.Out_Coil_ID}PDO資料", TypeConvertUtil.CleanInvalidChar(e.Message));
                return false;
            }
        }

        public bool CreatePDO(PDOEntity.TBL_PDO pdo)
        {
            try
            {
                var insertNum = _coilLogic.NewPDOData(pdo);
                _log.I($"新增鋼卷{pdo.Out_Coil_ID}PDO資料", $"鋼卷{pdo.Out_Coil_ID}產生PDO => {insertNum > 0}");
                return insertNum > 0;

            }
            catch (Exception e)
            {
                _log.E($"新增鋼卷{pdo.Out_Coil_ID}PDO資料", TypeConvertUtil.CleanInvalidChar(e.Message));
                return false;
            }
        }
        public bool CreatePdoUploadedReply(Msg_Res_RcvPDO mmsResPDO)
        {
            mmsResPDO.VaildObjectNull("mmsResPDO", "新增上傳PDO的回覆失敗");
            try
            {
                var entity = mmsResPDO.ToTblPDOUploadedReply();
                var insertNum = _coilLogic.CreatePDOUploadedReply(entity);
                var inserOK = insertNum > 0;
                _log.I($"新增上傳PDO的回覆成功", $"新增上傳PDO的回覆成功，計畫號={entity.Plan_No}、材料號={entity.Out_Coil_ID}");
                //_log.I($"接收上傳PDO的回覆:{mmsResPDO.MsgID}", $"【鋼捲】:{mmsResPDO.Respon_Coil_No.ToStr().Trim()},【計劃號】:{mmsResPDO.Respon_Plan_No.ToStr().Trim()}新增上傳PDO的回覆成功");
                return inserOK;
            }
            catch (Exception e)
            {
                _log.E("新增上傳PDO的回覆失敗", e.ToString().CleanInvalidChar());
                //_log.E($"接收鋼捲PDI:{mmsResPDO.MsgID}", $"【鋼捲】:{mmsResPDO.Respon_Coil_No.ToStr().Trim()},【計劃號】:{mmsResPDO.Respon_Plan_No.ToStr().Trim()}新增上傳PDO的回覆失敗:" + e.Message.CleanInvalidChar());
                return false;
            }
        }

        public void UpdatePDOWeight(string outCoilID, float coilWt)
        {
            var pdo = new PDOEntity.TBL_PDO();
            var planNo = string.Empty;

            // PDO
            try
            {
                pdo = _coilLogic.GetPDO(outCoilID);
                planNo = pdo.Plan_No;
            }
            catch (Exception e)
            {
                _log.E("撈取PDO失敗", $"{outCoilID} PDO撈取失敗," + TypeConvertUtil.CleanInvalidChar(e.Message));
            }

            var coilCutRecord = new LkUpTableModel.GenPDODataPara();


            // 索取各個重量
            if (pdo != null)
            {
                //襯紙
                if (!pdo.Out_Paper_Code.Equals(string.Empty))
                {   
                    
                    //取得襯紙類型
                    var coilPaperData = GetPaperWt(pdo.Out_Paper_Code);
                    coilCutRecord.Paper_Width = coilPaperData.Paper_Width;    // 單位: mm

                    //判斷襯紙方式 
                    
                    switch (pdo.Out_Paper_Req_Code)
                    {
                        //襯紙重量計算(kg):  襯紙基重(kg/m2) * 襯紙寬度(m) * PDO鋼捲長度(m)
                        //0 不墊
                        case "0":
                            coilCutRecord.PaperWt = 0;
                             break;
                        //1 整捲墊
                        case "1":
                            coilCutRecord.PaperWt = ((float)coilPaperData.Paper_Base_Weight / 1000) * (coilCutRecord.Paper_Width / 1000) * pdo.Out_Coil_Length;
                            break;
                        //2 頭尾各50米
                        case "2":
                            coilCutRecord.PaperWt = (coilPaperData.Paper_Base_Weight / 1000) * (coilCutRecord.Paper_Width / 1000) * 100;
                            break;
                        //3 頭尾各30米
                        case "3":
                            coilCutRecord.PaperWt = (coilPaperData.Paper_Base_Weight / 1000) * (coilCutRecord.Paper_Width / 1000) * 60;
                            break;
                        //4 尾端80米
                        case "4":
                            coilCutRecord.PaperWt = (coilPaperData.Paper_Base_Weight / 1000) * (coilCutRecord.Paper_Width / 1000) * 80;
                            break;
                        //5 尾端200米
                        case "5":
                            coilCutRecord.PaperWt = (coilPaperData.Paper_Base_Weight / 1000) * (coilCutRecord.Paper_Width / 1000) * 200;
                            break;
                        default:
                            _log.I("襯紙類型/襯紙方式 ", $"類型{coilPaperData.Paper_Code },方式{pdo.Out_Paper_Req_Code} ");
                            coilCutRecord.PaperWt = 0;
                            break;                   
                    }
                    //coilCutRecord.Paper_Thick = coilPaperData.Paper_Thick;
                }

 
                //套筒
                if (!pdo.Out_Sleeve_Type_Code.Equals(string.Empty))
                    coilCutRecord.SleeveWt = GetSleeveWt(pdo.Out_Sleeve_Type_Code );

                //導帶 0:不使用  1：使用
                //導帶 0:不使用  1：使用


                //Density         
                int  Density_Head_Leader = 0;
                int  Density_Tail_Leader = 0;
                switch (pdo.Head_Leader_St_No)
                {
                    case "301":
                        Density_Head_Leader = 7930;
                        break;
                    case "304":
                        Density_Head_Leader = 7930;
                        break;
                    case "409":
                        Density_Head_Leader = 7740;
                        break;
                    case "443":
                        Density_Head_Leader = 7740;
                        break;
                    case "430":
                        Density_Head_Leader = 7700;
                        break;
                    default:
                        _log.I("Head_Leader Density ", $"St_No {pdo.Head_Leader_St_No}, Density_Head_Leader = 0 ");
                        Density_Head_Leader = 0;
                        break;
                }
                switch (pdo.Tail_Leader_St_No)
                {
                    case "301":
                        Density_Head_Leader = 7930;
                        break;
                    case "304":
                        Density_Head_Leader = 7930;
                        break;
                    case "409":
                        Density_Head_Leader = 7740;
                        break;
                    case "443":
                        Density_Head_Leader = 7740;
                        break;
                    case "430":
                        Density_Head_Leader = 7700;
                        break;
                    default:
                        _log.I("Tail_Leader Density ", $"St_No {pdo.Tail_Leader_St_No}, Density_Tail_Leader = 0 ");
                        Density_Tail_Leader = 0;
                        break;
                }

                // 頭段導帶重量  長度(m) * 寬度(m) * 厚度(m) * 密度(kg/m3)  ex. 12(m) * 1.26(m) * 0.003(m) * 7930 (kg/m3)
                if (!pdo.Head_Leader_St_No.Equals(string.Empty))
                    coilCutRecord.HeadLeaderWt = (pdo.Head_Leader_Length * pdo.Head_Leader_Width / 1000 * pdo.Head_Leader_Thickness / 1000) * (float)Density_Head_Leader;
                // 尾段
                if (!pdo.Tail_Leader_St_No.Equals(string.Empty))
                    coilCutRecord.TailLeaderWt = (pdo.Tail_Leader_Length * pdo.Tail_Leader_Width / 1000 * pdo.Tail_Leader_Thickness / 1000) * (float)Density_Tail_Leader;


            }

            // 更新重量 - 淨重
            var coilPureWt = coilWt - coilCutRecord.TotalWt;
            _log.I("各項目重量", $"襯紙{coilCutRecord.PaperWt },套筒{coilCutRecord.SleeveWt},頭段導帶{coilCutRecord.HeadLeaderWt }， 尾段導帶{coilCutRecord.HeadLeaderWt} ");
            try
            {
                var updateNum = _coilLogic.UpdateExitCoilWt(planNo,outCoilID, coilWt, coilPureWt);
                _log.I("更新PDO重量", $"更新PDO{outCoilID},{planNo},毛重{coilWt}， 淨重{coilPureWt}=>{updateNum > 0} ");
            }
            catch (Exception e)
            {
                _log.E("更新PDO淨重失敗", $"{outCoilID},{planNo} 更新PDO重量," + TypeConvertUtil.CleanInvalidChar(e.Message));
            }

        }

        public void UpdatePDOFinishTime(string coilNo, DateTime finishTime) {


            try
            {
                var updateNum = _coilLogic.UpdatePDOFinishTime(coilNo, finishTime);
                _log.I("紀錄PDO結束時間", $"紀錄{coilNo}PDO結束時間{finishTime} => {updateNum>0}");
            }
            catch (Exception e)
            {
                _log.E("紀錄PDO結束時間失敗", $"更新PDO結束時間失敗" + TypeConvertUtil.CleanInvalidChar(e.Message));
            }
        }

        public bool GenEmptyPDO(PDIEntity.TBL_PDI pdi)
        {
            try
            {
              
                var pdo = pdi.GenEmptyPDO();
                var insertNum = _coilLogic.NewPDOData(pdo);
                _log.I($"鋼卷{pdi.In_Coil_ID}產生PDO", $"鋼卷{pdi.In_Coil_ID}產生PDO => {insertNum > 0}");
                return insertNum > 0;

            }
            catch (Exception e)
            {
                _log.E($"鋼卷{pdi.In_Coil_ID}產生PDO", TypeConvertUtil.CleanInvalidChar(e.Message));
                return false;
            }
        }

        public bool GenPDO(L1L2Rcv.Msg_124_StripBrakeSignal msg, PDIEntity.TBL_PDI pdi)
        {
            try
            {
                var pdo = msg.GenPDO(pdi);
                var insertNum = _coilLogic.NewPDOData(pdo);
                _log.I($"鋼卷{pdi.In_Coil_ID}產生PDO", $"鋼卷{pdi.In_Coil_ID}產生PDO => {insertNum > 0}");
                return insertNum > 0;
            }
            catch (Exception e)
            {
                _log.E($"鋼卷{pdi.In_Coil_ID}產生PDO", TypeConvertUtil.CleanInvalidChar(e.Message));
                return false;
            }
        }




        public void UpdateUploadFlag(string planNo,string coilNo, bool upload)
        {
            try
            {
                var updateNum = _coilLogic.UpdateUploadFlag(planNo,coilNo, upload);
                _log.I("更新PDO UPLOAD旗標", $"更新旗標{upload} => {updateNum > 0}");
            }
            catch (Exception e)
            {
                _log.E("更新PDO UPLOAD旗標", $"更新旗標{upload} => {e.Message.CleanInvalidChar()}");
            }
        }



        public bool CreateL25PDO(string planNo, string outCoilID)
        {
            outCoilID.VaildStrNullOrEmpty("coilID", "存取PDO至L25失敗");


            try
            {
                var pdo = _coilLogic.GetPDO(outCoilID);
                var PaperData = GetPaperWt(pdo.Out_Paper_Code);
                var SleevData = GetSleeveWt(pdo.Out_Sleeve_Type_Code);


                var entity = EntityFactory.To25CoilPDOEntity(pdo, PaperData.Paper_Base_Weight, SleevData); ;
                var insetNum = _coilLogic.CreateL25PDO(entity);
                var insertOK = insetNum > 0;
                _log.I($"鋼捲PDO新增資料至L25:{outCoilID}", $"【鋼捲】:{outCoilID} 新增L25_PDO{(insertOK).ToStr()}");
                return insertOK;
                //return true;

                //insetNum = _coilLogic.CreateL25PDOHis(entity);
                //insertOK = insetNum > 0;
                //_log.I($"鋼捲PDO新增資料至L25歷史資料庫:{outCoilID}", $"【鋼捲】:{outCoilID} 新增L25_PDO{(insertOK).ToStr()}");



            }
            catch (Exception e)
            {
                _log.E("存取PDO至L25失敗", e.ToString().CleanInvalidChar());
                return false;
            }
        }

        #endregion

        #region -- 排程相關 --

        public bool UpdateScheduleStatuts(string coilNo, string statuts)
        {
            coilNo.VaildStrNullOrEmpty("coilNo", "更新鋼捲狀態失敗");
            statuts.VaildStrNullOrEmpty("statuts", "更新鋼捲狀態失敗");

            try
            {
                var updateNum = _coilLogic.UpdateScheduleStatus(coilNo, statuts);
                _log.I("更新鋼捲狀態成功", $"更新鋼捲{coilNo}狀態為{statuts}");

                //if (statuts.Equals(CoilDef.EntryCoilDone_Statuts))
                //    _coilLogic.UpdateScheduleSeqNo(coilNo, CoilDef.ScheduleDoneSeqDef);

                return updateNum > 0;
            }
            catch (Exception e)
            {
                _log.E("更新鋼捲狀態失敗", e.ToString().CleanInvalidChar());
                return false;
            }
        }
        public List<string> QueryScheduleRequestCoils(int num)
        {
            try
            {
                var coilIDs = _coilLogic.GetScheduleRequestCoilIDs(num);
                _log.I("撈取鋼捲排程狀態為[要求入料]", $"共{coilIDs.Count.ToString()}筆");
                return coilIDs;
            } 
            catch (Exception e)
            {
                _log.E("撈取鋼捲排程狀態為[要求入料]", $"撈取失敗" + e.Message.CleanInvalidChar());
                return null;
            }
        }
       public List<string> QueryUnscheduleCoils(int num)
        {

            try
            {
                var coilIDs = _coilLogic.GetUnScheduleCoilIDs(num);
                _log.I("撈取未上卷鋼捲排程", $"共{num}筆");
                return coilIDs;
            }
            catch (Exception e)
            {
                _log.E("撈取未上卷鋼捲排程", $"撈取失敗"+ e.Message.CleanInvalidChar());
                return null;
            }
        }
        public List<string> QueryCoilScheduleIDs(int num)
        {
            try
            {
                List<string> coilScheduleIDs = _coilLogic.QueryCollScheduleIDs(num);
                _log.I("撈取Coil Schedule", $"撈取Coil Schedule{coilScheduleIDs != null && coilScheduleIDs.Count > 0}");
                return coilScheduleIDs;
            }
            catch (Exception e)
            {
                _log.E("撈取Coil Schedule失敗", TypeConvertUtil.CleanInvalidChar(e.Message));
                return null;
            }

        }
        public bool VaildHasCoilSchedule(string coilID)
        {
            bool hasCoilSchedule = false;

            try
            {
                hasCoilSchedule = _coilLogic.VaildHasCoilSchedule(coilID);
                _log.I("查詢是否有此排程", $"是否有{coilID}排程=>{hasCoilSchedule}");
            }
            catch (Exception e)
            {
                _log.E("查詢是否有排程資訊失敗", TypeConvertUtil.CleanInvalidChar(e.Message));
            }

            return hasCoilSchedule;
        }

        /// <summary>
        /// 順序插入鋼捲排程資料
        /// </summary>
        public bool SequenceCreateSchedule(string coilCluster, int coilNum)
        {

            var productSched = new List<TBL_Production_Schedule>();

            // 鋼捲號分切 
            var coiIDs = TypeConvertUtil.StrSplitBySpecificLength(coilCluster, CoilDef.UnitCoilIDMsgCharLen);

            //TotalCount
            var SeqN0 = (Int16)_coilLogic.GetCoilScheduleTotalCountFromDB() ;
            _log.D("鋼捲生產命令", $"目前共有{SeqN0}筆鋼捲在排程中");
            
            // TODO 暫時先用For處理, 之後轉RX
            int count = 1;
            int repeatCount = 0;
            foreach (string coilID in coiIDs)
            {
                var getStatus = _coilLogic.GetScheduleStatuts(coilID);
                if (string.IsNullOrEmpty(getStatus))
                {
                    try
                    {   
                        SeqN0++;
                        var insertNum = _coilLogic.CreateSchedule(coilID, SeqN0);
                        if (insertNum > 0)
                        { 
                            SeqN0++;
                            _log.I("鋼捲生產命令", $"新增鋼捲{coilID}至排成資料庫");
                        }
                    }
                    catch (Exception e)
                    {
                        _log.E("鋼捲插入失敗", e.Message.CleanInvalidChar());
                    }

                    count++;
                    // 根據下發鋼捲數(CoilNum)判定 (第1筆到第Num筆)
                    if (count > coilNum-repeatCount)
                        break;
                }
                else
                {
                    repeatCount++;
                    _log.I("排程重複", $"鋼捲{coilID}狀態{getStatus}");
                }
                
            };

            // 鋼捲插入成功失敗待議
            return true;
        }

        /// <summary>
        /// 順序插入鋼捲排程資料
        /// </summary>
        //public bool SequentialInsertInDB(string coilCluster, int coilNum)
        //{
        //    var productSched = new List<CoilScheduleEntity.TBL_Production_Schedule>();

        //    // 鋼捲號分切
        //    var coiIDs = TypeConvertUtil.StrSplitBySpecificLength(coilCluster, MMSSysDef.CoilIDLength);
        //    // 資料庫鋼捲筆數
        //    var SeqN0 = (Int16)_coilLogic.GetCoilScheduleTotalCountFromDB();
        //    _log.D("鋼捲排程處理", $"目前共有{SeqN0}筆鋼捲在排程中");

        //    // 將資料載入Dt
        //    DataTable dt = new DataTable(DBColumnDef.CoilSchedTbl);
        //    dt.Columns.Add(DBColumnDef.CoilSchedCoilID);         // Coil_ID
        //    dt.Columns.Add(DBColumnDef.CoilSchedSeqNo);          //Seq_No
        //    dt.Columns.Add(DBColumnDef.CoilSchedScheduleStatus); //Schedule_Status
        //    dt.Columns.Add(DBColumnDef.CoilSchedUpdateSource);   //Update_Source
        //    dt.Columns.Add(DBColumnDef.CoilSchedUpdateTime);      //UpdateTime

        //    int insertDoneCnt = 1;
        //    foreach (string coilID in coiIDs)
        //    {

        //        SeqN0++;

        //        var row = dt.NewRow();
        //        row[DBColumnDef.CoilSchedCoilID] = coilID;
        //        row[DBColumnDef.CoilSchedSeqNo] = SeqN0;
        //        row[DBColumnDef.CoilSchedScheduleStatus] = CoilDef.ScheduleStatuts.NewCoil_Statuts;
        //        row[DBColumnDef.CoilSchedUpdateSource] = L2SystemDef.UpdateSourceMMS;
        //        row[DBColumnDef.CoilSchedUpdateTime] = DateTime.Now;
        //        dt.Rows.Add(row);

        //        insertDoneCnt++;
        //        // 根據下發鋼捲數(CoilNum)判定 (第1筆到第Num筆)
        //        if (insertDoneCnt > coilNum)
        //            break;

        //    }

        //    // 插入
        //    try
        //    {
        //        _log.D("鋼捲插入", $"新增鋼捲{coilNum}筆");
        //        _coilLogic.CreateSchedules(dt);
        //        return true;

        //    }
        //    catch (Exception e)
        //    {
        //        _log.E("鋼捲插入", TypeConvertUtil.CleanInvalidChar(e.Message));
        //        return false;
        //    }
        //}
        public int CreateCoilSchedule(string coilID, short SeqN0)
        {
            try
            {
                var insertNum = _coilLogic.CreateCoilSchedule(coilID, SeqN0);
                _log.I("新增鋼卷排程", $"新增鋼卷{coilID} Seq:{SeqN0} => {insertNum>0}");
                return insertNum;
            }
            catch (Exception e)
            {
                _log.E("新增鋼捲排程失敗", TypeConvertUtil.CleanInvalidChar(e.Message));
                return -1;
            }
        }
        public bool RemoveScheduleCoilIDs(string coilID)
        {
            int deleteNum = 0;
            
            try
            {
                var seqNo = _coilLogic.GetSeqNo(coilID);
                var scheduleStatuts = _coilLogic.GetScheduleStatuts(coilID);
                deleteNum = _coilLogic.DeleteCoildSchedsAfterSeqNo(seqNo);
                _log.I("刪除鋼捲排程", $"已刪除{coilID}以下鋼捲{deleteNum}筆");

            }
            catch (Exception e)
            {
                _log.E("刪除鋼捲排程失敗", TypeConvertUtil.CleanInvalidChar(e.Message));
            }

            return deleteNum > 0;
        }
        public string GetFirstCoilSchedule()
        {
            try
            {
                var coilID = _coilLogic.GetFirstCoilSchedule();
                _log.I("取得排程(自动入料)", $"取得排程第一颗钢卷{coilID}");
                return coilID;
            }
            catch (Exception e)
            {
                _log.E("取得排程(自动入料)", TypeConvertUtil.CleanInvalidChar(e.Message));
                return "";
            }

        }
        public void DeleteAllCoilSchedule()
        {
            try
            {
                var deleteNum = _coilLogic.DeleteAllSchedule();
                _log.I("鋼捲排程刪除", $"刪除資料庫所有排程, 共刪除{deleteNum}筆 => {deleteNum > 0}, ");
            }
            catch (Exception e)
            {
                _log.E("鋼捲排程刪除", $"刪除資料庫所有排程 " + e.Message.CleanInvalidChar());
            }

        }

        public void CreateDummyCoil(MMSL2Rcv.Msg_Dummy_Coil_List dummyCoil)
        {
            try
            {
                var insertNum = _coilLogic.CreateDummyCoilPDI(dummyCoil);
                _log.I("插入Dummy鋼捲", $"插入Dummy{dummyCoil.CoilNoID}鋼捲 : {insertNum > 0}");

            }
            catch (Exception e)
            {
                _log.E("插入Dummy鋼捲", TypeConvertUtil.CleanInvalidChar(e.Message));
            }
        }
        public bool DeleteCoilSchedule(string coilID)
        {

            try
            {
                var deleteOk = _coilLogic.DeleteSchedule(coilID);
                _log.I("刪除鋼捲排程", $"刪除鋼捲{coilID}排程{deleteOk.ToStr()}");
                return deleteOk;
            }
            catch (Exception e)
            {
                _log.E("刪除鋼捲排程", TypeConvertUtil.CleanInvalidChar(e.Message));
                return false;
            }

        }
        public bool DeleteCoilScheduleOnly(string coilID)
        {

            try
            {
                var deleteOk = _coilLogic.DeleteScheduleOnly(coilID);
                _log.I("刪除鋼捲排程", $"刪除鋼捲{coilID},狀態為P");
                return deleteOk;
            }
            catch (Exception e)
            {
                _log.E("刪除鋼捲排程", TypeConvertUtil.CleanInvalidChar(e.Message));
                return false;
            }

        }
        public void DeleteAllIdleSchedule()
        {
            try
            {
                var deleteNum = _coilLogic.DeleteAllIdleSchedule();
                _log.I("刪除所有Idle排程成功", $"共刪除{deleteNum}筆");
               

            }
            catch (Exception e)
            {
                _log.E("刪除所有Idle排程失敗", e.ToString().CleanInvalidChar());
               
            }

        }
        public IEnumerable<TBL_PDI> QueryCoilScheduleByPlanNo(string planNo)
        {
            try
            {
                var coilData = _coilLogic.QueryScheduleCoilIDByPlanNo(planNo);
                return coilData;

            }
            catch (Exception e)
            {
                _log.E("從計畫號索取鋼捲號", TypeConvertUtil.CleanInvalidChar(e.Message));
                return null;
            }
        }



        public bool CreateTempCoilScheduleDelRecord(string coilID, string operatorID="", string reasonCode="")
        {
            try
            {
                var delCoilSchedData = coilID.ToL3L2TBLScheduleDeleteCoilRejectRecordTemp(operatorID, reasonCode) ;
                var insertNum = _coilLogic.CreateDelScheduleRecordTemp(delCoilSchedData);
                var isSaveOk = insertNum > 0;
                _log.I("暫存刪除鋼捲排程", $"暫存鋼捲排程{coilID}{(isSaveOk).ToStr()}");
                return isSaveOk;
            }
            catch(Exception e)
            {
                _log.I("暫存刪除鋼捲排程", e.Message.CleanInvalidChar());
                return false;
            }
        }
        //VaildHasCoilRejectTemp
        public bool VaildHasCoilRejectTemp(string coilID, string planNo)
        {
            try
            {
                var vaild = _coilLogic.VaildHasCoilRejectTemp(coilID, planNo);
                _log.I("檢查是否有回退鋼捲(temp)刪除紀錄", $"是否有回退鋼捲(temp)刪除紀錄=>{vaild}");
                return vaild;

            }
            catch (Exception e)
            {
                throw new Exception(e.ToString().CleanInvalidChar());
            }
        }

        public bool VaildHasScheduleTemp(string coilID)
        {
            try
            {
                var vaild = _coilLogic.VaildHasScheduleTemp(coilID);
                _log.I("檢查是否有鋼捲刪除紀錄", $"是否有鋼捲刪除紀錄=>{vaild}");
                return vaild;

            }catch(Exception e)
            {
                throw new Exception(e.ToString().CleanInvalidChar());   
            }
        }
        public bool VaildHasCoilMap(string coilID,short skidPos)
        {
            try
            {
                var vaild = _coilLogic.VaidHasCoilMap(coilID, skidPos);
                _log.I("檢查是否有鋼捲刪除紀錄", $"是否有鋼捲刪除紀錄=>{vaild}");
                return vaild;

            }
            catch (Exception e)
            {
                throw new Exception(e.ToString().CleanInvalidChar());
            }
        }
        public bool DelCoilScheduleDelTempRecord(string coilID)
        {
            try
            {           
                var deleteNum = _coilLogic.DeleteDelScheduleTempRecord(coilID);
                var isDeleteOk = deleteNum > 0;
                _log.I("暫存刪除鋼捲排程", $"暫存鋼捲排程{coilID}{(isDeleteOk).ToStr()}");
                return isDeleteOk;
            }
            catch (Exception e)
            {
                _log.I("暫存刪除鋼捲排程", e.Message.CleanInvalidChar());
                return false;
            }
        }
        //DelCoilRejectTempRecord
        public bool DelCoilRejectTempRecord(string coilID,string planNo)
        {
            try
            {
                var deleteOK = _coilLogic.DelCoilRejectTempRecord(coilID, planNo);
                _log.I("刪除回退鋼捲暫存紀錄", $"刪除回退鋼捲暫存紀錄 捲號{coilID} 計畫號{planNo} {(deleteOK.ToString())}");
                return deleteOK;
            }
            catch (Exception e)
            {
                _log.I("刪除回退鋼捲暫存紀錄", e.Message.CleanInvalidChar());
                return false;
            }
        }
        public bool CreateCoilScheduleDelRecords(string coilID, string operatorId = "", string reasonCode = "", string remarks = "")
        {
            try
            {
                var delCoilSchedData = coilID.ToTblCoilScheduleDelete(operatorId, reasonCode, remarks);
                var insertNum = _coilLogic.CreateDelScheduleRecord(delCoilSchedData);
                var isDeleteOk = insertNum > 0;
                _log.I("存取刪除排程鋼捲", $"存取刪除排程鋼捲{coilID}{(isDeleteOk).ToStr()}");
                return isDeleteOk;
            }
            catch (Exception e)
            {
                _log.I("存取刪除排程鋼捲", e.Message.CleanInvalidChar());
                return false;
            }
        }


        #endregion

        #region -- Weld Record || Weld Belt相關 || Coil Reject || Defect Data || 其他 --

        public void UpdateExitCoilGrossWeight(string exitCoilNo, float CoilWeight)
        {
            int updateNum = 0;

            try
            {
                updateNum = _coilLogic.UpdateExitCoilGrossWeight(exitCoilNo, CoilWeight);
                if (updateNum > 0)
                    _log.I("更新PDO毛重成功", $"已更新PDO毛重 {CoilWeight}");
                else
                    _log.E("更新PDO毛重失敗", $"更新PDO毛重 {CoilWeight} 失敗, 無{exitCoilNo}PDO");
            }
            catch (Exception e)
            {
                _log.E("更新PDO毛重失敗", TypeConvertUtil.CleanInvalidChar(e.Message));
            }
        }


        public ReturnCoilEntity.TBL_RetrunCoil_Temp GetCoilRejctTemp(string coilNo,string planNo)
        {
            try
            {
                var entity = _coilLogic.GetCoilRejectTemp(coilNo,planNo);
                _log.I("取得鋼捲回退實績暫存", $"取得鋼捲{coilNo},計畫號{planNo}回退實績暫存 => {entity != null}");
                return entity;

            }catch(Exception e)
            {
                _log.E("取得鋼捲回退實績暫存失敗", e.ToString().CleanInvalidChar());
                return null;
            }
        }

        public CoilRejResultEntity.TBL_CoilRejectResult GetCoilRejectResult(string coilNo)
        {

            try
            {
                var coilReject = _coilLogic.GetCoilRejectResult(coilNo);
                _log.I("取得鋼捲回退實績", $"取得鋼捲{coilNo}回退實績 => {coilReject != null}");
                return coilReject;
            }
            catch (Exception e)
            {
                _log.E("取得鋼捲回退實績失敗", TypeConvertUtil.CleanInvalidChar(e.Message));
                return null;
            }


        }

        public bool UpdateBeltAccLength(int mountGrNo, float beltLength, float stLength)
        {
            try
            {
                var updateNum = _coilLogic.UpdateBeltAccLengthByGrNo(mountGrNo, beltLength, stLength);
                _log.I($"更新{mountGrNo}Belt Length", $"更新{mountGrNo}Belt Length {beltLength}，stLength {stLength} => {updateNum > 0}");
                return updateNum > 0;
            }
            catch (Exception e)
            {
                _log.E($"更新{mountGrNo}Belt Length", TypeConvertUtil.CleanInvalidChar(e.Message));
                return false;
            }
        }

        public bool UpdateAccLengthByBeltNo(string beltNo, float beltLength, float stLength)
        {
            try
            {
                var updateNum = _coilLogic.UpdateAccLengthByBeltNo(beltNo, beltLength, stLength);
                _log.I($"更新 {beltNo} Belt Length", $"更新{beltNo} Belt Length {beltLength}，stLength {stLength} => {updateNum > 0}");
                return updateNum > 0;
            }
            catch (Exception e)
            {
                _log.E($"更新{beltNo}Belt Length", TypeConvertUtil.CleanInvalidChar(e.Message));
                return false;
            }
        }

        public bool UpdateGrNoByBeltNo(string beltNo, int grNo)
        {
            try
            {
                var updateNum = _coilLogic.UpdateGrNoByBeltNo(beltNo, grNo);
                _log.I($"更新 {beltNo} GrNo", $"更新{beltNo} GrNo {grNo} => {updateNum > 0}");
                return updateNum > 0;
            }
            catch (Exception e)
            {
                _log.E($"更新{beltNo} GrNo", TypeConvertUtil.CleanInvalidChar(e.Message));
                return false;
            }
        }

        public BeltAccEntity.TBL_Belts GetBelt(string beltNo)
        {
            try
            {
                var belt = _coilLogic.GetBelt(beltNo);
                _log.I($"獲取 {beltNo} 皮帶資訊", $"獲取{beltNo}皮帶資訊 => {belt!=null} ");
                return belt;
            }
            catch (Exception e)
            {
                _log.E($"獲取 {beltNo} 皮帶資訊", TypeConvertUtil.CleanInvalidChar(e.Message));
                return null;
            }
        }

        public bool CreateDefectData(L1L2Rcv.Msg_108_Defect_Data msg)
        {
            try
            {
                var defectData = msg.ToTblDefectData();
                var insertNum = _coilLogic.CreateDefectData(defectData);
                _log.I($"新增 {msg.CoilID} Defect資訊", $"新增Defect Data資訊 => {insertNum > 0} ");
                return insertNum > 0;
            }
            catch (Exception e)
            {
                _log.E($"新增 {msg.CoilID} Defect資訊", TypeConvertUtil.CleanInvalidChar(e.Message));
                return false;
            }

        }

        public bool UpdateDefectData(L1L2Rcv.Msg_108_Defect_Data msg, float starLength)
        {
            try
            {
                var defectData = msg.ToTblDefectData(starLength);
                var updateNum = _coilLogic.UpdateDefectData(defectData);
                _log.I($"更新 {msg.CoilID} Defect資訊", $"更新 {msg.CoilID} Defect Data資訊 => {updateNum > 0} ");
                return updateNum > 0;
            }
            catch (Exception e)
            {
                _log.E($"新增 {msg.CoilID} Defect資訊", TypeConvertUtil.CleanInvalidChar(e.Message));
                return false;
            }

        }

        public L3L2_TBL_DefectData GetPreDefectDataByPlcMsg(L1L2Rcv.Msg_108_Defect_Data msg)
        {
            try
            {
                var defectData = _coilLogic.GetDefectData(msg);
                _log.I($"撈取資料庫最後一筆Defect資訊", $"撈取資料庫最後一筆Defect資訊 => {defectData != null} ");
                return defectData;

            }
            catch(Exception e)
            {
                _log.E($"撈取資料庫最後一筆Defect資訊", TypeConvertUtil.CleanInvalidChar(e.Message));
                return null;
            };
        }

        public float GetDefectRecordLastLength()
        {
            try
            {
                var lastLength = _coilLogic.GetDefectRecordLastLength();
                _log.I($"撈取資料庫最後一筆Defect資訊與頭端的長度", $"撈取資料庫最後一筆Defect資訊 => {lastLength} ");
                return lastLength;

            }
            catch (Exception e)
            {
                _log.E($"撈取資料庫最後一筆Defect資訊與頭端的長度", TypeConvertUtil.CleanInvalidChar(e.Message));
                return 0;
            };
        }

        

        public IEnumerable<BeltPatternsEntity.TBL_BeltPatterns> QueryBeltPatterns(string coilNo)
        {
            try
            {
                var beltPattern = _coilLogic.QueryBeltPatterns(coilNo);
                _log.I("撈取研磨機資料", $"撈取{coilNo}研磨機資料 => {beltPattern != null}");
                return beltPattern;
            }
            catch (Exception e)
            {
                _log.E($"撈取研磨機資料", $"撈取{coilNo}研磨機資料 "+ e.Message.CleanInvalidChar());
                return null;
            };
        }

        public IEnumerable<GrindPlanEntity.TBL_GrindPlan> QueryBeltPlans(string coilNo)
        {
            try
            {
                var beltPlans = _coilLogic.QueryBeltPlans(coilNo);
                _log.I("撈取研磨機Belt Plan資料", $"撈取{coilNo}研磨機Belt Pass資料 => {beltPlans.Count()>0}");
                return beltPlans;
            }
            catch (Exception e)
            {
                _log.E($"撈取研磨機Belt Plan資料", TypeConvertUtil.CleanInvalidChar(e.Message));
                return null;
            };
        }

        public void CreateGrindPlanHistory(string coilID, string planNo, IEnumerable<GrindPlanEntity.TBL_GrindPlan> beltPlans)
        {

            if (beltPlans == null)
            {
                _log.E("Belt Plan無資料", $"鋼捲{coilID}無Belt Plan資料");
                return;
            }

            foreach(GrindPlanEntity.TBL_GrindPlan beltPlan in beltPlans)
            {
                var griHis = new GrindPlanHistoryEntity.TBL_GrindPlanHistory();

                griHis.LoadBeltPassData(coilID, planNo, beltPlan);

                try
                {
                    var insertNum = _coilLogic.CreateGrindPlanHistory(griHis);
                    _log.I("紀錄Belt Plan資料", $"紀錄{coilID}  {beltPlan.Pass_Section} Belt Plan資料 => {insertNum > 0}");

                }catch(Exception e)
                {
                    _log.E("紀錄Belt Plan資料", e.Message.CleanInvalidChar());
                }
                
                LoadBeltPatternsRecords(coilID, beltPlan.BeltPattern, beltPlan.Pass_Section, planNo);
            }
        }

        private void LoadBeltPatternsRecords(string coilID, string beltPatternNumber, string pssSection, string planNo)
        {
            try
            {
                var beltPatterns = _coilLogic.QueryBeltPatternsByBelt(beltPatternNumber);

                foreach (TBL_BeltPatterns beltPattern in beltPatterns)
                {
                    var grindRecord = beltPattern.ToBeltPatternsRecords(coilID, pssSection, planNo);
                    var insertNum = _coilLogic.CreateBeltPatternsRecord(grindRecord);
                    Thread.Sleep(10);
                }

            }
            catch (Exception e)
            {
                _log.E("存取Belt Record失敗", e.Message.CleanInvalidChar());
            }
        }

        public IEnumerable<L3L2_TBL_DefectData> QueryDefectData(string coilNo, int count)
        {
            // Load Defect Data
            try
            {
                var defectDatas = _coilLogic.QueryDefect(coilNo, count);
                return defectDatas;
            }
            catch (Exception e)
            {
                _log.E("Defect Data處理錯誤", $"{e.Message}");
                return null;
            }
        }

        public bool SyncCoilRejectData(string rejectCoilNo, string planNo)
        {

            try
            {
                var temp = _coilLogic.GetCoilRejectTemp(rejectCoilNo,planNo);
                var entity = temp.ToCoilRejectReuslt();
                var insertNum = _coilLogic.CreateCoilRejectResult(entity);

                //給L2.5回退實績
                var L25entity = temp.ToL25CoilRejectReuslt();
                var insertL25Num = _coilLogic.CreateL25CoilRejectResult(L25entity);
                var insertL25OK = insertL25Num > 0;
                if (insertL25OK)
                    _log.I("L25回退實績", $"捲號{rejectCoilNo} 回退實績資料 => {insertL25Num > 0}"); ;


                var insertOK = insertNum > 0;
                if(insertOK)
                    _coilLogic.DeleteCoilRejectTemp(rejectCoilNo);
                return insertOK;

            }
            catch(Exception e)
            {

                _log.E("同步回退Temp資料與實際資料錯誤", $"{e.Message}");
                return false;
            }

        }

        #endregion

        #region -- LookUp Table --

        public LkUpTableModel.Preset204 GetPreset204LkTableData(PDIEntity.TBL_PDI pdi)
        {
            if (pdi == null)
                return new LkUpTableModel.Preset204();

            var lk204 = new LkUpTableModel.Preset204();
            //var matericalGrade = string.Empty;   //鋼種
            //var Yield_Stress = string.Empty;     //屈服強度
            var SteelNoToMaterialGrade = new TBL_SteelNoToMaterialGrade();

            // 撈取鋼種,屈服強度
            try
            {
                SteelNoToMaterialGrade = _coilLogic.GetYield_Stress(pdi.St_No.Substring(1, 5));
                if (SteelNoToMaterialGrade != null)
                {
                      _log.I("撈取鋼種屈服強度表", $"SteelNo:{pdi.St_No.Substring(1, 5)}");
                }
                else
                {
                    _log.E("無鋼種屈服強度資料", $"SteelNo:{pdi.St_No.Substring(1, 5)}");
                }

            

            }
            catch (Exception e)
            {
                _log.E("撈取鋼種屈服強度表失敗", e.Message.CleanInvalidChar());
                return lk204;
            }

            


            // 整平機
            try
            {
                _log.I("撈取整平機資料", $"YieldStress:{SteelNoToMaterialGrade.YS}  Thick{pdi.In_Coil_Thick}");
                var flattener = _coilLogic.GetFlatterBySYandThick(SteelNoToMaterialGrade.St_No, Convert.ToInt32(SteelNoToMaterialGrade.YS) / 10, pdi.In_Coil_Thick);
              
                if (flattener != null)
                {
                    lk204.FlatenerDepth1 = flattener.Intermesh_Num1;
                    lk204.FlatenerDepth2 = flattener.Intermesh_Num2;
                }
                else
                {
                    _log.E("無整平機資料", $"YieldStress:{pdi.StripYieldStress}  Thick{pdi.In_Coil_Thick}");
                }

            }
            catch (Exception e)
            {
                _log.E("撈取整平機資料失敗", TypeConvertUtil.CleanInvalidChar(e.Message));
            }

            // 套筒
            try
            {
                _log.I("撈取套筒資料", $"CODE:{pdi.In_Sleeve_Type_Code}");
                var sleeve = _coilLogic.GetSleeveData(pdi.In_Sleeve_Type_Code);

                if(sleeve != null)
                {
                    lk204.SleeveThickness = sleeve.Sleeve_Thick;
                    lk204.SleeveWidth = sleeve.Sleeve_Width;
                }
                else
                {
                    _log.E("無套筒資料", $"CODE:{pdi.In_Sleeve_Type_Code}");
                }

            }
            catch (Exception e)
            {
                _log.E("撈取套筒失敗", $"CODE:{pdi.In_Sleeve_Type_Code}" + e.Message.CleanInvalidChar());
            }


            // 張力機
            try
            {
                //_log.I("撈取張力機", $"StNo:{pdi.St_No}  Mat Thick:{pdi.In_Coil_Thick} Width:{ pdi.In_Coil_Width } ");
                var lineTension = _coilLogic.GetLineTensionByGradeAndThick(SteelNoToMaterialGrade.St_No, pdi.In_Coil_Thick,pdi.In_Coil_Width,"C");
                if (lineTension != null)
                {
                    lk204.UnitTension = lineTension.TRTension;
                    _log.I("撈取張力機资料", $"SteelNo:{SteelNoToMaterialGrade.St_No} Thick:{pdi.In_Coil_Thick} Width:{pdi.In_Coil_Width}");
                }
                else
                {
                    _log.E("無張力機資料", $"SteelNo:{SteelNoToMaterialGrade.St_No} Thick:{pdi.In_Coil_Thick} Width:{pdi.In_Coil_Width}");
                }

            }
            catch (Exception e)
            {
                _log.E("撈取張力機失敗", $"StNo:{pdi.St_No}  Mat Thick:{pdi.In_Coil_Thick} " + e.Message.CleanInvalidChar());
            }

            return lk204;
        }


        #endregion

        #region Preset
        public bool CreatePreset204Record(L2L1Snd.Msg_204_PDI_TM3 msg)
        {
            msg.VaildObjectNull("msg", "存取Preset失敗");
            try
            {

                return true;

       

            }
            catch (Exception e)
            {
              
                return false;
            };
        }
        public bool CreatePreset205Record(L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            msg.VaildObjectNull("msg", "存取Preset失敗");
            try
            {


                return true;


            }
            catch (Exception e)
            {

                return false;
            };
        }
        #endregion
        #region -- 鋼卷分切 --
        public int GetCntSplitRec(string ParentCoilNo)
        {
            int childrenCoilCnt = 0;
            try
            {
                childrenCoilCnt = _coilLogic.GetParentCnt(ParentCoilNo);
                return childrenCoilCnt;
            }
            catch (Exception e)
            {
                _log?.I("撈取分切紀錄失敗", e.Message.CleanInvalidChar());
                return -1;
            }

        }

        public string GetSplitParentCoilNo(string childCoilID)
        {
            try
            {
                var parentCoil = _coilLogic.GetSplitParentCoilNo(childCoilID);
                _log.I("撈取分切母卷號", $"子卷號:{childCoilID},母卷:{parentCoil}");
                return parentCoil.Trim();
            }
            catch(Exception e)
            {
                _log.E("撈取分切母卷號", e.Message.ToString().CleanInvalidChar());
                return string.Empty;
            }
          
        }

        public string GenSplitChildrenCoilNo(string parentCoilID)
        {

            int childrenCoilCnt = 0;

            try
            {
                childrenCoilCnt = _coilLogic.GetParentCnt(parentCoilID);
                
                _log?.I("撈取分切紀錄", $"撈取母捲之子捲筆數=>{childrenCoilCnt}");                
            }
            catch (Exception e)
            {
                _log?.I("撈取分切紀錄失敗", e.Message.CleanInvalidChar());
                return "";
            }
            //var childCoilNo = parentCoilID + (childrenCoilCnt + 1).ToString();

            var childNo = _coilLogic.SplitCoilPro(parentCoilID, childrenCoilCnt);
            _log?.I("鋼捲分切結果", $"鋼捲分切 {parentCoilID}=>{childNo}");       
            return childNo;
        }
        public bool VaildNewChildCoilNoData(string childCoil, string parentCoil)
        {
            try
            {
                var insertNum = _coilLogic.CreateChildCoilData(childCoil, parentCoil);
                _log.I($"紀錄母捲分切結果 => {insertNum > 0}", $"紀錄母捲:{parentCoil} 分切結果:{childCoil}");
                return insertNum > 0;
            }
            catch (Exception e)
            {
                _log.E("紀錄母捲分切結果 => false}", e.Message.CleanInvalidChar());
                return false;
            }
        }
        [Obsolete]
        public void UpdateHasScrapedFlag(bool hasScraped, string entryCoilID)
        {
            try
            {
                var updateNum = _coilLogic.UpdateHasScrapedFlag(hasScraped, entryCoilID);
                _log.I("紀錄PDI斷帶旗標", $"存取{entryCoilID}斷帶旗標{hasScraped}=>{updateNum > 0}");
            }
            catch (Exception e)
            {
                _log.E("紀錄PDI斷帶旗標", e.Message.CleanInvalidChar()); ;
            }
        }

        #endregion


        #region -- 套筒墊紙同步處理 --


        public bool SyncSleeveValue(MMSL2Rcv.Msg_Sleeve_Value_Synchronize msg)
        {
            if (msg.Deal_Flag.ToStr().Equals(MMSSysDef.Cmd.SyncValueDelete))
            {
                try
                {
                    var code = msg.SleeveCode.ToStr();
                    var delNum = _coilLogic.DeleteSleeveValue(code);
                    _log.I($"套筒資料同步訊息:{msg.Code.ToStr()}", $"刪除資料 CODE:{code}=>{delNum > 0}");

                    return delNum > 0;

                }
                catch (Exception e)
                {
                    _log.E($"套筒資料同步訊息:{msg.Code.ToStr()}", "刪除失敗:" + e.Message.CleanInvalidChar());
                    return false;
                }
            }

            //判断是否存在套筒代码
            var Exist = _coilLogic.ExistSleeveCode(msg.SleeveCode.ToStr());

            if (Exist)
            {
                //更新
                try
                {
                    var updateNum = _coilLogic.UpdateSleeveValue(msg);
                    _log.I($"套筒資料同步訊息:{msg.Code.ToStr()}", $"更新資料 CODE:{msg.SleeveCode.ToStr()}=>{updateNum > 0}");
                    return updateNum > 0;

                }
                catch (Exception e)
                {
                    _log.E($"套筒資料同步訊息:{msg.Code.ToStr()}", "更新失敗:" + e.Message.CleanInvalidChar());
                    return false;
                }
            }
            else
            {
                //新增
                try
                {
                    var insertNum = _coilLogic.CreateSleeveValue(msg);
                    _log.I($"套筒資料同步訊息:{msg.Code.ToStr()}", $"新增資料 CODE:{msg.SleeveCode.ToStr()}=>{insertNum > 0}");
                    return insertNum > 0;

                }
                catch (Exception e)
                {
                    _log.E($"套筒資料同步訊息:{msg.Code.ToStr()}", "新增失敗:" + e.Message.CleanInvalidChar());
                    return false;
                }
            }




            //if (msg.Deal_Flag.ToStr().Equals(MMSSysDef.Cmd.SyncValueInsert))
            //{

            //    try
            //    {
            //        var insertNum = _coilLogic.CreateSleeveValue(msg);
            //        _log.I($"套筒資料同步訊息:{msg.Code.ToStr()}", $"新增資料 CODE:{msg.SleeveCode.ToStr()}=>{insertNum > 0}");
            //        return insertNum>0;

            //    }
            //    catch (Exception e)
            //    {
            //        _log.E($"套筒資料同步訊息:{msg.Code.ToStr()}", "新增失敗:" + e.Message.CleanInvalidChar());
            //        return false;
            //    }


            //}

            //if (msg.Deal_Flag.ToStr().Equals(MMSSysDef.Cmd.SyncValueUpdate))
            //{
            //    try
            //    {
            //        var updateNum = _coilLogic.UpdateSleeveValue(msg);
            //        _log.I($"套筒資料同步訊息:{msg.Code.ToStr()}", $"更新資料 CODE:{msg.SleeveCode.ToStr()}=>{updateNum > 0}");
            //        return updateNum > 0;

            //    }
            //    catch (Exception e)
            //    {
            //        _log.E($"套筒資料同步訊息:{msg.Code.ToStr()}", "更新失敗:" + e.Message.CleanInvalidChar());
            //        return false; 
            //    }


            //}


            //return false;

        }

        
        public bool SyncPaperValue(MMSL2Rcv.Msg_Paper_Value_Synchronize msg)
        {
            if (msg.Deal_Flag.ToStr().Equals(MMSSysDef.Cmd.SyncValueDelete))
            {
                try
                {
                    var code = msg.PaperCode.ToStr();
                    var delNum = _coilLogic.DeletePaperValue(code);
                    _log.I($"墊紙資料同步訊息:{msg.Code.ToStr()}", $"刪除資料 CODE:{code}=>{delNum > 0}");
                    return delNum > 0;

                }
                catch (Exception e)
                {
                    _log.E($"墊紙資料同步訊息:{msg.Code.ToStr()}", "刪除失敗"+e.Message.CleanInvalidChar());
                    return false;
                }

              
            }
            //判断是否存在衬纸代码
            var Exist = _coilLogic.ExistPaperValue(msg.PaperCode.ToStr());

            if (Exist)
            {
                //更新
                try
                {
                    var updateNum = _coilLogic.UpdatePaperValue(msg);
                    _log.I($"墊紙資料同步訊息:{msg.Code.ToStr()}", $"更新資料 CODE:{msg.PaperCode.ToStr()}=>{updateNum > 0}");
                    return updateNum > 0;

                }
                catch (Exception e)
                {
                    _log.E($"墊紙資料同步訊息:{msg.Code.ToStr()}", "更新失敗:" + e.Message.CleanInvalidChar());
                    return false;
                }
            }
            else
            {
               //新增
                try
                {
                    var insertNum = _coilLogic.CreatePaperValue(msg);
                    _log.I($"墊紙資料同步訊息:{msg.Code.ToStr()}", $"新增資料 CODE:{msg.PaperCode.ToStr()}=>{insertNum > 0}");
                    return insertNum > 0;

                }
                catch (Exception e)
                {
                    _log.E($"墊紙資料同步訊息:{msg.Code.ToStr()}", "新增失敗:" + e.Message.CleanInvalidChar());
                    return false;
                }
            }


            //if (msg.Deal_Flag.ToStr().Equals(MMSSysDef.Cmd.SyncValueInsert))
            //{

                //    try
                //    {
                //        var insertNum = _coilLogic.CreatePaperValue(msg);
                //        _log.I($"墊紙資料同步訊息:{msg.Code.ToStr()}", $"新增資料 CODE:{msg.PaperCode.ToStr()}=>{insertNum > 0}");
                //        return insertNum > 0;

                //    }
                //    catch (Exception e)
                //    {
                //        _log.E($"墊紙資料同步訊息:{msg.Code.ToStr()}", "新增失敗:"+e.Message.CleanInvalidChar());
                //        return false;
                //    }


                //}

                //if (msg.Deal_Flag.ToStr().Equals(MMSSysDef.Cmd.SyncValueUpdate))
                //{
                //    try
                //    {
                //        var updateNum = _coilLogic.UpdatePaperValue(msg);
                //        _log.I($"墊紙資料同步訊息:{msg.Code.ToStr()}", $"更新資料 CODE:{msg.PaperCode.ToStr()}=>{updateNum > 0}");
                //        return updateNum > 0;

                //    }
                //    catch (Exception e)
                //    {
                //        _log.E($"墊紙資料同步訊息:{msg.Code.ToStr()}", "更新失敗:"+e.Message.CleanInvalidChar());
                //        return false;
                //    }
                //}



                //return false;
        }

        #endregion
    }
}
