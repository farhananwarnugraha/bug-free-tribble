using System;
using HydraAPI.Interfaces;
using HydraAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HydraAPI.Repository;

public class EvaluationCandidateRepository : IEvaluationCandidate
{
    private readonly HydraContext _dbContext;

    public EvaluationCandidateRepository(HydraContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int Count(string fullName)
    {
        return _dbContext.CandidateEvaluations
        .Include(ce => ce.Candidate)
        .Include(ce => ce.Course)
            .ThenInclude(c => c.TrainerSkillDetail)
                .ThenInclude(tsd => tsd.Skill)
        .Where(ce => (ce.Candidate.FirstName + "  " + ce.Candidate.LastName).ToLower().Contains(fullName.ToLower()))
            // (string.IsNullOrWhiteSpace(fullName) || 
            //     (evaluation.Candidate.FirstName + " " + evaluation.Candidate.LastName).ToLower().Contains(fullName.ToLower())) &&
            // (string.IsNullOrWhiteSpace(courseName) || evaluation.Course.TrainerSkillDetail.Skill.Name.ToLower().Contains(courseName.ToLower()))
        .Count();
    }

    public List<CandidateEvaluation> GetCandidateEavaluations(int pageNumber, int pageSize, string fullName)
    {
        return _dbContext.CandidateEvaluations
        .Include(ce => ce.Candidate)
        .Include(ce => ce.Course)
            .ThenInclude(c => c.TrainerSkillDetail)
            .ThenInclude(tsd => tsd.Skill)
        .Where(ce => (ce.Candidate.FirstName + ce.Candidate.LastName).ToLower().Contains(fullName??"".ToLower()))
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public void Insert(List<CandidateEvaluation> candidateEvaluation)
    {
        _dbContext.AddRange(candidateEvaluation);
        _dbContext.SaveChanges();
    }

    public void Insert(CandidateEvaluation candidateEvaluation)
    {
        _dbContext.Add(candidateEvaluation);
        _dbContext.SaveChanges();
    }
}
