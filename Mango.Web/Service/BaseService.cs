using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility.Enums;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;

namespace Mango.Web.Service
{
	public class BaseService : IBaseService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ITokenProvider _tokenProvider;	
		public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
		{
			_httpClientFactory = httpClientFactory;
			_tokenProvider = tokenProvider;
		}
		public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true)
		{
	
			HttpClient client = _httpClientFactory.CreateClient("MangoAPI");

			HttpRequestMessage message = new();
			HttpResponseMessage? apiResponse = null;

			message.Headers.Add("Accept", "application/json");

			//token
			if (withBearer)
			{
				var token = _tokenProvider.GetToken();
				message.Headers.Add("Authorization", $"Bearer {token}");
			}

			message.RequestUri = new Uri(requestDto.Url);

			if(requestDto.Data != null)
			{
				message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), System.Text.Encoding.UTF8, "application/json");
			}


			switch (requestDto.ApiType)
			{
				case ApiType.POST:
					message.Method = HttpMethod.Post;
					break;

				case ApiType.DELETE:
					message.Method = HttpMethod.Delete;
					break;

				case ApiType.PUT:
					message.Method = HttpMethod.Put;
					break;

				default: 
					message.Method = HttpMethod.Get;
					break;
					
			}


			apiResponse = await client.SendAsync(message);

			try
			{
				switch (apiResponse.StatusCode)
				{
					case HttpStatusCode.NotFound:
						return new() { IsSuccess = false, Message = "Not Found" };

					case HttpStatusCode.Forbidden:
						return new() { IsSuccess = false, Message = "Forbidden Access" };

					case HttpStatusCode.Unauthorized:
						return new() { IsSuccess = false, Message = "Unauthorized Access" };

					case HttpStatusCode.InternalServerError:
						return new() { IsSuccess = false, Message = "Internal Server Error" };

					default:
						var apiContent = await apiResponse.Content.ReadAsStringAsync();
						var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
						return apiResponseDto;

				}
			}
			catch(Exception ex)
			{
				var dto = new ResponseDto
				{
					Message = ex.Message.ToString(),
					IsSuccess = false
				};
				return dto;

			}
			
				


		}
	}
}
