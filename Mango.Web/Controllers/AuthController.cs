﻿using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mango.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
		private readonly ITokenProvider _tokenProvider;

		public AuthController(IAuthService authService, ITokenProvider tokenProvider)
		{
			_authService = authService;
			_tokenProvider = tokenProvider;
		}

		[HttpGet]
		public IActionResult Login()
		{
			LoginRequestDto loginRequestDto = new();

			return View(loginRequestDto);
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginRequestDto obj)
		{

			ResponseDto? responseDto = await _authService.LoginAsync(obj);

			if (responseDto != null && responseDto.IsSuccess == true)
			{
				LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));

				_tokenProvider.SetToken(loginResponseDto.Token);

				await SignInUser(loginResponseDto);

				return RedirectToAction("Index", "Home");
			}
			else
			{
				TempData["error"] = responseDto.Message;

				return View(obj);
			}


		}

		[HttpGet]
		public IActionResult Register()
		{

			var roleList = new List<SelectListItem>()
			{
				new SelectListItem{Text = ApiBase.RoleAdmin, Value=ApiBase.RoleAdmin },
				new SelectListItem{Text = ApiBase.RoleCustomer, Value=ApiBase.RoleCustomer },
			};

			ViewBag.RoleList = roleList;

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegistrationRequestDto obj)
		{

			ResponseDto? result = await _authService.RegisterAsync(obj);

			ResponseDto? assingRole;

			if(result != null && result.IsSuccess == true)
			{
				if (string.IsNullOrEmpty(obj.Role))
				{
					obj.Role = ApiBase.RoleCustomer;
				}

				assingRole = await _authService.AssignRoleAsync(obj);

				if(assingRole != null && assingRole.IsSuccess == true)
				{
					TempData["success"] = "Registration Successful!";

					return RedirectToAction(nameof(Login));
				}
			}
			else
			{
				TempData["error"] = result.Message;
			}

				var roleList = new List<SelectListItem>()
			{
				new SelectListItem{Text = ApiBase.RoleAdmin, Value=ApiBase.RoleAdmin },
				new SelectListItem{Text = ApiBase.RoleCustomer, Value=ApiBase.RoleCustomer },
			};

			ViewBag.RoleList = roleList;

			return View(obj);
		}


		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();

			_tokenProvider.ClearToken();

			return RedirectToAction("Index","Home");
		}


		private async Task SignInUser(LoginResponseDto model)
		{
			var handler = new JwtSecurityTokenHandler();

			var jwt = handler.ReadJwtToken(model.Token);

			var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

			identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
			identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
			identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));


			identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
			identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));


			var principal = new ClaimsPrincipal(identity);

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
		}

	}
}
