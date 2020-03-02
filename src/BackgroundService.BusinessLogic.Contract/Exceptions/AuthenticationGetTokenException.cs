using System;
namespace BackgroundService.BusinessLogic.Contract.Exceptions
{
    public class AuthenticationGetTokenException : Exception
    {
        public AuthenticationGetTokenException(string message)
            : base(message)
        {
        }
    }
}
