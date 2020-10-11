﻿
namespace Entities.DataTransferObject
{
   public class PositionRequest
    {
        const int maxPageSize = 15;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 15;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
