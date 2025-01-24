namespace MobotApp.Library.Models
{
	public class RobotInfo
	{
		public int Id { get; set; }
		public string Alias { get; set; }
		public int Run_state { get; set; }
		public string Run_state_str { get; set; }
		public int Mission_id { get; set; }
		public int Mission_state { get; set; }
		public int Last_spot_id { get; set; }
		public int Next_spot_id { get; set; }
		public int Update_stamp { get; set; }
		public string Map_name { get; set; }
		public double X { get; set; }
		public double Y { get; set; }
		public double Theta { get; set; }
		public double Battery { get; set; }
		public double Battery_temp { get; set; }
	}
}


