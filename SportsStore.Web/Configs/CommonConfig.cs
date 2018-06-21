using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Web.Configs
{
	public class CommonConfig : ICommonConfig
	{
		public int ElementsPerPage => 4;
	}
}
