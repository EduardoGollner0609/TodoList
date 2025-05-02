using TodoList.Entities.Enums;
using Task = TodoList.Entities.Task;

namespace TodoList.Dto
{
    internal class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Importance Importance { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }

        public TaskDto() { }

        public TaskDto(Task task)
        {
            Id = task.Id;
            Title = task.Title;
            Description = task.Description;
            Importance = task.Importance;
            Date = task.Date;
            Status = task.Status;
        }

        public override string ToString()
        {
            return $"ID: {Id} | " +
                $"Titulo: {Title} | " +
                $"Descrição: {Description} | " +
                $"Prazo: {Date.ToString("dd/MM/yyyy")} | " +
                $"Importância: {Importance} | " +
                $"Status: {Status}";
        }
    }
}
