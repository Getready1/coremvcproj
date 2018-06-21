using Newtonsoft.Json;
using System;

namespace SportsStore.Web.ViewModels
{
	public class PagingInfo
    {
		public bool FilledManually { get; set; }

		public int TotalItems { get; set; }
		public int ItemsPerPage { get; set; }
		[JsonProperty("currentPage")]
		public int CurrentPage { get; set; } = 1;
		public int TotalPages => (int) Math.Ceiling((decimal) TotalItems / (ItemsPerPage == 0 ? 1 : ItemsPerPage));
	}
}
