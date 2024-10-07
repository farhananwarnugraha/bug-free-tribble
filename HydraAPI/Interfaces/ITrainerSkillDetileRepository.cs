using System;
using HydraAPI.Models;

namespace HydraAPI.Interfaces;

public interface ITrainerSkillDetileRepository
{
    List<TrainerSkillDetail> GetTrainerSkillDetile();
    TrainerSkillDetail GetTrainerSkillDetile(int id);
    void AddTrainerSkillDetile(TrainerSkillDetail trainerSkillDetail);
    void DeleteTrainerSkillDetile(int trainerId, string skillId);
    void UpdateTrainerSkillDetile(TrainerSkillDetail trainerSkillDetail);
}
