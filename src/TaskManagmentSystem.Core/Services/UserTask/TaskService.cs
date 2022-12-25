﻿namespace TaskManagementSystem.Core.Services.UserTask
{
    using TaskManagementSystem.Core.Contracts.UserTask;
    using TaskManagementSystem.Core.Models.UserTaskModels;
    using TaskManagementSystem.Infrastructure.Common;
    using TaskManagementSystem.Infrastructure.Emuns;
    using TaskManagementSystem.Infrastructure.Models;

    public class TaskService : ITaskService
    {
        private readonly IRepository repository;

        public TaskService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task AddTaskAsync(AddTaskModel request, Guid userId)
        {
            var importanceIsValid = Enum.TryParse<Importance>(request.Importance, out var importanceType);

            if (!importanceIsValid)
            {
                throw new ArgumentException("Importance type is not valid!");
            }

            var stateIsValid = Enum.TryParse<State>(request.State, out var stateType);

            if (!stateIsValid)
            {
                throw new ArgumentException("State type is not valid!");
            }

            var task = new UserTask()
            {
                Title = request.Title,
                Description = request.Description,
                Importance = importanceType,
                State = stateType,
                ApplicationUserId = userId
            };

            await this.repository.AddAsync<UserTask>(task);
            await this.repository.SaveChangesAsync();
        }

        public Task FinishTaskAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task MoveTaskAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}