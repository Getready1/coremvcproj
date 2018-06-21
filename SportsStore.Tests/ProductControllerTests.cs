using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.DataLayer.Repositories.Interfaces;
using SportsStore.DomainModels;
using SportsStore.Web.Configs;
using SportsStore.Web.Controllers;
using SportsStore.Web.Profiles;
using SportsStore.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SportsStore.Tests
{
	public class ProductControllerTests
    {
		Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>();
		Mock<ICommonConfig> configMock = new Mock<ICommonConfig>();

		public ProductControllerTests()
		{
			productRepositoryMock.Setup(m => m.Products).Returns((GetProducts()).AsQueryable());
		}

		[Fact]
		public void Can_Paginate()
		{
			Mock<IMapper> mapper = new Mock<IMapper>();
			mapper.Setup(m => m.Map<List<Product>, List<ProductViewModel>>(It.IsAny<List<Product>>())).Returns(GetProductsViewModel().ToList());

			HomeController controller = new HomeController(productRepositoryMock.Object, mapper.Object, configMock.Object);
			int itemsPerPage = 3;
			configMock.Setup(x => x.ElementsPerPage).Returns(itemsPerPage);

			var currentPage = 2;

			ListViewModel<ProductViewModel> result = (controller.Index(currentPage) as ViewResult).ViewData.Model as ListViewModel<ProductViewModel>;

			var resultArray = result.List.ToArray();
			Assert.True(resultArray.Length == 3);
			Assert.Equal("P4", resultArray[0].Name);
			Assert.Equal("P5", resultArray[1].Name);
			Assert.Equal("P6", resultArray[2].Name);
		}

		[Fact]
		public void Check_Pagination_DefaultModel()
		{
			Mock<IMapper> mapper = new Mock<IMapper>();
			mapper.Setup(m => m.Map<List<Product>, List<ProductViewModel>>(It.IsAny<List<Product>>())).Returns(GetProductsViewModel().ToList());

			int itemsPerPage = 3;
			configMock.Setup(x => x.ElementsPerPage).Returns(itemsPerPage);
			HomeController controller = new HomeController(productRepositoryMock.Object, mapper.Object, configMock.Object);

			ListViewModel<ProductViewModel> result = (controller.Index() as ViewResult).ViewData.Model as ListViewModel<ProductViewModel>;

			var pageNumber = 1;
			var totalItems = GetProductsViewModel().Count();

			Assert.Equal(pageNumber, result.PagingInfo.CurrentPage);
			Assert.Equal(totalItems, result.PagingInfo.TotalItems);
			Assert.Equal(itemsPerPage, result.PagingInfo.ItemsPerPage);
			Assert.Equal((int)Math.Ceiling((double)totalItems / itemsPerPage), result.PagingInfo.TotalPages);
		}

		[Fact]
		public void Check_Pagination_Model_Provided()
		{
			Mock<IMapper> mapper = new Mock<IMapper>();
			mapper.Setup(m => m.Map<List<Product>, List<ProductViewModel>>(It.IsAny<List<Product>>())).Returns(GetProductsViewModel().ToList());

			int itemsPerPage = 3;
			configMock.Setup(x => x.ElementsPerPage).Returns(itemsPerPage);
			HomeController controller = new HomeController(productRepositoryMock.Object, mapper.Object, configMock.Object);

			var pageNumber = 2;
			var totalItems = GetProductsViewModel().Count();

			ListViewModel<ProductViewModel> result = (controller.Index(pageNumber) as ViewResult).ViewData.Model as ListViewModel<ProductViewModel>;

			Assert.Equal(pageNumber, result.PagingInfo.CurrentPage);
			Assert.Equal(itemsPerPage, result.PagingInfo.ItemsPerPage);
			Assert.Equal(totalItems, result.PagingInfo.TotalItems);
			Assert.Equal((int)Math.Ceiling((double)totalItems / itemsPerPage), result.PagingInfo.TotalPages);
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
