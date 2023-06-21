using Core.Define;
using Core.Util;
using DBService.Repository.BeltPatterns;
using MsgStruct;
using System.Collections.Generic;
using System.Linq;

namespace MsgConvert.Msg
{
    public static class Preset204GrindProFactory
    {       
        public static L2L1Snd.Msg_204_PDI_TM3 Load204BeltPattern(this L2L1Snd.Msg_204_PDI_TM3 msg, IEnumerable<BeltPatternsEntity.TBL_BeltPatterns> beltPatterns)
        {
            var beltPatternsList = beltPatterns.ToList();

            try
            {

                msg.PassNumberForCoilHeadGrinding = beltPatternsList.Where(x => x.Pass_Section.Equals("H")).Select(v => v.PassNumber.ToShort()).FirstOrDefault();
                msg.PassNumberForCoilCenterGrinding = beltPatternsList.Where(x => x.Pass_Section.Equals("M")).Select(v => v.PassNumber.ToShort()).FirstOrDefault();
                msg.PassNumberForCoilTailGrinding = beltPatternsList.Where(x => x.Pass_Section.Equals("T")).Select(v => v.PassNumber.ToShort()).FirstOrDefault();
            }
            catch
            {
                msg.PassNumberForCoilHeadGrinding = 0;
                msg.PassNumberForCoilCenterGrinding = 0;
                msg.PassNumberForCoilTailGrinding = 0;
            }

            foreach (BeltPatternsEntity.TBL_BeltPatterns beltPattern in beltPatterns)
            {
                for (int i = beltPattern.Pass_From; i <= beltPattern.Pass_To; i++)
                    LoadGRData(i, beltPattern, ref msg);
            }
            return msg;
        }

        #region Det Pos
        private static void LoadGRData(int passNum, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_204_PDI_TM3 msg)
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
        private static void LoadHeadPass(int pass, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_204_PDI_TM3 msg)
        {            
            switch (pass)
            {
                case 1:
                    LoadHeadPass1GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed1Head = beltData.LineSpeed.ToShort();
                    break;
                case 2:
                    LoadHeadPass2GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed2Head = beltData.LineSpeed.ToShort();
                    break;
                case 3:
                    LoadHeadPass3GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed3Head = beltData.LineSpeed.ToShort();
                    break;
                case 4:
                    LoadHeadPass4GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed4Head = beltData.LineSpeed.ToShort();
                    break;
            }
        }
        private static void LoadCenterPass(int pass, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_204_PDI_TM3 msg)
        {
            switch (pass)
            {

                case 1:
                    LoadCenterPass1GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed1Center = beltData.LineSpeed.ToShort();
                    break;
                case 2:
                    LoadCenterPass2GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed2Center = beltData.LineSpeed.ToShort();
                    break;
                case 3:
                    LoadCenterPass3GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed3Center = beltData.LineSpeed.ToShort();
                    break;
                case 4:
                    LoadCenterPass4GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed4Center = beltData.LineSpeed.ToShort();
                    break;
            }
        }
        private static void LoadTailPass(int pass, BeltPatternsEntity.TBL_BeltPatterns beltData, ref L2L1Snd.Msg_204_PDI_TM3 msg)
        {
            switch (pass)
            {

                case 1:
                    LoadTailPass1GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed1Tail = beltData.LineSpeed.ToShort();
                    break;
                case 2:
                    LoadTailPass2GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed2Tail = beltData.LineSpeed.ToShort();
                    break;
                case 3:
                    LoadTailPass3GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed3Tail = beltData.LineSpeed.ToShort();
                    break;
                case 4:
                    LoadTailPass4GR(beltData.GR_NO, beltData, ref msg);
                    msg.LineSpeed4Tail = beltData.LineSpeed.ToShort();
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
                    msg.No1GrGrinderMotorCurrentSet1Head = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed1Head = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind1Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness1Head = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection1Head = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet1Head = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed1Head = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind1Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness1Head = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection1Head = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet1Head = beltData.GR_Current.ToShort();                   
                    break;
                case 4:
                    msg.No4GrAbBeltKind1Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness1Head = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection1Head = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet1Head = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind1Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness1Head = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection1Head = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet1Head = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind1Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness1Head = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection1Head = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet1Head = beltData.GR_Current.ToShort();
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
                    msg.No1GrGrinderMotorCurrentSet2Head = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed2Head = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind2Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness2Head = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection2Head = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet2Head = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed2Head = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind2Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness2Head = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection2Head = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet2Head = beltData.GR_Current.ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind2Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness2Head = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection2Head = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet2Head = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind2Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness2Head = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection2Head = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet2Head = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind2Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness2Head = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection2Head = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet2Head = beltData.GR_Current.ToShort();
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
                    msg.No1GrGrinderMotorCurrentSet3Head = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed3Head = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind3Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness3Head = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection3Head = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet3Head = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed3Head = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind3Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness3Head = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection3Head = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet3Head = beltData.GR_Current.ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind3Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness3Head = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection3Head = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet3Head = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind3Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness3Head = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection3Head = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet3Head = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind3Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness3Head = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection3Head = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet3Head = beltData.GR_Current.ToShort();
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
                    msg.No1GrGrinderMotorCurrentSet4Head = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed4Head = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind4Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness4Head = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection4Head = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet4Head = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed4Head = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind4Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness4Head = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection4Head = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet4Head = beltData.GR_Current.ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind4Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness4Head = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection4Head = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet4Head = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind4Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness4Head = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection4Head = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet4Head = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind4Head = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness4Head = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection4Head = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet4Head = beltData.GR_Current.ToShort();
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
                    msg.No1GrGrinderMotorCurrentSet1Center = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed1Center = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind1Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness1Center = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection1Center = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet1Center = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed1Center = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind1Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness1Center = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection1Center = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet1Center = beltData.GR_Current.ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind1Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness1Center = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection1Center = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet1Center = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind1Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness1Center = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection1Center = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet1Center = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind1Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness1Center = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection1Center = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet1Center = beltData.GR_Current.ToShort();
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
                    msg.No1GrGrinderMotorCurrentSet2Center = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed2Center = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind2Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness2Center = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection2Center = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet2Center = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed2Center = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind2Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness2Center = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection2Center = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet2Center = beltData.GR_Current.ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind2Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness2Center = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection2Center = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet2Center = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind2Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness2Center = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection2Center = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet2Center = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind2Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness2Center = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection2Center = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet2Center = beltData.GR_Current.ToShort();
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
                    msg.No1GrGrinderMotorCurrentSet3Center = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed3Center = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind3Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness3Center = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection3Center = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet3Center = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed3Center = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind3Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness3Center = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection3Center = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet3Center = beltData.GR_Current.ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind3Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness3Center = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection3Center = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet3Center = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind3Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness3Center = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection3Center = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet3Center = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind3Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness3Center = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection3Center = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet3Center = beltData.GR_Current.ToShort();
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
                    msg.No1GrGrinderMotorCurrentSet4Center = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed4Center = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind4Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness4Center = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection4Center = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet4Center = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed4Center = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind4Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness4Center = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection4Center = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet4Center = beltData.GR_Current.ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind4Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness4Center = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection4Center = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet4Center = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind4Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness4Center = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection4Center = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet4Center = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind4Center = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness4Center = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection4Center = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet4Center = beltData.GR_Current.ToShort();
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
                    msg.No1GrGrinderMotorCurrentSet1Tail = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed1Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind1Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness1Tail = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection1Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet1Tail = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed1Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind1Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness1Tail = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection1Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet1Tail = beltData.GR_Current.ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind1Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness1Tail = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection1Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet1Tail = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind1Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness1Tail = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection1Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet1Tail = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind1Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness1Tail = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection1Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet1Tail = beltData.GR_Current.ToShort();
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
                    msg.No1GrGrinderMotorCurrentSet2Tail = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed2Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind2Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness2Tail = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection2Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet2Tail = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed2Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind2Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness2Tail = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection2Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet2Tail = beltData.GR_Current.ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind2Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness2Tail = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection2Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet2Tail = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind2Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness2Tail = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection2Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet2Tail = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind2Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness2Tail = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection2Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet2Tail = beltData.GR_Current.ToShort();
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
                    msg.No1GrGrinderMotorCurrentSet3Tail = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed3Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind3Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness3Tail = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection3Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet3Tail = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed3Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind3Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness3Tail = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection3Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet3Tail = beltData.GR_Current.ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind3Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness3Tail = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection3Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet3Tail = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind3Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness3Tail = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection3Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet3Tail = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind3Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness3Tail = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection3Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet3Tail = beltData.GR_Current.ToShort();
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
                    msg.No1GrGrinderMotorCurrentSet4Tail = beltData.GR_Current.ToShort();
                    msg.No1GrAbBeltSpeed4Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 2:
                    msg.No2GrAbBeltKind4Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No2GrAbBeltRoughness4Tail = beltData.Belt_ParticalNumber;
                    msg.No2GrAbBeltRotatingDirection4Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No2GrGrinderMotorCurrentSet4Tail = beltData.GR_Current.ToShort();
                    msg.No2GrAbBeltSpeed4Tail = beltData.Belt_Speed.ToShort();
                    break;
                case 3:
                    msg.No3GrAbBeltKind4Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No3GrAbBeltRoughness4Tail = beltData.Belt_ParticalNumber;
                    msg.No3GrAbBeltRotatingDirection4Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No3GrGrinderMotorCurrentSet4Tail = beltData.GR_Current.ToShort();
                    break;
                case 4:
                    msg.No4GrAbBeltKind4Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No4GrAbBeltRoughness4Tail = beltData.Belt_ParticalNumber;
                    msg.No4GrAbBeltRotatingDirection4Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No4GrGrinderMotorCurrentSet4Tail = beltData.GR_Current.ToShort();
                    break;
                case 5:
                    msg.No5GrAbBeltKind4Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No5GrAbBeltRoughness4Tail = beltData.Belt_ParticalNumber;
                    msg.No5GrAbBeltRotatingDirection4Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No5GrGrinderMotorCurrentSet4Tail = beltData.GR_Current.ToShort();
                    break;
                case 6:
                    msg.No6GrAbBeltKind4Tail = beltData.Belt_MaterialCode.ToCByteArray(2);
                    msg.No6GrAbBeltRoughness4Tail = beltData.Belt_ParticalNumber;
                    msg.No6GrAbBeltRotatingDirection4Tail = beltData.Belt_RotateDir.ToShort();
                    msg.No6GrGrinderMotorCurrentSet4Tail = beltData.GR_Current.ToShort();
                    break;

            }
        }
        #endregion

    }
}
