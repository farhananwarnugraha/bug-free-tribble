using System;

namespace HydraAPI.Courses.DTO;

public class AddCourseBootcampDTO
{
    public int TrainerId { get; set; }
    public string SkillId { get; set; } =null!;
    public int BootcampClass { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
