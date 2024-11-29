using System;
using HydraAPI.Bootcamp;
using HydraAPI.Bootcamp.DTO;
using HydraAPI.Models;
using HydraAPI.Shared;
using HydraAPI.Shared.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HydraAPI.Controllers;

[Route("api/v1")]
[ApiController]
public class BootcamclassController : ControllerBase
{
    private readonly BootcampService _bcService;

    public BootcamclassController(BootcampService bcService)
    {
        _bcService = bcService;
    }

    [HttpGet("bootcampAll")]
    [Authorize(Roles = "TrainingManager,Administrator,Recruiter")]
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
    [Authorize(Roles = "Administrator,Recruiter,TrainingManager")]
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
    [Authorize(Roles = "Administrator,Recruiter")]
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

    [HttpPost("bootcampclass")]
    [Authorize(Roles = "Administrator,Recruiter")]
    public IActionResult Insert([FromBody]BootcampInsertDTO request){
        try{
            _bcService.Insert(request);
            var response = new ResponseDTO<string>(){
                status = 200,
                Message = "Success",
                Data = "Berhasil Menambahkan Data"
            };
            return Ok(response);
        }
        catch (System.Exception){
            var response = new ResponseDTO<string>(){
                status = 400,
                Message = "Failed",
                Data = "Gagal Menambahkan Data"
            };
            return BadRequest(response);
        }
    } 
    [HttpPut("bootcamp")]
    [Authorize(Roles = "Administrator,Recruiter")]
    public IActionResult Update( [FromBody]BootcampUpdateDTO request){
        try{
            _bcService.Update(request);
            var response = new ResponseDTO<string>(){
                status = 200,
                Message = "Success",
                Data = "Bootcamp " + request.Description + " Bersil di ubah"
            };
            return Ok(response);
        }
        catch(System.Exception){
            return BadRequest(new ResponseDTO<string>(){
                status = 400,
                Message = "Failed",
                Data = "Gagal melakukan update data"
            });
        }
    }
    [HttpGet("bootcamp/planed")]
    [Authorize(Roles ="TrainingManager")]
    public IActionResult GetPlanedBootcamp(int pageNumber = (int)Pagination.PAGE_NUMBER, int pageSize = (int)Pagination.PAGE_SIZE, int batchBootcamp = 0, string bootcampName = ""){
        try
        {
            var bootcamp = _bcService.GetBootcampPlaned(pageNumber, pageSize, batchBootcamp, bootcampName);
            var response = new ResponseDTO<BootcampPlanedDTO>(){
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

    [HttpGet("bootcamp/planedall")]
    [Authorize(Roles ="Adminstration, Recruiter")]
    public IActionResult GetBootcampPlanedAll(){
        try{
            var bcp = _bcService.GetBootcampPlanned();
            var response = new ResponseDTO<List<BootcampDTO>>{
                status = 200,
                Message = "Success",
                Data = bcp
            };
            return Ok(response);
        }
        catch (System.Exception){
            var response = new ResponseDTO<string>{
                status = 400,
                Message = "Failed",
                Data = null

            };
            return BadRequest(response);
        }
    }
    [HttpGet("bootcamp/active")]
    [Authorize(Roles ="TrainingManager")]
    public IActionResult GetActiveBootcamp(int pageNumber = (int)Pagination.PAGE_NUMBER, int pageSize = (int)Pagination.PAGE_SIZE, int batchBootcamp = 0, string bootcampName = ""){
        try
        {
            var bootcamp = _bcService.GetBootcampActive(pageNumber, pageSize, batchBootcamp, bootcampName);
            var response = new ResponseDTO<BootcampPlanedDTO>(){
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
    [HttpGet("bootcamp/completed")]
    [Authorize(Roles ="TrainingManager")]
    public IActionResult GetCompletedBootcamp(int pageNumber = (int)Pagination.PAGE_NUMBER, int pageSize = (int)Pagination.PAGE_SIZE, int batchBootcamp = 0, string bootcampName = ""){
        try
        {
            var bootcamp = _bcService.GetBootcampCompleted(pageNumber, pageSize, batchBootcamp, bootcampName);
            var response = new ResponseDTO<BootcampPlanedDTO>(){
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
    [HttpGet("bootcamp/{bootcampId}/detail")]
    [Authorize(Roles ="TrainingManager")]
    public IActionResult GetDetailBootcamp(int bootcampId){
        try
        {
            var bootcamp = _bcService.GetBootcampActiveDetile(bootcampId);
            var response = new ResponseDTO<BootcampActiveDetileDTO>(){
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
                Data = "Bootcamp still has running classes or planned classes"
            };
            return BadRequest(response);
        }
    }
    [HttpGet("bootcamp/active/{batchBootcamp}")]
    [Authorize(Roles ="TrainingManager")]
    public IActionResult GetAvtiveBootcamp(int batchBootcamp){
        try{
            var bootcamp = _bcService.GetScheduleAciveDetail(batchBootcamp);
            var response = new ResponseDTO<BootcampActiveDetileDTO>(){
                status = 200,
                Message = "Success",
                Data = bootcamp
            };
            return Ok(response);
        }catch(Exception){
            var response = new ResponseDTO<string>(){
                status = 400,
                Message = "Failed",
                Data = null
            };
            return BadRequest(response);
        }
    }
    [HttpPut("bootcamp/end/{id}")]
    [Authorize(Roles ="TrainingManager")]
    public IActionResult EndBootcamp(int id, EndBootcampDTO endBootcamp){
        try{
            _bcService.EndBootcamp(id, endBootcamp);
            var response = new ResponseDTO<string>(){
                status = 200,
                Message = "Success",
                Data = "Bootcamp Berhasil diakhiri"
            };
            return Ok(response);
        }
        catch(Exception){
            var response = new ResponseDTO<string>(){
                status = 400,
                Message = "Failed",
                Data = "Gagal Mengubah Data"
            };
            return BadRequest(response);
        }
    }

    [HttpPut("bootcamp/activate/{id}")]
    [Authorize(Roles ="TrainingManager")]
    public IActionResult ActivateBootcamp(int id){
        try{
            _bcService.ActivedBootcamp(id);
            var response = new ResponseDTO<string>(){
                status = 200,
                Message = "Success",
                Data = "Bootcamp Berhasil diaktifkan"
            };
            return Ok(response);
        }
        catch(Exception){
            var response = new ResponseDTO<string>(){
                status = 400,
                Message = "Failed",
                Data = "Gagal Mengaktifkan Bootcamp"
            };
            return BadRequest(response);
        }
    }

    [HttpPut("bootcamp/deactivate/{id}")]
    [Authorize(Roles ="TrainingManager")]
    public IActionResult DeadActivedBootcamp(int id){
        try{
            _bcService.DeadActivedBootcamp(id);
            var response = new ResponseDTO<string>(){
                status = 200,
                Message = "Success",
                Data = "Bootcamp Berhasil dinonaktifkan"
            };
            return Ok(response);
        }
        catch(Exception){
            var response = new ResponseDTO<string>(){
                status = 400,
                Message = "Failed",
                Data = "Gagal Menonaktifkan Bootcamp"
            };
            return BadRequest(response);
        }   
    }
}