namespace Eros.API.Overrides;

public class IdentityApiEndpointRouteBuilderOptions
{
	public bool UseRegister {  get; set; }
	public bool UseLogin { get; set; }
	public bool UseRefresh { get; set; }
	public bool UseConfirmEmail { get; set; }
	public bool UseResendConfirmationEmail { get; set; }
	public bool UseForgotPassword { get; set; }
	public bool UseResetPassword { get; set; }
	public bool Use2fa { get; set; }
	public bool UseInfo { get; set; }
}
