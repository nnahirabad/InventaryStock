using InventarioComercio.Application.Features.Categorias.Commands.CreateCategorie;
using InventarioComercio.Application.Features.Categorias.Commands.DeleteCategorie;
using InventarioComercio.Application.Features.Categorias.Queries.GetCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventarioComercio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriaController(IMediator mediator)
        {
            _mediator = mediator; 
        }



        // GET: api/<CategoriaController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllCategoriesQuery();
            var resultado = await _mediator.Send(query);
            return Ok(resultado); 
        }


        // POST api/<CategoriaController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategorieCommand request)
        {
            var result = await  _mediator.Send(request);
            return Ok(result); 
        }

       

        // DELETE api/<CategoriaController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteCategorieCommand(id);
            var result = await _mediator.Send(command);
            return Ok(result); 
        }
    }
}
