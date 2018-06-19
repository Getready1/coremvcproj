using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsStore.DataLayer.Repositories.Interfaces;
using SportsStore.DomainModels;
using SportsStore.Web.ViewModels;

namespace SportsStore.Web.Controllers
{
    public class HomeController : Controller
    {
		private IProductRepository productRepository;
		private IMapper mapper;

		public int PageSize { get; set; } = 3;

		public HomeController(IProductRepository productRepository, IMapper mapper)
		{
			this.productRepository = productRepository;
			this.mapper = mapper;
		}

		public IActionResult Index(int productPage = 1)
        {
			var list = mapper.Map<List<Product>, List<ProductViewModel>>(productRepository.Products.ToList());

			return View(
						list.OrderBy(p => p.ProductId)
							.Skip((productPage - 1) * PageSize)
							.Take(PageSize)
							.ToList()
							);
        }
    }
}