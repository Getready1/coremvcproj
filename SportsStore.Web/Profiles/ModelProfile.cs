using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Web.Profiles
{
    public class ModelProfile : Profile
    {
		public ModelProfile()
		{
			CreateMap<DomainModels.Product, ViewModels.ProductViewModel>();
		}
    }
}
