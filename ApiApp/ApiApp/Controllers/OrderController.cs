using ApiApp.Data;
using ApiApp.DTO;
using ApiApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public class OrderController : ControllerBase
    {
        private readonly DBData dB;

        public OrderController(DBData dB)
        {
            this.dB = dB;
        }

        [HttpGet("orders/{id}")]
        public async Task<IActionResult> GetOrders(int id)
        {
            var order = await dB.orders.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (order != null)
            {
                dtoOrder dto = new()
                {
                    orderId = order.Id,
                    Orderdate = order.Createdate,
                    orderItems = new List<dtoOrderItem>()
                };
                if (order.orderItems != null && order.orderItems.Any()) {
                    foreach (var ordritem in order.orderItems)
                    {
                        dtoOrderItem item = new()
                        {
                            itemId = ordritem.Id,
                            ItemName = ordritem.Items.Name,
                            price = ordritem.Price,
                            quantity = 1
                        };
                        dto.orderItems.Add(item);
                    }
                }
                return Ok(dto);
            }


            return NotFound($"OrderId {id} is not exist");
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] dtoOrder order)
        {
            if (ModelState.IsValid)
            {
                Order mdl = new()
                {
                    Createdate = order.Orderdate,
                    orderItems = new List<OrderItem>()
                };
                foreach (var item in order.orderItems)
                {
                    OrderItem orderItem = new()
                    {
                        ItemId = item.itemId,
                        Price = item.price,
                    };
                    mdl.orderItems.Add(orderItem);
                }
                await dB.orders.AddAsync(mdl);
                await dB.SaveChangesAsync();
                order.orderId = mdl.Id;
                return Ok(order);
            }
            return BadRequest();
        }


        /*  [HttpGet("orders/{id}")]
          public async Task<IActionResult> GetOrders(int id)
          {
              var order = dB.orders.ToList();
              // Assuming you're filtering orders based on the provided id
              var filteredOrders = order.Where(o => o.Id == id).ToList();
              return Ok(filteredOrders);
          }

          [HttpGet("[action]/{orderId:int}")]
          public async Task<IActionResult> GetOrder(int orderId)
          {
              var order = dB.orders.ToList();
              // Assuming you're fetching a single order based on orderId
              var singleOrder = order.FirstOrDefault(o => o.Id == orderId);
              if (singleOrder == null)
              {
                  return NotFound(); // Return 404 if order is not found
              }
              return Ok(singleOrder);
          }*/


        /* [HttpGet("{id}")] // <-- Added id parameter to the route
         public async Task<IActionResult> GetOrderById(int id)
         {
             var Order = await dB.orders.FindAsync(id);
             if (Order == null)
             {
                 return NotFound($"Order with ID {id} not found");
             }
             return Ok(Order);
         }
 */

        /*  [HttpGet("GetOrderByCategoryId/{CategoryId}")] // <-- Added id parameter to the route
          public async Task<IActionResult> GetOrderByCategoryId(int CategoryId)
          {
              var Order = await dB.orders.Where(x => x.CategoryId == CategoryId).ToListAsync();
              if (Order == null)
              {
                  return NotFound($"Order with ID {CategoryId} not found");
              }
              return Ok(Order);
          }*/
        /*   [HttpGet]
           public async Task<IActionResult> GetOrders()
           {
               var order = dB.orders.FirstOrDefault(x => x.Id == 1);

               if (order != null)
               {
                   var orderItem = order.orderItems.FirstOrDefault();

                   if (orderItem != null)
                   {
                       var items = orderItem.Items;
                       return Ok(items);
                   }
               }

               return NotFound(); // يُعيد Not Found في حالة عدم العثور على البيانات المطلوبة
           }
   */
        /*[HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] Order order)
        {

            await dB.orders.AddAsync(order);
            await dB.SaveChangesAsync();
            return Ok(order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderById(int id)
        {
            var Order = await dB.orders.FirstOrDefaultAsync(x => x.Id == id);
            if (Order == null)
            {
                return NotFound($"Order {id} is not found ");

            }
            dB.orders.Remove(Order);
            await dB.SaveChangesAsync();
            return Ok(Order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id)
        {
            var Order = dB.orders.FirstOrDefault(x => x.Id == id);
            if (Order == null)
            {
                return NotFound($"Order {id} is not found");
            }
            dB.orders.Update(Order);
            dB.SaveChanges();

            return Ok(Order);

        }*/


        /*    [HttpPost]
            public async Task<IActionResult> AddOrder([FromBody] string OrderName)
            {
                if (!string.IsNullOrEmpty(OrderName))
                {
                    Order Order = new Order { Name = OrderName };
                    await dB.orders.AddAsync(Order);
                    await dB.SaveChangesAsync();
                    return Ok(Order);
                }
                return BadRequest("Order name is required");
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order Order)
            {
                if (id != Order.Id)
                {
                    return BadRequest("Order ID mismatch");
                }

                var existingOrder = await dB.orders.FindAsync(id);
                if (existingOrder == null)
                {
                    return NotFound($"Order with ID {id} not found");
                }

                existingOrder.Name = Order.Name;
                await dB.SaveChangesAsync();
                return Ok(existingOrder);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteOrder(int id)
            {
                var Order = await dB.orders.FindAsync(id);
                if (Order == null)
                {
                    return NotFound($"Order with ID {id} not found");
                }

                dB.orders.Remove(Order);
                await dB.SaveChangesAsync();
                return Ok(Order);
            }

            [HttpPatch("{id}")]
            public async Task<IActionResult> UpdateOrderPatch(int id, [FromBody] JsonPatchDocument<Order> OrderPatch)
            {
                var Order = await dB.orders.FindAsync(id);
                if (Order == null)
                {
                    return NotFound($"Order with ID {id} not found");
                }

                OrderPatch.ApplyTo(Order, ModelState);
                await dB.SaveChangesAsync();
                return Ok(Order);
            }*/
    }
}
