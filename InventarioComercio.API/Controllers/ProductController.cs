using InventarioComercio.Application.Dtos;
using InventarioComercio.Application.Features.Productos.Commands.CreateProduct;
using InventarioComercio.Application.Features.Productos.Commands.DeleteProduct;
using InventarioComercio.Application.Features.Productos.Commands.UpdateProduct;
using InventarioComercio.Application.Features.Productos.Queries.GetAllProducts;
using InventarioComercio.Application.Features.Productos.Queries.GetProductByName;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventarioComercio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // GET Productos : api/<ProductController>
        [HttpGet(Name = "GetProductos")]
        public async Task<IActionResult> GetProducts()
        {
            var query = new GetAllProductsQuery();
            var resultado = await _mediator.Send(query); 
            return Ok(resultado);
        }

        // GET Product By Name 
        [HttpGet("by-name")]
        public async Task<IActionResult> GetProductByName([FromQuery] string name)
        {
            var query = new GetProductByNameQuery(name);
            var resultado = await _mediator.Send(query);
            if(resultado == null)
            {
                return NotFound(); 
            }
            return Ok(resultado); 

        }

        // POST Product api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand request)
        {
            
            var result = await _mediator.Send(request);
            return Ok(result); 
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand request)
        {
            if (id != request.Id)
                return BadRequest("El ID del producto no coincide.");

            var result = await _mediator.Send(request);
            return Ok(result);

        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Guid>> Delete(Guid id)
        {
            var command = new DeleteProductCommand(id);
            return  await _mediator.Send(command);
           
        }
    }
}
