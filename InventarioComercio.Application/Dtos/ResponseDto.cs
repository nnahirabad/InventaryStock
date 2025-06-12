﻿

namespace InventarioComercio.Application.Dtos
{
    public class ResponseDto<T>
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; } = string.Empty;

        public List<string> Errors { get; set; } = new();

        public T? Data { get; set; }

        // Métodos estáticos para facilitar la creación de respuestas
        public static ResponseDto<T> Success(T data, string message = "")
        {
            return new ResponseDto<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message
            };
        }

        public static ResponseDto<T> Failure(List<string> errors, string message = "Se produjeron errores.")
        {
            return new ResponseDto<T>
            {
                IsSuccess = false,
                Errors = errors,
                Message = message
            };
        }
    }

}
