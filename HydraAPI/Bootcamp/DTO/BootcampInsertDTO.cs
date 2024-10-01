using System;

namespace HydraAPI.Bootcamp.DTO;

public class BootcampInsertDTO
{
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
