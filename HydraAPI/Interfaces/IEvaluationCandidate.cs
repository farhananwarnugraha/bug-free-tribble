using System;
using HydraAPI.Models;

namespace HydraAPI.Interfaces;

public interface IEvaluationCandidate
{
    List<CandidateEvaluation> GetCandidateEavaluations(int pageNumber, int pageSize, string fullName, string courseName);

    int Count(string fullName, string courseName);
}
