using AutoMapper;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.Dto;

namespace Mango.Services.ShoppingCartAPI.AutoMapperConfig
{
	public class Mapper : Profile
	{
		public Mapper()
		{
			CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
			CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
		}
	}
}
