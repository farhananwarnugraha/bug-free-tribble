using System;
using HydraAPI.Interfaces;
using HydraAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HydraAPI.Repository;

public class TrainerRepository : ITrainerRepository
{
    private readonly HydraContext _dbContext;

    public TrainerRepository(HydraContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Trainer trainer)
    {
        _dbContext.Add(trainer);
        _dbContext.SaveChanges();
    }

    public void Delete(Trainer trainer)
    {
        _dbContext.Remove(trainer);
        _dbContext.SaveChanges();
    }

    public List<Trainer> GetAll()
    {
        return _dbContext.Trainers.ToList();
    }

    public Trainer GetById(int id)
    {
        return _dbContext.Trainers.Find(id)?? throw new Exception("Trainer Not Found");
    }

    public List<Trainer> GetTrainerByCourse(string skillId)
    {
        return _dbContext.Trainers
        .Include(t => t.TrainerSkillDetails)
        .Where(t => t.TrainerSkillDetails.Any(td=> td.SkillId == skillId)).ToList();
    }

    public void Update(Trainer trainer)
    {
        _dbContext.Update(trainer);
        _dbContext.SaveChanges();
    }
}
