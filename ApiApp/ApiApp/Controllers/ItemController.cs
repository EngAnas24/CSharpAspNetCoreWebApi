using ApiApp.Data;
using ApiApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace ApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly DBData dB;

        public ItemController(DBData dB)
        {
            this.dB = dB;
        }
        [HttpGet("{id}")] // <-- Added id parameter to the route
        public async Task<IActionResult> GetItemById(int id)
        {
            var Item = await dB.items.FindAsync(id);
            if (Item == null)
            {
                return NotFound($"Item with ID {id} not found");
            }
            return Ok(Item);
        }
        [HttpGet("GetItemByCategoryId/{CategoryId}")] // <-- Added id parameter to the route
        public async Task<IActionResult> GetItemByCategoryId(int CategoryId)
        {
            var Item = await dB.items.Where(x=>x.CategoryId== CategoryId).ToListAsync();
            if (Item == null)
            {
                return NotFound($"Item with ID {CategoryId} not found");
            }
            return Ok(Item);
        }
        [HttpGet] // <-- No parameters in the route
        public async Task<IActionResult> Getitems()
        {
            var items = await dB.items.ToListAsync();
            if (items == null || items.Count == 0)
            {
                return NotFound("Item list is empty");
            }
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem([FromForm] mlItem mlItem)
        {
            using var stream = new MemoryStream();
            await mlItem.Image.CopyToAsync(stream);
            var item = new Item
            {
                Name = mlItem.Name,
                Price = mlItem.Price,
                Notes = mlItem.Notes,
                CategoryId = mlItem.CategoryId,
                Image = stream.ToArray()
            };
            await dB.items.AddAsync(item);
            await dB.SaveChangesAsync();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemById(int id)
        {
            var item = await dB.items.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                return NotFound($"Item {id} is not found ");

            }
                dB.items.Remove(item);
            await dB.SaveChangesAsync();    
            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id,[FromForm]mlItem ml)
        {
            var item = dB.items.FirstOrDefault(x => x.Id == id);
            if(item == null)
            {
                return NotFound($"Item {id} is not found");
            }
            var Iscategoryexist = await dB.categories.AnyAsync(x => x.Id == ml.CategoryId);
            if (!Iscategoryexist)
            {
                return NotFound($"Category {id} is not found");

            }
            if (ml.Image != null)
            {
               using var memory = new MemoryStream();
                  await  ml.Image.CopyToAsync(memory);
                item.Image = memory.ToArray(); 
            }
            item.Name = ml.Name;
            item.Price = ml.Price;
            item.Notes = ml.Notes;
            item.CategoryId = ml.CategoryId;
                
             dB.SaveChanges();

            return Ok(item);

        }


        /*    [HttpPost]
            public async Task<IActionResult> AddItem([FromBody] string ItemName)
            {
                if (!string.IsNullOrEmpty(ItemName))
                {
                    Item Item = new Item { Name = ItemName };
                    await dB.items.AddAsync(Item);
                    await dB.SaveChangesAsync();
                    return Ok(Item);
                }
                return BadRequest("Item name is required");
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateItem(int id, [FromBody] Item Item)
            {
                if (id != Item.Id)
                {
                    return BadRequest("Item ID mismatch");
                }

                var existingItem = await dB.items.FindAsync(id);
                if (existingItem == null)
                {
                    return NotFound($"Item with ID {id} not found");
                }

                existingItem.Name = Item.Name;
                await dB.SaveChangesAsync();
                return Ok(existingItem);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteItem(int id)
            {
                var Item = await dB.items.FindAsync(id);
                if (Item == null)
                {
                    return NotFound($"Item with ID {id} not found");
                }

                dB.items.Remove(Item);
                await dB.SaveChangesAsync();
                return Ok(Item);
            }

            [HttpPatch("{id}")]
            public async Task<IActionResult> UpdateItemPatch(int id, [FromBody] JsonPatchDocument<Item> ItemPatch)
            {
                var Item = await dB.items.FindAsync(id);
                if (Item == null)
                {
                    return NotFound($"Item with ID {id} not found");
                }

                ItemPatch.ApplyTo(Item, ModelState);
                await dB.SaveChangesAsync();
                return Ok(Item);
            }*/
    }
}
