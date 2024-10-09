using System;

namespace HydraAPI.Auth.Users.DTO;

public class LoginResponseDTO
{
    public string Username { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string Role { get; set; } = null!;
}
