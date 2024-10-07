using System;
using HydraAPI.Interfaces;
using HydraAPI.Models;

namespace HydraAPI.Repository;

public class CandidateEvaluationRepository : ICandidateEvaluationRepository
{
    private readonly HydraContext _dbContext;

    public CandidateEvaluationRepository(HydraContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(CandidateEvaluation candidateEvaluation)
    {
        _dbContext.Add(candidateEvaluation);
        _dbContext.SaveChanges();
    }

    public void Delete(CandidateEvaluation candidateEvaluation)
    {
        _dbContext.Remove(candidateEvaluation);
        _dbContext.SaveChanges();
    }

    public CandidateEvaluation Get(string id)
    {
        return _dbContext.CandidateEvaluations.Find(id) ?? throw new Exception("CandidateEvaluation Not Found");
    }

    public List<CandidateEvaluation> GetAll()
    {
        return _dbContext.CandidateEvaluations.ToList();
    }

    public void Update(CandidateEvaluation candidateEvaluation)
    {
        _dbContext.Update(candidateEvaluation);
        _dbContext.SaveChanges();
    }
}
