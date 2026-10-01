using DebugBotDemo.Api.Models;
using DebugBotDemo.Api.Services;
using Xunit;

namespace DebugBotDemo.Api.Tests;

public class ItemsStoreTests
{
    [Fact]
    public void Add_NewItem_AssignsIncrementingId()
    {
        var store = new ItemsStore();

        var created = store.Add(new CreateItemRequest("Widget D", "a fresh item"));

        Assert.Equal(4, created.Id);
        Assert.Equal("Widget D", created.Name);
    }

    [Fact]
    public void GetPage_FirstPage_ReturnsFirstPageOfSeededItems()
    {
        var store = new ItemsStore();

        var result = store.GetPage(page: 1, pageSize: 2);

        Assert.Equal(3, result.Total);
        Assert.Equal(2, result.Items.Count);
        Assert.Equal("Widget A", result.Items[0].Name);
        Assert.Equal("Widget B", result.Items[1].Name);
    }

    [Fact]
    public void GetPage_EmptyStore_ReturnsEmptyAndZeroTotal()
    {
        var store = new ItemsStore();
        // Exhaust every seeded item via a page far beyond the data.
        var result = store.GetPage(page: 100, pageSize: 10);

        Assert.Empty(result.Items);
        Assert.Equal(3, result.Total);
    }
}
