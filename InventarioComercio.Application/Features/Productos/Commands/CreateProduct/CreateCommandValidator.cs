using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Features.Productos.Commands.CreateProduct
{
    public class CreateCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(p => p.Nombre)
            .NotEmpty().WithMessage("El nombre del producto es obligatorio.")
            .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.")
            .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.");

            RuleFor(p => p.StockActual)
                .GreaterThanOrEqualTo(0).WithMessage("El stock actual no puede ser negativo.");



        }

    }
}
