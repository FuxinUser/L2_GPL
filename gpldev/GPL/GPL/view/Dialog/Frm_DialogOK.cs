using System;
using System.Drawing;
using System.Windows.Forms;


namespace GPLManager
{
    public partial class Frm_DialogOK : Frm_DialogBase
    {

        #region Color
        public Color colorFrmBack;
        public Color colorBroder;
        #endregion

        /// <summary>
        ///     拖曳用的鎖定旗標
        /// </summary>
        public bool Dragging { get; set; }

        /// <summary>
        ///     移動時紀錄的座標
        /// </summary>
        public Point DragOffset { get; set; }

        public Frm_DialogOK()
        {
            InitializeComponent();
        }

       
        private void Frm_DialogOK_Load(object sender, EventArgs e)
        {
          
        }

        private void Btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strMessage">內容</param>
        /// <param name="strTitle">標題</param>
        /// <param name="img">圖示</param>
        /// <param name="intStatus">0:Information ,1:Question ,2:Warning ,3:Error;4:CheckOK </param>
        public void DialogShow(string strMessage, string strTitle, Image img = null ,int intStatus = 0)
        {
            if (img == null)
            {
                switch (intStatus)
                {
                    case 0:
                        img = Properties.Resources.dialogInformation;
                        break;
                    case 1:
                        img = Properties.Resources.dialogQuestion;
                        break;
                    case 2:
                        img = Properties.Resources.dialogWarning;
                        break;
                    case 3:
                        img = Properties.Resources.dialogCancel;
                        break;
                    case 4:
                        img = Properties.Resources.dialogCheck;
                        break;

                    default:
                        img = Properties.Resources.dialogInformation;
                        break;
                }

            }
            /*= Properties.Resources.dialogQuestion*/
            Fun_GetColor(intStatus);
            Fun_SetColor();

            if (strMessage.Length > 245)
            {
                Txt_ShowMessage.ScrollBars = ScrollBars.Both;
            }

            Lbl_Tile.Text = strTitle;
            Txt_ShowMessage.Text = strMessage;
            Pic_Type.BackgroundImage = img;

            Txt_ShowMessage.Select(0, 0);
            Lbl_Tile.Focus();
        }

        private void Fun_SetColor()
        {
            this.BackColor = colorFrmBack;

            Txt_ShowMessage.BackColor = colorFrmBack;

            Pnl_Broder_Top.BackColor = colorBroder;           
            Pnl_Broder_Left.BackColor = colorBroder;
            Pnl_Broder_Right.BackColor = colorBroder;
            Pnl_Broder_Bottom.BackColor = colorBroder;

            Pnl_Border_One.BackColor = colorBroder;
            Pnl_Border_Two.BackColor = colorBroder;
           
        }

        private void Fun_GetColor(int intStatus)
        {           
            switch (intStatus)
            {
                //Information
                case 0:
                //Check
                case 4:
                    colorFrmBack = Color.LightCyan;
                    colorBroder = Color.CornflowerBlue;
                    break;

                //Question
                case 1:
                    colorFrmBack = Color.LemonChiffon;
                    colorBroder = Color.Gold;
                    break;

                //Warning
                case 2: 
                //Error
                case 3:
                    colorFrmBack = Color.MistyRose;
                    colorBroder = Color.Tomato;
                    break;

                default:
                    colorFrmBack = Color.LightCyan;
                    colorBroder = Color.CornflowerBlue;
                    break;
            }

            //return imgNew;
        }

       

        //public enum StatusImg
        //{
        //    Information,//提示:藍底圓形中有一個小寫字母 i
        //    Question,//問題:圓形中有一個問號&打勾
        //    Warning,//警告:黃底三角形中有一個驚嘆號
        //    Error,//錯誤: 紅底圓形中有一個白色 X
        //    Check//OK:打勾
        //}
    }
}
