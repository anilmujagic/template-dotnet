using Microsoft.AspNetCore.Mvc;
using MyApp.Core.Dtos;
using MyApp.Core.Interfaces;

namespace MyApp.Api.Controllers;

[ApiController]
[Route("items")]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet]
    public async Task<IEnumerable<ItemDto>> Get([FromQuery] string? nameSearch)
    {
        return await _itemService.GetItems(nameSearch);
    }
}
