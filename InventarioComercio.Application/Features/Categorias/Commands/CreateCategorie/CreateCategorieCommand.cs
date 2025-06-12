using Amazon.Runtime.Internal;
using InventarioComercio.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Features.Categorias.Commands.CreateCategorie
{
    public class CreateCategorieCommand : IRequest<string>
    {

        public string Nombre { get; set; } = null!; 
    }
}
