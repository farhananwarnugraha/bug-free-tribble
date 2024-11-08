using System;
using HydraAPI.Shared;

namespace HydraAPI.EvaluationCandidate.DTO;

public class EvaluationCandidateResponseDTO
{
    public List<EvaluationCandidateDTO>? EvaluationCandidates { get; set; }
    public PaginationDTO? Paginations { get; set; }
    public string? FullName { get; set; }
    // public string? CourseName { get; set; }
}
