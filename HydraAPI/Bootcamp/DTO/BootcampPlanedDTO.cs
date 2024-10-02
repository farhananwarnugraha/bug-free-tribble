using System;
using HydraAPI.Shared;

namespace HydraAPI.Bootcamp.DTO;

public class BootcampPlanedDTO
{
    public List<BootcampDTO>? BootcampsData { get; set; }
    public PaginationDTO? Pagination { get; set; }
    public int batchBootcamp { get; set; }
    public string? DescriptionBootcamp { get; set; }
}
