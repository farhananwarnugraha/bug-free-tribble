using System;

namespace HydraAPI.EvaluationCandidate.DTO;

public class EvaluationCandidateDTO
{
    public string FullName { get; set; } = null!;
    public string CourseName { get; set; } = null!;
    public int Mark { get; set; }
    public bool IsPassed { get; set; }
}
