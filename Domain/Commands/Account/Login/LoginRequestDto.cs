﻿namespace Domain.Commands.Account.Login
{
    public sealed class LoginRequestDto
    {
        public string Email { get; set;  } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}