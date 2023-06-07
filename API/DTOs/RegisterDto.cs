using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis.Elfie.Model.Strings;

namespace API.DTOs;

public class RegisterDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    [StringLength(8, MinimumLength = 4)]
    public string Password { get; set; }
}