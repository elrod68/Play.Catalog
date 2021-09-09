using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Entities;
using Play.Catalog.Repositories;
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
        //private static readonly List<ItemDto> items = new()
        //{
        //    new ItemDto(Guid.NewGuid(), "Potion", "Restores a small amount of HP", 5, DateTimeOffset.UtcNow),
        //    new ItemDto(Guid.NewGuid(), "Antidote", "Cures poison", 7, DateTimeOffset.UtcNow),
        //    new ItemDto(Guid.NewGuid(), "Bronze sword", "Just a bronze sword", 20, DateTimeOffset.UtcNow)
        //};

        private readonly ItemsRepository itemsRepository = new();

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetAsync()
        {
            var items = (await itemsRepository.GetAllAsync()).Select(item => item.AsDto());
            return items;
        }

        //GET Items/{id}
        [HttpGet("id")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
        {
            var item = await itemsRepository.GetAsync(id);
            if (item != null) return item.AsDto();
            else return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto)
        {

            var item = new Item()
            { Name= createItemDto.Name,
              Description= createItemDto.Description,
              Price= createItemDto.Price,
              CreatedDate=  DateTimeOffset.UtcNow
            };
                
            await itemsRepository.CreateAsync(item);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>  PutAsync(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = await itemsRepository.GetAsync(id);
            if (existingItem == null) return NotFound();
            else
            {
                existingItem.Name = updateItemDto.Name;
                existingItem.Description = updateItemDto.Description;
                existingItem.Price = updateItemDto.Price;
                await itemsRepository.UpdateAsync(existingItem);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var existingItem = await itemsRepository.GetAsync(id);
            if (existingItem == null) return NotFound();
            else
            {
                await itemsRepository.RemoveAsync(existingItem.Id);
            }

            return NoContent();
        }

    }
}
