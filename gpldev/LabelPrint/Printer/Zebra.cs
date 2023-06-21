using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using System.Net;
using System.Timers;

namespace LabelPrint.Printer
{
    public class Zebra
    {
        #region [Property]

        /// <summary>
        /// False = No response
        /// </summary>
        public bool State_Connected
        {
            get
            {
                return _State_Connected;
            }
            private set
            {
                if (_State_Connected != value)
                {
                    _State_Connected = value;
                    StateChange?.Invoke();
                }
            }
        }

        /// <summary>
        /// 缺紙
        /// </summary>
        public bool State_PaperOut
        {
            get
            {
                return _State_PaperOut;
            }
            private set
            {
                if (_State_PaperOut != value)
                {
                    _State_PaperOut = value;
                    _State_Change = true;
                }
            }
        }

        /// <summary>
        /// 碳帶不足
        /// </summary>
        public bool State_RibbonOut
        {
            get
            {
                return _State_RibbonOut;
            }
            private set
            {
                if (_State_RibbonOut != value)
                {
                    _State_RibbonOut = value;
                    _State_Change = true;
                }
            }
        }

        /// <summary>
        /// 暫停狀態
        /// </summary>
        public bool State_Pause
        {
            get
            {
                return _State_Pause;
            }
            private set
            {
                if (_State_Pause != value)
                {
                    _State_Pause = value;
                    _State_Change = true;
                }
            }
        }

        /// <summary>
        /// Printer Errorrrrrrrrrrrrr!!
        /// </summary>
        public bool State_Error
        {
            get
            {
                return _State_Error;
            }
            private set
            {
                if (_State_Error != value)
                {
                    _State_Error = value;
                    _State_Change = true;
                }
            }
        }

        /// <summary>
        /// Printer Warning!!
        /// </summary>
        public bool State_Warning
        {
            get
            {
                return _State_Warning;
            }
            private set
            {
                if (_State_Warning != value)
                {
                    _State_Warning = value;
                    _State_Change = true;
                }
            }
        }

        /// <summary>
        /// Error Number
        /// </summary>
        public int State_ErrorNum
        {
            get
            {
                return _State_ErrorNum;
            }
            private set
            {
                if (_State_ErrorNum != value)
                {
                    _State_ErrorNum = value;
                    _State_Change = true;
                }
            }
        }

        /// <summary>
        /// Warning Number
        /// </summary>
        public int State_WarningNum
        {
            get
            {
                return _State_WarningNum;
            }
            private set
            {
                if (_State_WarningNum != value)
                {
                    _State_WarningNum = value;
                    _State_Change = true;
                }
            }
        }

        /// <summary>
        /// Return Program Error
        /// </summary>
        public int State_SystemNum
        {
            get
            {
                return _State_SystemNum;
            }
            private set
            {
                if (_State_SystemNum != value)
                {
                    _State_SystemNum = value;
                    _State_Change = true;
                }
            }
        }

        private bool _State_Change = false;
        private bool _State_Connected = false;
        private bool _State_PaperOut = false;
        private bool _State_RibbonOut = false;
        private bool _State_Pause = false;
        private bool _State_Error = false;
        private bool _State_Warning = false;
        private int _State_ErrorNum = 0;
        private int _State_WarningNum = 0;
        private int _State_SystemNum = 0;

        #endregion

        public event StateChangeEventHandler StateChange;
        public delegate void StateChangeEventHandler();

        private const int CheckInterval = 5; //sec
        private UdpClient ClientZP = null;
        private IPEndPoint SiteEndPoint = null;
        private DateTime LastCheckDT = new DateTime();
        private Timer timCheckState = null;
        private Timer timCheckConn = null;
        private Timer timInit = null;

        private string PrinterIP = "";
        private int PrinterPort = 0;

        public Zebra(string IP, int Port)
        {
            try
            {
                //Init
                SiteEndPoint = new IPEndPoint(IPAddress.Any, Port);

                // Set Parameter
                this.PrinterIP = IP;
                this.PrinterPort = Port;

                //Init Call
                CheckState();

                // Start Check State
                timCheckState = new Timer(1000 * CheckInterval);
                timCheckState.Elapsed += new ElapsedEventHandler(timCheckState_Tick);
                timCheckState.Enabled = true;

                // Start Check Conn
                timCheckConn = new Timer(1000 * CheckInterval);
                timCheckConn.Elapsed += new ElapsedEventHandler(timCheckConn_Tick);
                timCheckConn.Enabled = true;

                // Only Call First Time
                timInit = new Timer(10);
                timInit.Elapsed += new ElapsedEventHandler(timInit_Tick);
                timInit.Enabled = true;


            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Check Printer State
        /// </summary>
        public void CheckState()
        {
            try
            {
                SendZPL("~HS~HQES");
            }
            catch (Exception ex)
            {
                Console.WriteLine("CheckState : " + ex.Message);
            }
        }

        /// <summary>
        /// 傳送 ZPL Code
        /// </summary>
        /// <param name="Code"></param>
        public void SendZPL(string Code)
        {
            try
            {
                if (Code.Length < 1) return;

                ClientZP = ClientZP ?? new UdpClient(PrinterIP, PrinterPort);
                ClientZP.BeginReceive(UDPRecv, null);
                ClientZP.Send(Encoding.ASCII.GetBytes(Code), Encoding.ASCII.GetBytes(Code).Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SendZPL : {ex.Message} Code : {Code}");
            }
        }

        /// <summary>
        /// Handle Receive Data
        /// </summary>
        /// <param name="ar"></param>
        private void UDPRecv(IAsyncResult ar)
        {
            try
            {
                if (ar != null)
                {
                    Byte[] recvBytes = ClientZP.EndReceive(ar, ref SiteEndPoint);
                    String recvMsg = Encoding.ASCII.GetString(recvBytes);

                    // Message Handling
                    recvMsg = recvMsg.Replace("\u0002", "").Replace("\u0003", "").Replace(" ", "").Replace("\r\nPRINTERSTATUS", "").Replace("\r\n\r\n", "");
                    recvMsg = Regex.Replace(recvMsg, @"(\r\n)$", "");
                    String pat = @"\,|\r\nERRORS:|\r\nWARNINGS:|\r\n";
                    string[] recvData = Regex.Split(recvMsg, pat);

                    // Set Printer State
                    LastCheckDT = DateTime.Now;
                    if (recvData.Length == 27)
                    {
                        State_Connected = true;
                        State_PaperOut = recvData[1] == "1" ? true : false;
                        State_Pause = recvData[2] == "1" ? true : false;
                        State_RibbonOut = recvData[15] == "1" ? true : false;
                        State_Error = recvData[25].Substring(0, 1) == "1" ? true : false;
                        State_Warning = recvData[26].Substring(0, 1) == "1" ? true : false;
                        State_ErrorNum = int.TryParse(recvData[25].Substring(9, 8), out int eNum) ? eNum : 0;
                        State_WarningNum = int.TryParse(recvData[26].Substring(9, 8), out int wNum) ? wNum : 0;

                        // State Change Event
                        if (_State_Change)
                        {
                            _State_Change = false;
                            StateChange?.Invoke();
                        }
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("RECV : " + ex.Message);
            }
        }

        /// <summary>
        /// loop check printer state
        /// </summary>
        private void timCheckState_Tick(object sender, ElapsedEventArgs e)
        {
            timCheckState.Stop();
            CheckState();
            timCheckState.Start();
        }

        private void timInit_Tick(object sender, ElapsedEventArgs e)
        {
            timInit.Stop();
            while (StateChange == null)
            {
                System.Threading.Thread.Sleep(5);
            }
            StateChange?.Invoke();
            timInit.Enabled = false;
        }

        /// <summary>
        /// Connection Check
        /// </summary>
        private void timCheckConn_Tick(object sender, ElapsedEventArgs e)
        {
            timCheckConn.Stop();
            TimeSpan UpdateDuration = new TimeSpan(DateTime.Now.Ticks - LastCheckDT.Ticks);
            if (UpdateDuration.TotalSeconds > CheckInterval * 2)
            {
                State_Connected = false;
            }
            timCheckConn.Start();
        }

        /// <summary>
        /// Set a bit value in a int
        /// </summary>
        /// <param name="Target"></param>
        /// <param name="Value"></param>
        /// <param name="Position"></param>
        private void SetBit(ref int Target, bool Value, int Position)
        {
            if (Value)
            {
                Target |= 1 << Position;
            }
            else
            {
                Target &= ~(1 << Position);
            }
        }
    }
}
