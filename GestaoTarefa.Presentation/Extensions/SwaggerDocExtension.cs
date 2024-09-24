using Microsoft.OpenApi.Models;
using System.Reflection;

namespace GestaoTarefa.Presentation.Extensions
{
    public static class SwaggerDocExtension
    {
        public static IServiceCollection AddSwaggerDoc(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "TarefasApp API",
                        Description = "API para controle de tarefas.",
                        Version = "2.0",
                        Contact = new OpenApiContact
                        {
                            Name = "Rômulo Alves",
                            Email = "romuloalves.br@gmail.com.br",
                            Url = new Uri("https://www.linkedin.com/in/rômulo-alves-a4144113b/")
                        }
                    });

                    //configuração para incluir os comentários na documentação
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    options.IncludeXmlComments(xmlPath);
                });

            return services;
        }

        /// <summary>
        /// Método para configurar a execução do Swagger
        /// </summary>
        public static IApplicationBuilder UseSwaggerDoc(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "TarefasApp");
            });

            return app;
        }
    }
}