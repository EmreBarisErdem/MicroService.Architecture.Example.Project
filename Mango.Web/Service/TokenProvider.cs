﻿using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
	public class TokenProvider : ITokenProvider
	{
		private readonly IHttpContextAccessor _contextAccessor;

		public TokenProvider(IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor;
		}

		public void ClearToken()
		{
			_contextAccessor.HttpContext?.Response.Cookies.Delete(ApiBase.TokenCookie);
		}

		public string? GetToken()
		{
			string? token = null;

			bool? hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(ApiBase.TokenCookie, out token);

			return hasToken is true ? token : null;
		}

		public void SetToken(string token)
		{
			_contextAccessor.HttpContext?.Response.Cookies.Append(ApiBase.TokenCookie, token);

		}
	}
}
