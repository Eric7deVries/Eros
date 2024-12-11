using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Eros.API.Models;

public class User : IdentityUser
{
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
}
