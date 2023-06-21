using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPLManager
{
    public class DrawItemHandler
    {
        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly DrawItemHandler INSTANCE = new DrawItemHandler();
        }

        public static DrawItemHandler Instance { get { return SingletonHolder.INSTANCE; } }

        public void DrawItem(TabControl TabName , DrawItemEventArgs e)
        {
            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;// 設置文字置中
            SolidBrush bru = new SolidBrush(Color.FromArgb(72, 181, 250));
            SolidBrush bruFont = new SolidBrush(Color.Black);// 字體色彩 
            Font font = new Font("微軟正黑體", 17, GraphicsUnit.Pixel);//字體樣式 
            for (int i = 0; i < TabName.TabPages.Count; i++)
            {
                //獲取頁籤的工作區域 
                Rectangle recChild = TabName.GetTabRect(i);
                if (TabName.SelectedIndex == i) //Selected顏色
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.RoyalBlue), recChild);
                    bruFont = new SolidBrush(Color.White);
                    font = new Font("微軟正黑體", 17, GraphicsUnit.Pixel);
                    //繪制文字
                    e.Graphics.DrawString(TabName.TabPages[i].Text, font, bruFont, recChild, StrFormat);
                }
                else  //未Selected顏色
                {
                    bruFont = new SolidBrush(Color.Black);
                    font = new Font("微軟正黑體", 17, GraphicsUnit.Pixel);
                    //繪制文字 
                    e.Graphics.DrawString(TabName.TabPages[i].Text, font, bruFont, recChild, StrFormat);
                }
            }
        }
    }
}
