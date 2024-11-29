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
            _ecService.Insert(dto);
            return Ok("Berhasil");
        }
        catch (System.Exception)
        {
            
            return BadRequest("Gagal");
        }
    }
}
