using BookLibraryManagerApi.Data;
using BookLibraryManagerApi.Modules.Author;
using BookLibraryManagerApi.Modules.Book;
using BookLibraryManagerApi.Modules.Publisher;
using BookLibraryManagerApi.Modules.Rating;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextPool<BookManagerContext>(opt =>
{
    opt.UseNpgsql("Host=localhost;Database=BookLibraryManager;Username=postgres;Password=postgres", 
        x => x.UseNodaTime());
    opt.EnableSensitiveDataLogging();
});

builder.Services.AddCors();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors(opt =>
{
    opt.AllowAnyOrigin();
    opt.AllowAnyHeader();
    opt.AllowAnyMethod();
});

app.UseHttpsRedirection();
app.MapPublisherEndpoints();
app.MapRatingEndpoints();
app.MapBooksEndpoints();
app.MapAuthorEndpoints();


app.Run();