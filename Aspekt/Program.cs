using EvolutionaryArchitecture.Aspekt.Common.Clock;
using EvolutionaryArchitecture.Aspekt.Common.ErrorHandling;
using EvolutionaryArchitecture.Aspekt.Common.Events.EventBus;
using EvolutionaryArchitecture.Aspekt.Common.Validation.Requests;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddExceptionHandling();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEventBus();
builder.Services.AddRequestsValidations();
builder.Services.AddClock();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//ne-builder.Services.AddEndpointsApiExplorer();
//ne-builder.Services.AddSwaggerGen();

//builder.Services.AddApplications(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseApplications();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseErrorHandling();

app.MapControllers();

//app.MapApplications();

app.Run();

namespace EvolutionaryArchitecture.Aspekt   
{
    [UsedImplicitly]
    public sealed class Program;
}