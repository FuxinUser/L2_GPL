using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPLManager
{
    class CurrentTracking
    {
        public class SkidTracker : INotifyPropertyChanged
        {
            string strCurrentValue = "";
            public event PropertyChangedEventHandler PropertyChanged;

            public SkidTracker()
            {
            }

            public string CurrentValue
            {
                get { return strCurrentValue; }
                set
                {
                    RunCompute(value);
                    strCurrentValue = value;
                }
            }

            protected void OnPropertyChanged(string name)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(name));
                }
            }

            protected void RunCompute(string strNewValue)
            {
                if (strNewValue != strCurrentValue && strNewValue == "")

                {
                    OnPropertyChanged("CoilOut");
                }

                else if (strNewValue != strCurrentValue && strNewValue != "")
                {
                    OnPropertyChanged("CoilIn");
                }
            }
        }

        private void sk01_PropertyChangeCallBack(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CoilIn":
                    MessageBox.Show("CoilIn");
                    break;

                case "CoilOut":
                    MessageBox.Show("CoilOut");
                    break;
            }
            SkidTracker sk01 = new SkidTracker();
        }
    }
}
