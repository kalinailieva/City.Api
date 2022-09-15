namespace CityInfo.Api.Services
{
    public class PaginationMetaData
    {
        public int TotalItemCount { get; set; }

        public int TotalPageCount { get; set; }

        public int PageSize { get; set; }
        public int CurrentPage { get; set; }

        public PaginationMetaData(int totalItemsCount, int pageSize, int currentPage)
        {
            TotalItemCount = totalItemsCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPageCount = (int)Math.Ceiling(totalItemsCount / (double)pageSize);
        }
    }
}
