using System;

namespace Api.Exceptions
{
    public class NotModifiedException : Exception
    {
        public NotModifiedException(string message) : base(message)
        {
        }
    }
}