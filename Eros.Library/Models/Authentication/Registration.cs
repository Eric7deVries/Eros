namespace Eros.Library.Models.Authentication;

public class Registration : DataModel
{
	public string Name { get; set; } = string.Empty;
	public string EmailAddress { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
}
