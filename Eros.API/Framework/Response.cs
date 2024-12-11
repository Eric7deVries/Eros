using System.Reflection;

namespace Eros.API.Framework;

public abstract class Response
{
	public Error Error { get; set; }
}

public abstract class ModelResponse
{
	public string ID { get; set; }
}

public static class ResponseExtensions
{
	public static T Throw<T>(this T response, Error error) where T : Response, new()
	{
		return new T
		{
			Error = error
		};
	}
}