
using ChoiceManager;
using Data;
using Data.Repositories;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cors = "cors";
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: cors, policy =>
                {
                    policy.WithOrigins("https://codechallenge.boohma.com", "http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddTransient<IPlayRoundRepository, PlayRoundRepository>();

            builder.Services.AddTransient<IGameService, GameService>();

            builder.Services.AddHttpClient<IGameService, GameService>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["RandomNumberServiceUri"]);
            });

            builder.Services.AddDbContext<EntityDbContext>(options =>
            {
                options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("inMemory"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(cors);
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
