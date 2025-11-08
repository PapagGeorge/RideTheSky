using GenericHttpClient.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

 builder.Services.AddGenericHttpClients(clients =>
 {
     clients
         .AddClient("UsersApi", "https://api.users.com")
         .AddClient("ProductsApi", "https://api.products.com")
         .AddClient("OrdersApi", "https://api.orders.com");
 });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();