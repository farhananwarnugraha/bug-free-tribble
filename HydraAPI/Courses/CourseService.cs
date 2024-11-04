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
            Id = "BC/"+detailBootcampActive.Id.ToString("000")+"/"+request.SkillId,
            BootcampClassId = detailBootcampActive.Id,
            TrainerId = request.TrainerId,
            SkillId = request.SkillId,
            StartDate = request.StartDate??DateTime.Now,
            EndDate = request.EndDate??DateTime.Now.AddMonths(1),
            Progress = 1
        };
        _courseRepository.Add(course);
    }

    public List<CourseScheduuleDTO> GetSchedule(int bootcampId){
        return _courseRepository.GetSchedule(bootcampId)
            .Select(
                schedule => new CourseScheduuleDTO{
                    courseId = schedule.Id,
                    MateriBootcamp = schedule.TrainerSkillDetail.Skill.Name,
                    TrainerName = schedule.TrainerSkillDetail.Trainer.FirstName + " " + schedule.TrainerSkillDetail.Trainer.LastName,
                    StartDate = schedule.StartDate.ToString("dd/MM/yyyy"),
                    EndDate = schedule.EndDate.ToString("dd/MM/yyyy"),
                    Status = schedule.Progress.ToString() == 1.ToString() ? "Planed" : 
                            schedule.Progress.ToString() == 2.ToString() ? "Active" : 
                            schedule.Progress.ToString() == 3.ToString() ? "Completed" : "Cancelled",
                    EvaluationDate = schedule.EvaluationDate?.ToString("dd/MM/yyyy")
                }
            ).ToList();
    }
}
