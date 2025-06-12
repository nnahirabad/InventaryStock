using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Features.Productos.Commands.UpdateProduct
{
    public  class UpdateCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("El Id del producto es obligatorio.");

            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.");

            RuleFor(p => p.Descripcion)
                .MaximumLength(250).WithMessage("La descripción no puede superar los 250 caracteres.");

           

            RuleFor(p => p.CategoriaId)
                .NotEmpty().WithMessage("Debe seleccionarse una categoría válida.");
        }
    }

}

