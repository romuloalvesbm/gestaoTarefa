﻿using GestaoTarefa.Application.Interfaces;
using GestaoTarefa.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Application.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //configurando o MediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });

            //configurando o AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //registrando as interfaces/classes de serviço da aplicação
            services.AddTransient<ITarefaAppService, TarefaAppService>();
            services.AddTransient<ISetorAppService, SetorAppService>();

            return services;
        }
    }
}

