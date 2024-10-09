using System;
using HydraAPI.Auth.Users;
using HydraAPI.Auth.Users.DTO;
using HydraAPI.Shared;
using Microsoft.AspNetCore.Mvc;

namespace HydraAPI.Controllers;

[Route("api/v1/auth")]
[ApiController]
public class UserController :ControllerBase
{
    private readonly UsersService _service;

    public UserController(UsersService service)
    {
        _service = service;
    }
    [HttpPost("/register")]
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

    // public IActionResult 
}
