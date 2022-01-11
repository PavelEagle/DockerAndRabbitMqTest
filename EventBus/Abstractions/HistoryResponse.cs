using System;

namespace EventBus.Abstractions
{
    public class HistoryResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}