using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Features.Usuarios.Commands.CreateUser
{
    public class CreateUserValidator : AbstractValidator<RegisterCommand>
    {

        public CreateUserValidator()
        {
            RuleFor(u => u.Nombre)
                .NotEmpty().WithMessage("Nombre no puede estar vacio")
                .MaximumLength(50).WithMessage("Nombre no debe superar los 50 caracteres");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email no puede estar vacio")
                .EmailAddress().WithMessage("El email no tiene formato valido");
            RuleFor(u => u.Username)
                .NotEmpty().WithMessage("Username no puede estar vacio");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("No puede estar vacio")
                .MinimumLength(6).WithMessage("La contraseña debe contener al menos 6 caracteres");

            RuleFor(u => u.Rol)
                .NotEmpty().WithMessage("Rol no puede estar vacio")
                .Must(rol => rol == "Admin" || rol == "Vendedor")
                .WithMessage("El rol ingresado no es valido"); 
        }
    }
}
