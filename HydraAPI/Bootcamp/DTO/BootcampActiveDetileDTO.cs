using System;

namespace HydraAPI.Bootcamp.DTO;

public class BootcampActiveDetileDTO
{
    public int BootcampId { get; set; }
    public string? SkillId { get; set; }
    public int? TrainerId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
