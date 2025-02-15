﻿using HydraAPI.Candidates;
using HydraAPI.Candidates.DTO;
using HydraAPI.Shared;
using HydraAPI.Shared.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HydraAPI.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    [Authorize(Roles = "TrainingManager,Recruiter,Administrator")]
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
            if(request.BootcampClass == 2 || request.BootcampClass == 3){
                var response = new ResponseDTO<string>()
                {
                    status = 400,
                    Message = "Failed",
                    Data = "Bootcamp Sedang Berjalan atau sudah selesai"
                };
                return BadRequest(response);
                
            }else{
                _candidateService.Insert(request);
                var response = new ResponseDTO<string>()
                {
                    status = 200,
                    Message = "Success",
                    Data = "Berhasil Menambahkan Data"
                };
                return Ok(response);
            }
            // try
            // {
            //     _candidateService.Insert(request);
            //     var response = new ResponseDTO<string>()
            //     {
            //         status = 200,
            //         Message = "Success",
            //         Data = "Berhasil Menambahkan Dta"
            //     };
            //     return Ok(response);
            // }
            // catch (Exception)
            // {
            //     var response = new ResponseDTO<string>()
            //     {
            //         status = 400,
            //         Message = "Failed",
            //         Data = "Gagal Menambahkan Data"
            //     };
            //     return BadRequest(response);
            // }
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

        [HttpGet("candidates/{courseId}/{bootcampId}")]
        [Authorize(Roles = "TrainingManager")]
        public IActionResult GetCandidate(string courseId, int bootcampId){
            try
            {
                var candidate = _candidateService.GetCandidates(courseId, bootcampId);
                var response = new ResponseDTO<List<CandidateByCourseBootcamp>>()
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
    }
}