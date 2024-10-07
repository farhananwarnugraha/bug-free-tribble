using System;
using HydraAPI.Interfaces;
using HydraAPI.Models;

namespace HydraAPI.Repository;

public class TrainerSkillDetailRepository : ITrainerSkillDetileRepository
{
    private readonly HydraContext _dbContext;

    public TrainerSkillDetailRepository(HydraContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddTrainerSkillDetile(TrainerSkillDetail trainerSkillDetail)
    {
        _dbContext.Add(trainerSkillDetail);
        _dbContext.SaveChanges();
    }

    public void DeleteTrainerSkillDetile(int trainerId, string skillId)
    {
        throw new NotImplementedException();
    }

    public List<TrainerSkillDetail> GetTrainerSkillDetile()
    {
        return _dbContext.TrainerSkillDetails.ToList();
    }

    public TrainerSkillDetail GetTrainerSkillDetile(int id)
    {
        return _dbContext.TrainerSkillDetails.Find(id)??throw new Exception("TrainerSkillDetail Not Found");
    }

    public void UpdateTrainerSkillDetile(TrainerSkillDetail trainerSkillDetail)
    {
        _dbContext.Update(trainerSkillDetail);
        _dbContext.SaveChanges();
    }
}
