using SportsStore.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsStore.DataLayer.Repositories.Interfaces
{
    public interface IProductRepository
    {
		IQueryable<Product> Products { get; }
    }
}
