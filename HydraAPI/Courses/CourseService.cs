using System;
using HydraAPI.Courses.DTO;
using HydraAPI.Interfaces;
using HydraAPI.Models;

namespace HydraAPI.Courses;

public class CourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IBootcampClass _bootcampClassRepository;

    public CourseService(ICourseRepository courseRepository, IBootcampClass bootcampClassRepository)
    {
        _courseRepository = courseRepository;
        _bootcampClassRepository = bootcampClassRepository;
    }

    public void Insert(int bootcampId, AddCourseBootcampDTO request){
        var detailBootcampActive = _bootcampClassRepository.Get(bootcampId);
        var course = new Course{
            Id = "BC/00"+detailBootcampActive.Id.ToString()+"/"+request.SkillId,
            BootcampClassId = detailBootcampActive.Id,
            TrainerId = request.TrainerId,
            SkillId = request.SkillId,
            StartDate = request.StartDate??DateTime.Now,
            EndDate = request.EndDate??DateTime.Now.AddMonths(1),
            Progress = 1
        };
        _courseRepository.Add(course);
    }
}
