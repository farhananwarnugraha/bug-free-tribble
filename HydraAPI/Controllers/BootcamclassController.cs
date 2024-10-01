using System;
using HydraAPI.Bootcamp;
using HydraAPI.Bootcamp.DTO;
using HydraAPI.Shared;
using HydraAPI.Shared.Enum;
using Microsoft.AspNetCore.Mvc;

namespace HydraAPI.Controllers;

[Route("api/v1")]
public class BootcamclassController : ControllerBase
{
    private readonly BootcampService _bcService;

    public BootcamclassController(BootcampService bcService)
    {
        _bcService = bcService;
    }

    [HttpGet("bootcampAll")]
    public IActionResult Get(){
        try
        {
            var bootcamp = _bcService.Get();
            var response = new ResponseDTO<List<BootcampDTO>>(){
                status = 200,
                Message = "Success",
                Data = bootcamp
            };
            return Ok(response);
        }
        catch (System.Exception)
        {
            var response = new ResponseDTO<string>(){
                status = 400,
                Message = "Failed",
                Data = null
            };
            return BadRequest(response);
        }
    }

    [HttpGet("bootcamp")]
    public IActionResult Get(int pageNumber = (int)Pagination.PAGE_NUMBER, int pageSize = (int)Pagination.PAGE_SIZE, int batchBootcamp = 0, string bootcampName = ""){
        try
        {
            var bootcamp = _bcService.Get(pageNumber, pageSize, batchBootcamp, bootcampName);
            var response = new ResponseDTO<BootcampPageDTO>(){
                status = 200,
                Message = "Success",
                Data = bootcamp
            };
            return Ok(response);
        }
        catch (System.Exception)
        {
            var response = new ResponseDTO<string>(){
                status = 400,
                Message = "Failed",
                Data = null
            };
            return BadRequest(response);
        }
    }
    [HttpGet("bootcamp/{bootcampId}")]
    public IActionResult Get(int bootcampId){
        try
        {
            var bootcamp = _bcService.Get(bootcampId);
            var response = new ResponseDTO<BootcampDTO>(){
                status = 200,
                Message = "Success",
                Data = bootcamp
            };
            return Ok(response);
        }
        catch (System.Exception)
        {
            var response = new ResponseDTO<string>(){
                status = 400,
                Message = "Failed",
                Data = null
            };
            return BadRequest(response);
        }
    }
}
