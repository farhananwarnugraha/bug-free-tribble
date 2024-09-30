using System;
using HydraAPI.Shared;

namespace HydraAPI.Candidates.DTO;

public class CandidateIndexDTO
{
    public int CandidateId { get; set; }
    public string FullName { get; set; } = null!;
    public int BatchBootcamp { get; set; }
    public string ContactCandidate { get; set; } = null!;
    public string Domicile { get; set; } = null!;
    public PaginationDTO? Paginations { get; set; } 
    public string? FullNames { get; set; }
    public int BootcampBatch { get; set; }
}
