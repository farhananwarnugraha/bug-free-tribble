using System;
using HydraAPI.Shared;

namespace HydraAPI.Bootcamp.DTO;

public class BootcampActiveDTO
{
    public List<BootcampDTO>? BooctamopActiveList { get; set; }
    public PaginationDTO? Paginations { get; set; }
    public string? BootcampName { get; set; }
    public int BatchBootcamp { get; set; }
}
