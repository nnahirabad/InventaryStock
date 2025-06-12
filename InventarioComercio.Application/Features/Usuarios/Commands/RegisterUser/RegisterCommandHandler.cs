
using AutoMapper;
using InventarioComercio.Application.Contracts.Persistence;
using InventarioComercio.Application.Dtos;
using InventarioComercio.Application.Dtos.Identity;
using InventarioComercio.Application.Helpers;
using InventarioComercio.Application.Utils;
using InventarioComercio.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Features.Usuarios.Commands.CreateUser
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ResponseDto<AuthResponse>>
    {
        private readonly ILogger<RegisterCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly JwtTokenGenerator _jwtToken; 

        public RegisterCommandHandler(ILogger<RegisterCommandHandler> logger,
            IUnitOfWork unitOfWork, IMapper mapper, JwtTokenGenerator jwtToken)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtToken = jwtToken; 
        }

        public async Task<ResponseDto<AuthResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<AuthResponse>();

            try
            {
                // 1. Validaciones 
                var existeMail = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
                if (existeMail != null)
                {
                    _logger.LogWarning($"Este email ya esta registrado {request.Email}");
                    response.IsSuccess = false;
                    response.Message = "El email ya existe en la base de datos";
                    return response;
                }

                var username = await _unitOfWork.UserRepository.ExistByUsername(request.Username);
                if (username)
                {
                    _logger.LogWarning($"Este username ya esta registrado {request.Username}");
                    response.IsSuccess = false;
                    response.Message = "El username ya existe en la base de datos";
                    return response;
                }

                // 2. Mapear y hashear contraseña 
                var nuevoUsuario = _mapper.Map<Usuario>(request);
                nuevoUsuario.PasswordHash = PasswordHasher.Hash(request.Password);

                // 3. Guardar en BD
                _unitOfWork.UserRepository.AddEntity(nuevoUsuario);
                var result = await _unitOfWork.Complete();
                if (result <= 0)
                {
                    _logger.LogError("Error al guardar en la base de datos");
                    response.IsSuccess = false;
                    response.Message = "No se pudo guardar el usuario";
                    return response;
                }

                // 4. Generar token
                var token = _jwtToken.GenerateToken(nuevoUsuario.Email, nuevoUsuario.Username, nuevoUsuario.Rol);

                // 5. Devolver respuesta OK
                var authResponse = new AuthResponse
                {
                    Username = nuevoUsuario.Username,
                    Token = token,
                    Email = nuevoUsuario.Email
                };

                return ResponseDto<AuthResponse>.Success(authResponse, "Usuario registrado correctamente");
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error al guardar en la base de datos");
                return ResponseDto<AuthResponse>.Failure( new List<string> { dbEx.InnerException?.Message ?? dbEx.Message }, "Error al guardar en BD");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado");
                return ResponseDto<AuthResponse>.Failure( new List<string> { ex.Message }, "Error inseperado");
            }
        }

    }
}
