using System;

namespace backend.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Guid OwnerId { get; set; }
    }
}