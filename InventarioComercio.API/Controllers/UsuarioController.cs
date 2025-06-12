using InventarioComercio.Application.Dtos;
using InventarioComercio.Application.Dtos.Identity;
using InventarioComercio.Application.Features.Usuarios.Commands.CreateUser;
using InventarioComercio.Application.Features.Usuarios.Commands.DeleteUser;
using InventarioComercio.Application.Features.Usuarios.Queries.GetAllUsers;
using InventarioComercio.Application.Features.Usuarios.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventarioComercio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IMediator _mediator; 

        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator; 
        }





        // GET: api/<UsuarioController>
        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<UserDto>>> Get()
        {
            var query = new GetAllUsersQuery();
            return await _mediator.Send(query); 
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsuarioController>
        [HttpPost("Register")]
        public async Task<ActionResult<ResponseDto<AuthResponse>>> RegisterUser([FromBody] RegisterCommand command)
        {
             
            return await _mediator.Send(command);

        }

        [HttpPost("Login")]
        public async Task<ActionResult<ResponseDto<AuthResponse>>> Login([FromBody] LoginQuery query) 
        {


            return await _mediator.Send(query);
           
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete([FromQuery] Guid id)
        {
            var command = new DeleteUserCommand(id); 
            return await _mediator.Send(command); 
        }
    }
}
