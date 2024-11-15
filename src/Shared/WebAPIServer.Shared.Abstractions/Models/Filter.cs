﻿namespace WebAPIServer.Shared.Abstractions.Models
{
    public class Filter
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
        public string? SortColumn { get; set; }
        public bool IsDescending { get; set; } = true;
    }
}