using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace GPLManager
{
    public class ZedGraphSetData
    {
        public DataTable DtTrend { get; set; } = null;
        public string StrXpoint { get; set; } = string.Empty;
        public string StrYpoint { get; set; } = string.Empty;
        public string StrTitle { get; set; } = string.Empty;
        public string StrXTitle { get; set; } = string.Empty;
        public string StrYTitle { get; set; } = string.Empty;
        public string StrLineItem_Title { get; set; } = string.Empty;
        public Color LineItem_Color { get; set; }
        public SymbolType LineItem_Type { get; set; }

        public string StrTag { get; set; } = string.Empty;
    }
}
