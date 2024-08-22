namespace NewDemoWebApp.Common.DTO
{
    public abstract class PagingDTO
    {
        public int PageIndex { get; set; } = 0;

        public int PageSize { get; set; }

        public string? SortField { get; set; }

        public bool IsSortDesc { get; set; }
    }
}
