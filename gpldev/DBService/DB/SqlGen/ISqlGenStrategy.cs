using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author: ICSC SPYUA
 * Date: 2019/12/24
 * Desc: Sql語法產生介面
 */

namespace DBService.DB
{
    public interface ISqlGenStrategy
    {
        string Create(string table, object data);

        string Update(string table, object data, string condition);

        string Read(string table, bool isReadAll, int num = 1, string condition = "", string otherCondition = "");

        string Delete(string table, string condition);
              
    }
}
