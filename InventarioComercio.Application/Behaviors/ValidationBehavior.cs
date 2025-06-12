using FluentValidation;
using InventarioComercio.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Any())
                {
                    var errorMessages = failures.Select(f => f.ErrorMessage).ToList();

                    // Obtener el tipo real de "T" en ResponseDto<T>
                    var responseType = typeof(TResponse);

                    if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(ResponseDto<>))
                    {
                        var innerType = responseType.GetGenericArguments()[0];
                        var method = typeof(ResponseDto<>)
                            .MakeGenericType(innerType)
                            .GetMethod(nameof(ResponseDto<object>.Failure))!;

                        var responseDto = method.Invoke(null, new object[] { errorMessages, "Errores de validación" });

                        return (TResponse)responseDto!;
                    }

                    throw new InvalidCastException("El tipo de respuesta esperado debe ser ResponseDto<T>.");
                }
            }

            return await next();
        }
    }

}
