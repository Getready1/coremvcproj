using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Web.ViewModels
{
    public class ListViewModel<T>
    {
		public List<T> List { get; set; }
		public PagingInfo PagingInfo { get; set; }
	}
}
