using AutoMapper;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;

namespace Mango.Services.ProductAPI.AutoMapperConfig
{
	public class Mapper : Profile
	{
		public Mapper()
		{
			CreateMap<ProductDto, Product>().ReverseMap();
		}
	}
}
