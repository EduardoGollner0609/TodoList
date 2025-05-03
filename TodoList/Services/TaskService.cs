using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Dto;
using TodoList.Factory;
using TodoList.Repository;
using TodoList.Services.Exceptions;
using Task = TodoList.Entities.Task;

namespace TodoList.Services
{
    internal class TaskService
    {
        private ICrudRepository<Task> _taskRepository;

        public TaskService(ICrudRepository<Task> crudRepository)
        {
            _taskRepository = crudRepository;
        }

        public void Insert(TaskDto dto)
        {
            Task task = ConvertDtoToEntity(dto);
            _taskRepository.Insert(task);
        }

        public TaskDto FindById(int id)
        {
            Task task = _taskRepository.FindById(id);
            if (task == null)
            {
                throw new ResourceNotFoundException($"Tarefa do id {id} não foi encontrada!");
            }
            return new(task);
        }

        public List<TaskDto> FindAll()
        {
            List<Task> tasks = _taskRepository.FindAll();
            return tasks.Select(task => new TaskDto(task)).ToList();
        }

        public bool ExistsById(int id)
        {
            return _taskRepository.ExistsById(id);
        }

        public void Update(TaskDto dto, int id)
        {
            Task task = ConvertDtoToEntity(dto);
            _taskRepository.Update(task, id);
        }

        public void DeleteById(int id)
        {
            if (!_taskRepository.ExistsById(id))
            {
                throw new ResourceNotFoundException($"Tarefa do id {id} não foi encontrada!");
            }
            _taskRepository.DeleteById(id);
        }

        public Task ConvertDtoToEntity(TaskDto dto)
        {
            return new(dto.Title, dto.Description, dto.Importance, dto.Date, dto.Status);
        }
    }
}
