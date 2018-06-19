using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SportsStore.DomainModels;

namespace SportsStore.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
		{
			
		}

		public DbSet<Product> Products { get; set; }
	}
}
