namespace NewDemoWebApp.Common.DTO
{
    public class PagedResultDTO<T> where T : class
    {
        public int TotalRecord { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public List<T> Items { get; set; }
    }
}
