﻿namespace tennismanager.shared.Models;

public class PagedResponse<T> where T : class
{
    
    public PagedResponse(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
    
    public List<T> Items { get; set; } = [];

    public int TotalItems { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
}