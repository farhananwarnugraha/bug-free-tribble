using System;

namespace HydraAPI.Candidates.DTO;

public class CandidateUpdateDTO
{
    public int CandidateId { get; set; }
    public int BootcampClass { get; set; }
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string Gender { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public string Address { get; set; } = null!;
    public string Domicile { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public bool? IsActive { get; set; }
}
