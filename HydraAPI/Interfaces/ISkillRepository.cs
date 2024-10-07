using System;
using HydraAPI.Models;

namespace HydraAPI.Interfaces;

public interface ISkillRepository
{
    List<Skill> GetAll();
    Skill GetSkillById(int id);
    Skill GetSkillByName(string name);
    Skill Add(Skill skill);
    Skill Update(Skill skill);
    void Delete(int id);
}
