using System;
using BCrypt.Net;
using HydraAPI.Interfaces;
using HydraAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HydraAPI.Repository;

public class BootcampClassRepository : IBootcampClass
{
    private readonly HydraContext _dbContext;

    public BootcampClassRepository(HydraContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<BootcampClass> BetBootcampPlaned(int pageNumber, int pageSize, int batchBootcamp, string bootcampName)
    {
        return _dbContext.BootcampClasses
        .Include(bc => bc.Candidates)
        .Where(
            bootcampPlaned => (bootcampPlaned.Description??"").ToLower().Contains(bootcampName??"".ToLower())
            && (batchBootcamp == 0 || bootcampPlaned.Id == batchBootcamp) 
            && bootcampPlaned.Progress == 1
        )
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public int Count(int batchBootcamp, string bootcampName)
    {
        return _dbContext.BootcampClasses
        .Where(
            bootcamp => (bootcamp.Description??"").ToLower().Contains(bootcampName??"".ToLower())
            && 
            (batchBootcamp == 0 || bootcamp.Id == batchBootcamp)
        ).Count();
    }

    public int CountBootcampActive(int batchBootcamp, string bootcampDescription)
    {
        return _dbContext.BootcampClasses
        .Include(bc => bc.Courses.OrderByDescending(c => c.EndDate))
            .ThenInclude(c => c.TrainerSkillDetail)
                .ThenInclude(tsd => tsd.Trainer)
        .Include(bc => bc.Courses.OrderByDescending( c => c.EndDate))
            .ThenInclude(c => c.TrainerSkillDetail)
                .ThenInclude(tsd => tsd.Skill)
        .Where(
            bootcamp => 
                (bootcamp.Description??"").ToLower().Contains(bootcampDescription??"".ToLower()) && 
                (batchBootcamp == 0 || bootcamp.Id == batchBootcamp) 
                &&  (bootcamp.Progress == 2)
        )
        .Count();
    }

    public int CountBootcampCompleted(int batchBootacamp, string bootcampDescription)
    {
        return _dbContext.BootcampClasses
        .Include(bootcamCandidate => bootcamCandidate.Candidates)
        .Where(
            bootcamp => 
                (bootcamp.Description??"").ToLower().Contains(bootcampDescription??"".ToLower()) && 
                (batchBootacamp == 0 || bootcamp.Id == batchBootacamp) 
                && bootcamp.Progress == 3
        ).Count();
    }

    public int CountBootcampPlaned(int batchBootacamp, string bootcampDescription)
    {
        return _dbContext.BootcampClasses
        .Include(bc => bc.Candidates)
        .Where(
            bootcampPlaned => (bootcampPlaned.Description??"").ToLower().Contains(bootcampDescription??"".ToLower())
            && (batchBootacamp == 0 || bootcampPlaned.Id == batchBootacamp) 
            && bootcampPlaned.Progress == 1
        ).Count();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public List<BootcampClass> Get()
    {
        return _dbContext.BootcampClasses.ToList();
    }

    public List<BootcampClass> Get(int pageNumber, int pageSize, int batchBootcamp, string bootcampName)
    {
        return _dbContext.BootcampClasses
        .Where(
            bootcamp => 
                (bootcamp.Description??"").ToLower().Contains(bootcampName??"".ToLower()) && (batchBootcamp == 0 || bootcamp.Id == batchBootcamp)
        )
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public BootcampClass Get(int bootcampId)
    {
        return _dbContext.BootcampClasses.Find(bootcampId) ?? throw new Exception("Bootcam Not Found");
    }

    public List<BootcampClass> GetBootcampActive(int pageNumber, int pageSize, int batchBootcamp, string bootcampName)
    {
        return _dbContext.BootcampClasses
        .Include(bc => bc.Courses.OrderByDescending(c => c.EndDate))
            .ThenInclude(c => c.TrainerSkillDetail)
                .ThenInclude(tsd => tsd.Trainer)
        .Include(bc => bc.Courses.OrderByDescending( c => c.EndDate))
            .ThenInclude(c => c.TrainerSkillDetail)
                .ThenInclude(tsd => tsd.Skill)
        .Where(
            bootcamp => 
                (bootcamp.Description??"").ToLower().Contains(bootcampName??"".ToLower()) && 
                (batchBootcamp == 0 || bootcamp.Id == batchBootcamp) 
                &&  (bootcamp.Progress == 2)
                // (bootcamp.Progress == 2 || bootcamp.Courses.Any(c => c.Progress == 2 &&
                //     (c.Progress !=3 || c.EvaluationDate == null) &&
                //     _dbContext.TrainerSkillDetails.Any(
                //         tsd => tsd.TrainerId == c.TrainerId && tsd.SkillId == c.SkillId)
                //     )
                // )
        )
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public List<BootcampClass> GetBootcampCompleted(int pageNumber, int pageSize, int batchBootcamp, string bootcampName)
    {
        return _dbContext.BootcampClasses
        .Include(bootcamCandidate => bootcamCandidate.Candidates)
        .Where(
            bootcamp => 
                (bootcamp.Description??"").ToLower().Contains(bootcampName??"".ToLower()) && 
                (batchBootcamp == 0 || bootcamp.Id == batchBootcamp) 
                && bootcamp.Progress == 3
        )
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public List<BootcampClass> GetBootcampPlaned()
    {
        return _dbContext.BootcampClasses
            .Where(bootcamp => bootcamp.Progress == 1)
            .ToList();
    }

    public BootcampClass GetBootcampPlaned(int batchBootcamp)
    {
        return _dbContext.BootcampClasses
        .Include(bc => bc.Candidates)
        .Where(
            bootcampPlaned => (batchBootcamp == 0 || bootcampPlaned.Id == batchBootcamp) 
            && bootcampPlaned.Progress == 1
        ).FirstOrDefault() ?? throw new Exception("Bootcam Not Found"); 
    }

    // get detail bootcamp active
    public BootcampClass GetDetailBootcamp(int batchBootcamp)
    {
        return _dbContext.BootcampClasses
        .Include(bc => bc.Courses.OrderByDescending(c => c.EndDate))
            .ThenInclude(c => c.TrainerSkillDetail)
                .ThenInclude(tsd => tsd.Trainer)
        .Include(bc => bc.Courses.OrderByDescending( c => c.EndDate))
            .ThenInclude(c => c.TrainerSkillDetail)
                .ThenInclude(tsd => tsd.Skill)
        .Where(
            bootcamp => (bootcamp.Id == batchBootcamp) 
                && 
                (bootcamp.Progress == 2) && bootcamp.Courses.All(c => c.Progress == 3)
                // (bootcamp.Progress == 2 || bootcamp.Courses.Any(c => c.Progress == 2 &&
                //     (c.Progress !=3 || c.EvaluationDate == null) &&
                //     _dbContext.TrainerSkillDetails.Any(
                //         tsd => tsd.TrainerId == c.TrainerId && tsd.SkillId == c.SkillId)
                //     )
                // )
        ).FirstOrDefault() ?? throw new Exception("Bootcam Not Found");
    }

    public void Insert(BootcampClass bootcampClass)
    {
        _dbContext.Add(bootcampClass);
        _dbContext.SaveChanges();
    }

    public void Update(BootcampClass bootcampClass)
    {
        _dbContext.Update(bootcampClass);
        _dbContext.SaveChanges();
    }
}
