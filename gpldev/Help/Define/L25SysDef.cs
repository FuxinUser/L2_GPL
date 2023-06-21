using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Define
{
    public class L25SysDef
    {

        public class MsgCode
        {
            public static string Msg101PDI = "101";
            public static string Msg102PDO = "102";
            public static string Msg103DownTime = "103";
            public static string Msg104ENGC = "104";
            public static string Msg105LineSpeedCT = "105";
            public static string Msg106LineTensionCT = "106";
            public static string Msg107LineRunDirectionCT = "107";
            public static string Msg108No1GRAbrasiveBeltMotorCurrentCT = "108";
            public static string Msg109No2GRAbrasiveBeltMotorCurrentCT = "109";
            public static string Msg110No3GRAbrasiveBeltMotorCurrentCT = "110";
            public static string Msg111No4GRAbrasiveBeltMotorCurrentCT = "111";
            public static string Msg112No5GRAbrasiveBeltMotorCurrentCT = "112";
            public static string Msg113No6GRAbrasiveBeltMotorCurrentCT = "113";
            public static string Msg114No1GRAbrasiveBeltSpeedCT = "114";
            public static string Msg115No2GRAbrasiveBeltSpeedCT = "115";
            //Grind
            public static string Msg116CurrentPassNumberCT = "116";
            public static string Msg117CurrentSessionCT = "117";
            public static string Msg118GRDLineSpeedCT = "118";
            public static string Msg119No1GRAB_beltKindCT = "119";
            public static string Msg120No1GRAB_beltRoughnessCT = "120";
            public static string Msg121No1GRAB_beltMotorCurrentCT = "121";
            public static string Msg122No1GRAB_beltSpeedCT = "122";
            public static string Msg123No1GRAB_beltRotateDirectionCT = "123";
            public static string Msg124No2GRAB_beltKindCT = "124";
            public static string Msg125No2GRAB_beltRoughnessCT = "125";
            public static string Msg126No2GRAB_beltCurrentCT = "126";
            public static string Msg127No2GRAB_beltSpeedCT = "127";
            public static string Msg128No2GRAB_beltRotateDirectionCT = "128";
            public static string Msg129No3GRAB_beltKindCT = "129";
            public static string Msg130No3GRAB_beltRoughnessCT = "130";
            public static string Msg131No3GRAB_beltCurrentCT = "131";
            public static string Msg132No3GRAB_beltRotateDirectionCT = "132";
            public static string Msg133No4GRAB_beltKindCT = "133";
            public static string Msg134No4GRAB_beltRoughnessCT = "134";
            public static string Msg135No4GRAB_beltCurrentCT = "135";
            public static string Msg136No4GRAB_beltRotateDirectionCT = "136";
            public static string Msg137No5GRAB_beltKindCT = "137";
            public static string Msg138No5GRAB_beltRoughnessCT = "138";
            public static string Msg139No5GRAB_beltCurrentCT = "139";
            public static string Msg140No5GRAB_beltRotateDirectionCT = "140";
            public static string Msg141No6GRAB_beltKindCT = "141";
            public static string Msg142No6GRAB_beltRoughnessCT = "142";
            public static string Msg143No6GRAB_beltCurrentCT = "143";
            public static string Msg144No6GRAB_beltRotateDirectionCT = "144";
            //BrushRollCurrent
            public static string Msg145No1BrushRollCurrentCT = "145";
            public static string Msg146No2BrushRollCurrentCT = "146";
            public static string Msg147CoilMap = "147";
            public static string Msg148L1L2DisConnection = "148";
            public static string Msg150Alive = "150";
            public static string Msg151CoilRejectResult = "151";
        }

        public class MsgLength
        {
            public static string Msg101PDI = "2264";
            public static string Msg102PDO = "694";
            public static string Msg103DownTime = "638";
            public static string Msg104ENGC = "41";
            public static string Msg105LineSpeedCT = "140";
            public static string Msg106LineTensionCT = "140";
            public static string Msg107LineRunDirectionCT = "140";
            public static string Msg108No1GRAbrasiveBeltMotorCurrentCT = "140";
            public static string Msg109No2GRAbrasiveBeltMotorCurrentCT = "140";
            public static string Msg110No3GRAbrasiveBeltMotorCurrentCT = "140";
            public static string Msg111No4GRAbrasiveBeltMotorCurrentCT = "140";
            public static string Msg112No5GRAbrasiveBeltMotorCurrentCT = "140";
            public static string Msg113No6GRAbrasiveBeltMotorCurrentCT = "140";
            public static string Msg114No1GRAbrasiveBeltSpeedCT = "140";
            public static string Msg115No2GRAbrasiveBeltSpeedCT = "140";
            //Grind
            public static string Msg116CurrentPassNumberCT = "140";
            public static string Msg117CurrentSessionCT = "140";
            public static string Msg118GRDLineSpeedCT = "140";
            public static string Msg119No1GRAB_beltKindCT = "140";
            public static string Msg120No1GRAB_beltRoughnessCT = "140";
            public static string Msg121No1GRAB_beltMotorCurrentCT = "140";
            public static string Msg122No1GRAB_beltSpeedCT = "140";
            public static string Msg123No1GRAB_beltRotateDirectionCT = "140";
            public static string Msg124No2GRAB_beltKindCT = "140";
            public static string Msg125No2GRAB_beltRoughnessCT = "140";
            public static string Msg126No2GRAB_beltCurrentCT = "140";
            public static string Msg127No2GRAB_beltSpeedCT = "140";
            public static string Msg128No2GRAB_beltRotateDirectionCT = "140";
            public static string Msg129No3GRAB_beltKindCT = "140";
            public static string Msg130No3GRAB_beltRoughnessCT = "140";
            public static string Msg131No3GRAB_beltCurrentCT = "140";
            public static string Msg132No3GRAB_beltRotateDirectionCT = "140";
            public static string Msg133No4GRAB_beltKindCT = "140";
            public static string Msg134No4GRAB_beltRoughnessCT = "140";
            public static string Msg135No4GRAB_beltCurrentCT = "140";
            public static string Msg136No4GRAB_beltRotateDirectionCT = "140";
            public static string Msg137No5GRAB_beltKindCT = "140";
            public static string Msg138No5GRAB_beltRoughnessCT = "140";
            public static string Msg139No5GRAB_beltCurrentCT = "140";
            public static string Msg140No5GRAB_beltRotateDirectionCT = "140";
            public static string Msg141No6GRAB_beltKindCT = "140";
            public static string Msg142No6GRAB_beltRoughnessCT = "140";
            public static string Msg143No6GRAB_beltCurrentCT = "140";
            public static string Msg144No6GRAB_beltRotateDirectionCT = "140";
            //BrushRollCurrent
            public static string Msg145No1BrushRollCurrentCT = "140";
            public static string Msg146No2BrushRollCurrentCT = "140";
            public static string Msg147CoilMap = "241";
            public static string Msg148L1L2DisConnection = "32";
            public static string Msg150Alive = "25";
            public static string Msg151CoilRejectResult = "104";
        }
        public class ProcessData
        {

            public class Code
            {
                //public static string Speed = "CPUNC0" + L2SystemDef.GPLSysNumber + "C0001";
                //public static string UNCTension = "CPUNC0" + L2SystemDef.GPLSysNumber + "C0002";
                //public static string UNCCurrent = "CPUNC0" + L2SystemDef.GPLSysNumber + "C0003";
                //public static string RECTension = "CPREC0" + L2SystemDef.GPLSysNumber + "C0001";
                //public static string RECCurrent = "CPREC0" + L2SystemDef.GPLSysNumber + "C0002";

                //UNC
                public static string LineSpeed = "GPUNC01C0001";
                public static string LineTension = "GPUNC01C0002";
                public static string Linerundirection = "GPUNC01C0003";
                public static string TensionReelSpeed = "GPUNC01C0004";
                public static string ThreadingSpeed = "GPUNC01C0005";


                //GPM
                public static string No1GRABeltMotorCurrent = "GPGPM01C0001";
                public static string No1GRABeltSpeed = "GPGPM01C0007";
                public static string No2GRABeltMotorCurrent = "GPGPM01C0002";
                public static string No2GRABeltSpeed = "GPGPM01C0008";
                public static string No3GRABeltMotorCurrent = "GPGPM01C0003";
                public static string No4GRABeltMotorCurrent = "GPGPM01C0004";
                public static string No5GRABeltMotorCurrent = "GPGPM01C0005";
                public static string No6GRABeltMotorCurrent = "GPGPM01C0006";


                //public static string CoolantOilTankTemp = "GPUNC01C0001";
                //public static string AlkaliSolutionTankTemp = "GPUNC01C0001";
                //public static string PrimaryRinseWaterTankTemp = "GPUNC01C0001";
                //public static string FinishRinseTankTemp = "GPUNC01C0001";
                //public static string StripDryerTemp = "GPUNC01C0001";

                //DEG
                public static string BrushRollCurrent1 = "GPDEG01C0001";
                public static string BrushRollCurrent2 = "GPDEG01C0002";




            }

            public class Desc
            {
                public static string Speed = "speed";
                public static string Tension = "tension";
                public static string Current = "current";
                public static string direction = "direction";
            }

            public class SeqUnit
            {
                public static string LenUnit = "M";
                //public static string Speed = "M";
                //public static string UNCTension = "M";
                //public static string UNCCurrent = "M";
                //public static string RECTension = "M";
                //public static string RECCurrent = "M";
            }

            public class ResultUnit
            {
                public static string Speed = "mpm";
                public static string Tension = "kg/mm2";
                public static string Current = "A";
                public static string temperature = "°C";
                public static string direction = "";

            }


            public class Frenquency
            {
                public static string FSecond = "5s";
            }
        }
        public class GrindData
        {
            public class Code
            {
                //GPM
                public static string CurrentPassNumber = "GPGPM01C0009";
                public static string CurrentSession = "GPGPM01C0010";
                public static string GRDLineSpeed = "GPGPM01C0011";
                public static string No1GRAB_beltKind = "GPGPM01C0012";
                public static string No1GRAB_beltRoughness = "GPGPM01C0013";
                public static string No1GRAB_beltMotorCurrent = "GPGPM01C0014";
                public static string No1GRAB_beltSpeed = "GPGPM01C0015";
                public static string No1GRAB_beltRotateDireion = "GPGPM01C0016";
                public static string No2GRAB_beltKind = "GPGPM01C0017";
                public static string No2GRAB_beltRoughness = "GPGPM01C0018";
                public static string No2GRAB_beltCurrent = "GPGPM01C0019";
                public static string No2GRAB_beltSpeed = "GPGPM01C0020";
                public static string No2GRAB_beltRotateDireion = "GPGPM01C0021";
                public static string No3GRAB_beltKind = "GPGPM01C0022";
                public static string No3GRAB_beltRoughness = "GPGPM01C0023";
                public static string No3GRAB_beltCurrent = "GPGPM01C0024";
                public static string No3GRAB_beltRotateDireion = "GPGPM01C0025";
                public static string No4GRAB_beltKind = "GPGPM01C0026";
                public static string No4GRAB_beltRoughness = "GPGPM01C0027";
                public static string No4GRAB_beltCurrent = "GPGPM01C0028";
                public static string No4GRAB_beltRotateDireion = "GPGPM01C0029";
                public static string No5GRAB_beltKind = "GPGPM01C0030";
                public static string No5GRAB_beltRoughness = "GPGPM01C0031";
                public static string No5GRAB_beltCurrent = "GPGPM01C0032";
                public static string No5GRAB_beltRotateDireion = "GPGPM01C0033";
                public static string No6GRAB_beltKind = "GPGPM01C0034";
                public static string No6GRAB_beltRoughness = "GPGPM01C0035";
                public static string No6GRAB_beltCurrent = "GPGPM01C0036";
                public static string No6GRAB_beltRotateDireion = "GPGPM01C0037";
            }
            public class Desc
             {
                    public static string Speed = "speed";
                    public static string Current = "current";
                    public static string direction = "direction";
                    public static string Roughness = "Roughness";
                    public static string PassNumber = "PassNumber";
                    public static string Kind = "Kind";
             }
            public class SeqUnit
             {
                    public static string LenUnit = "M";
             }
            public class ResultUnit
             {
                    public static string Speed = "mpm";
                    public static string Current = "A";
                    public static string direction = "";
                    public static string Kind = "";
                    public static string Roughness = "";
                    public static string PassNumber = "";
             }
            public class Frenquency
             {
                    public static string FSecond = "5s";
             }

           
        }
        public enum L25CTData
        {
            LineSpeed,
            LineTension,
            LineRunDirection,
            No1GRAbrasiveBeltMotorCurrent,
            No2GRAbrasiveBeltMotorCurrent,
            No3GRAbrasiveBeltMotorCurrent,
            No4GRAbrasiveBeltMotorCurrent,
            No5GRAbrasiveBeltMotorCurrent,
            No6GRAbrasiveBeltMotorCurrent,
            No1GRAbrasiveBeltSpeed,
            No2GRAbrasiveBeltSpeed,
            BrushRollCurrent1,
            BrushRollCurrent2
        }
        public enum L25GrindData
        {
            CurrentPassNumber,
            CurrentSession,
            GRDLineSpeed,
            No1GRAB_beltKind,
            No1GRAB_beltRoughness,
            No1GRAB_beltMotorCurrent,
            No1GRAB_beltSpeed,
            No1GRAB_beltRotateDireion,
            No2GRAB_beltKind,
            No2GRAB_beltRoughness,
            No2GRAB_beltCurrent,
            No2GRAB_beltSpeed,
            No2GRAB_beltRotateDireion,
            No3GRAB_beltKind,
            No3GRAB_beltRoughness,
            No3GRAB_beltCurrent,
            No3GRAB_beltRotateDireion,
            No4GRAB_beltKind,
            No4GRAB_beltRoughness,
            No4GRAB_beltCurrent,
            No4GRAB_beltRotateDireion,
            No5GRAB_beltKind,
            No5GRAB_beltRoughness,
            No5GRAB_beltCurrent,
            No5GRAB_beltRotateDireion,
            No6GRAB_beltKind,
            No6GRAB_beltRoughness,
            No6GRAB_beltCurrent,
            No6GRAB_beltRotateDireion
        }
    }
}
