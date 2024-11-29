using System;
using HydraAPI.Models;

namespace HydraAPI.Interfaces;

public interface ITrainerRepository
{
    List<Trainer> GetAll();
    Trainer GetById(int id);
    List<Trainer> GetTrainerByCourse(string skillId);
    void Add(Trainer trainer);
    void Update(Trainer trainer);
    void Delete(Trainer trainer);
}
