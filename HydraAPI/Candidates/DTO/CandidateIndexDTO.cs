using System;
using HydraAPI.Shared;

namespace HydraAPI.Candidates.DTO;

public class CandidateIndexDTO
{
    public List<CandidateDTO>? Candidates { get; set; }
    public PaginationDTO? Paginations { get; set; } 
    public string? FullNames { get; set; }
    public int BootcampBatch { get; set; }
}
