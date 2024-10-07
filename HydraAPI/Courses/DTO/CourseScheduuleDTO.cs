using System;

namespace HydraAPI.Courses.DTO;

public class CourseScheduuleDTO
{
    public string courseId { get; set; } =null!;
    public string MateriBootcamp { get; set; } = null!;
    public string TrainerName { get; set; } = null!;
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string Status { get; set; } =null!;
    public string? EvaluationDate { get; set; }
}
