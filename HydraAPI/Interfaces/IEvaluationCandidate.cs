using System;
using HydraAPI.Models;

namespace HydraAPI.Interfaces;

public interface IEvaluationCandidate
{
    List<CandidateEvaluation> GetCandidateEavaluations(int pageNumber, int pageSize, string fullName);

    int Count(string fullName);

    void Insert(List<CandidateEvaluation>  candidateEvaluation);

    void Insert(CandidateEvaluation candidateEvaluation);
}
