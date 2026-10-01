namespace DebugBotDemo.Api.Models;

public record Item(int Id, string Name, string? Description, DateTime CreatedAt);

public record CreateItemRequest(string Name, string? Description);

public record PagedResult(List<Item> Items, int Total);
