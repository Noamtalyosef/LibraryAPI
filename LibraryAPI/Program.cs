
using LibraryAPI.Helpers;
using LibraryAPI.Interfaces;
using LibraryAPI.Reposetories;

namespace LibraryAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSingleton<IAuthorReposetory, AuthorReposetory>();
            builder.Services.AddSingleton<IBookReposetory, BookReposetory>();
            builder.Services.AddSingleton<IStartupConfig, StartupConfig>();
            builder.Services.AddSingleton<IBookFilesHelper, BookFilesHelper>();
            builder.Services.AddHttpClient();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddCors(options =>
            {
              options.AddPolicy("AllowAll",
              builder =>
              {
               builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
             });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseStaticFiles();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
