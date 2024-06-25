namespace tennismanager.shared.Models;

public class PagedResponse<T> where T : class
{

    public List<T> Items { get; set; } = [];
    
    public int PageNumber { get; set; }
    
    public int PageSize { get; set; }
    
    private int totalCount { get; set; }

    public int TotalCount
    {
        get => (int)Math.Ceiling((double)totalCount / PageSize);
        set => totalCount = value;
    }
}