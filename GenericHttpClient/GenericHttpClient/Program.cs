using GenericHttpClient.Application;
using GenericHttpClient.Application.Configuration;
using GenericHttpClient.Application.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// Bind configuration sections
builder.Services.Configure<EmailReputationConfig>(
    builder.Configuration.GetSection("EmailReputationConfig"));

builder.Services.Configure<ValidationApiConfig>(
    builder.Configuration.GetSection("ValidationApiConfig"));

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddGenericHttpClients(builder.Configuration, clients =>
{
    var sp = builder.Services.BuildServiceProvider();
    clients.AddClient(HttpClientsRegistry.EmailReputation(
        sp.GetRequiredService<IOptions<EmailReputationConfig>>()));

    clients.AddClient(HttpClientsRegistry.ValidationApi(
        sp.GetRequiredService<IOptions<ValidationApiConfig>>()));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();