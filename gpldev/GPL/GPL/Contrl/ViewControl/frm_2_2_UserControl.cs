using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPLManager
{
    public partial class frm_2_2_UserControl : UserControl
    {
        public frm_2_2_UserControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// FirstName = 部位
        /// LastName = 道次
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="textBox"></param>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        public void ForNumber(DataTable dt, TextBox textBox,string FirstName,string LastName)
        {
            string Name = "";
            for (int Number = 0; Number < dt.Rows.Count; Number++)
            {
                FirstName += Number.ToString();
                LastName += Number.ToString();
                Name = FirstName + LastName;
                TextBoxSetting(textBox,Name);
            }
        }
        public void TextBoxSetting(TextBox textBox , string Name )
        {
            textBox.Name = Name + textBox.Name;
        }
    }
}
