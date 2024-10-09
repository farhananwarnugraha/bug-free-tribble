using System;
using System.Globalization;
using HydraAPI.Bootcamp.DTO;
using HydraAPI.Interfaces;
using HydraAPI.Models;
using HydraAPI.Shared;
using HydraAPI.Shared.Enum;

namespace HydraAPI.Bootcamp;

public class BootcampService
{
    private readonly IBootcampClass _bootcampClassRepository;
    private readonly ICourseRepository _courseRepository;

    public BootcampService(IBootcampClass bootcampClassRepository, ICourseRepository courseRepository)
    {
        _bootcampClassRepository = bootcampClassRepository;
        _courseRepository = courseRepository;
    }

    public List<BootcampDTO> Get() => 
        _bootcampClassRepository.Get()
        .Select(
            static bootcamp => new BootcampDTO(){
                BootcampId = bootcamp.Id,
                Description = bootcamp.Description??"Not Available",
                StartDate = bootcamp.StartDate.ToString("yyyy-MM-dd"),
                EndDate = bootcamp.EndDate?.ToString("yyyy-MM-dd"),
            }
        ).ToList();

    public BootcampPageDTO Get(int pageNumber, int pageSize, int batchBootcamp, string bootcampName){
        var model = _bootcampClassRepository.Get(pageNumber, pageSize, batchBootcamp, bootcampName)
            .Select(
                bootcamp => new BootcampDTO(){
                 BootcampId = bootcamp.Id,
                 Description = bootcamp.Description??"Not Set",
                 StartDate = bootcamp.StartDate.ToString("yyyy-MM-dd"),
                 EndDate = bootcamp.EndDate?.ToString("yyyy-MM-dd"),
                }
            );
        return new BootcampPageDTO(){
            BootcampClasses = model.ToList(),
            Pagination = new PaginationDTO(){
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRows = _bootcampClassRepository.Count(batchBootcamp, bootcampName)
            }
        };
    }

    public BootcampDTO Get(int id){
        var model = _bootcampClassRepository.Get(id);
        return new BootcampDTO(){
            BootcampId = model.Id,
            Description = model.Description??"Not Set",
            StartDate = model.StartDate.ToString("yyyy-MM-dd"),
            EndDate = model.EndDate?.ToString("yyyy-MM-dd"),
        };
    }

    public void Insert(BootcampInsertDTO request) => 
        _bootcampClassRepository.Insert(new BootcampClass{
          Description = request.Description,
          StartDate = request.StartDate,
          EndDate = request.EndDate,
          Progress = 1,
        });

    public void Update(BootcampUpdateDTO request) => 
        _bootcampClassRepository.Update(new BootcampClass{
            Id = request.BootcampId,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        });

    public BootcampPlanedDTO GetBootcampPlaned(int pageNumber, int pageSize, int batchBootcamp, string bootcampName){
        var model = _bootcampClassRepository.BetBootcampPlaned(pageNumber, pageSize, batchBootcamp, bootcampName)
            .Select(
                 bootcamp => new BootcampDTO(){
                 BootcampId = bootcamp.Id,
                 Description = bootcamp.Description??"Not Set",
                 StartDate = bootcamp.StartDate.ToString("yyyy-MM-dd"),
                 EndDate = bootcamp.EndDate?.ToString("yyyy-MM-dd"),
                 TotalCandidates = bootcamp.Candidates.Count(),
                }
            ) ;
        return new BootcampPlanedDTO(){
            BootcampsData = model.ToList(),
            Pagination = new PaginationDTO(){
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRows = _bootcampClassRepository.Count(batchBootcamp, bootcampName)
            }
        };
    }

    public BootcampPlanedDTO GetBootcampActive(int pageNumber, int pageSize, int batchBootcamp, string bootcampName){
        var model = _bootcampClassRepository.GetBootcampActive(pageNumber, pageSize, batchBootcamp, bootcampName)
            .Select(
                 bootcamp => new BootcampDTO(){
                 BootcampId = bootcamp.Id,
                 Description = bootcamp.Description??"Not Set",
                 StartDate = bootcamp.StartDate.ToString("yyyy-MM-dd"),
                 EndDate = bootcamp.EndDate?.ToString("yyyy-MM-dd"),
                 TrainerName = bootcamp.Courses.Select(
                    course => course.TrainerSkillDetail.Trainer.FirstName + " " + course.TrainerSkillDetail.Trainer.LastName).FirstOrDefault()??"Not Set",
                 CourseName = bootcamp.Courses.Select(
                    course => course.TrainerSkillDetail.Skill.Name).FirstOrDefault()??"Not Set",               
                }
            );
        return new BootcampPlanedDTO(){
            BootcampsData = model.ToList(),
            Pagination = new PaginationDTO(){
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRows = _bootcampClassRepository.Count(batchBootcamp, bootcampName)
            }
        };
    }

    public BootcampPlanedDTO GetBootcampCompleted(int pageNumber, int pageSize, int batchBootcamp, string bootcampName){
        var model = _bootcampClassRepository.GetBootcampCompleted(pageNumber, pageSize, batchBootcamp, bootcampName)
            .Select(
                 bootcamp => new BootcampDTO(){
                 BootcampId = bootcamp.Id,
                 Description = bootcamp.Description??"Not Set",
                 StartDate = bootcamp.StartDate.ToString("yyyy-MM-dd"),
                 EndDate = bootcamp.EndDate?.ToString("yyyy-MM-dd"),
                 TotalCandidates = bootcamp.Candidates.Count(),
                }
            ) ;
        return new BootcampPlanedDTO(){
            BootcampsData = model.ToList(),
            Pagination = new PaginationDTO(){
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRows = _bootcampClassRepository.Count(batchBootcamp, bootcampName)
            }
        };
    }

    public BootcampActiveDetileDTO GetBootcampActiveDetile(int id){
        var model = _bootcampClassRepository.GetDetailBootcamp(id);
        return new BootcampActiveDetileDTO(){
            BootcampId = model.Id,
            TrainerId = model.Courses.Select(
                trainer => trainer.TrainerSkillDetail.Trainer.Id).FirstOrDefault(),
            SkillId = model.Courses.Select(
                course => course.TrainerSkillDetail.Skill.Id).FirstOrDefault()??"Not Set",
            StartDate = model.StartDate,
            EndDate = model.EndDate,
        };
    }

    public BootcampActiveDetileDTO GetScheduleAciveDetail(int batchBootcamp){
        bool isActive = false;
        var model = _courseRepository.GetSchedule(batchBootcamp);
        foreach (var course in model){
            if(course.Progress == 2 || course.Progress==1){
                isActive = true;
            };
        }
        if(isActive){
            var bootcamp = _bootcampClassRepository.Get(batchBootcamp);
            return new BootcampActiveDetileDTO(){
                BootcampId = bootcamp.Id,
                EndDate = bootcamp.EndDate,
            };
        }
        else{
            return new BootcampActiveDetileDTO(){
                EndDate = null
            };
        }
    }

    public string EndBootcamp(int id, BootcampUpdateDTO bootcampUpdateDTO){
        bool isActive = false;
        var model = _courseRepository.GetSchedule(id);
        foreach (var course in model){
            if(course.Progress == 2 || course.Progress==1){
                isActive = true;
            };
        }
        if(isActive){
            return "Bootcamp Already Active";
        }else{
            // selesai
            var bootcamp = _bootcampClassRepository.Get(id);
            bootcamp.EndDate = bootcampUpdateDTO.EndDate;
            bootcamp.Progress = 3;
            _bootcampClassRepository.Update(bootcamp);
            return "Success";
        }
    }
}
