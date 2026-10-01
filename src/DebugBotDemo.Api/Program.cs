using DebugBotDemo.Api.Models;
using DebugBotDemo.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ItemsStore>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.MapGet("/api/items", (ItemsStore store, int page = 1, int pageSize = 10) =>
{
    var result = store.GetPage(page, pageSize);
    return Results.Ok(result);
})
.WithName("GetItems")
.WithOpenApi();

app.MapPost("/api/items", (ItemsStore store, CreateItemRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.Name))
    {
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            ["name"] = new[] { "name is required" },
        });
    }
    var item = store.Add(request);
    return Results.Created($"/api/items/{item.Id}", item);
})
.WithName("CreateItem")
.WithOpenApi();

app.Run();

public partial class Program { }
