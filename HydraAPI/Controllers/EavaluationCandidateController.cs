using System;
using HydraAPI.EvaluationCandidate;
using HydraAPI.EvaluationCandidate.DTO;
using HydraAPI.Shared;
using HydraAPI.Shared.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HydraAPI.Controllers;
[Route("api/v1")]
[ApiController]
public class EavaluationCandidateController : ControllerBase
{
    private readonly EvaluationCandidateService _ecService;

    public EavaluationCandidateController(EvaluationCandidateService ecService)
    {
        _ecService = ecService;
    }

    [HttpGet("evaluation-result")]
    [Authorize(Roles = "TrainingManager")]
    public IActionResult GetEvaluationResult(int pageNumber = (int)Pagination.PAGE_NUMBER, int pageSize = (int)Pagination.PAGE_SIZE, string fullName = " "){
        var evaluationData = _ecService.GetEvaluation(pageNumber, pageSize, fullName);
        var response = new ResponseDTO<EvaluationCandidateResponseDTO>(){
            Data = evaluationData,
            Message = "Success",
            status = 200
        };
        return Ok(response);
    }

    [HttpPost("evaluation-result")]
    [Authorize(Roles = "TrainingManager")]
    public IActionResult Insert(AddEvaliationCandidateDTO dto){
        try
        {
            _ecService.Inserts(dto);
            return Ok("Berhasil");
        }
        catch (System.Exception)
        {
            
            return BadRequest("Gagal");
        }
    }
    [HttpPost("add-evaluation")]
    [Authorize(Roles = "TrainingManager, Trainer")]
    public IActionResult AddEvaluation(EvaluationCandidateReqDTO dto){
        try
        {
            _ecService.Insert(dto);
            var response = new ResponseDTO<string>(){
                status = 200,
                Message = "Success",
                Data = "Berhasil Menambahkan Data"
            };
            return Ok(response);
        }
        catch (System.Exception)
        {
            var response = new ResponseDTO<string>(){
                status = 400,
                Message = "Failed",
                Data = "Gagal Menambahkan Data"
            };
            return BadRequest(response);
        }
    }
}
