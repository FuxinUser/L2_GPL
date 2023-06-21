using System;
using System.Data;
using System.Windows.Forms;

namespace GPLManager.Util
{
    public static class InvaildUtil
    {
        public static bool IsNull(this DataTable dataTable)
        {
            return dataTable == null || dataTable.Rows.Count == 0;
        }

        public static bool DgvIsNull(this DataGridView Dgv)
        { 
            return Dgv == null || Dgv.Rows.Count == 0;
        }

        public static bool CurrentIsNull(this DataGridView Dgv)
        {
            return Dgv.CurrentRow == null || Dgv.CurrentRow.Index == -1 || Dgv.SelectedRows.Count == 0;
        }

        public static bool IsEmpty(this string str)
        {
            return str.Trim().Equals(string.Empty);
        }
        public static bool DateTimeRangeIsFail(this DateTimePicker Dtp_start, DateTime DtEnd)
        {
            return Dtp_start.Value > DtEnd;
        }
    }
}
