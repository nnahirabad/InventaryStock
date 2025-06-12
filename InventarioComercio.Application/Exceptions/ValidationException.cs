using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<string> Errors { get; }

        public ValidationException(IEnumerable<string> errors)
            : base("Se produjeron errores de validación.")
        {
            Errors = errors.ToList();
        }
    }
}
