namespace tennismanager.shared.Models;

public class PagedResponse<T> where T : class
{

    public List<T> Items { get; set; } = [];
    
    public int PageNumber { get; set; }
    
    public int PageSize { get; set; }
    
    private int _totalCount { get; set; }

    public int TotalPages
    {
        get => (int)Math.Ceiling((double)_totalCount / PageSize);
        set => _totalCount = value;
    }
}