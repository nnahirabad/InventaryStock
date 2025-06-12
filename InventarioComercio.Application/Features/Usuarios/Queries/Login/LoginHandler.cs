
using InventarioComercio.Application.Contracts.Persistence;
using InventarioComercio.Application.Dtos;
using InventarioComercio.Application.Dtos.Identity;
using InventarioComercio.Application.Helpers;
using InventarioComercio.Application.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace InventarioComercio.Application.Features.Usuarios.Queries.Login
{
    public class LoginHandler : IRequestHandler<LoginQuery, ResponseDto<AuthResponse>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<LoginHandler> _logger;
        private readonly JwtTokenGenerator _generatorToken; 

        public LoginHandler(IUnitOfWork unitOfWork, ILogger<LoginHandler> logger, JwtTokenGenerator generator)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _generatorToken = generator;
        }

        public async Task<ResponseDto<AuthResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            //// LOGICA 
            //  1. Buscar usuario por mail desde el repositorio. 
            //  2. Si no existe, lanzar excepcion
            //  3. Verificar que la contraseña coincida usando Validate. 
            //  4. Si no coincide, lanzar excepcion. 
            //  5. Crear un JWT Token con Email, Username, Rol
            //  / Tiempo de expiracion
            //  / Firma con key secret
            //  6. Devolver un AuthResponse. 
            var response = new ResponseDto<AuthResponse>(); 
            try {
                var usuario = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
                if (usuario == null)
                {
                    _logger.LogWarning($"No existe usuario registrado con mail: {request.Email}");
                    throw new Exception("Datos incorrectos");

                }
                var contraseña = PasswordHasher.Verify(request.Password, usuario.PasswordHash);
                if (contraseña.Equals(false))
                {
                    _logger.LogWarning("La contraseña no coincide con el mail registrado");
                    throw new Exception("Contraseña incorrecta");
                }

                var token = _generatorToken.GenerateToken(usuario.Email, usuario.Username, usuario.Rol);

                var authResponse = new AuthResponse
                {
                    Username = usuario.Username,
                    Token = token,
                    Email = usuario.Email,
                    Rol = usuario.Rol
                };


                return ResponseDto<AuthResponse>.Success(authResponse, "Inicio de sesion exitoso");

            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error al guardar en la base de datos");
                return ResponseDto<AuthResponse>.Failure(new List<string> { dbEx.InnerException?.Message ?? dbEx.Message }, "Error al guardar en BD");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado");
                return ResponseDto<AuthResponse>.Failure(new List<string> { ex.Message }, "Error inseperado");
            }

            











        }


    }
}
