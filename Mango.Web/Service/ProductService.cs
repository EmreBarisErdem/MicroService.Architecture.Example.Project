 using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Mango.Web.Utility.Enums;

namespace Mango.Web.Service
{
	public class ProductService : IProductService
	{
		private readonly IBaseService _baseService;

		public ProductService(IBaseService baseService)
		{
			_baseService = baseService;
		}

		public async Task<ResponseDto?> CreateProductAsync(ProductDto productDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.POST,
				Url = ApiBase.ProductAPIBase + $"/api/product",
				Data = productDto
			});
		}

		public async Task<ResponseDto?> DeleteProductAsync(int id)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.DELETE,
				Url = ApiBase.ProductAPIBase + $"/api/product/{id}",
			});
		}

		public async Task<ResponseDto?> GetAllProductsAsync()
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.GET,
				Url = ApiBase.ProductAPIBase + $"/api/product",
				
			});
		}

		public async Task<ResponseDto?> GetProductByIdAsync(int id)
		{
			return await _baseService.SendAsync(new RequestDto()
			{ 
				ApiType = ApiType.GET,
				Url = ApiBase.ProductAPIBase + $"/api/product/{id}",
				

			});
		}

		public async Task<ResponseDto?> UpdateProductAsync(ProductDto productDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.PUT,
				Url = ApiBase.ProductAPIBase + $"/api/product",
				Data = productDto
			});
		}
	}
}
