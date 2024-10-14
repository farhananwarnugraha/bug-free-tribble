using System;
using HydraAPI.Auth.Users;
using HydraAPI.Auth.Users.DTO;
using HydraAPI.Shared;
using Microsoft.AspNetCore.Mvc;

namespace HydraAPI.Controllers;

[Route("api/v1")]
[ApiController]
public class UserController :ControllerBase
{
    private readonly UsersService _service;

    public UserController(UsersService service)
    {
        _service = service;
    }
    [HttpPost("register")]
    public IActionResult RegisterUser([FromBody] ResgisterDTO registerDTO){
        try
        {
            _service.Register(registerDTO);
            var response = new ResponseDTO<string>(){
                status = 201,
                Message = "Success",
                Data = "Register Berhasil"
            };
            return Ok(response); 
        }
        catch (Exception e)
        {
            var response = new ResponseDTO<string>(){
                status = 400,
                Message = "Failed",
                Data = e.Message
            };
            return BadRequest(response);
        }
    }

    [HttpPost("login")]
    public IActionResult LoginUser([FromBody] LoginDTO loginDTO){
        try
        {
            var token = _service.LoginUser(loginDTO);
            var response = new ResponseDTO<LoginResponseDTO>(){
                status = 200,
                Message = "Success",
                Data = token
            };
            return Ok(response);
        }
        catch (System.Exception e)
        {
            var response = new ResponseDTO<string>(){
                status = 400,
                Message = "Failed",
                Data = "Login Gagal, Periksa Kembali Username dan Password Anda " + e.Message 
            };
            return BadRequest(response);
        }
    }
}
