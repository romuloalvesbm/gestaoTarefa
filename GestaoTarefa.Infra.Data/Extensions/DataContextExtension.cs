using GestaoTarefa.Domain.Interfaces.Repositories;
using GestaoTarefa.Infra.Data.Context;
using GestaoTarefa.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Infra.Data.Extensions
{
    public static class DataContextExtension
    {
        public static IServiceCollection AddDataContext(this IServiceCollection services, IConfiguration configuration)
        {
            //injeção de dependência do DataContext
            services.AddDbContext<DataContext>
                (options => options.UseSqlServer(configuration.GetConnectionString("BDGestaoTarefaApp")));

            //injeção de dependência do UnitOfWork
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ISetorRepository, SetorRepository>();
            services.AddTransient<ITarefaRepository, TarefaRepository>();

            return services;
        }
    }
}
