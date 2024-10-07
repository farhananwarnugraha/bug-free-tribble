using System;
using HydraAPI.Interfaces;
using HydraAPI.Models;

namespace HydraAPI.Repository;

public class SkillRepository : ISkillRepository
{
    private readonly HydraContext _dbContext;
    public SkillRepository(HydraContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Skill Add(Skill skill)
    {
        _dbContext.Add(skill);
        _dbContext.SaveChanges();
        return skill;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public List<Skill> GetAll()
    {
        return _dbContext.Skills.ToList();
    }

    public Skill GetSkillById(int id)
    {
        return _dbContext.Skills.Find(id) ?? throw new Exception("Skill Not Found");
    }

    public Skill GetSkillByName(string name)
    {
        return _dbContext.Skills
            .Where(
                s => s.Name == name
            ).FirstOrDefault()
            ?? throw new Exception("Skill Not Found");
    }

    public Skill Update(Skill skill)
    {
        _dbContext.Update(skill);
        _dbContext.SaveChanges();
        return skill;
    }
}
