using System;

namespace Consumer.Infrastructure.DAL.History.Models
{
    public class DbHistory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}