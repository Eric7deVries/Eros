using Eros.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Eros.API;
public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
		builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
			options.UseSqlServer(connectionString));

		builder.Services.AddCors(options =>
		{
			options.AddPolicy("AllowSwaggerUI", builder =>
			{
				builder.AllowAnyMethod()
					   .AllowAnyHeader();
			});
		});

		builder.Services.AddControllers();

		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
		builder.Services.AddOpenApi();

		builder.Services.AddAuthorization();

		builder.Services.AddIdentityApiEndpoints<User>()
			.AddEntityFrameworkStores<ApplicationDbContext>();

		builder.Services.Configure<IdentityOptions>(options =>
		{
			options.User.RequireUniqueEmail = true;
		});

		var app = builder.Build();

		app.MapIdentityApiFilterable<User>(new Overrides.IdentityApiEndpointRouteBuilderOptions
		{
			UseConfirmEmail = true,
			Use2fa = true,
			UseForgotPassword = true,
			UseInfo = false,
			UseLogin = true,
			UseRefresh = true,
			UseRegister = true,
			UseResendConfirmationEmail = true,
			UseResetPassword = true
		});

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.MapOpenApi();
			app.UseSwagger();
			app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
			{
				options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
				options.RoutePrefix = string.Empty;
			});
		}

		app.MapSwagger().RequireAuthorization();

		app.UseCors("AllowSwaggerUI");

		app.UseRouting();
		app.UseAuthorization();
		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers();
		});

		app.UseHttpsRedirection();

		app.MapControllers();

		using (IServiceScope scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
		{
			scope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
		}

		app.Run();
	}
}
