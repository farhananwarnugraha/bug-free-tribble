using System;
using HydraAPI.EvaluationCandidate;
using HydraAPI.EvaluationCandidate.DTO;
using HydraAPI.Shared;
using HydraAPI.Shared.Enum;
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
    public IActionResult GetEvaluationResult(int pageNumber = (int)Pagination.PAGE_NUMBER, int pageSize = (int)Pagination.PAGE_SIZE, string fullName = " "){
        var evaluationData = _ecService.GetEvaluation(pageNumber, pageSize, fullName);
        var response = new ResponseDTO<EvaluationCandidateResponseDTO>(){
            Data = evaluationData,
            Message = "Success",
            status = 200
        };
        return Ok(response);
    }
}
