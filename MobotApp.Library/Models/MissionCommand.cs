namespace MobotApp.Library.Models
{
	public class MissionCommand
	{
		public int Mission_id { get; set; }
		public string Mission_uuid { get; set; }
		public int Cmd { get; set; }
		public bool Sync { get; set; }
		public int Id { get; set; }
		public int Result { get; set; }
		public string Err_msg { get; set; }
	}
}


