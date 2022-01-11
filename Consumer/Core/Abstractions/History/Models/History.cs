using System;

namespace Consumer.Core.Abstractions.History.Models
{
    public class History
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}