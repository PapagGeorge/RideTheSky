using GenericHttpClient.Application;
using GenericHttpClient.Application.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddGenericHttpClients(builder.Configuration, clients =>
 {
     clients
         .AddClient("EmailReputationApi", "https://emailreputation.abstractapi.com")
         .AddClient("ValidationApi", "https://api.apyhub.com", httpClientBuilder =>
         {
             // Get the API key from configuration
             var config = builder.Configuration.GetSection("ValidationApiConfig")
                 .Get<ValidationApiConfig>();

             var apiKey = config?.VatValidation?.ApiKey ?? string.Empty;

             httpClientBuilder.ConfigureHttpClient(httpClient =>
             {
                 httpClient.DefaultRequestHeaders.Add("apy-token", apiKey);
             });
         });
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