using System.Collections.Generic;

namespace MobotApp.Library.Models
{
	public class TSPRequest
	{
		public string Map_Name { get; set; }
		public int Start { get; set; }
		public List<int> Passings { get; set; }
		public int End { get; set; }
	}
}


