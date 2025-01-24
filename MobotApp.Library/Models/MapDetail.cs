using System;
using System.Collections.Generic;
using System.Text;

namespace MobotApp.Library.Models
{
	public class MapDetail
	{
		public string Name { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public List<Node> Nodes { get; set; }
		public List<Edge> Edges { get; set; }
	}
}

