using Amazon.Runtime.Internal;
using InventarioComercio.Application.Dtos;
using InventarioComercio.Application.Features.MovimientoStock.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Features.MovimientoStock.Queries.GetMovimientoByFecha
{
    public class GetMovimientoByFechaQuery : IRequest<List<MovimientoStockDto>>
    {
        public DateTime Desde { get; set;  }

        public DateTime Hasta { get; set;  }  

        public GetMovimientoByFechaQuery(DateTime desde, DateTime hasta)
        {
            Desde = desde;
            Hasta = hasta; 
        }
    }
}
