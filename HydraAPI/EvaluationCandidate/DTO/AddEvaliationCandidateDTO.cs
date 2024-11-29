using System;

namespace HydraAPI.EvaluationCandidate.DTO;

public class AddEvaliationCandidateDTO
{
    public List<string> CourseId { get; set; } = null!;
    public List<int> CandidateId { get; set; } = null!;
    public List<int> Marks { get; set; } = null!;
    public List<string>? Notes { get; set; }
}
