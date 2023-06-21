using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Help.DumpRawDataHelp
{
    public interface IDumpRawData
    {
        void DumpMsg(byte[] data, string filePath);

        string GetMsgID(byte[] data);
    }
}
