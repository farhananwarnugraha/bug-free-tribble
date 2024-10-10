using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HydraAPI.Auth.Users.DTO;
using HydraAPI.Interfaces;
using HydraAPI.Models;
using Isopoh.Cryptography.Argon2;
using Microsoft.IdentityModel.Tokens;

namespace HydraAPI.Auth.Users;

public class UsersService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IConfiguration _configuration;

    public UsersService(IUserRepository userRepository, IRoleRepository roleRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _configuration = configuration;
    }

    // Register User
    public void Register(ResgisterDTO model)
    {
        List<Role> roles = new List<Role>();
        foreach (var roleId in model.RoleIds){
            var role = _roleRepository.GetRoleById(roleId);
            if(role !=null ){
                roles.Add(role);
            }
        }
        var user = new User(){
            Username = model.Username,
            Password = Argon2.Hash(model.Password),
            Email = model.Email,
            Roles = roles
        };
        _userRepository.Register(user);
    }

    //Login
    public LoginResponseDTO LoginUser(LoginDTO request){
        var userAcc = _userRepository.GetByUsername(request.Username) ?? throw new Exception("User Not Found");
        var isPasswoodValid = Argon2.Verify(userAcc.Password, request.Password);
        if(isPasswoodValid){
            return new LoginResponseDTO(){
                Username = request.Username,
                Token = CreateToken(userAcc),
                Role = _userRepository.GetRoleName(request.Username)
                
            };
        }else{
            throw new Exception("Username And Password incorect");
        }
    } 

    //create token
    public string CreateToken(User user){
        // Claims
        List<Claim> claims = new List<Claim>(){
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Role, _userRepository.GetRoleName(user.Username))
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSetting:Token").Value ?? "Not Found"
            )
        );

        var  credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credential
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
