using System;

namespace GameStore.BLL.Infrastructure.CustomExceptions
{
    public class AccessException : Exception
    {
        public AccessException(string message, string prop, Exception innerException = null)
            : base(message, innerException)
        {
            Property = prop;
        }

        public AccessException()
        {
        }

        public string Property { get; protected set; }
    }
}