using System.Collections.Generic;
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
    public IEnumerable<ItemDto> Get([FromQuery] string nameSearch)
    {
        return _itemService.GetItems(nameSearch);
    }
}
