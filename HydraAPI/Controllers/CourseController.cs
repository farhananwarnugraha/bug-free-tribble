using HydraAPI.Courses;
using HydraAPI.Courses.DTO;
using HydraAPI.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HydraAPI.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost("/bootcampclass/course/{batchBootcamp}")]
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
    }
}
