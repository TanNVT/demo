namespace NewDemoWebApp.Common
{
    public abstract class PagingDTO
    {
        public int PageIndex { get; set; } = 0;

        public int PageCount { get; set; }
    }
}
