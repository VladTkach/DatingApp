﻿using System.ComponentModel.DataAnnotations;

namespace DatingApp.Common.DTO.User;

public class RegisterDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}