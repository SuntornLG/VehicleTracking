using System;

namespace Entities.DataTransferObject
{
    public class Position
    {
        public int DeviseId { get; set; }
        public float Latitude { get; set; }
        public float Longtitude { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
