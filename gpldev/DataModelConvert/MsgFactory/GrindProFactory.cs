using Core.Define;
using Core.Util;
using DBService.Repository.BeltPatterns;
using MsgStruct;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MsgConvert.Msg
{
    public static class GrindProFactory
    {

        public static void LoadBeltPattern(IEnumerable<BeltPatternsEntity.TBL_BeltPatterns> beltPatterns, ref L2L1Snd.Msg_204_PDI_TM3 preset204Msg, ref L2L1Snd.Msg_205_PDI_TM3_2 preset205Msg)
        {
            var beltPatternsList = beltPatterns.ToList();
            try
            {
                preset204Msg.PassNumberForCoilHeadGrinding = beltPatternsList.Where(x => x.Pass_Section.Equals("H")).Select(v => v.PassNumber.ToShort()).FirstOrDefault();
                preset204Msg.PassNumberForCoilCenterGrinding = beltPatternsList.Where(x => x.Pass_Section.Equals("M")).Select(v => v.PassNumber.ToShort()).FirstOrDefault();
                preset204Msg.PassNumberForCoilTailGrinding = beltPatternsList.Where(x => x.Pass_Section.Equals("T")).Select(v => v.PassNumber.ToShort()).FirstOrDefault();
            }
            catch
            {
                preset204Msg.PassNumberForCoilHeadGrinding = 0;
                preset204Msg.PassNumberForCoilCenterGrinding =0;
                preset204Msg.PassNumberForCoilTailGrinding = 0;
            }
          

            foreach (BeltPatternsEntity.TBL_BeltPatterns beltPattern in beltPatterns)
            {
                for (int i = beltPattern.Pass_From; i <= beltPattern.Pass_To; i++)
                    LoadGRData(i, beltPattern, ref preset204Msg, ref preset205Msg);
            }

        }

        private static void LoadGRData(int passNum, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_204_PDI_TM3 preset204Msg, ref L2L1Snd.Msg_205_PDI_TM3_2 preset205Msg)
        {
            switch (beltData.Pass_Section)
            {
                case MMSSysDef.Cmd.PassSectionHead:
                    LoadHeadPass(passNum, beltData, ref preset204Msg, ref preset205Msg);
                    break;
                case MMSSysDef.Cmd.PassSectionCenter:
                    LoadCenterPass(passNum, beltData, ref preset204Msg, ref preset205Msg);
                    break;
                case MMSSysDef.Cmd.PassSectionTail:
                    LoadTailPass(passNum, beltData, ref preset204Msg, ref preset205Msg);
                    break;
            }

        }

        #region Det Pos
        private static void LoadHeadPass(int pass, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_204_PDI_TM3 preset204Msg, ref L2L1Snd.Msg_205_PDI_TM3_2 preset205Msg)
        {
            switch (pass)
            {
                case 1:
                    LoadHeadPass1GR(beltData.GR_NO, beltData, ref preset204Msg);
                    preset204Msg.LineSpeed1Head = beltData.LineSpeed.ToShort();
                    break;
                case 2:
                    LoadHeadPass2GR(beltData.GR_NO, beltData, ref preset204Msg);
                    preset204Msg.LineSpeed2Head = beltData.LineSpeed.ToShort();
                    break;
                case 3:
                    LoadHeadPass3GR(beltData.GR_NO, beltData, ref preset204Msg);
                    preset204Msg.LineSpeed3Head = beltData.LineSpeed.ToShort();
                    break;
                case 4:
                    LoadHeadPass4GR(beltData.GR_NO, beltData, ref preset204Msg);
                    preset204Msg.LineSpeed4Head = beltData.LineSpeed.ToShort();
                    break;
                case 5:
                    LoadHeadPass5GR(beltData.GR_NO, beltData, ref preset205Msg);
                    preset205Msg.LineSpeed5Head = beltData.LineSpeed.ToShort();
                    break;
                case 6:
                    LoadHeadPass6GR(beltData.GR_NO, beltData, ref preset205Msg);
                    preset205Msg.LineSpeed6Head = beltData.LineSpeed.ToShort();
                    break;
                case 7:
                    LoadHeadPass7GR(beltData.GR_NO, beltData, ref preset205Msg);
                    preset205Msg.LineSpeed7Head = beltData.LineSpeed.ToShort();
                    break;
                case 8:
                    LoadHeadPass8GR(beltData.GR_NO, beltData, ref preset205Msg);
                    preset205Msg.LineSpeed8Head = beltData.LineSpeed.ToShort();
                    break;
                case 9:
                    LoadHeadPass9GR(beltData.GR_NO, beltData, ref preset205Msg);
                    preset205Msg.LineSpeed9Head = beltData.LineSpeed.ToShort();
                    break;
            }
        }
        private static void LoadCenterPass(int pass, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_204_PDI_TM3 preset204Msg, ref L2L1Snd.Msg_205_PDI_TM3_2 preset205Msg)
        {
            switch (pass)
            {

                case 1:
                    LoadCenterPass1GR(beltData.GR_NO, beltData, ref preset204Msg);
                    preset204Msg.LineSpeed1Center = beltData.LineSpeed.ToShort();
                    break;
                case 2:
                    LoadCenterPass2GR(beltData.GR_NO, beltData, ref preset204Msg);
                    preset204Msg.LineSpeed2Center = beltData.LineSpeed.ToShort();
                    break;
                case 3:
                    LoadCenterPass3GR(beltData.GR_NO, beltData, ref preset204Msg);
                    preset204Msg.LineSpeed3Center = beltData.LineSpeed.ToShort();
                    break;
                case 4:
                    LoadCenterPass4GR(beltData.GR_NO, beltData, ref preset204Msg);
                    preset204Msg.LineSpeed4Center = beltData.LineSpeed.ToShort();
                    break;
                case 5:
                    LoadCenterPass5GR(beltData.GR_NO, beltData, ref preset205Msg);
                    preset205Msg.LineSpeed5Center = beltData.LineSpeed.ToShort();
                    break;
                case 6:
                    LoadCenterPass6GR(beltData.GR_NO, beltData, ref preset205Msg);
                    preset205Msg.LineSpeed6Center = beltData.LineSpeed.ToShort();
                    break;
                case 7:
                    LoadCenterPass7GR(beltData.GR_NO, beltData, ref preset205Msg);
                    preset205Msg.LineSpeed7Center = beltData.LineSpeed.ToShort();
                    break;
                case 8:
                    LoadCenterPass8GR(beltData.GR_NO, beltData, ref preset205Msg);
                    preset205Msg.LineSpeed8Center = beltData.LineSpeed.ToShort();
                    break;
                case 9:
                    LoadCenterPass9GR(beltData.GR_NO, beltData, ref preset205Msg);
                    preset205Msg.LineSpeed9Center = beltData.LineSpeed.ToShort();
                    break;

            }
        }
        private static void LoadTailPass(int pass, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_204_PDI_TM3 preset204Msg, ref L2L1Snd.Msg_205_PDI_TM3_2 preset205Msg)
        {
            switch (pass)
            {

                case 1:
                    LoadTailPass1GR(beltData.GR_NO, beltData, ref preset204Msg);
                    preset204Msg.LineSpeed1Tail = beltData.LineSpeed.ToShort();
                    break;
                case 2:
                    LoadTailPass2GR(beltData.GR_NO, beltData, ref preset204Msg);
                    preset204Msg.LineSpeed2Tail = beltData.LineSpeed.ToShort();
                    break;
                case 3:
                    LoadTailPass3GR(beltData.GR_NO, beltData, ref preset204Msg);
                    preset204Msg.LineSpeed3Tail = beltData.LineSpeed.ToShort();
                    break;
                case 4:
                    LoadTailPass4GR(beltData.GR_NO, beltData, ref preset204Msg);
                    preset204Msg.LineSpeed4Tail = beltData.LineSpeed.ToShort();
                    break;
                case 5:
                    LoadTailPass5GR(beltData.GR_NO, beltData, ref preset205Msg);
                    preset205Msg.LineSpeed5Tail = beltData.LineSpeed.ToShort();
                    break;
                case 6:
                    LoadTailPass6GR(beltData.GR_NO, beltData, ref preset205Msg);
                    preset205Msg.LineSpeed6Tail = beltData.LineSpeed.ToShort();
                    break;
                case 7:
                    LoadTailPass7GR(beltData.GR_NO, beltData, ref preset205Msg);
                    preset205Msg.LineSpeed7Tail = beltData.LineSpeed.ToShort();
                    break;
                case 8:
                    LoadTailPass8GR(beltData.GR_NO, beltData, ref preset205Msg);
                    preset205Msg.LineSpeed8Tail = beltData.LineSpeed.ToShort();
                    break;
                case 9:
                    LoadTailPass9GR(beltData.GR_NO, beltData, ref preset205Msg);
                    preset205Msg.LineSpeed9Tail = beltData.LineSpeed.ToShort();
                    break;
            }
        }
        #endregion

        #region Head Pass
        private static void LoadHeadPass1GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_204_PDI_TM3 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind1Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness1Head = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection1Head = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet1Head = ((beltData.GR_Current*10)).ToShort();
                    msg.No1GrAbBeltSpeed1Head = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind1Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness1Head = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection1Head = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet1Head = ((beltData.GR_Current*10)).ToShort();
                    msg.No2GrAbBeltSpeed1Head = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind1Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness1Head = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection1Head = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet1Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind1Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness1Head = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection1Head = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet1Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind1Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness1Head = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection1Head = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet1Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind1Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness1Head = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection1Head = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet1Head = ((beltData.GR_Current*10) ).ToShort();
                    break;


            }
        }
        private static void LoadHeadPass2GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_204_PDI_TM3 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind2Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness2Head = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection2Head = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet2Head = ((beltData.GR_Current*10) ).ToShort();
                    msg.No1GrAbBeltSpeed2Head = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind2Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness2Head = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection2Head = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet2Head = ((beltData.GR_Current*10) ).ToShort();
                    msg.No2GrAbBeltSpeed2Head = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind2Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness2Head = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection2Head = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet2Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind2Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness2Head = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection2Head = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet2Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind2Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness2Head = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection2Head = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet2Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind2Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness2Head = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection2Head = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet2Head = ((beltData.GR_Current*10) ).ToShort();
                    break;

            }
        }
        private static void LoadHeadPass3GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_204_PDI_TM3 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind3Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness3Head = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection3Head = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet3Head = ((beltData.GR_Current*10) ).ToShort();
                    msg.No1GrAbBeltSpeed3Head = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind3Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness3Head = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection3Head = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet3Head = ((beltData.GR_Current*10) ).ToShort();
                    msg.No2GrAbBeltSpeed3Head = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind3Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness3Head = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection3Head = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet3Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind3Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness3Head = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection3Head = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet3Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind3Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness3Head = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection3Head = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet3Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind3Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness3Head = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection3Head = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet3Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
            }
        }
        private static void LoadHeadPass4GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_204_PDI_TM3 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind4Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness4Head = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection4Head = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet4Head = ((beltData.GR_Current*10) ).ToShort();
                    msg.No1GrAbBeltSpeed4Head = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind4Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness4Head = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection4Head = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet4Head = ((beltData.GR_Current*10) ).ToShort();
                    msg.No2GrAbBeltSpeed4Head = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind4Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness4Head = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection4Head = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet4Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind4Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness4Head = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection4Head = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet4Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind4Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness4Head = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection4Head = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet4Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind4Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness4Head = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection4Head = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet4Head = ((beltData.GR_Current*10) ).ToShort();
                    break;

            }
        }
        private static void LoadHeadPass5GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind5Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness5Head = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection5Head = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet5Head = ((beltData.GR_Current*10) ).ToShort();
                    msg.No1GrAbBeltSpeed5Head = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind5Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness5Head = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection5Head = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet5Head = ((beltData.GR_Current*10) ).ToShort();
                    msg.No2GrAbBeltSpeed5Head = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind5Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness5Head = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection5Head = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet5Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind5Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness5Head = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection5Head = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet5Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind5Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness5Head = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection5Head = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet5Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind5Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness5Head = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection5Head = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet5Head = ((beltData.GR_Current*10) ).ToShort();
                    break;

            }
        }
        private static void LoadHeadPass6GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind6Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness6Head = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection6Head = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet6Head = ((beltData.GR_Current*10) ).ToShort();
                    msg.No1GrAbBeltSpeed6Head = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind6Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness6Head = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection6Head = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet6Head = ((beltData.GR_Current*10) ).ToShort();
                    msg.No2GrAbBeltSpeed6Head = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind6Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness6Head = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection6Head = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet6Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind6Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness6Head = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection6Head = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet6Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind6Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness6Head = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection6Head = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet6Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind6Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness6Head = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection6Head = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet6Head = ((beltData.GR_Current*10) ).ToShort();
                    break;

            }
        }
        private static void LoadHeadPass7GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind7Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness7Head = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection7Head = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet7Head = ((beltData.GR_Current*10) ).ToShort();
                    msg.No1GrAbBeltSpeed7Head = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind7Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness7Head = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection7Head = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet7Head = ((beltData.GR_Current*10) ).ToShort();
                    msg.No2GrAbBeltSpeed7Head = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind7Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness7Head = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection7Head = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet7Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind7Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness7Head = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection7Head = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet7Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind7Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness7Head = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection7Head = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet7Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind7Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness7Head = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection7Head = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet7Head = ((beltData.GR_Current*10) ).ToShort();
                    break;

            }
        }
        private static void LoadHeadPass8GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind8Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness8Head = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection8Head = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet8Head = ((beltData.GR_Current*10) ).ToShort();
                    msg.No1GrAbBeltSpeed8Head = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind8Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness8Head = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection8Head = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet8Head = ((beltData.GR_Current*10) ).ToShort();
                    msg.No2GrAbBeltSpeed8Head = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind8Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness8Head = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection8Head = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet8Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind8Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness8Head = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection8Head = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet8Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind8Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness8Head = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection8Head = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet8Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind8Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness8Head = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection8Head = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet8Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
            }
        }
        private static void LoadHeadPass9GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind9Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness9Head = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection9Head = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet9Head = ((beltData.GR_Current*10) ).ToShort();
                    msg.No1GrAbBeltSpeed9Head = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind9Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness9Head = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection9Head = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet9Head = ((beltData.GR_Current*10) ).ToShort();
                    msg.No2GrAbBeltSpeed9Head = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind9Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness9Head = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection9Head = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet9Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind9Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness9Head = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection9Head = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet9Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind9Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness9Head = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection9Head = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet9Head = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind9Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness9Head = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection9Head = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet9Head = ((beltData.GR_Current*10) ).ToShort();
                    break;

            }
        }
        #endregion

        #region Medium Pass
        private static void LoadCenterPass1GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_204_PDI_TM3 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind1Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness1Center = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection1Center = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet1Center = ((beltData.GR_Current*10) ).ToShort();
                    msg.No1GrAbBeltSpeed1Center = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind1Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness1Center = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection1Center = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet1Center = ((beltData.GR_Current*10) ).ToShort();
                    msg.No2GrAbBeltSpeed1Center = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind1Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness1Center = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection1Center = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet1Center = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind1Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness1Center = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection1Center = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet1Center = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind1Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness1Center = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection1Center = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet1Center = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind1Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness1Center = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection1Center = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet1Center = ((beltData.GR_Current*10) ).ToShort();
                    break;

            }
        }
        private static void LoadCenterPass2GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_204_PDI_TM3 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind2Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness2Center = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection2Center = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet2Center = ((beltData.GR_Current*10) ).ToShort();
                    msg.No1GrAbBeltSpeed2Center = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind2Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness2Center = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection2Center = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet2Center = ((beltData.GR_Current*10) ).ToShort();
                    msg.No2GrAbBeltSpeed2Center = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind2Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness2Center = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection2Center = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet2Center = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind2Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness2Center = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection2Center = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet2Center = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind2Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness2Center = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection2Center = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet2Center = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind2Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness2Center = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection2Center = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet2Center = ((beltData.GR_Current*10) ).ToShort();
                    break;

            }
        }
        private static void LoadCenterPass3GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_204_PDI_TM3 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind3Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness3Center = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection3Center = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet3Center = ((beltData.GR_Current*10) ).ToShort();
                    msg.No1GrAbBeltSpeed3Center = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind3Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness3Center = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection3Center = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet3Center = ((beltData.GR_Current*10) ).ToShort();
                    msg.No2GrAbBeltSpeed3Center = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind3Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness3Center = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection3Center = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet3Center = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind3Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness3Center = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection3Center = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet3Center = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind3Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness3Center = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection3Center = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet3Center = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind3Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness3Center = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection3Center = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet3Center = ((beltData.GR_Current*10) ).ToShort();
                    break;

            }
        }
        private static void LoadCenterPass4GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_204_PDI_TM3 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind4Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness4Center = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection4Center = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet4Center = ((beltData.GR_Current*10) ).ToShort();
                    msg.No1GrAbBeltSpeed4Center = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind4Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness4Center = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection4Center = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet4Center = ((beltData.GR_Current*10) ).ToShort();
                    msg.No2GrAbBeltSpeed4Center = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind4Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness4Center = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection4Center = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet4Center = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind4Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness4Center = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection4Center = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet4Center = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind4Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness4Center = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection4Center = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet4Center = ((beltData.GR_Current*10) ).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind4Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness4Center = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection4Center = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet4Center = ((beltData.GR_Current*10) ).ToShort();
                    break;

            }
        }
        private static void LoadCenterPass5GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind5Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness5Center = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection5Center = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet5Center = (beltData.GR_Current*10).ToShort();
                    msg.No1GrAbBeltSpeed5Center = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind5Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness5Center = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection5Center = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet5Center = (beltData.GR_Current*10).ToShort();
                    msg.No2GrAbBeltSpeed5Center = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind5Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness5Center = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection5Center = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet5Center = (beltData.GR_Current*10).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind5Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness5Center = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection5Center = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet5Center = (beltData.GR_Current*10).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind5Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness5Center = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection5Center = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet5Center = (beltData.GR_Current*10).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind5Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness5Center = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection5Center = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet5Center = (beltData.GR_Current*10).ToShort();
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
                    msg.No1GrGrinderMotorCurrentSet6Center = (beltData.GR_Current*10).ToShort();
                    msg.No1GrAbBeltSpeed6Center = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind6Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness6Center = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection6Center = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet6Center = (beltData.GR_Current*10).ToShort();
                    msg.No2GrAbBeltSpeed6Center = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind6Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness6Center = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection6Center = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet6Center = (beltData.GR_Current*10).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind6Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness6Center = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection6Center = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet6Center = (beltData.GR_Current*10).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind6Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness6Center = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection6Center = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet6Center = (beltData.GR_Current*10).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind6Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness6Center = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection6Center = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet6Center = (beltData.GR_Current*10).ToShort();
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
                    msg.No1GrGrinderMotorCurrentSet7Center = (beltData.GR_Current*10).ToShort();
                    msg.No1GrAbBeltSpeed7Center = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind7Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness7Center = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection7Center = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet7Center = (beltData.GR_Current*10).ToShort();
                    msg.No2GrAbBeltSpeed7Center = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind7Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness7Center = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection7Center = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet7Center = (beltData.GR_Current*10).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind7Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness7Center = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection7Center = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet7Center = (beltData.GR_Current*10).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind7Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness7Center = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection7Center = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet7Center = (beltData.GR_Current*10).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind7Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness7Center = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection7Center = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet7Center = (beltData.GR_Current*10).ToShort();
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
                    msg.No1GrGrinderMotorCurrentSet8Center = (beltData.GR_Current*10).ToShort();
                    msg.No1GrAbBeltSpeed8Center = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind8Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness8Center = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection8Center = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet8Center = (beltData.GR_Current*10).ToShort();
                    msg.No2GrAbBeltSpeed8Center = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind8Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness8Center = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection8Center = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet8Center = (beltData.GR_Current*10).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind8Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness8Center = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection8Center = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet8Center = (beltData.GR_Current*10).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind8Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness8Center = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection8Center = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet8Center = (beltData.GR_Current*10).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind8Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness8Center = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection8Center = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet8Center = (beltData.GR_Current*10).ToShort();
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
                    msg.No1GrGrinderMotorCurrentSet9Center = (beltData.GR_Current*10).ToShort();
                    msg.No1GrAbBeltSpeed9Center = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind9Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness9Center = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection9Center = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet9Center = (beltData.GR_Current*10).ToShort();
                    msg.No2GrAbBeltSpeed9Center = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind9Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness9Center = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection9Center = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet9Center = (beltData.GR_Current*10).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind9Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness9Center = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection9Center = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet9Center = (beltData.GR_Current*10).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind9Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness9Center = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection9Center = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet9Center = (beltData.GR_Current*10).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind9Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness9Center = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection9Center = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet9Center = (beltData.GR_Current*10).ToShort();
                    break;

            }
        }
        #endregion

        #region Tail Pass
        private static void LoadTailPass1GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_204_PDI_TM3 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind1Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness1Tail = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection1Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet1Tail = (beltData.GR_Current*10).ToShort();
                    msg.No1GrAbBeltSpeed1Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind1Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness1Tail = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection1Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet1Tail = (beltData.GR_Current*10).ToShort();
                    msg.No2GrAbBeltSpeed1Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind1Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness1Tail = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection1Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet1Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind1Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness1Tail = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection1Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet1Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind1Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness1Tail = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection1Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet1Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind1Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness1Tail = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection1Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet1Tail = (beltData.GR_Current*10).ToShort();
                    break;

            }
        }
        private static void LoadTailPass2GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_204_PDI_TM3 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind2Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness2Tail = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection2Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet2Tail = (beltData.GR_Current*10).ToShort();
                    msg.No1GrAbBeltSpeed2Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind2Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness2Tail = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection2Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet2Tail = (beltData.GR_Current*10).ToShort();
                    msg.No2GrAbBeltSpeed2Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind2Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness2Tail = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection2Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet2Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind2Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness2Tail = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection2Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet2Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind2Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness2Tail = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection2Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet2Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind2Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness2Tail = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection2Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet2Tail = (beltData.GR_Current*10).ToShort();
                    break;

            }
        }
        private static void LoadTailPass3GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_204_PDI_TM3 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind3Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness3Tail = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection3Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet3Tail = (beltData.GR_Current*10).ToShort();
                    msg.No1GrAbBeltSpeed3Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind3Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness3Tail = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection3Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet3Tail = (beltData.GR_Current*10).ToShort();
                    msg.No2GrAbBeltSpeed3Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind3Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness3Tail = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection3Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet3Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind3Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness3Tail = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection3Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet3Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind3Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness3Tail = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection3Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet3Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind3Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness3Tail = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection3Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet3Tail = (beltData.GR_Current*10).ToShort();
                    break;

            }
        }
        private static void LoadTailPass4GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_204_PDI_TM3 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind4Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness4Tail = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection4Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet4Tail = (beltData.GR_Current*10).ToShort();
                    msg.No1GrAbBeltSpeed4Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind4Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness4Tail = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection4Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet4Tail = (beltData.GR_Current*10).ToShort();
                    msg.No2GrAbBeltSpeed4Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind4Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness4Tail = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection4Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet4Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind4Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness4Tail = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection4Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet4Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind4Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness4Tail = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection4Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet4Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind4Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness4Tail = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection4Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet4Tail = (beltData.GR_Current*10).ToShort();
                    break;

            }
        }
        private static void LoadTailPass5GR(int grNo, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_205_PDI_TM3_2 msg)
        {
            switch (grNo)
            {
                case 1:
                    msg.No1GrAbBeltKind5Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No1GrAbBeltRoughness5Tail = beltData.Belt_ParticalNumber;
                    msg.No1GrAbBeltRotatingDirection5Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No1GrGrinderMotorCurrentSet5Tail = (beltData.GR_Current*10).ToShort();
                    msg.No1GrAbBeltSpeed5Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind5Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness5Tail = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection5Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet5Tail = (beltData.GR_Current*10).ToShort();
                    msg.No2GrAbBeltSpeed5Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind5Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness5Tail = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection5Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet5Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind5Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness5Tail = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection5Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet5Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind5Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness5Tail = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection5Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet5Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind5Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness5Tail = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection5Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet5Tail = (beltData.GR_Current*10).ToShort();
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
                    msg.No1GrGrinderMotorCurrentSet6Tail = (beltData.GR_Current*10).ToShort();
                    msg.No1GrAbBeltSpeed6Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind6Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness6Tail = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection6Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet6Tail = (beltData.GR_Current*10).ToShort();
                    msg.No2GrAbBeltSpeed6Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind6Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness6Tail = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection6Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet6Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind6Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness6Tail = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection6Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet6Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind6Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness6Tail = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection6Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet6Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind6Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness6Tail = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection6Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet6Tail = (beltData.GR_Current*10).ToShort();
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
                    msg.No1GrGrinderMotorCurrentSet7Tail = (beltData.GR_Current*10).ToShort();
                    msg.No1GrAbBeltSpeed7Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind7Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness7Tail = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection7Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet7Tail = (beltData.GR_Current*10).ToShort();
                    msg.No2GrAbBeltSpeed7Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind7Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness7Tail = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection7Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet7Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind7Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness7Tail = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection7Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet7Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind7Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness7Tail = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection7Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet7Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind7Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness7Tail = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection7Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet7Tail = (beltData.GR_Current*10).ToShort();
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
                    msg.No1GrGrinderMotorCurrentSet8Tail = (beltData.GR_Current*10).ToShort();
                    msg.No1GrAbBeltSpeed8Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind8Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness8Tail = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection8Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet8Tail = (beltData.GR_Current*10).ToShort();
                    msg.No2GrAbBeltSpeed8Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind8Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness8Tail = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection8Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet8Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind8Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness8Tail = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection8Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet8Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind8Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness8Tail = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection8Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet8Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind8Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness8Tail = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection8Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet8Tail = (beltData.GR_Current*10).ToShort();
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
                    msg.No1GrGrinderMotorCurrentSet9Tail = (beltData.GR_Current*10).ToShort();
                    msg.No1GrAbBeltSpeed9Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind9Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness9Tail = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection9Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet9Tail = (beltData.GR_Current*10).ToShort();
                    msg.No2GrAbBeltSpeed9Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind9Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness9Tail = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection9Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet9Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind9Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness9Tail = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection9Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet9Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind9Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness9Tail = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection9Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet9Tail = (beltData.GR_Current*10).ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind9Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness9Tail = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection9Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet9Tail = (beltData.GR_Current*10).ToShort();
                    break;

            }
        }
        #endregion

    }
}
