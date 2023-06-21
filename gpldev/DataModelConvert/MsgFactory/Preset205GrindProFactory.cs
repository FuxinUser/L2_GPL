using Core.Define;
using Core.Util;
using DBService.Repository.BeltPatterns;
using MsgStruct;
using System.Collections.Generic;
using System.Linq;

namespace MsgConvert.Msg
{
    public static class Preset205GrindProFactory
    {

        public static L2L1Snd.Msg_205_PDI_TM3_2 Load205BeltPattern(this L2L1Snd.Msg_205_PDI_TM3_2 msg, IEnumerable<BeltPatternsEntity.TBL_BeltPatterns> beltPatterns)
        {
          
            foreach (BeltPatternsEntity.TBL_BeltPatterns beltPattern in beltPatterns)
            {

                var beltPatternsList = beltPatterns.ToList();
                
                for (int i = beltPattern.Pass_From; i <= beltPattern.Pass_To; i++)
                    LoadGRData(i, beltPattern, ref msg);
            }
            return msg;
        }

        #region Det Pos
        private static void LoadGRData(int passNum, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            switch (beltData.Pass_Section)
            {
                case MMSSysDef.Cmd.PassSectionHead:
                    LoadHeadPass(passNum, beltData, ref msg);
                    break;
                case MMSSysDef.Cmd.PassSectionCenter:
                    LoadCenterPass(passNum, beltData, ref msg);
                    break;
                case MMSSysDef.Cmd.PassSectionTail:
                    LoadTailPass(passNum, beltData, ref msg);
                    break;
            }

        }
        //
        private static void LoadHeadPass(int pass, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            switch (pass)
            {
                case 5:
                    LoadHeadPass5GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed5Head = beltData.LineSpeed.ToShort();
                    break;
                case 6:
                    LoadHeadPass6GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed6Head = beltData.LineSpeed.ToShort();
                    break;
                case 7:
                    LoadHeadPass7GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed7Head = beltData.LineSpeed.ToShort();
                    break;
                case 8:
                    LoadHeadPass8GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed8Head = beltData.LineSpeed.ToShort();
                    break;
                case 9:
                    LoadHeadPass9GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed9Head = beltData.LineSpeed.ToShort();
                    break;

            }
        }
        private static void LoadCenterPass(int pass, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            switch (pass)
            {

                case 5:
                    LoadCenterPass5GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed5Center = beltData.LineSpeed.ToShort();
                    break;
                case 6:
                    LoadCenterPass6GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed6Center = beltData.LineSpeed.ToShort();
                    break;
                case 7:
                    LoadCenterPass7GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed7Center = beltData.LineSpeed.ToShort();
                    break;
                case 8:
                    LoadCenterPass8GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed8Center = beltData.LineSpeed.ToShort();
                    break;
                case 9:
                    LoadCenterPass9GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed9Center = beltData.LineSpeed.ToShort();
                    break;

            }
        }
        private static void LoadTailPass(int pass, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            switch (pass)
            {

                case 5:
                    LoadTailPass5GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed5Tail = beltData.LineSpeed.ToShort();
                    break;
                case 6:
                    LoadTailPass6GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed6Tail = beltData.LineSpeed.ToShort();
                    break;
                case 7:
                    LoadTailPass7GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed7Tail = beltData.LineSpeed.ToShort();
                    break;
                case 8:
                    LoadTailPass8GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed8Tail = beltData.LineSpeed.ToShort();
                    break;
                case 9:
                    LoadTailPass9GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed9Tail = beltData.LineSpeed.ToShort();
                    break;
            }
        }
        #endregion

        #region Head Pass
        private static void LoadHeadPass5GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            switch (grNo)
            {
                case 1:
                    //msg.No1GrAbBeltKind5Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness5Head = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection5Head = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet5Head = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed5Head = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    //msg.No2GrAbBeltKind1Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness5Head = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection5Head = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet5Head = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed5Head = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    //msg.No3GrAbBeltKind5Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness5Head = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection5Head = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet5Head = beltData.GR_Current.ToShort();
                    break;
                case 4:
                    //msg.No4GrAbBeltKind1Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness5Head = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection5Head = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet5Head = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    //msg.No4GrAbBeltKind1Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness5Head = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection5Head = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet5Head = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    //msg.No4GrAbBeltKind1Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness5Head = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection5Head = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet5Head = beltData.GR_Current.ToShort();
                    break;

            }
        }
        private static void LoadHeadPass6GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            switch (grNo)
            {
                case 1:
                    //msg.No1GrAbBeltKind6Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness6Head = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection6Head = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet6Head = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed6Head = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    //msg.No2GrAbBeltKind2Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness6Head = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection6Head = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet6Head = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed6Head = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    //msg.No3GrAbBeltKind6Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness6Head = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection6Head = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet6Head = beltData.GR_Current.ToShort();
                    break;
                case 4:
                    //msg.No4GrAbBeltKind6Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness6Head = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection6Head = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet6Head = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    //msg.No4GrAbBeltKind6Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness6Head = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection6Head = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet6Head = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    //msg.No4GrAbBeltKind6Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness6Head = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection6Head = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet6Head = beltData.GR_Current.ToShort();
                    break;

            }
        }
        private static void LoadHeadPass7GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            switch (grNo)
            {
                case 1:
                    //msg.No1GrAbBeltKind6Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness7Head = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection7Head = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet7Head = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed7Head = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    //msg.No2GrAbBeltKind2Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness7Head = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection7Head = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet7Head = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed7Head = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    //msg.No3GrAbBeltKind6Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness7Head = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection7Head = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet7Head = beltData.GR_Current.ToShort();
                    break;
                case 4:
                    //msg.No4GrAbBeltKind6Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness7Head = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection7Head = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet7Head = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    //msg.No4GrAbBeltKind6Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness7Head = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection7Head = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet7Head = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    //msg.No4GrAbBeltKind6Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness7Head = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection7Head = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet7Head = beltData.GR_Current.ToShort();
                    break;

            }
        }
        private static void LoadHeadPass8GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            switch (grNo)
            {
                case 1:
                    //msg.No1GrAbBeltKind6Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness8Head = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection8Head = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet8Head = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed8Head = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    //msg.No2GrAbBeltKind2Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness8Head = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection8Head = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet8Head = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed8Head = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    //msg.No3GrAbBeltKind6Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness8Head = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection8Head = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet8Head = beltData.GR_Current.ToShort();
                    break;
                case 4:
                    //msg.No4GrAbBeltKind6Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness8Head = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection8Head = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet8Head = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    //msg.No4GrAbBeltKind6Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness8Head = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection8Head = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet8Head = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    //msg.No4GrAbBeltKind6Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness8Head = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection8Head = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet8Head = beltData.GR_Current.ToShort();
                    break;
            }
        }
        private static void LoadHeadPass9GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            switch (grNo)
            {
                case 1:
                    //msg.No1GrAbBeltKind6Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness9Head = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection9Head = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet9Head = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed9Head = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    //msg.No2GrAbBeltKind2Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness9Head = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection9Head = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet9Head = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed9Head = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    //msg.No3GrAbBeltKind6Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness9Head = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection9Head = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet9Head = beltData.GR_Current.ToShort();
                    break;
                case 4:
                    //msg.No4GrAbBeltKind6Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness9Head = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection9Head = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet9Head = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    //msg.No4GrAbBeltKind6Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness9Head = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection9Head = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet9Head = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    //msg.No4GrAbBeltKind6Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness9Head = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection9Head = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet9Head = beltData.GR_Current.ToShort();
                    break;

            }
        }
        #endregion

        #region Medium Pass
        private static void LoadCenterPass5GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind5Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness5Center = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection5Center = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet5Center = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed5Center = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind5Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness5Center = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection5Center = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet5Center = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed5Center = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind5Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness5Center = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection5Center = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet5Center = beltData.GR_Current.ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind5Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness5Center = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection5Center = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet5Center = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind5Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness5Center = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection5Center = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet5Center = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind5Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness5Center = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection5Center = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet5Center = beltData.GR_Current.ToShort();
                    break;

            }
        }
        private static void LoadCenterPass6GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind6Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness6Center = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection6Center = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet6Center = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed6Center = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind6Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness6Center = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection6Center = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet6Center = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed6Center = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind6Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness6Center = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection6Center = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet6Center = beltData.GR_Current.ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind6Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness6Center = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection6Center = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet6Center = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind6Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness6Center = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection6Center = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet6Center = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind6Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness6Center = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection6Center = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet6Center = beltData.GR_Current.ToShort();
                    break;

            }
        }
        private static void LoadCenterPass7GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind7Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness7Center = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection7Center = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet7Center = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed7Center = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind7Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness7Center = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection7Center = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet7Center = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed7Center = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind7Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness7Center = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection7Center = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet7Center = beltData.GR_Current.ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind7Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness7Center = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection7Center = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet7Center = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind7Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness7Center = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection7Center = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet7Center = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind7Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness7Center = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection7Center = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet7Center = beltData.GR_Current.ToShort();
                    break;

            }
        }
        private static void LoadCenterPass8GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind8Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness8Center = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection8Center = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet8Center = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed8Center = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind8Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness8Center = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection8Center = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet8Center = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed8Center = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind8Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness8Center = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection8Center = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet8Center = beltData.GR_Current.ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind8Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness8Center = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection8Center = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet8Center = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind8Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness8Center = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection8Center = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet8Center = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind8Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness8Center = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection8Center = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet8Center = beltData.GR_Current.ToShort();
                    break;

            }
        }
        private static void LoadCenterPass9GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind9Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness9Center = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection9Center = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet9Center = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed9Center = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind9Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness9Center = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection9Center = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet9Center = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed9Center = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind9Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness9Center = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection9Center = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet9Center = beltData.GR_Current.ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind9Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness9Center = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection9Center = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet9Center = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind9Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness9Center = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection9Center = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet9Center = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind9Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness9Center = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection9Center = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet9Center = beltData.GR_Current.ToShort();
                    break;

            }
        }

        #endregion

        #region Tail Pass
        private static void LoadTailPass5GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind5Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness5Tail = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection5Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet5Tail = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed5Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind5Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness5Tail = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection5Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet5Tail = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed5Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind5Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness5Tail = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection5Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet5Tail = beltData.GR_Current.ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind5Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness5Tail = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection5Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet5Tail = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind5Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness5Tail = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection5Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet5Tail = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind5Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness5Tail = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection5Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet5Tail = beltData.GR_Current.ToShort();
                    break;

            }
        }
        private static void LoadTailPass6GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind6Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness6Tail = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection6Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet6Tail = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed6Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind6Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness6Tail = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection6Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet6Tail = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed6Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind6Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness6Tail = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection6Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet6Tail = beltData.GR_Current.ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind6Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness6Tail = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection6Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet6Tail = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind6Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness6Tail = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection6Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet6Tail = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind6Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness6Tail = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection6Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet6Tail = beltData.GR_Current.ToShort();
                    break;

            }
        }
        private static void LoadTailPass7GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind7Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness7Tail = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection7Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet7Tail = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed7Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind7Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness7Tail = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection7Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet7Tail = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed7Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind7Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness7Tail = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection7Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet7Tail = beltData.GR_Current.ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind7Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness7Tail = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection7Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet7Tail = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind7Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness7Tail = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection7Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet7Tail = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind7Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness7Tail = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection7Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet7Tail = beltData.GR_Current.ToShort();
                    break;

            }
        }
        private static void LoadTailPass8GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind8Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness8Tail = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection8Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet8Tail = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed8Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind8Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness8Tail = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection8Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet8Tail = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed8Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind8Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness8Tail = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection8Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet8Tail = beltData.GR_Current.ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind8Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness8Tail = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection8Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet8Tail = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind8Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness8Tail = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection8Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet8Tail = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind8Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness8Tail = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection8Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet8Tail = beltData.GR_Current.ToShort();
                    break;

            }
        }
        private static void LoadTailPass9GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind9Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness9Tail = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection9Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet9Tail = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed9Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind9Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness9Tail = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection9Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet9Tail = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed9Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind9Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness9Tail = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection9Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet9Tail = beltData.GR_Current.ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind9Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness9Tail = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection9Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet9Tail = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind9Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness9Tail = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection9Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet9Tail = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind9Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness9Tail = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection9Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet9Tail = beltData.GR_Current.ToShort();
                    break;

            }
        }
        #endregion

    }
}
