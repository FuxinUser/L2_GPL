using System.Drawing;
using System.Windows.Forms;

namespace GPLManager
{
    public class ReadOnlyHandler
    {
        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly ReadOnlyHandler INSTANCE = new ReadOnlyHandler();
        }

        public static ReadOnlyHandler Instance { get { return SingletonHolder.INSTANCE; } }

        public void ReadOnly(Control[] ctrContainer, bool bolReadOnly)
        {
            foreach (Control Ctl1 in ctrContainer)
            {
                if (Ctl1 is TextBox)
                {
                    TextBox ibc = Ctl1 as TextBox;
                    if (ibc is TextBox)
                    {
                        ibc.ReadOnly = bolReadOnly;
                        if (bolReadOnly)
                            ibc.BackColor = Color.Gainsboro;
                        else
                            ibc.BackColor = Color.LightYellow;
                    }
                }
                if (Ctl1 is ComboBox)
                {
                    ComboBox ibc = Ctl1 as ComboBox;
                    if (ibc is ComboBox)
                    {
                        ibc.Enabled = !bolReadOnly;
                        if (bolReadOnly)
                            ibc.BackColor = Color.Gainsboro;
                        else
                            ibc.BackColor = Color.LightYellow;
                    }

                }
                //if (Ctl1 is CtrNumTextBox)
                //{
                //    CtrNumTextBox ibc = Ctl1 as CtrNumTextBox;
                //    if (ibc is CtrNumTextBox)
                //    {
                //        ibc.ReadOnly = bolReadOnly;
                //        if (bolReadOnly)
                //            ibc.BackColor = Color.Gainsboro;
                //        else
                //            ibc.BackColor = Color.LightYellow;
                //    }
                //}
                if (Ctl1 is DateTimePicker)
                {
                    DateTimePicker ibc = Ctl1 as DateTimePicker;
                    if (ibc is DateTimePicker)
                    {
                        ibc.Enabled = !bolReadOnly;
                        if (!bolReadOnly)
                            ibc.BackColor = Color.Gainsboro;
                        else
                            ibc.BackColor = Color.LightYellow;
                    }
                }
            }
        }

        public void ReadOnly(TabPage page1, bool bolReadOnly)
        {
            Control[] ctrContainer = { page1 };
            foreach (Control container in ctrContainer)
            {
                Fun_FindControlReadOnly(container, bolReadOnly);
                foreach (Control ctrl in container.Controls)
                {
                    Fun_FindControlReadOnly(ctrl, bolReadOnly);
                    foreach (Control ctrl2 in ctrl.Controls)
                    {
                        Fun_FindControlReadOnly(ctrl2, bolReadOnly);
                    }
                }
            }
        }

        public void Fun_FindControlReadOnly(Control Ctl, bool bolReadOnly)
        {
            //判斷是否有子控制項
            if (Ctl.Controls.Count > 0)
                foreach (Control Ctl1 in Ctl.Controls)
                {
                    if (Ctl1 is TextBox)
                    {
                        TextBox ibc = Ctl1 as TextBox;
                        if (ibc is TextBox)
                        {
                            ibc.ReadOnly = bolReadOnly;
                            if (bolReadOnly)
                                ibc.BackColor = Color.Gainsboro;
                            else
                                ibc.BackColor = Color.LightYellow;
                        }
                    }
                    if (Ctl1 is ComboBox)
                    {
                        ComboBox ibc = Ctl1 as ComboBox;
                        if (ibc is ComboBox)
                        {
                            ibc.Enabled = !bolReadOnly;
                            if (bolReadOnly)
                                ibc.BackColor = Color.Gainsboro;
                            else
                                ibc.BackColor = Color.LightYellow;
                        }

                    }
                    if (Ctl1 is DateTimePicker)
                    {
                        DateTimePicker ibc = Ctl1 as DateTimePicker;
                        if (ibc is DateTimePicker)
                        {
                            ibc.Enabled = !bolReadOnly;
                            if (!bolReadOnly)
                                ibc.BackColor = Color.Gainsboro;
                            else
                                ibc.BackColor = Color.LightYellow;
                        }
                    }
                }
        }
        public void ReadOnlyGroupBox(GroupBox group, bool bolReadOnly)
        {
            Control[] ctrContainer = { group };
            foreach (Control container in ctrContainer)
            {
                Fun_FindControlEnable(container, bolReadOnly);
                foreach (Control ctrl in container.Controls)
                {
                    Fun_FindControlEnable(ctrl, bolReadOnly);
                    foreach (Control ctrl2 in ctrl.Controls)
                    {
                        Fun_FindControlEnable(ctrl2, bolReadOnly);
                    }
                }
            }
        }
        public void Fun_PanelClear(Panel pnl)
        {
            Control[] ctrContainer = { pnl };
            foreach (Control container in ctrContainer)
            {
                Fun_FindControlClear(container);
                foreach (Control ctrl in container.Controls)
                {
                    Fun_FindControlClear(container);
                    foreach (Control ctrl2 in ctrl.Controls)
                    {
                        Fun_FindControlClear(container);
                    }
                }
            }
        }
        public void Fun_PageClear(TabPage page1)
        {
            Control[] ctrContainer = { page1 };
            foreach (Control container in ctrContainer)
            {
                Fun_FindControlClear(container);
                foreach (Control ctrl in container.Controls)
                {
                    Fun_FindControlClear(container);
                    foreach (Control ctrl2 in ctrl.Controls)
                    {
                        Fun_FindControlClear(container);
                    }
                }
            }
        }
        public void Fun_GroupBoxClear(GroupBox group)
        {
            Control[] ctrContainer = { group };
            foreach (Control container in ctrContainer)
            {
                Fun_FindControlClear(container);
                foreach (Control ctrl in container.Controls)
                {
                    Fun_FindControlClear(container);
                    foreach (Control ctrl2 in ctrl.Controls)
                    {
                        Fun_FindControlClear(container);
                    }
                }
            }
        }

       
        public void Fun_FindControlEnable(Control Ctl, bool bolReadOnly)
        {
            //判斷是否有子控制項
            if (Ctl.Controls.Count > 0)
                foreach (Control Ctl1 in Ctl.Controls)
                {
                    if (Ctl1 is TextBox)
                    {
                        TextBox ibc = Ctl1 as TextBox;
                        if (ibc is TextBox)
                        {
                            ibc.Enabled = !bolReadOnly;
                        }
                    }
                    if (Ctl1 is ComboBox)
                    {
                        ComboBox ibc = Ctl1 as ComboBox;
                        if (ibc is ComboBox)
                        {
                            ibc.Enabled = !bolReadOnly;
                        }

                    }
                }
        }
        public void ReadOnlyForFrm_5_2(TabPage page1, bool bolReadOnly)
        {
            Control[] ctrContainer = { page1 };
            foreach (Control container in ctrContainer)
            {
                Fun_FindTextBoReadOnly(container, bolReadOnly);
                foreach (Control ctrl in container.Controls)
                {
                    Fun_FindTextBoReadOnly(ctrl, bolReadOnly);
                    foreach (Control ctrl2 in ctrl.Controls)
                    {
                        Fun_FindTextBoReadOnly(ctrl2, bolReadOnly);
                    }
                }
            }
        }
        public void Fun_FindTextBoReadOnly(Control Ctl, bool bolReadOnly)
        {
            //判斷是否有子控制項
            if (Ctl.Controls.Count > 0)
                foreach (Control Ctl1 in Ctl.Controls)
                {
                    if (Ctl1 is TextBox)
                    {
                        TextBox ibc = Ctl1 as TextBox;
                        if (ibc is TextBox)
                        {
                            ibc.ReadOnly = bolReadOnly;
                            if (bolReadOnly)
                                ibc.BackColor = Color.Gainsboro;
                            else
                                ibc.BackColor = Color.LightYellow;
                        }
                    }
                }
        }

        /// <summary>
        /// 清除Text內容
        /// </summary>
        /// <param name="ctrControl"></param>
        public void ClearControl(Control[] ctrControl)
        {
            Control[] ctrContainer = ctrControl;
            foreach (Control container in ctrContainer)
            {
                Fun_FindControlClear(container);
                foreach (Control ctrl in container.Controls)
                {
                    Fun_FindControlClear(ctrl);
                    foreach (Control ctrl2 in ctrl.Controls)
                    {
                        Fun_FindControlClear(ctrl2);
                    }
                }
            }
        }

        public void Fun_FindControlClear(Control Ctl)
        {
            //判斷是否有子控制項
            if (Ctl.Controls.Count > 0)
                foreach (Control Ctl1 in Ctl.Controls)
                {
                    if (Ctl1 is TextBox)
                    {
                        TextBox ibc = Ctl1 as TextBox;
                        if (ibc is TextBox)
                        {
                            ibc.Text = string.Empty;
                        }
                    }
                    if (Ctl1 is ComboBox)
                    {
                        ComboBox ibc = Ctl1 as ComboBox;
                        if (ibc is ComboBox)
                        {
                            ibc.SelectedIndex = 0;
                        }

                    }
                }
        }


    }
}
