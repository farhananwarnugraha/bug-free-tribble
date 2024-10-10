using HydraAPI.Candidates;
using HydraAPI.Candidates.DTO;
using HydraAPI.Shared;
using HydraAPI.Shared.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HydraAPI.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    [Authorize(Roles = "Training Manager")]
    public class CandidateController : ControllerBase
    {
        private readonly CandidateService _candidateService;

        public CandidateController(CandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet("allcandidate")]
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
        [HttpGet("candidate")]
        public IActionResult Get(int pageNumber = (int)Pagination.PAGE_NUMBER, int pageSize = (int) Pagination.PAGE_SIZE, string fullName="", int batchBootacamp=0){
            var allCandidate = _candidateService.GetCandidate(pageNumber, pageSize, fullName, batchBootacamp);
            var response = new ResponseDTO<CandidateIndexDTO>()
            {
                status = 200,
                Message = "Berhasil",
                Data = allCandidate
            };
            return Ok(response);
        }
        [HttpPost("candidate")]
        public IActionResult Insert(CandidateReqDTO request){
            try
            {
                _candidateService.Insert(request);
                var response = new ResponseDTO<string>()
                {
                    status = 200,
                    Message = "Success",
                    Data = "Berhasil Menambahkan Dta"
                };
                return Ok(response);
            }
            catch (Exception)
            {
                var response = new ResponseDTO<string>()
                {
                    status = 400,
                    Message = "Failed",
                    Data = "Gagal Menambahkan Data"
                };
                return BadRequest(response);
            }
        }
        [HttpGet("candidate/{candidateId}")]
        public IActionResult Get(int candidateId){
            try
            {
                var candidate = _candidateService.GetById(candidateId);
                var response = new ResponseDTO<CandidateUpdateDTO>()
                {
                    status = 200,
                    Message = "Berhasil",
                    Data = candidate
                };
                return Ok(response);
            }
            catch (Exception)
            {
                var response = new ResponseDTO<string>()
                {
                    status = 500,
                    Message = "Gagal",
                    Data = null
                };
                return BadRequest(response);
            }
        }

        [HttpPut("candidate")]
        public IActionResult Update(CandidateUpdateDTO request){
            try
            {
                _candidateService.Update(request);
                var response = new ResponseDTO<string>()
                {
                    status = 200,
                    Message = "Success",
                    Data = "Berhasil Mengubah Data"
                };
                return Ok(response);
            }
            catch (Exception)
            {
                var response = new ResponseDTO<string>()
                {
                    status = 400,
                    Message = "Failed",
                    Data = "Gagal Mengubah Data"
                };
                return BadRequest(response);
            }
        }
    }
}