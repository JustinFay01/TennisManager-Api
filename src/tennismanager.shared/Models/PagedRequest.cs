namespace tennismanager.shared.Models;

public class PagedRequest
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    
    public PagedRequest(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}