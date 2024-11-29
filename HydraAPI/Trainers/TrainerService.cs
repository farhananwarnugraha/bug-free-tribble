using System;
using HydraAPI.Interfaces;
using HydraAPI.Trainers.DTO;

namespace HydraAPI.Trainers;

public class TrainerService
{
    private readonly ITrainerRepository _trainerRepository;

    public TrainerService(ITrainerRepository trainerRepository)
    {
        _trainerRepository = trainerRepository;
    }

    public List<TrainerDTO> Get(){
        var model = _trainerRepository.GetAll()
        .Select(trainer => new TrainerDTO(){
            TrainerId = trainer.Id,
            TrainerName = trainer.FirstName + " " + trainer.LastName 
        }).ToList();
        return model;
    }

    public List<TrainerDTO> GetTrainerBySkill(string skillId){
        var model = _trainerRepository.GetTrainerByCourse(skillId)
        .Select(trainer => new TrainerDTO(){
            TrainerId = trainer.Id,
            TrainerName = trainer.FirstName + " " + trainer.LastName
        }
        ).ToList();
        return model;
    }
}
