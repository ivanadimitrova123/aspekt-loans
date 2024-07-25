using Aspekt.Common.Clock;
using Aspekt.Common.ErrorHandling;
using Aspekt.Common.Events.EventBus;
using Aspekt.Common.Validation.Requests;
using Aspekt.Contacts;
using Aspekt.Applications;
using Aspekt.Loans;

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

builder.Services.AddContacts(builder.Configuration);
builder.Services.AddApplications(builder.Configuration);
builder.Services.AddLoans(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseContacts();
app.UseApplications();
app.UseLoans();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseErrorHandling();

app.MapControllers();

app.MapContacts();
app.MapApplications();
app.MapLoans();

app.Run();

namespace Aspekt   
{
    [UsedImplicitly]
    public sealed class Program;
}