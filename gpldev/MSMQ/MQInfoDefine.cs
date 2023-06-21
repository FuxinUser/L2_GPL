using System;


/**
 * Author: 余士鵬
 * Data: 2019/12/19
 * Desc: MQ傳輸類別事件定義
 *          Type
 *          101-150 InfoCoil
 *          151-200 InfoTrk
 *          201-250 InfoHMI
 *          251-300 InfoWMS
 *          301-400 InfoL1
 *          401-500 InfoMMS
 *          501-550 InfoDataSetup
 *          550-650 InfoLog
 *          651-700 InfoDtgr
 *          701-750 InfoBCScn
 *          751-800 InfoPrinter
 */
namespace MSMQ
{
    namespace Core.MSMQ
    {
        [Serializable]
        public class MQInfoType
        {
            /// <summary>
            /// 類別碼
            /// </summary>
            public int Event { get; private set; }
            /// <summary>
            /// 描述行為
            /// </summary>
            public string Description { get; private set; }

            public MQInfoType(int type, string description)
            {
                Event = type;
                Description = description;
            }

            public override string ToString()
            {
                return string.Format("Type Code: {0}, Description: {1}", Event, Description);
            }

            public void SetDesc(string desc)
            {
                Description = desc;
            }

            //轉Message (不更動原本架構)
            public MQPool.MQMessage Data(object data)
            {
                return new MQPool.MQMessage
                {
                    ID = Event,
                    Data = data
                };
            }

            

        }

        public class InfoCoil : MQInfoType
        {
            /// <summary>
            /// 鋼捲生產排程調整:鋼捲調整
            /// </summary>
            public static readonly InfoCoil UpdateCoilSchedule = new InfoCoil(0x101, "Info Coil Mgr Update Coil Schedule");
            /// <summary>
            /// 鋼捲生產排程調整:鋼捲刪除
            /// </summary>
            public static readonly InfoCoil DeleteCoilSchedule = new InfoCoil(0x102, "Info Coil Mgr Delete Coil Schedule");
            /// <summary>
            /// 鋼捲生產PDI
            /// </summary>
            public static readonly InfoCoil SaveCoilPDI = new InfoCoil(0x103, "Info Coil Mgr Save MMS PDI Data");
            /// <summary>
            /// 要求PDO上傳
            /// </summary>
            public static readonly InfoCoil AskSndPDO = new InfoCoil(0x104, "Info Coil Mgr Snd PDO");
            /// <summary>
            /// 檢查Exit Cut Model 
            /// </summary>
            public static readonly InfoCoil DetExCoilCut = new InfoCoil(0x105, "Info Coil Mgr det exit coil cut mode");
            /// <summary>
            /// 存取Schedule 
            /// </summary>
            public static readonly InfoCoil SaveSchedule = new InfoCoil(0x106, "Info Coil Mgr save coil schedule");
            /// <summary>
            /// 告知回退實績並須做判斷
            /// </summary>
            public static readonly InfoCoil CoilRejectResult = new InfoCoil(0x107, "Info Coil Mgr det coil reject result");
            /// <summary>
            /// 生产实绩请求(要PDO)
            /// </summary>
            public static readonly InfoCoil ReqPDO = new InfoCoil(0x108, "Info Coil Mgr info mms snd pdo");
            /// <summary>
            /// 要求MMS發送排程
            /// </summary>
            public static readonly InfoCoil AskCoilSchedule = new InfoCoil(0x109, "Info Coil Mgr ask MMS Snd Coil Schedule");
            /// <summary>
            /// 向MMS要求PDI
            /// </summary>
            public static readonly InfoCoil AskPDI = new InfoCoil(0x110, "Info Coil Mgr ask MMS Snd PDI");
            /// <summary>
            /// 三級要求整計畫刪除
            /// </summary>
            public static readonly InfoCoil DeleteShcedPlanNo = new InfoCoil(0x111, "Info Coil Mgr delete schedule by planNo");
            /// <summary>
            /// 發送能源消耗訊息 
            /// </summary>
            public static readonly InfoCoil SndEnergyConsumpInfo = new InfoCoil(0x112, "Info Coil Mgr snd enerfy consumption info");           
            /// <summary>
            /// 更新PDO預存資料（PDO_TEMP) No_Leader_Code=0
            /// </summary>
            public static readonly InfoCoil SetPDOTempLeaderCode = new InfoCoil(0x114, "Info Coil Mgr set pdo temp no leader code flag");
            /// <summary>
            /// 存取Dummy Coil至PDI資料庫
            /// </summary>
            public static readonly InfoCoil SaveDummyCoil = new InfoCoil(0x115, "Info Coil save dummy data");

            // GPL

            /// <summary>
            /// Update Belt Acc Length (皮帶里程數)
            /// </summary>
            public static readonly InfoCoil UpdateBeltAccLength = new InfoCoil(0x116, "Info Coil update belt length");
            
            /// <summary>
            /// 焊接資訊處理判定處理PDO
            /// </summary>
            public static readonly InfoCoil ProWeldRecordGenPDO = new InfoCoil(0x117, "Info Coil update PDI StarTime or EndTime");
            
            /// <summary>
            /// 通知Coil更換皮帶
            /// </summary>
            public static readonly InfoCoil ChangeBelt = new InfoCoil(0x117, "Info Coil change belt");
            
            /// <summary>
            /// 紀錄Defect Data
            /// </summary>
            public static readonly InfoCoil SaveDefectData = new InfoCoil(0x118, "Info Coil change belt");
            
            /// <summary>
            /// 結算PDO
            /// </summary>
            public static readonly InfoCoil AccountPDO = new InfoCoil(0x119, "Info Coil Mgr account PDO result");
            
            /// <summary>
            /// 更新PDO淨重
            /// </summary>
            public static readonly InfoCoil UpdateOutMatPureWT = new InfoCoil(0x120, "Info Coil Mgr update gross wt");          
            
            /// <summary>
            /// Strip Brake 斷帶資料
            /// </summary>
            public static readonly InfoCoil StripBakeInfo = new InfoCoil(0x121, "Info Coil Strip Brake");
            
            /// <summary>
            /// Strip Brake 斷帶更改Coil ID
            /// </summary>
            public static readonly InfoCoil StripBrakeModifyCoilName = new InfoCoil(0x121, "Info rename coil");

            /// <summary>
            /// Coil Split
            /// </summary>
            public static readonly InfoCoil CoilSplt = new InfoCoil(0x122, "Info coil split");

            /// <summary>
            /// 套筒静态数据同步
            /// </summary>
            public static readonly InfoCoil SyncSleeveValue = new InfoCoil(0x123, "Info coil sync sleeve value");

            /// <summary>
            /// 垫纸静态数据同步
            /// </summary>
            public static readonly InfoCoil SyncPaperValue = new InfoCoil(0x124, "Info coil sync paper value");

            /// <summary>
            /// 無鋼捲PDI回覆
            /// </summary>
            public static readonly InfoCoil ResNoPDI = new InfoCoil(0x125, "Info coil no pdi");

            /// <summary>
            /// 無鋼捲生產回覆
            /// </summary>
            public static readonly InfoCoil ResNoCoil = new InfoCoil(0x126, "Info coil no coil");

            /// <summary>
            /// 通知Coil發送Preset通知給DataSetup
            /// </summary>
            public static readonly InfoCoil SndPresetInfo = new InfoCoil(0x127, "Info coil send Preset");


            /// <summary>
            /// 通知Coil Del Dummy Coil Result
            /// </summary>
            public static readonly InfoCoil InfoDelDummyResult = new InfoCoil(0x128, "Info del coil result");

            /// <summary>
            /// 通知MMS Del Dummy Coil Result
            /// </summary>
            public static readonly InfoCoil InfoMMSDelDummyResult = new InfoCoil(0x129, "Info dummy coil");

            /// <summary>
            /// 通知L1 CoilID 給TR
            /// </summary>
            public static readonly InfoCoil InfoSndL1TRCoilID = new InfoCoil(0x130, "Info l1 tr coil id");

            /// <summary>
            /// 操作通知修改POR子捲號
            /// </summary>
            public static readonly InfoCoil ModifyPORCoilID = new InfoCoil(0x131, "Info modify por coilID");


            /// <summary>
            /// 回應是否接收PDO  added 2023.04.25
            /// </summary>
            public static readonly InfoCoil ResponRcvPDO = new InfoCoil(0x132, "Info Respon Rcv PDO");

            public InfoCoil(int type, string description) : base(type, description)
            {

            }


        }

        public class InfoTrk : MQInfoType
        {
         
            /// <summary>
            /// 確認鋼捲開始供料
            /// </summary>
            public static readonly InfoTrk CheckCoilEnterStar = new InfoTrk(0x151, "Info Trk Check Coil Enter Star");
            /// <summary>
            /// 確認鋼捲停止供料
            /// </summary>
            public static readonly InfoTrk CheckCoilEnterEnd = new InfoTrk(0x152, "Info Trk Check Coil Enter End");
            /// <summary>
            /// 鋼捲生產退料
            /// </summary>
            public static readonly InfoTrk ReturnCoil = new InfoTrk(0x153, "Info Trk Return Coil");
            /// <summary>
            /// 出口位置鋼捲號
            /// </summary>
            public static readonly InfoTrk TrackMapExCoilNo = new InfoTrk(0x154, "Info Trk Exit tracking No, send from L1");
            /// <summary>
            /// 入口位置鋼捲號
            /// </summary>
            public static readonly InfoTrk TrackMapEnCoilNo = new InfoTrk(0x155, "Info Trk Entry tracking No, send from L1");
            /// <summary>
            /// 入料/出料/退料完成訊息
            /// </summary>
            public static readonly InfoTrk WMSActionFinish = new InfoTrk(0x156, "Info Trk WMS Action Finish");
            /// <summary>
            /// 入口段鋼捲ID更正
            /// </summary>
            public static readonly InfoTrk ScnRenameCoil = new InfoTrk(0x157, "Info Trk rename coil ID");
            /// <summary>
            /// 要求發送目前Track Map
            /// </summary>
            public static readonly InfoTrk SndCurCoilMap = new InfoTrk(0x158, "Info Trk snd current coil map");
            /// <summary>
            ///  直接發入料要求
            /// </summary>
            public static readonly InfoTrk SndEntryCoilReqMsg = new InfoTrk(0x159, "Info Trk snd current coil map");
            /// <summary>
            /// 手動入料 - 操作於指定鞍座上操作入料指示
            /// </summary>
            public static readonly InfoTrk SndSkidFeedMsg = new InfoTrk(0x160, "Info Trk snd skid feed");
            /// <summary>
            /// Coil Mount 掛上準備生產
            /// </summary>
            public static readonly InfoTrk CoilMount = new InfoTrk(0x161, "Info coil mount");

            /// <summary>
            /// 開始/停止進料確認通知
            /// </summary>
            public static readonly InfoTrk CheckCoilEnterInfo = new InfoTrk(0x170, "Info Trk Enter Info");
            /// <summary>
            /// Trk Map更新
            /// </summary>
            public static readonly InfoTrk UpdateTrkMap = new InfoTrk(0x171, "Info Trk update TrkMap");

          
            /// <summary>
            /// 入料/出料/退料要求回覆
            /// </summary>
            public static readonly InfoTrk WMSCoilProResRequest = new InfoTrk(0x173, "Info Trk WMS Res Request ");

            /// <summary>
            /// 手動刪除安座鋼捲號.送L1 210訊號
            /// </summary>
            public static readonly InfoTrk InfoL1DelCoilIDOnSk = new InfoTrk(0x174, "Info Trk del coil on sk");

            /// <summary>
            /// 通知入料開始條件是否符合
            /// </summary>
            public static readonly InfoTrk InfoEntryStartCondition = new InfoTrk(0x175, "Info Trk del coil on sk");

            /// <summary>
            /// 天車入料時選擇鋼捲ID
            /// </summary>
            public static readonly InfoTrk CarneEntryCoilSelect = new InfoTrk(0x176, "Info Trk del coil on sk");


            /// <summary>
            /// 天車入料時選擇鋼捲ID
            /// </summary>
            public static readonly InfoTrk DeliveryCoilOut = new InfoTrk(0x177, "Info Trk delvery coil out");

            /// <summary>
            /// 天車入料時選擇鋼捲ID
            /// </summary>
            public static readonly InfoTrk DeliveryCoilReady = new InfoTrk(0x178, "Info Delivery Coil Ready");


            public InfoTrk(int type, string description) : base(type, description)
            {

            }
        }

        public class InfoHMI : MQInfoType
        {
       
            /// <summary>
            /// 鋼捲刪除,調整處理後通知HMI
            /// </summary>
            public static readonly InfoHMI UpdateCoilSchedView = new InfoHMI(0x203, "Info HMI Update Coil Shedule View");
            public static readonly InfoHMI BarcodeScanResult = new InfoHMI(0x204, "Info HMI Barcode Compare Result");
            public static readonly InfoHMI EventPush = new InfoHMI(0x205, "Info HMI Event");
            public static readonly InfoHMI ScheduleChangeNotice = new InfoHMI(0x206, "Info HMI Schedule Change");
            public static readonly InfoHMI StripBrakeMessage = new InfoHMI(0x207, "Info HMI Strip brake message");
            public static readonly InfoHMI CraneEntryCoil = new InfoHMI(0x208, "Info HMI Crane Entry Coil");
            // 三級是否接收PDO回應處理  MWW-added 2023.04.25
            public static readonly InfoHMI PdoUploadedReply = new InfoHMI(0x209, "Info HMI Update Pdo Uploaded Reply");



            public InfoHMI(int type, string description) : base(type, description)
            {

            }
        }

        public class InfoWMS : MQInfoType
        {
            /// <summary>
            /// 發送入料要求 (PW15)
            /// </summary>
            public static readonly InfoWMS InfoCoilEntryOrDeliveryReq = new InfoWMS(0x251, "通知WM要求入料");

            /// <summary>
            /// 發送退料要求 
            /// </summary>
            public static readonly InfoWMS RejectCoilReqMsg = new InfoWMS(0x253, "通知WMS要求退料");

            /// <summary>
            /// 鋼捲排程編號資訊
            /// </summary>
            public static readonly InfoWMS InfoCoilScheduleMsg = new InfoWMS(0x254, "通知WMS鋼捲排程編號資料");

            /// <summary>
            /// 鋼卷產出資訊
            /// </summary>
            public static readonly InfoWMS InfoiCoilPDOMsg = new InfoWMS(0x255, "通知WMS鋼卷產出資訊");

            /// <summary>
            /// 通知產線入口/出口 Tracking
            /// </summary>
            public static readonly InfoWMS InfoTrackMap = new InfoWMS(0x256, "通知WMS Tracking Map");


            /// <summary>
            /// 通知掃描ID
            /// </summary>
            public static readonly InfoWMS InfoBCSScanID = new InfoWMS(0x257, "通知WMS Scan Coil ID");

            public InfoWMS(int type, string description) : base(type, description)
            {

            }
        }

        public class InfoL1 : MQInfoType
        {
            /// <summary>
            /// 發送DelSkid (Entry)
            /// </summary>
            public static readonly InfoL1 SndDelSkEntryID = new InfoL1(0x301, "Ask L1 Delete coil data on entry coil skid");

            /// <summary>
            /// 發送Preset(204) 
            /// </summary>            
            public static readonly InfoL1 SndPreset204Msg = new InfoL1(0x302, "Ask snd preset 204 msg");
            /// <summary>
            /// 發送Preset(205)
            /// </summary>
            public static readonly InfoL1 SndPreset205Msg = new InfoL1(0x303, "Ask snd preset 205 msg");
            /// <summary>
            /// 發送Preset(202)
            /// </summary>
            public static readonly InfoL1 SndPreset202Msg = new InfoL1(0x304, "Ask snd preset 205 msg");
            /// <summary>
            /// 發送DelCoil(210)
            /// </summary>
            public static readonly InfoL1 SndDelCoil210Msg = new InfoL1(0x305, "Delete coil id on delivery TOP");
            /// <summary>
            /// 發送PDI_TM2 (203)
            /// </summary>
            public static readonly InfoL1 SndPDITM2203Msg = new InfoL1(0x306, "Coil ID Confirm result");         
            /// <summary>
            /// 發送新皮帶資訊 (206)
            /// </summary>
            public static readonly InfoL1 SndNewBeltInfo206Msg = new InfoL1(0x307, "New Belt Msg");
            /// <summary>
            /// 更改CoilID (211)
            /// </summary>
            public static readonly InfoL1 SndModifyCoilID211Msg = new InfoL1(0x308, "New Belt Msg");
         
            /// <summary>
            /// 出口掃描確認 (209) DELIVERY_BC_CONFIRM
            /// </summary>
            public static readonly InfoL1 SndDeliveryBCConfirm209Msg = new InfoL1(0x309, "Delivery BC Confirm");
            /// <summary>
            /// (207) Entry Take OVer Star Cmd
            /// </summary>
            public static readonly InfoL1 SndEntryTakeOVerStarCmd207Msg = new InfoL1(0x310, "Delivery BC Confirm");
            /// <summary>
            /// (208) Delivery To Cmd
            /// </summary>
            public static readonly InfoL1 SndDeliveryToCmd208Msg = new InfoL1(0x311, "Delivery To Cmd");


            // TESTUSING
            public static readonly InfoL1 ReSndPreset204Msg = new InfoL1(0x320, "Re snd preset 204 msg");
            public static readonly InfoL1 ReSndPreset205Msg = new InfoL1(0x321, "Re snd preset 205 msg");

            public InfoL1(int type, string description) : base(type, description)
            {

            }
        }

        public class InfoMMS : MQInfoType
        {

            /// <summary>
            /// 發送鋼捲上鞍座訊號通知
            /// </summary>
            public static readonly InfoMMS CoilLoadedOnSk = new InfoMMS(0x401, "Info MMS the coil load on sk");
            public static readonly InfoMMS CoilSceduleChanged = new InfoMMS(0x402, "Info MMS the coil schedule changed");
            public static readonly InfoMMS CoilSceduleDeleted = new InfoMMS(0x403, "Info MMS the coil schedule deleted");
            public static readonly InfoMMS CoilRejectResult = new InfoMMS(0x404, "Info MMS the coil reject data");
            public static readonly InfoMMS UploadLineFaultRecord = new InfoMMS(0x405, "Info MMS Snd Equipment Down Result ");
            public static readonly InfoMMS ClientInfoSndPDO = new InfoMMS(0x406, "Info MMS Client ask snd PDO to MMS  ");
            public static readonly InfoMMS ResCoilSchedResult = new InfoMMS(0x407, "Info MMS Client response coil schedule result ");
            public static readonly InfoMMS SndPDIProResult = new InfoMMS(0x408, "Info MMS Client send pdi pro result ");
            public static readonly InfoMMS MMSInfoSndkPDO = new InfoMMS(0x409, "Info MMS send PDO");
            public static readonly InfoMMS AskCoilSchedule = new InfoMMS(0x410, "Info MMS ask coil schedule");
            public static readonly InfoMMS AskPDI = new InfoMMS(0x411, "Info MMS ask pdi");
            public static readonly InfoMMS ResPlanNoShedDelResult = new InfoMMS(0x412, "Info MMS Client response coil schedule delete result by planNo ");
            public static readonly InfoMMS SndEnergyConsumptionInfo = new InfoMMS(0x413,"Info MMS Snd energy consumption info");
            public static readonly InfoMMS RequestDummyCoil = new InfoMMS(0x414, "Info MMS request dummy coil");
            public static readonly InfoMMS DeleteDummyCoil = new InfoMMS(0x415, "Info MMS delete dummy coil");
            public static readonly InfoMMS UploadEnergyConsumptionInfo = new InfoMMS(0x416, "Info MMS Upload energy consumption info");

            public InfoMMS
                (int type, string description) : base(type, description)
            {

            }
        }

        public class InfoDataSetup : MQInfoType
        {

            public static readonly InfoDataSetup CoilSchedIDTo_204_205Msg = new InfoDataSetup(0x501, "Info DataSetup get pdi from shedule ids convert to Preset Data");
 
            public static readonly InfoDataSetup CoilIDTo202Msg = new InfoDataSetup(0x502, "Info DataSetup get pdi from shedule ids convert to 202 Preset Data");

            public static readonly InfoDataSetup SpecificIDTo_204_205Msg = new InfoDataSetup(0x503, "Info DataSetup get pdi from specic ids convert to 204 205 Preset Data");


            // TESTUSING - Resend
            public static readonly InfoDataSetup ResendL1Msg = new InfoDataSetup(0x504, "Re Send L1 Msg");

            public InfoDataSetup(int type, string description) : base(type, description)
            {

            }
        }

        public class InfoLog : MQInfoType
        {
            public static readonly InfoLog SaveLogMsg = new InfoLog(0x551, "Info LogMg Save Log Msg");

            public InfoLog
               (int type, string description) : base(type, description)
            {

            }
        }

        public class InfoDtGtr : MQInfoType
        {
  
            public static readonly InfoDtGtr SaveL1106WeldData = new InfoDtGtr(0x651, "Info Data Garthering Save 106");
            public static readonly InfoDtGtr SaveL1107GrindRecords = new InfoDtGtr(0x652, "Info Data Garthering Save 107");
            public static readonly InfoDtGtr SaveL1104ProcessData = new InfoDtGtr(0x653, "Info Data Garthering Save 104");
            public static readonly InfoDtGtr SaveL112Utility = new InfoDtGtr(0x654, "Info Data Garthering Save 112");
            public static readonly InfoDtGtr SaveL1124StripBrakeSignal = new InfoDtGtr(0x655, "Info Data Garthering Save 124");
            public static readonly InfoDtGtr SaveL1111LineFault = new InfoDtGtr(0x656, "Info Data Garthering Save 111");         
            public static readonly InfoDtGtr SaveL1125CoilShareCutData = new InfoDtGtr(0x657, "Info Data Garthering Save 125");
            public static readonly InfoDtGtr SaveL1126CoilUmountPOR = new InfoDtGtr(0x658, "Info Data Garthering Save 126");
            public static readonly InfoDtGtr UploadLineFault = new InfoDtGtr(0x659, "Info Data Garthering upload line fault data");

            public InfoDtGtr(int type, string description) : base(type, description)
            {

            }
        }

        public class InfoBCScn : MQInfoType
        {
            public static readonly InfoBCScn ScanEntryCoilNo = new InfoBCScn(0x701, "");
            public static readonly InfoBCScn ScanDeliveryCoilNo = new InfoBCScn(0x702, "");

            public InfoBCScn
               (int type, string description) : base(type, description)
            {

            }
        }

        public class InfoLpr : MQInfoType
        {
            public static readonly InfoLpr ManualPrint = new InfoLpr(0x751, "Info Printer do manual print");

            public static readonly InfoLpr CoilInExitSK2 = new InfoLpr(0x752, "Info Printer coil in exit sk2 to print");


            public InfoLpr
               (int type, string description) : base(type, description)
            {

            }
        }
    }
}
