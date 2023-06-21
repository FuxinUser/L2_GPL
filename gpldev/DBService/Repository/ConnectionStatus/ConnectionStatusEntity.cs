using DBService.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.ConnectionStatus
{
	public class ConnectionStatusEntity
	{
		public class TBL_ConnectionStatus : BaseRepositoryModel
		{
			[PrimaryKey]
			public string Connection_From { get; set; }
			[PrimaryKey]
			public string Connection_To { get; set; }
			public string Connection_Type { get; set; }
			public string Connection_IP { get; set; }
			public string Connection_Port { get; set; }
			public string Connection_Status { get; set; }
			public System.DateTime Create_DateTime { get; set; }
		}
	}
}
