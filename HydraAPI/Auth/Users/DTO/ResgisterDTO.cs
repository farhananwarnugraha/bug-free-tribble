using System;

namespace HydraAPI.Auth.Users.DTO;

public class ResgisterDTO
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public List<int> RoleIds { get; set; } = null!;
}
