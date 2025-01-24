using System.Collections.Generic;

namespace MobotApp.Library.Models
{
	public class MissionInfo
	{
		public string Ref_uuid { get; set; }
		public string Src { get; set; }
		public string Description { get; set; }
		public int Req_Robot { get; set; }
		public string Req_Rbogroups { get; set; }
		public int Priority { get; set; }
		public List<Step> Steps { get; set; }
		public int Id { get; set; }
		public int Robot_Id { get; set; }
		public int State { get; set; }
		public string Err_msg { get; set; }
	}
}


