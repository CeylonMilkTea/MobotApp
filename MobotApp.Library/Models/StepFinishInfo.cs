namespace MobotApp.Library.Models
{
	public class StepFinishInfo
	{
		public int Mission_id { get; set; }
		public string Mission_uuid { get; set; }
		public int Robot_id { get; set; }
		public int State { get; set; }
		public int Index { get; set; }
		public string Map_name { get; set; }
		public int Target { get; set; }
		public string Target_code { get; set; }
		public string Action { get; set; }
	}
}


