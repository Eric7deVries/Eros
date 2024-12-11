using Eros.API.Framework;
using Eros.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eros.API.Controllers;

[Description("Get user by ID")]
public class GetUserRequest
{

}

[Description("Lists all available users")]
public class ListUserRequest
{

}

[Description("Updates the current user.")]
public class UpdateUserRequest
{

}

[Description("Deletes a user.")]
public class DeleteUserRequest
{
	[Description("Leave empty to delete current user.")]
	public string? UserID { get; set; }
}

public class DeleteUserResponse : Response
{

}

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
	private readonly SignInManager<User> _signInManager;
	private readonly UserManager<User> _userManager;

	public UserController(SignInManager<User> signInManager, UserManager<User> userManager)
	{
		_signInManager = signInManager;
		_userManager = userManager;
	}

	[HttpPost]
	public async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request)
	{
		await _signInManager.SignOutAsync();

		if (request.UserID is not null)
		{
			var user = await _userManager.FindByIdAsync(request.UserID);

			if (user is null)
			{
				return new DeleteUserResponse().Throw(Errors.Global.IDNotFound.ToError(request.UserID));
			}

			var currentUser = await _userManager.GetUserAsync(User);
			if (currentUser == user || await _userManager.IsInRoleAsync(currentUser, "Admin"))
			{
				await _userManager.DeleteAsync(user);
			}
			else
			{
				return new DeleteUserResponse().Throw(Errors.Global.NotAllowed);
			}
		}
		else
		{
			var currentUser = await _userManager.GetUserAsync(User);
			await _userManager.DeleteAsync(currentUser);
		}

		return new DeleteUserResponse();
	}
}
