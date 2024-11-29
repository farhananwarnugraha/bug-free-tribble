using System;
using HydraAPI.Shared;
using HydraAPI.Trainers;
using HydraAPI.Trainers.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HydraAPI.Controllers;

[ApiController]
[Route("api/v1")]
public class TrainerController : ControllerBase
{
    private readonly TrainerService _trainerService;

    public TrainerController(TrainerService trainerService)
    {
        _trainerService = trainerService;
    }

    [HttpGet("trainer")]
    public IActionResult Get(){
        try
        {
            var trainer = _trainerService.Get();
            var response = new ResponseDTO<List<TrainerDTO>>(){
                status = 200,
                Message = "Success",
                Data = trainer
            };
            return Ok(response);
        }
        catch (System.Exception)
        {
            var response = new ResponseDTO<string>(){
                status = 400,
                Message = "Failed",
                Data = "Trainer Not Fpund"
            };
            return BadRequest(response);
        }
    }

    [HttpGet("trainer/{skillId}")]
    public IActionResult GetTrainerBySkill(string skillId){
        try
        {
            var trainer = _trainerService.GetTrainerBySkill(skillId);
            var response = new ResponseDTO<List<TrainerDTO>>(){
                status = 200,
                Message = "Success",
                Data = trainer
            };
            return Ok(response);
        }
        catch (System.Exception)
        {
            var response = new ResponseDTO<string>(){
                status = 400,
                Message = "Failed",
                Data = "Trainer Not Fpund"
            };
            return BadRequest(response);
        }
    }
}
