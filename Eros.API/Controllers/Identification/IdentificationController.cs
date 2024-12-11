using Eros.API.Extensions;
using Eros.API.Framework;
using Eros.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Eros.API.Controllers.Identification;

public class LoginRequest
{
	[Required]
	public string Email { get; set; }
	[Required]
	public string Password { get; set; }
}

public class LoginResponse : Response
{
	public bool Succeeded { get; set; }
	public bool RequiresTwoFactor { get; set; }
}

public class RegisterRequest
{
	[Required]
	public string Email { get; set; }
	[Required]
	public string Password { get; set; }
}

public class RegisterResponse : Response
{
	public bool Succeeded { get; set; }
}

[ApiController]
[Route("[controller]")]
public class IdentificationController : ControllerBase
{
	private readonly SignInManager<User> _signInManager;
	private readonly UserManager<User> _userManager;

	public IdentificationController(SignInManager<User> signInManager, UserManager<User> userManager)
	{
		_signInManager = signInManager;
		_userManager = userManager;
	}

	//[HttpPost(nameof(Login))]
	//public async Task<LoginResponse> Login(LoginRequest loginRequest)
	//{
	//	var user = await _userManager.FindByEmailAsync(loginRequest.Email);

	//	if (user == null)
	//	{
	//		return new LoginResponse().Throw(Errors.Global.EmailDoesntExist);
	//	}

	//	var signInResult = await _signInManager.PasswordSignInAsync(
	//		user,
	//		password: loginRequest.Password,
	//		isPersistent: false,
	//		lockoutOnFailure: false
	//		);

	//	return new LoginResponse
	//	{
	//		RequiresTwoFactor = signInResult.RequiresTwoFactor,
	//		Succeeded = signInResult.Succeeded
	//	};
	//}

	//[HttpPost(nameof(Register))]
	//public async Task<RegisterResponse> Register(RegisterRequest registerRequest)
	//{
	//	var email = registerRequest.Email;

	//	var user = new User
	//	{
	//		Email = registerRequest.Email,
	//		UserName = registerRequest.Email
	//	};

	//	if (string.IsNullOrEmpty(email) || !_emailAddressAttribute.IsValid(email))
	//	{
	//		return CreateValidationProblem(IdentityResult.Failed(userManager.ErrorDescriber.InvalidEmail(email)));
	//	}

	//	var result = await _userManager.CreateAsync(user, registerRequest.Password);

	//	if (result.Succeeded)
	//	{
	//		return new RegisterResponse
	//		{
	//			Succeeded = result.Succeeded
	//		};
	//	}

	//	return new RegisterResponse().Throw(result.Errors.FirstOrDefault().ToError());
	//}
}
