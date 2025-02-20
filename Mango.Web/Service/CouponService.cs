using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Mango.Web.Utility.Enums;

namespace Mango.Web.Service
{
	public class CouponService : ICouponService
	{
		private readonly IBaseService _baseService;

		public CouponService(IBaseService baseService)
		{
			_baseService = baseService;
		}

		public async Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.POST,
				Url = ApiBase.CouponAPIBase + $"api/coupon",
				Data = couponDto //Already Serialized in the base service

			});
		}

		public async Task<ResponseDto?> DeleteCouponAsync(int id)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.DELETE,
				Url = ApiBase.CouponAPIBase + $"api/coupon/{id}",

			});
		}

		public async Task<ResponseDto?> GetAllCouponsAsync()
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.GET,
				Url = ApiBase.CouponAPIBase + "/api/coupon",

			});
		}

		public async Task<ResponseDto?> GetCouponByCodeAsync(string couponCode)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.GET,
				Url = ApiBase.CouponAPIBase + $"api/coupon/GetByCode/{couponCode}",

			});
		}

		public async Task<ResponseDto?> GetCouponByIdAsync(int id)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.GET,
				Url = ApiBase.CouponAPIBase + $"api/coupon/{id}",

			});
		}

		public async Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.PUT,
				Url = ApiBase.CouponAPIBase + $"api/coupon",
				Data = couponDto //Already Serialized in the base service

			});
		}
	}
}
