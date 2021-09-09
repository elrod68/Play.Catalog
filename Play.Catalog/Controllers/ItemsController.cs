using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Play.Catalog.Controllers
{
    [ApiController]
    [Route("Items")]
    public class ItemsController:ControllerBase
    {
        private static readonly List<ItemDto> items = new()
        {
            new ItemDto(Guid.NewGuid(), "Potion", "Restores a small amount of HP", 5, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Antidote", "Cures poison", 7, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Bronze sword", "Just a bronze sword", 20, DateTimeOffset.UtcNow)
        };

        [HttpGet]
        public IEnumerable<ItemDto> Get()
        {
            return items;
        }

        //GET Items/{id}
        [HttpGet("id")]
        public ItemDto GetById(Guid id)
        {
            var item = items.Where(item => item.Id == id).SingleOrDefault();
            return item;
        }
    }
}
