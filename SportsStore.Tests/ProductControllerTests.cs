using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.DataLayer.Repositories.Interfaces;
using SportsStore.DomainModels;
using SportsStore.Web.Controllers;
using SportsStore.Web.Profiles;
using SportsStore.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SportsStore.Tests
{
	public class ProductControllerTests
    {
		[Fact]
		public void Can_Paginate()
		{
			Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>();
			productRepositoryMock.Setup(m => m.Products).Returns((GetProducts()).AsQueryable());

			Mock<IMapper> mapper = new Mock<IMapper>();

			mapper.Setup(m => m.Map<List<Product>, List<ProductViewModel>>(It.IsAny<List<Product>>())).Returns(GetProductsViewModel().ToList());

			HomeController controller = new HomeController(productRepositoryMock.Object, mapper.Object);
			controller.PageSize = 3;

			List<ProductViewModel> result = (controller.Index(2) as ViewResult).ViewData.Model as List<ProductViewModel>;

			var resultArray = result.ToArray();
			Assert.True(resultArray.Length == 3);
			Assert.Equal("P4", resultArray[0].Name);
			Assert.Equal("P5", resultArray[1].Name);
			Assert.Equal("P6", resultArray[2].Name);
		}

		Product[] GetProducts()
		{
			return new Product[] {
					new Product { ProductId = 1, Name = "P1"},
					new Product { ProductId = 2, Name = "P2" },
					new Product { ProductId = 3, Name = "P3" },
					new Product { ProductId = 4, Name = "P4" },
					new Product { ProductId = 5, Name = "P5" },
					new Product { ProductId = 6, Name = "P6" }
				};
		}

		ProductViewModel[] GetProductsViewModel()
		{
			return new ProductViewModel[] {
					new ProductViewModel { ProductId = 1, Name = "P1"},
					new ProductViewModel { ProductId = 2, Name = "P2" },
					new ProductViewModel { ProductId = 3, Name = "P3" },
					new ProductViewModel { ProductId = 4, Name = "P4" },
					new ProductViewModel { ProductId = 5, Name = "P5" },
					new ProductViewModel { ProductId = 6, Name = "P6" }
				};
		}
    }
}
