using DBService.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.WorkSchedule
{
    public class WorkScheduleEntity
    {
		public class TBL_WorkSchedule : BaseRepositoryModel
		{			
			[PrimaryKey]
			public int SerNo { get; set; }
			public int Shift { get; set; }
			public string Team { get; set; }
		
			public string ShiftDate { get; set; }

			public int Mode { get; set; }

			public string ShiftStartTime { get; set; }

			public string ShiftEndTime { get; set; }

			public string ShiftPerson { get; set; }
			public override DateTime CreateTime { get; set; }
		}


	}
}
