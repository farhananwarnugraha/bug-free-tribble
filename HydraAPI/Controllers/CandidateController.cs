using HydraAPI.Candidates;
using HydraAPI.Candidates.DTO;
using HydraAPI.Shared;
using Microsoft.AspNetCore.Mvc;

namespace HydraAPI.Controllers
{
    [Route("api/v1/candidate")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly CandidateService _candidateService;

        public CandidateController(CandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var allCandidate = _candidateService.Get();
                var response = new ResponseDTO<List<CandidateDTO>>()
                {
                    status = 200,
                    Message = "Berhasil",
                    Data = allCandidate
                };
                return Ok(response);
            }
            catch (Exception)
            {
                var response = new ResponseDTO<List<string>>()
                {
                    status = 500,
                    Message = "Gagal",
                };
                return BadRequest(response);
            }
            
        }
    }
}
