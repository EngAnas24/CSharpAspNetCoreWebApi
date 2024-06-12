using CQRS_Lib.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CQRS_Lib.Data.Models;
using MediatR;
using CQRS_Lib.CQRS.Queries;
using CQRS_Lib.CQRS.Commands;

namespace CQRS_Test.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItems repo;
        private readonly IMediator mediator;

        public ItemController(IItems repo,IMediator mediator )
        {
            this.repo = repo;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            //  var item = repo.GetItems();
            var result = await mediator.Send(new GetAllItemsQuery());
            return Ok(result);
        }
        /*[HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item =  repo.GetItem(id);
            return Ok(item);
        }*/
        [HttpPost]
        public async Task<IActionResult> AddItem(Items items)
        {
            //  repo.AddItem(items);
            var result = await mediator.Send(new InsertItemCommand(items));
            return Ok(result);
        }


    }
}
