using DebugBotDemo.Api.Models;

namespace DebugBotDemo.Api.Services;

public class ItemsStore
{
    private readonly List<Item> _items = new();
    private int _nextId = 1;

    public ItemsStore()
    {
        Add(new CreateItemRequest("Widget A", "The first seeded widget"));
        Add(new CreateItemRequest("Widget B", "The second seeded widget"));
        Add(new CreateItemRequest("Widget C", null));
    }

    public Item Add(CreateItemRequest request)
    {
        var item = new Item(_nextId++, request.Name, request.Description, DateTime.UtcNow);
        _items.Add(item);
        return item;
    }

    public PagedResult GetPage(int page, int pageSize)
    {
        var items = _items.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return new PagedResult(items, _items.Count);
    }
}
