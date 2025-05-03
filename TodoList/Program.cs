using Microsoft.Data.SqlClient;
using System.Globalization;
using TodoList.Database;
using TodoList.Database.Exceptions;
using TodoList.Dto;
using TodoList.Entities.Enums;
using TodoList.Factory;
using TodoList.Services;
using TodoList.Services.Exceptions;
using Task = TodoList.Entities.Task;

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
                try
                {
                    Console.WriteLine("Digite o número da opção que deseja realizar");
                    Console.WriteLine("1- Ver Tarefas");
                    Console.WriteLine("2- Criar nova tarefa");
                    Console.WriteLine("3- Marcar como concluida");
                    Console.WriteLine("4- Deletar Tarefa");
                    Console.WriteLine("5- Encerrar programa");
                    Console.Write("Resposta: ");
                    string optional = Console.ReadLine();

                    Console.WriteLine();

                    switch (optional)
                    {
                        case "1":
                            tasks = service.FindAll();
                            PrintTasks(tasks);
                            break;
                        case "2":
                            service.Insert(PrintForm());
                            break;
                        case "3":
                            Console.Write("ID: ");
                            id = int.Parse(Console.ReadLine());
                            TaskDto task = service.FindById(id);
                            task.Status = Status.Concluido;
                            service.Update(task, id);
                            break;
                        case "4":
                            Console.Write("ID: ");
                            id = int.Parse(Console.ReadLine());
                            service.DeleteById(id);
                            break;
                        case "5":
                            execute = false;
                            break;
                        default:
                            Console.WriteLine("Opção inválida!");
                            break;
                    }
                    Console.WriteLine();
                }
                catch (ResourceNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine();
                }
                catch (DatabaseException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine();
                }
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

        static TaskDto PrintForm()
        {
            Console.WriteLine("Digite os dados da tarefa.");
            Console.Write("Titulo da tarefa: ");
            string title = Console.ReadLine();
            Console.Write("Descrição: ");
            string description = Console.ReadLine();
            Console.Write("Importância (Baixa, Media, Alta): ");
            Importance importance = Enum.Parse<Importance>(Console.ReadLine());
            Console.Write("Prazo (dd/mm/yyyy):");
            DateTime date = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            return new(title, description, importance, date, Status.Pendente);
        }
    }
}
