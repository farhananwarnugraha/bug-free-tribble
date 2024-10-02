using System;
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
        .Include(bootcamCandidate => bootcamCandidate.Candidates)
        .Where(
            bootcamp => 
                (bootcamp.Description??"").ToLower().Contains(bootcampName??"".ToLower()) && 
                (batchBootcamp == 0 || bootcamp.Id == batchBootcamp) 
                && bootcamp.Progress == 2
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
