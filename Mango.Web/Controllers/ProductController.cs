using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet]
		public async Task<IActionResult> ProductIndex()
		{
			List<ProductDto>? list = new();

			ResponseDto? response = await _productService.GetAllProductsAsync();

			if (response != null && response.IsSuccess == true)
			{
				list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
			}
			else
			{
				TempData["error"] = response?.Message;
			}

			return View(list);
		}

		[HttpGet]
		public async Task<IActionResult> ProductCreate() 
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ProductCreate(ProductDto model)
		{

			if (ModelState.IsValid)
			{
				ResponseDto? result = await _productService.CreateProductAsync(model);

				if(result != null && result.IsSuccess == true)
				{

					TempData["success"] = "Product is Successfuly Created!";

					return RedirectToAction(nameof(ProductIndex));
				}
				else
				{
					TempData["error"] = result?.Message;
				}

			}

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> ProductDelete(int productId)
		{

			ResponseDto? result = await _productService.GetProductByIdAsync(productId);

			if(result != null && result.IsSuccess == true)
			{
				ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(result.Result));

				return View(model);
			}
			else
			{
				TempData["error"] = result?.Message;
			}

			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> ProductDelete(ProductDto productDto)
		{
			
			ResponseDto? result = await _productService.DeleteProductAsync(productDto.ProductId);

			if (result != null && result.IsSuccess == true)
			{

				TempData["success"] = "Product is Successfuly Deleted!";

				return RedirectToAction(nameof(ProductIndex));
			}
			else
			{
				TempData["error"] = result?.Message;
			}

			return View(productDto);
		}


		[HttpGet]
		public async Task<IActionResult> ProductEdit(int productId)
		{

			ResponseDto? response = await _productService.GetProductByIdAsync(productId);

			if (response != null && response.IsSuccess == true)
			{
				ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));

				return View(model);
			}
			else
			{
				TempData["error"] = response?.Message;
			}

			return NotFound();
		}


		[HttpPost]
		public async Task<IActionResult> ProductEdit(ProductDto productDto)
		{
			if (ModelState.IsValid)
			{
				ResponseDto? result = await _productService.UpdateProductAsync(productDto);

				if (result != null && result.IsSuccess == true)
				{

					TempData["success"] = "Product is Successfuly Updated!";

					return RedirectToAction(nameof(ProductIndex));
				}
				else
				{
					TempData["error"] = result?.Message;
				}

			}

			
			return View(productDto);
		}
	}
}
