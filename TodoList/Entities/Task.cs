
using TodoList.Entities.Enums;

namespace TodoList.Entities
{
    internal class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Importance Importance { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }

        public Task() { }

        public Task(string title, string description, Importance importance, DateTime date, Status status)
        {
            Title = title;
            Description = description;
            Importance = importance;
            Date = date;
            Status = status;
        }

        public Task(int id, string title, string description, Importance importance, DateTime date, Status status) : this(title, description, importance, date, status)
        {
            Id = id;
        }
    }
}
