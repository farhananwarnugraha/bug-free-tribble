using System;
using HydraAPI.Shared;
using HydraAPI.Skills;
using HydraAPI.Skills.DTO;
using Microsoft.AspNetCore.Mvc;

namespace HydraAPI.Controllers;

[ApiController]
[Route("api/v1/")]
public class SkillController : ControllerBase
{
    private readonly SkillService _skillSrvice;

    public SkillController(SkillService skillSrvice)
    {
        _skillSrvice = skillSrvice;
    }

    [HttpGet("skills")]
    public IActionResult Get(){
        var skill = _skillSrvice.Get();
        var response = new ResponseDTO<List<SkillDTO>>(){
            status = 200,
            Message = "Success",
            Data = skill
        };
        return Ok(response);
    }
}
