namespace Jivar.Service.Paging
{
    public class PagingAndSortingParams
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "Id";
        public bool IsDescending { get; set; } = false;
        public string? IncludeProperties { get; set; } = null;
    }

}
