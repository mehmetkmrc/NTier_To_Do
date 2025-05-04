using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Udemy.ToDoAppNTier.Business.Interfaces;
using Udemy.ToDoAppNTier.Business.Mappings.AutoMapper;
using Udemy.ToDoAppNTier.Business.Services;
using Udemy.ToDoAppNTier.Business.ValidationRules;
using Udemy.ToDoAppNTier.DataAccess.Context;
using Udemy.ToDoAppNTier.DataAccess.UnitOfWork;
using Udemy.ToDoAppNTier.Dtos.WorkDtos;

namespace Udemy.ToDoAppNTier.Business.DependencyResolvers.Microsoft
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            var connectionString = "Host=localhost;Port=5432;Username=postgres;Password=KMRC-computer-2025;Database=ToDoDb;";

            services.AddDbContext<ToDoContext>(opt =>
            {
                opt.UseNpgsql(connectionString, npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly("Your.Migrations.Assembly.Name");
                });
                opt.LogTo(Console.WriteLine, LogLevel.Information);
            });
            var configuration = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new WorkProfile());
            });

            var mapper = configuration.CreateMapper();
            services.AddSingleton(mapper); 

            services.AddScoped<IUow, Uow>();
            services.AddScoped<IWorkService, WorkService>();
            services.AddTransient<IValidator<WorkCreateDto>, WorkCreateDtoValidator>();
            services.AddTransient<IValidator<WorkUpdateDto>, WorkUpdateDtoValidator>();
        }
    }
}
