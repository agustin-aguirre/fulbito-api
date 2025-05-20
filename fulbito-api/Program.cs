using fulbito_api.Data;
using fulbito_api.Repositories;
using fulbito_api.Repositories.Interfaces;
using fulbito_api.Sanitizers;
using fulbito_api.Sanitizers.Interfaces;
using fulbito_api.Services;
using fulbito_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Adding DbContext
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));

// Add Application Services to the container.
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<ITournamentsService, TournamentsService>();

//Add Repositories here
builder.Services.AddScoped<IUsersRepo, UsersRepo>();
builder.Services.AddScoped<ITournamentsRepo, TournamentsRepo>();

// Add other services
builder.Services.AddAutoMapper(typeof(AppMapper));
builder.Services.AddScoped<ISanitizer, StringInputSanitizer>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
