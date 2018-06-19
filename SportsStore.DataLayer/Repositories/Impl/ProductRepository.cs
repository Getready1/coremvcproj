using SportsStore.DataLayer.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using SportsStore.DomainModels;
using SportsStore.DataAccess;

namespace SportsStore.DataLayer.Repositories.Impl
{
	public class ProductRepository : IProductRepository
	{
		private ApplicationDbContext context;

		public ProductRepository(ApplicationDbContext context)
		{
			this.context = context;
		}

		public IQueryable<Product> Products => context.Products;
	}
}
