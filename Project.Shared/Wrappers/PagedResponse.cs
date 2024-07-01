namespace Project.Shared.Configuration.Wrappers;

public class PagedResponse<T> : Response<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public PagedResponse(T? data, int pageNumber, int pageSize, int totalCount, string? message = null)
    {
        this.PageNumber = pageNumber;
        this.PageSize = pageSize;
        this.TotalCount = totalCount;
        this.AddSuccessResult(data, message);
    }
}