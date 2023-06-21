using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Handle
{
    public class LogHandler
    {
        public delegate void DelDebug(string format, params object[] args);
        public delegate void DelError(string format, params object[] args);
        public delegate void DelInfo(string format, params object[] args);
        public delegate void DelWarning(string format, params object[] args);

        public DelDebug _debug = null;
        public DelError _error = null;
        public DelInfo _info = null;
        public DelWarning _warning = null;


        public void Debug(string format, params object[] args)
        {
            if (_debug != null)
            {
                _debug(format, args);
            }
        }
        public void Error(string format, params object[] args)
        {
            if (_error != null)
            {
                _error(format, args);
            }
        }

        public void Info(string format, params object[] args)
        {
            if (_info != null)
            {
                _info(format, args);
            }
        }
        public void Warning(string format, params object[] args)
        {
            if (_warning != null)
            {
                _warning(format, args);
            }
        }
    }
}
