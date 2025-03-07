using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Mango.Web.Utility.Enums;

namespace Mango.Web.Service
{
	public class CartService : ICartService
	{
		private readonly IBaseService _baseService;

		public CartService(IBaseService baseService)
		{
			_baseService = baseService;
		}

		public async Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.POST,
				Url = ApiBase.ShoppingCartAPIBase + $"/api/cart/ApplyCoupon",
				Data = cartDto //Already Serialized in the base service

			});
		}

		public async Task<ResponseDto?> GetCartByUserIdAsync(string userId)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.GET,
				Url = ApiBase.ShoppingCartAPIBase + $"/api/cart/GetCart/{userId}",

			});
		}

		public async Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.DELETE,
				Url = ApiBase.ShoppingCartAPIBase + $"/api/cart/RemoveCart/{cartDetailsId}",
				Data = cartDetailsId

			});
		}

		public async Task<ResponseDto?> UpsertCartAsync(CartDto cartDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.POST,
				Url = ApiBase.ShoppingCartAPIBase + $"/api/cart/CartUpsert",
				Data = cartDto //Already Serialized in the base service

			});
		}
	}
}
