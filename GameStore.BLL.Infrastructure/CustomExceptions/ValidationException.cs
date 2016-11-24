using System;
using System.Collections.Generic;

namespace GameStore.BLL.Infrastructure.CustomExceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message, Dictionary<string, string> errors = null) : base(message)
        {
            Errors = errors;
        }

        public ValidationException()
        {
        }

        public Dictionary<string, string> Errors { get; protected set; }
    }
}