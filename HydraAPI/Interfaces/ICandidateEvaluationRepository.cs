using System;
using HydraAPI.Models;

namespace HydraAPI.Interfaces;

public interface ICandidateEvaluationRepository
{
    List<CandidateEvaluation> GetAll();
    CandidateEvaluation Get(string id);
    void Add(CandidateEvaluation candidateEvaluation);
    void Delete(CandidateEvaluation candidateEvaluation);
    void Update(CandidateEvaluation candidateEvaluation);
}
