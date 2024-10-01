using System;
using HydraAPI.Shared;

namespace HydraAPI.Bootcamp.DTO;

public class BootcampPageDTO
{
    public List<BootcampDTO>? BootcampClasses { get; set; }
    public PaginationDTO? Pagination { get; set; }
    public string? BootcampDescription { get; set; }
    public int BatchBootcamp { get; set; }
}
