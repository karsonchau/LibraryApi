using Library.Application.Interfaces;
using Library.Application.Services;
using Library.Infrastructure.Repository;
using Library.Infrastructure.Scoring;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILibrarySearchService, LibrarySearchService>();
builder.Services.AddScoped<IScoreStrategy, BasicScoringStrategy>();
builder.Services.AddSingleton<ILibraryRepository, LibraryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
