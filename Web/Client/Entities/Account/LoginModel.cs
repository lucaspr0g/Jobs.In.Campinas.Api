﻿using System.ComponentModel.DataAnnotations;

namespace Web.Client.Entities.Account
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
