using System;

namespace HydraAPI.Bootcamp.DTO;

public class BootcampDTO
{
    public int BootcampId { get; set; }
    public string Description { get; set; } = null!;
    public string StartDate { get; set; } = null!;
    public string? EndDate { get; set; } 
    public int? TotalCandidates { get; set; }
    public string? TrainerName { get; set; }
    public string? CourseName { get; set; }

    // public static implicit operator BootcampDTO(List<BootcampDTO> v)
    // {
    //     throw new NotImplementedException();
    // }
}
