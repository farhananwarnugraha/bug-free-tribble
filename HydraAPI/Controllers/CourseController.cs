using HydraAPI.Courses;
using HydraAPI.Courses.DTO;
using HydraAPI.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HydraAPI.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    [Authorize(Roles = "TrainingManager")]
    public class CourseController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost("bootcampclass/course/{batchBootcamp}")]
        public IActionResult AddCourseBootcamp(int batchBootcamp, [FromBody] AddCourseBootcampDTO request){
            try{
                _courseService.Insert(batchBootcamp,request);
                var respose = new ResponseDTO<string>(){
                    status = 201,
                    Message = "Success",
                    Data = "Berhasil Menambahkan Data"
                };
                return Ok(respose);
            }
            catch{
                var respose = new ResponseDTO<string>(){
                    status = 400,
                    Message = "Failed",
                    Data = "Gagal Menambahkan Data"
                };
                return BadRequest(respose);
            }
        }
        [HttpGet("bootcampclass/course/schedule/{batchBootcamp}")]
        public IActionResult GetSchedule(int batchBootcamp){
            try{
                var schedule = _courseService.GetSchedule(batchBootcamp);
                var respose = new ResponseDTO<List<CourseScheduuleDTO>>(){
                    status = 200,
                    Message = "Success",
                    Data = schedule
                };
                return Ok(respose);
            }
            catch{
                var respose = new ResponseDTO<List<CourseScheduuleDTO>>(){
                    status = 400,
                    Message = "Failed",
                    Data = null
                };
                return BadRequest(respose);
            }
        }

        [HttpPut("bootcampclass/course/{courseId}")]
        public IActionResult UpdateCourse(string courseId){
            // courseId = courseId.Replace("%2F", "/");
            // try{
               _courseService.UpdateProgress(courseId);
                var respose = new ResponseDTO<string>(){
                    status = 200,
                    Message = "Success",
                    Data = "Berhasil Mengubah Data"
                };
                return Ok(respose);
            // }
            // catch{
            //     var respose = new ResponseDTO<string>(){
            //         status = 400,
            //         Message = "Failed",
            //         Data = "Gagal Mengubah Data"
            //     };
            //     return BadRequest(respose);
            // }
        }
    }
}
