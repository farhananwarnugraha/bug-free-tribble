using System;

namespace HydraAPI.Bootcamp.DTO;

public class BootcampUpdateDTO
{
    public int BootcampId { get; set; }
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
