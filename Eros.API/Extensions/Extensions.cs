using Eros.API.Framework;
using Microsoft.AspNetCore.Identity;

namespace Eros.API.Extensions;

public static class Extensions
{
	public static Error ToError (this IdentityError? error)
	{
		if (error is null)
		{
			return Errors.Global.SomethingWentWrong;
		}

		return new Error(error.Code, error.Description);
	}
}
