using System;
using System.Text;
using System.Text.RegularExpressions;
using static LabelPrint.Model.ZebraModel;

namespace LabelPrint.Printer
{
    public static class ZebraCmd
    {
        public static string zplCmd(this string coilID)
        {
            //return $@"^XA
            //            ^CFA,30
            //            ^FO50,300^FDMade^FS
            //            ^FO50,340^FDIn^FS
            //            ^FO50,380^FDFuxin^FS
            //            ^FO50,380^FDFuxin^FS
            //            ^FO50,420^FDSteel^FS
            //            ^CFA,15
            //            ^FO600,300^GB150,150,3^FS
            //            ^FO638,340^FDFuxin^FS
            //            ^FO638,390^FDFX1234^FS
            //            ^FO50,500^GB700,1,3^FS
            //            ^BY5,2,270
            //            ^FO16,550^BC^FD{coilID}^FS
            //        ^XZ";

            //return $@"^XA
            //    ^BY6,2,175
            //    ^FO65,25^BCN,180,N,N,Y^FD{coilID}^FS
            //    ^FO265,240^AB,70,40^FD{coilID}^FS
            //    ^XZ";

            return $@"^XA
                ^BY6,2,175
                ^FO65,25^BCN,180,N,N,Y^FD{coilID}^FS
                ^FO260,220^AB,70,40^FD{coilID}^FS
                ^XZ";
        }

        public static string zplQRCmd(this string coilID, string thick, string sampleNo)
        {
            return $@"^XA

                    ^FO20,0
                    ^BY4,2.0,35
                    ^BQN,2,9
                    ^FDQA,{coilID}H^FS

                    ^FO220,35
                    ^CFA,45
                    ^FDSUH409L^FS

                    ^FO220,85
                    ^CFA,45
                    ^FDCAPL^FS

                    ^FO420,85
                    ^CFA,45
                    ^FD{thick}^FS

                    ^FO220,135
                    ^CFA,45
                    ^FD{DateTime.Now.ToString("yyyy/MM/dd HH:mm")}^FS

                    ^FO220,185
                    ^CFA,45
                    ^FD{sampleNo}^FS

                    ^FO15,245
                    ^CFA,45
                    ^FDCoil No:{coilID}^FS

                    ^XZ";
        }


        /// <summary>
        /// ZPL Decode
        /// </summary>      
        public static bool Decode(this byte[] rawData, out ZebraStatus status)
        {
            bool isOk = true;
            status = new ZebraStatus();
            try
            {
                string recvMsg = Encoding.ASCII.GetString(rawData);

                // Message Handling
                recvMsg = recvMsg.Replace("\u0002", "").Replace("\u0003", "").Replace(" ", "").Replace("\r\nPRINTERSTATUS", "").Replace("\r\n\r\n", "");
                recvMsg = Regex.Replace(recvMsg, @"(\r\n)$", "");
                string pat = @"\,|\r\nERRORS:|\r\nWARNINGS:|\r\n";
                string[] recvData = Regex.Split(recvMsg, pat);

                // Set Printer State
                status.CheckDT = DateTime.Now;
                if (recvData.Length == 27)
                {
                    status.IsConnected = true;
                    status.IsPaperOut = recvData[1] == "1" ? true : false;
                    status.IsPause = recvData[2] == "1" ? true : false;
                    status.IsRibbonOut = recvData[15] == "1" ? true : false;
                    status.IsError = recvData[25].Substring(0, 1) == "1" ? true : false;
                    status.IsWarning = recvData[26].Substring(0, 1) == "1" ? true : false;
                    status.ErrorNum = int.TryParse(recvData[25].Substring(9, 8), out int eNum) ? eNum : 0;
                    status.WarningNum = int.TryParse(recvData[26].Substring(9, 8), out int wNum) ? wNum : 0;
                }
                else
                {
                    isOk = false;
                }

                #region [~HS RESULT DESCRIPTION]
                // "\u0002030,0,0,0959,000,0,0,0,000,0,0,0\u0003\r\n\u0002001,0,0,0,1,2,6,0,00000000,1,000\u0003\r\n\u00021234,0\u0003\r\n"
                // String 1 <STX> aaa,b,c,dddd,eee,f,g,h,iii,j,k,l<ETX> <CR><LF>
                // 00 aaa  = communication(interface) settingsa
                // 01 b    = paper out flag (1 = paper out)
                // 02 c    = pause flag     (1 = pause active)
                // 03 dddd = label length(value in number of dots)
                // 04 eee  = number of formats in receive buffer
                // 05 f    = buffer full flag(1 = receive buffer full)
                // 06 g    = communications diagnostic mode flag(1 = diagnostic mode active)
                // 07 h    = partial format flag(1 = partial format in progress)
                // 08 iii  = unused (always 000)
                // 09 j    = corrupt RAM flag  (1 = configuration data lost)
                // 10 k    = temperature range (1 = under temperature)
                // 11 l    = temperature range (1 = over temperature)

                // String 2 < STX > mmm,n,o,p,q,r,s,t,uuuuuuuu,v,www<ETX> < CR >< LF >
                // 12 mmm = function settings
                // 13 n   = unused
                // 14 o   = head up flag(1 = head in up position)
                // 15 p   = ribbon out flag(1 = ribbon out)
                // 16 q   = thermal transfer mode flag(1 = Thermal Transfer Mode selected)
                // 17 r   = Print Mode
                // 18 s   = print width mode
                // 19 t   = label waiting flag(1 = label waiting in Peel - off Mode)
                // 20 uuuuuuuu = labels remaining in batch
                // 21 v   = format while printing flag(always 1)
                // 22 www = number of graphic images stored in memory

                // String 3 < STX > xxxx,y<ETX> < CR >< LF >
                // 23 xxxx = ???
                // 24 y    = 0(static RAM not installed) / 1(static RAM installed)
                #endregion

                #region [~HQES RESULT DESCRIPTION]
                // 25 Error   : Flag(1 char) + Group 2 Number (8 char HEX number) + Group 1 Number (8 char HEX number) 
                // 26 Warning : Flag(1 char) + Group 2 Number (8 char HEX number) + Group 1 Number (8 char HEX number) 
                #endregion

            }
            catch (Exception ex)
            {
                Console.WriteLine("RECV : " + ex.Message);
                isOk = false;
            }
            return isOk;
        }

    }
}
