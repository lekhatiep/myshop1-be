using System;

namespace Domain.Common.Paging
{
    public class PagedRequestBase
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; }

        private int _pageSize { get; set; }
        public int PageSize 
        { 
            get => _pageSize; 
            set => _pageSize = (value > maxPageSize) ? PageSize : value; 
        }

    }
}
