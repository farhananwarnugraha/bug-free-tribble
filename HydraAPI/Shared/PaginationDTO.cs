namespace HydraAPI.Shared
{
    public class PaginationDTO
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRows { get; set; }
        public int TotalPage {
            get
            {
                return (int)Math.Ceiling((double)PageNumber/PageSize);
            }
        }
    }
}
