using Amazon.Runtime.Internal;
using InventarioComercio.Application.Dtos;
using InventarioComercio.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Features.Categorias.Queries.GetCategories
{
    public class GetAllCategoriesQuery : IRequest<List<CategoriaDto>>
    {



    }
}
