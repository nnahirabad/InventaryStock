using InventarioComercio.Application.Features.MovimientoStock.Commands;
using InventarioComercio.Application.Features.MovimientoStock.Queries.GetAllMovements;
using InventarioComercio.Application.Features.MovimientoStock.Queries.GetMovimientoByFecha;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InventarioComercio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoStockController : ControllerBase
    {
        private IMediator _mediator;

        public MovimientoStockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("byDate")]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMovementByDate([FromQuery] DateTime desde, [FromQuery] DateTime hasta)
        {
            var query = new GetMovimientoByFechaQuery(desde, hasta);
            var resultado = await _mediator.Send(query);
            return Ok(resultado); 
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllMovements()
        {
            var query = new GetAllMovementsQuery();
            var resultado = await _mediator.Send(query);
            return Ok(resultado);
        }


        [HttpPost(Name = "RegisterMovement")]
     
        public async Task<ActionResult<bool>> RegisterMovement([FromBody] RegistrarMovimientoCommand command)
        {
            return await _mediator.Send(command); 
        }




    }


   

}
