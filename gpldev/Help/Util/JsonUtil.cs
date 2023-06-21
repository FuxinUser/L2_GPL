using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Util
{
    public  static class JsonUtil
    {
        public static string ToJson(this object obj, bool formatting = false)
        {
            if (formatting)
                return JsonConvert.SerializeObject(obj, Formatting.Indented);
            else
                return JsonConvert.SerializeObject(obj, Formatting.None);
        }

    }
}
