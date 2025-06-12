using Amazon.Runtime.Internal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Features.Categorias.Commands.DeleteCategorie
{
    public class DeleteCategorieCommand : IRequest<string>
    {

        public Guid Id { get; set;  }

        public DeleteCategorieCommand(Guid id)
        {
            Id = id; 
        }


    }
}
