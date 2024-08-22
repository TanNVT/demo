namespace NewDemoWebApp.Common.DTO
{
    public abstract class PagingDTO
    {
        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 20;

        public string? SortField { get; set; }

        public bool IsSortDesc { get; set; }
    }
}
