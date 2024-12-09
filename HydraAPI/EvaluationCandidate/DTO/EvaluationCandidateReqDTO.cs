using System;

namespace HydraAPI.EvaluationCandidate.DTO;

public class EvaluationCandidateReqDTO
{
    public string CourseId { get; set; } = null!;
    public int CandidateId { get; set; }
    public int Mark { get; set; }
    public string? Note { get; set; }
}
