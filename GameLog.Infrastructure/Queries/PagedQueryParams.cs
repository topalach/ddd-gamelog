namespace GameLog.Infrastructure.Queries;

public abstract class PagedQueryParams
{
    /// <summary>
    /// Starts from 1.
    /// </summary>
    public int PageNumber { get; set; }
    
    public int PageSize { get; set; }

    public int Skip => (PageNumber - 1) * PageSize;
    public int Take => PageSize;
}