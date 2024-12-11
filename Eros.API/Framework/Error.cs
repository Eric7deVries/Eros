using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;

namespace Eros.API.Framework;

public class Error
{
	public Error(string name)
	{
		var declaringType = MethodBase.GetCurrentMethod().DeclaringType;
		var parentClassName = declaringType.Name;
		var code = $"{parentClassName}.{name}";

		Code = code;
	}

	public Error(string name, string message) : this (name)
	{
		Message = message;
	}

	public string Message { get; set; }
	public string Code { get; set; }
}

public class Error<T> : Error
{
	public Error(string name, string messageTemplate)
		: base(name)
	{
		MessageTemplate = messageTemplate;
	}

	public string MessageTemplate { get; set; }

	public Error ToError (T parameter)
	{
		Message = string.Format(MessageTemplate, parameter);
		return this;
	}
}

public static class Errors
{
	public static class Global
	{
		//Only use this if no reason can be given.
		public static Error SomethingWentWrong = new Error(nameof(SomethingWentWrong), "An unexpected error occured.");
		public static Error EmailDoesntExist = new Error(nameof(EmailDoesntExist), "Couldn't find a user with this Emailadress");
		public static Error<string> IDNotFound = new Error<string>(nameof(IDNotFound), "Couldn't find item with ID: {0}");
		public static Error NotAllowed = new Error(nameof(NotAllowed), "You are not authorized to do this action.");
	}
}