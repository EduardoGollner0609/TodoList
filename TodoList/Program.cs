using Microsoft.Data.SqlClient;
using TodoList.Dto;
using TodoList.Factory;
using TodoList.Services;

namespace TodoList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int id;
            bool execute = true;
            List<TaskDto> tasks = new List<TaskDto>();
            TaskService service = new(ImplementationFactory.CreateTaskRepository());

            Console.WriteLine("Seja muito bem vindo ao TodoList!!");
            while (execute)
            {
                Console.WriteLine("Digite o número da opção que deseja realizar");
                Console.WriteLine("1- Ver Tarefas");
                Console.WriteLine("2- Marcar como concluida");
                Console.WriteLine("3- Deletar Tarefa");
                Console.WriteLine("4- Encerrar programa");
                Console.Write("Resposta: ");
                string optional = Console.ReadLine();

                switch (optional)
                {
                    case "1":
                        tasks = service.FindAll();
                        PrintTasks(tasks);
                        break;
                    case "2":
                        Console.WriteLine("ID: ");
                        id = int.Parse(Console.ReadLine());
                        TaskDto task = service.FindById(id);
                        Console.WriteLine("Atualizada com sucesso: " + task);
                        break;
                    case "3":
                        Console.WriteLine("ID: ");
                        id = int.Parse(Console.ReadLine());
                        service.DeleteById(id);
                        Console.WriteLine($"Tarefa do id {id} deletada com sucesso");
                        break;
                    case "4":
                        execute = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
                Console.WriteLine();
            }
        }

        static void PrintTasks(List<TaskDto> tasks)
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("Você não tem tarefas registradas");
                return;
            }
            foreach (TaskDto task in tasks)
            {
                Console.WriteLine(task);
            }
        }
    }
}
