using System;

namespace HydraAPI.Candidates.DTO;

public class CandidateByCourseBootcamp
{
    public string CourseId { get; set; } = null!;
    public int CandidateId { get; set; }
    public string FullName { get; set; } = null!;
}
