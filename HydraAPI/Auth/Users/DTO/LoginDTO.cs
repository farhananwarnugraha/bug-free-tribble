using System;

namespace HydraAPI.Auth.Users;

public class LoginDTO
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
