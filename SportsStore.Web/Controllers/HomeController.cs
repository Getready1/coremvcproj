using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsStore.DataLayer.Repositories.Interfaces;
using SportsStore.DomainModels;
using SportsStore.Web.Configs;
using SportsStore.Web.ViewModels;

namespace SportsStore.Web.Controllers
{
    public class HomeController : Controller
    {
		private IProductRepository productRepository;
		private IMapper mapper;
		private ICommonConfig configs;

		public HomeController(IProductRepository productRepository, IMapper mapper, ICommonConfig configs)
		{
			this.productRepository = productRepository;
			this.mapper = mapper;
			this.configs = configs;
		}

		public IActionResult Index(int page = 1)
        {
			var list = mapper.Map<List<Product>, List<ProductViewModel>>(productRepository.Products.ToList());

			PagingInfo pagingInfo = new PagingInfo
			{
				CurrentPage = page,
				TotalItems = list.Count,
				ItemsPerPage = configs.ElementsPerPage,
				FilledManually = true
			};

			var sortedList = list.OrderBy(p => p.ProductId)
								.Skip((pagingInfo.CurrentPage - 1) * pagingInfo.ItemsPerPage)
								.Take(pagingInfo.ItemsPerPage)
								.ToList();

			return View(new ListViewModel<ProductViewModel>
			{
				List = sortedList,
				PagingInfo = pagingInfo
			});
        }
    }
}