using AutoMapper;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;

namespace Mango.Services.CouponAPI.AutoMapperConfig
{
	public class Mapper : Profile
	{
		public Mapper()
		{
			CreateMap<CouponDto, Coupon>().ReverseMap();
		}
	}
}
