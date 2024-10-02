using System;

namespace HydraAPI.Bootcamp.DTO;

public class BootcampDTO
{
    public int BootcampId { get; set; }
    public string Description { get; set; } = null!;
    public string StartDate { get; set; } = null!;
    public string? EndDate { get; set; } 
    public int? TotalCandidates { get; set; }
}
