using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
    opt.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date",
        Example = new OpenApiString(DateTime.Today.ToString("yyyy-MM-dd"))
    })
);

var backendApi2Url = builder.Configuration.GetValue<string>("BackendAPIs:BackendAPI2");
var backendApi3Url = builder.Configuration.GetValue<string>("BackendAPIs:BackendAPI3");
builder.Services.AddHttpClient("BackendAPI2", client =>
{
    client.BaseAddress = new Uri(backendApi2Url);
});
builder.Services.AddHttpClient("BackendAPI3", client =>
{
    client.BaseAddress = new Uri(backendApi3Url);
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

app.MapControllers();

app.Run();
