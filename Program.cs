
using BookStoreApi.Models;
using BookStoreApi.Services;

namespace BookStoreApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers(); //singleton
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.Configure<BookStoreDatabaseSettings>(builder.Configuration.GetSection("BookStoreDatabase"));
            builder.Services.AddSingleton<BooksService>();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("policy1", policy =>
                {
                    policy.AllowAnyOrigin()
                   // .WithMethods("GET", "PUT", "POST", "DELETE", "OPTIONS")
                 //   .SetIsOriginAllowedToAllowWildcardSubdomains()
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
            app.UseAuthorization();
            app.UseCors();
            app.MapControllers();

            app.Run();
        }
    }
}