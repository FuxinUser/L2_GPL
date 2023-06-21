using static DBService.Base.DBAttributes;

namespace DBService.L1Repository
{
    public class L1L2MsgDBModel
    {

        public class L1L2_102
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public System.DateTime DateTime { get; set; }
            [PrimaryKey]
            public string CoilNo { get; set; }
            public string SteelGrade { get; set; }
            public short CoilThickness { get; set; }
            public short CoilWidth { get; set; }
            public short CoilRoughness { get; set; }
            public short CoilInnerdiameter { get; set; }
            public short Coilouterdiameter { get; set; }
            public short Coiltheoreticalweight { get; set; }
            public short IsCoilTurned { get; set; }
            public short SleeveInstallled { get; set; }
            public short SleeveWidth { get; set; }
            public short PaperInstalled { get; set; }
            public short DeliveryShearCoilHeadEndCutLength { get; set; }
            public short DeliveryShearCoilTailEndCutLength { get; set; }
            public short HeadEndLeader { get; set; }
            public short HeadEndLeaderLength { get; set; }
            public short TailEndLeader { get; set; }
            public short TailEndLeaderLength { get; set; }
            public int CoilLength { get; set; }
            public short DegreasingFlag { get; set; }
            public short ActualUncoilDirection { get; set; }
            public float RecoilerTension { get; set; }

            [PrimaryKey]
            public System.DateTime CreateTime { get; set; }
        }

        public class L1L2_104
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public System.DateTime DateTime { get; set; }
            public short Linespeed { get; set; }
            public short TensionReelSpeed { get; set; }
            public short ThreadingSpeed { get; set; }
            public short Linetension { get; set; }
            public short Linerundirection { get; set; }
            public short No1GRAbrasiveBeltMotorCurrent { get; set; }
            public short No1GRAbrasiveBeltSpeed { get; set; }
            public short No2GRAbrasiveBeltMotorCurrent { get; set; }
            public short No2GRAbrasiveBeltSpeed { get; set; }
            public short No3GRAbrasiveBeltMotorCurrent { get; set; }
            public short No4GRAbrasiveBeltMotorCurrent { get; set; }
            public short No5GRAbrasiveBeltMotorCurrent { get; set; }
            public short No6GRAbrasiveBeltMotorCurrent { get; set; }
            public short CoolantOilTankTemperature { get; set; }
            public short AlkaliSolutionTankTemperature { get; set; }
            public short PrimaryRinseWaterTankTemperature { get; set; }
            public short FinishRinseTankTemperature { get; set; }
            public short StripDryerTemperature { get; set; }
            public short BrushRollCurrent1 { get; set; }
            public short BrushRollCurrent2 { get; set; }
            [PrimaryKey]
            public System.DateTime CreateTime { get; set; }
        }

        public class L1L2_105
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public System.DateTime DateTime { get; set; }
            public string Pay_OffReel { get; set; }
            public string EntryCoilSkidNo1 { get; set; }
            public string EntryTOP { get; set; }
            public string EntryLiftCar { get; set; }
            public string TensionReel { get; set; }
            public string DeliveryCoilSkidNo1 { get; set; }
            public string DeliveryCoilSkidNo2 { get; set; }
            public string DeliveryTOP { get; set; }
            public string DeliveryLiftCar { get; set; }
            [PrimaryKey]
            public System.DateTime CreateTime { get; set; }
        }

        public class L1L2_106
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public System.DateTime DateTime { get; set; }
            [PrimaryKey]
            public string CoilID { get; set; }
            public short WeldVoltageSetPoint { get; set; }
            public short WeldCurrentSetPoint { get; set; }
            public short WeldWireSpeedSetPoint { get; set; }
            public short TorchCarriageWeldSpeedPetPoint { get; set; }
            public short ActualTorchCarriageWeldSpeed { get; set; }
            public short WeldGapSelected { get; set; }
            public short ActualWeldCurrent { get; set; }
            public short StartPuddleTime { get; set; }
            public short StopPuddleTime { get; set; }
            public short WeldScheduleNumber { get; set; }
            [PrimaryKey]
            public System.DateTime CreateTime { get; set; }
        }

        public class L1L2_107
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public System.DateTime DateTime { get; set; }
            [PrimaryKey]
            public string CoilID { get; set; }
            public short CurrentPassNumber { get; set; }
            public short CurrentSession { get; set; }
            public short LineSpeed { get; set; }
            public string No1GRABBeltKind { get; set; }
            public short No1GRABBeltRoughness { get; set; }
            public short No1GRABBeltMotorCurrent { get; set; }
            public short No1GRABBeltSpeed { get; set; }
            public short No1GRABBeltRotateDirection { get; set; }
            public string No2GRABBeltKind { get; set; }
            public short No2GRABBeltRoughness { get; set; }
            public short No2GRABBeltCurrent { get; set; }
            public short No2GRABBeltSpeed { get; set; }
            public short No2GRABBeltRotateDirection { get; set; }
            public string No3GRABBeltKind { get; set; }
            public short No3GRABBeltRoughness { get; set; }
            public short No3GRABBeltCurrent { get; set; }
            public short No3GRABBeltRotateDirection { get; set; }
            public string No4GRABBeltKind { get; set; }
            public short No4GRABBeltRoughness { get; set; }
            public short No4GRABBeltCurrent { get; set; }
            public short No4GRABBeltRotatedirection { get; set; }
            public string No5GRABBeltKind { get; set; }
            public short No5GRABBeltRoughness { get; set; }
            public short No5GRABBeltCurrent { get; set; }
            public short No5GRABBeltRotateDirection { get; set; }
            public string No6GRABBeltKind { get; set; }
            public short No6GRABBeltRoughness { get; set; }
            public short No6GRABBeltCurrent { get; set; }
            public short No6GRABBeltRotateDirection { get; set; }
            [PrimaryKey]
            public System.DateTime CreateTime { get; set; }
        }

        public class L1L2_108
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public System.DateTime DateTime { get; set; }
            [PrimaryKey]
            public string CoilNo { get; set; }
            public short PassNumber { get; set; }
            public short CurrentSession { get; set; }
            public float LengthFromHeadEnd { get; set; }
            public short DefectPosition { get; set; }
            public short DefectKind { get; set; }
            public string DefectGrade { get; set; }
            [PrimaryKey]
            public System.DateTime CreateTime { get; set; }
        }

        public class L1L2_109
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public System.DateTime DateTime { get; set; }
            public float No1GRAbrBeltAccGriBeltLen { get; set; }
            public float No1GRAbrBeltAccGriStLen { get; set; }
            public float No2GRAbrBeltAccGriBeltLen { get; set; }
            public float No2GRAbrBeltAccGriStLen { get; set; }
            public float No3GRAbrBeltAccGriBeltLen { get; set; }
            public float No3GRAbrBeltAccGriStLen { get; set; }
            public float No4GRAbrBeltAccGriBeltLen { get; set; }
            public float No4GRAbrBeltAccGriStLen { get; set; }
            public float No5GRAbrBeltAccGriBeltLen { get; set; }
            public float No5GRAbrBeltAccGriStLen { get; set; }
            public float No6GRAbrBeltAccGriBeltLen { get; set; }
            public float No6GRAbrBeltAccGriStLen { get; set; }
            [PrimaryKey]
            public System.DateTime CreateTime { get; set; }
        }

        public class L1L2_110
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public System.DateTime DateTime { get; set; }
            [PrimaryKey]
            public string CoilID { get; set; }
            public short CoilWeight { get; set; }
            [PrimaryKey]
            public System.DateTime CreateTime { get; set; }
        }

        public class L1L2_111
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public System.DateTime DateTime { get; set; }
            public short FaultCode { get; set; }
            public short StopCategory { get; set; }
            public short LineStatus { get; set; }
            [PrimaryKey]
            public System.DateTime CreateTime { get; set; }
        }

        public class L1L2_112
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public System.DateTime DateTime { get; set; }
            public float CompressedAir { get; set; }
            public float Steam { get; set; }
            public float RinseWater { get; set; }
            public float IndirectCollingWater { get; set; }
            [PrimaryKey]
            public System.DateTime CreateTime { get; set; }
        }

        public class L1L2_113
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public System.DateTime DateTime { get; set; }
            public int GRNo { get; set; }
            public string OldABBeltID { get; set; }
            public string NewABBeltID { get; set; }
            public float OldABBeltAccGriBeltLength { get; set; }
            public float OldABBeltAccGriStLength { get; set; }
            [PrimaryKey]
            public System.DateTime CreateTime { get; set; }
        }

        public class L1L2_114
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public System.DateTime DateTime { get; set; }
            [PrimaryKey]
            public string CoilID { get; set; }
            public System.DateTime CreateTime { get; set; }
        }

        public class L1L2_115
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public System.DateTime DateTime { get; set; }
            public string CoilID { get; set; }
            public System.DateTime CreateTime { get; set; }
        }

        public class L1L2_116
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public System.DateTime DateTime { get; set; }
            public short LineRunDirection { get; set; }
            [PrimaryKey]
            public System.DateTime CreateTime { get; set; }
        }

        public class L1L2_117
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public System.DateTime DateTime { get; set; }
            [PrimaryKey]
            public string CoilID { get; set; }
            public System.DateTime CreateTime { get; set; }
        }

        public class L1L2_118
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public System.DateTime DateTime { get; set; }
            public short ConditionFlag { get; set; }
            [PrimaryKey]
            public System.DateTime CreateTime { get; set; }
        }

        public class L1L2_119
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public System.DateTime DateTime { get; set; }
            [PrimaryKey]
            public System.DateTime CreateTime { get; set; }
        }

        public class L1L2_120
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public System.DateTime DateTime { get; set; }
            //public short Status { get; set; }
            [PrimaryKey]
            public System.DateTime CreateTime { get; set; }
        }

        public class L1L2_121
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public System.DateTime DateTime { get; set; }
            public short StartCondition { get; set; }
            [PrimaryKey]
            public System.DateTime CreateTime { get; set; }
        }

        public class L1L2_122
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public System.DateTime DateTime { get; set; }
            [PrimaryKey]
            public string CoilID { get; set; }
            [PrimaryKey]
            public System.DateTime CreateTime { get; set; }
        }

        public class L1L2_123
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public System.DateTime DateTime { get; set; }
            [PrimaryKey]
            public string CoilID { get; set; }
            //public short Status { get; set; }
            [PrimaryKey]
            public System.DateTime CreateTime { get; set; }
        }

        public class L1L2_124
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public System.DateTime DateTime { get; set; }
            public string UncoilerCoilNo { get; set; }
            public short UncoilerCoilThickness { get; set; }
            public short UncoilerCoilWidth { get; set; }
            public short UncoilerCoilLength { get; set; }
            public short UncoilerCoilInnerDiameter { get; set; }
            public short UncoilerCoilOuterDiameter { get; set; }
            public short UncoilerCoilTheoreticalWeight { get; set; }
            public string RecoilerCoilNo { get; set; }
            public short RecoilerCoilThickness { get; set; }
            public short RecoilerCoilWidth { get; set; }
            public short RecoilerCoilLength { get; set; }
            public short RecoilerCoilInnerDiameter { get; set; }
            public short RecoilerCoilOuterDiameter { get; set; }
            public short RecoilerCoilTheoreticalWeight { get; set; }
            public System.DateTime CreateTime { get; set; }
        }

        public class L1L2_125
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public System.DateTime DateTime { get; set; }
            public string CoilID { get; set; }
            public float CutLength { get; set; }
            public short CutMode { get; set; }
            public short RecoilerCoilTheoreticalWeight { get; set; }
            //public float DiamRec { get; set; }
            //public float LengthRec { get; set; }
            //public float CalculateWeightRec { get; set; }
            public System.DateTime CreateTime { get; set; }
        }

        public class L1L2_126
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public System.DateTime DateTime { get; set; }
            public string CoilID { get; set; }
            public float CoilWeight { get; set; }
            public int CoilLength { get; set; }
            public float Diameter { get; set; }
            public float CoiInsideDiam { get; set; }
            public float Width { get; set; }
        }

        public class L1L2_127
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public System.DateTime DateTime { get; set; }

            public short ModifyPosition { get; set; }
            public string CoilID { get; set; }
            public short ModifyReply { get; set; }
           
        }
    }
}
