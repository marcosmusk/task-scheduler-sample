using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskScheduler.Context
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            TaskDbContext context = serviceProvider.GetService<TaskDbContext>();
            context.Database.EnsureCreated();

            // Look for any uesrs.
            if (context.Tasks.Any())
            {
                return;   // DB has been seeded
            }

            context.Tasks.AddAsync(new Models.Task()
            {
                Title = "Titulo tarefa",
                Description = "Descrição...",
                Priority = 1,
                Important = true
            });

            context.SaveChanges();
        }
    }
}
