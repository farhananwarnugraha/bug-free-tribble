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
                TotalRows = _bootcampClassRepository.CountBootcampPlaned(batchBootcamp, bootcampName)
            }
        };
    }

    public List<BootcampDTO> GetBootcampPlanned() =>
        _bootcampClassRepository.GetBootcampPlaned()
        .Select(
            bcPlanned => new BootcampDTO(){
                BootcampId = bcPlanned.Id,
                Description = bcPlanned.Description??"Not Set",
                StartDate = bcPlanned.StartDate.ToString("yyyy-MM-dd"),
                EndDate = bcPlanned.EndDate?.ToString("yyyy-MM-dd"),
            }
        ).ToList();

    public BootcampPlanedDTO GetBootcampActive(int pageNumber, int pageSize, int batchBootcamp, string bootcampName){
        var model = _bootcampClassRepository.GetBootcampActive(pageNumber, pageSize, batchBootcamp, bootcampName)
            .Select(
                 bootcamp => new BootcampDTO(){
                 BootcampId = bootcamp.Id,
                 Description = bootcamp.Description??"Not Set",
                 StartDate = bootcamp.StartDate.ToString("yyyy-MM-dd"),
                 EndDate = bootcamp.EndDate?.ToString("yyyy-MM-dd"),
                 TrainerName = bootcamp.Courses.Select(C => C.Progress == 2).FirstOrDefault() ? 
                    bootcamp.Courses.Select(C => C.TrainerSkillDetail.Trainer.FirstName + " " + C.TrainerSkillDetail.Trainer.LastName ).FirstOrDefault() + " (Active) " : 
                    bootcamp.Courses.Select(C => C.Progress == 3).FirstOrDefault() ? 
                    "Last Trainer By " + bootcamp.Courses.Select(C => C.TrainerSkillDetail.Trainer.FirstName + " " + C.TrainerSkillDetail.Trainer.LastName ).FirstOrDefault() :  bootcamp.Courses.Select(C => C.TrainerSkillDetail.Trainer.FirstName + " " + C.TrainerSkillDetail.Trainer.LastName ).FirstOrDefault(),
                //  TrainerName = bootcamp.Courses.Select(
                //     course => course.TrainerSkillDetail.Trainer.FirstName + " " + course.TrainerSkillDetail.Trainer.LastName).FirstOrDefault()??"Not Set",
                 CourseName = bootcamp.Courses.Select(
                    course => course.TrainerSkillDetail.Skill.Name).FirstOrDefault()??"Not Set",               
                }
            );
        return new BootcampPlanedDTO(){
            BootcampsData = model.ToList(),
            Pagination = new PaginationDTO(){
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRows = _bootcampClassRepository.CountBootcampActive(batchBootcamp, bootcampName)
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
                TotalRows = _bootcampClassRepository.CountBootcampCompleted(batchBootcamp, bootcampName)
            }
        };
    }

    public BootcampActiveDetileDTO GetBootcampActiveDetile(int id){
        var model = _bootcampClassRepository.GetDetailBootcamp(id);
        return new BootcampActiveDetileDTO(){
            BootcampId = model.Id,
            // TrainerId = model.Courses.Select(
            //     trainer => trainer.TrainerSkillDetail.Trainer.Id).FirstOrDefault(),
            // SkillId = model.Courses.Select(
            //     course => course.TrainerSkillDetail.Skill.Id).FirstOrDefault()??"Not Set",
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
                TrainerId = bootcamp.Courses.Select(
                    trainer => trainer.TrainerSkillDetail.Trainer.Id).FirstOrDefault(),
                SkillId = bootcamp.Courses.Select(
                    course => course.TrainerSkillDetail.Skill.Id).FirstOrDefault()??"Not Set",
                StartDate = bootcamp.StartDate,
                EndDate = bootcamp.EndDate 
            };
        }
        else{
            return new BootcampActiveDetileDTO(){
                EndDate = null
            };
        }
    }

    public string EndBootcamp(int id, EndBootcampDTO endBootcamp){
            var bootcamp = _bootcampClassRepository.Get(id);
            bootcamp.Id = id;
            bootcamp.EndDate = endBootcamp.EndDate;
            bootcamp.Progress = 3;
            _bootcampClassRepository.Update(bootcamp);
            return "Success";
    }

    public void ActivedBootcamp(int id){
        var bootcamp = _bootcampClassRepository.Get(id);
        var totalCandidate = bootcamp.Candidates.Count();
        if(totalCandidate > 0){
            bootcamp.Progress = 2;
            _bootcampClassRepository.Update(bootcamp);
        }else{
        
        }
    }

    public void DeadActivedBootcamp(int id){
        var bootcamp = _bootcampClassRepository.Get(id);
        bootcamp.Progress = 0;
        _bootcampClassRepository.Update(bootcamp);
    }
}
