using Amazon.Runtime.Internal;
using InventarioComercio.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Features.Productos.Queries.GetProductByName
{
    public class GetProductByNameQuery : IRequest<List<ProductoDto>>
    {

        public string Name { get; set; } = string.Empty;

        public GetProductByNameQuery(string nombre)
        {
            Name = nombre; 
        }

    }
}
