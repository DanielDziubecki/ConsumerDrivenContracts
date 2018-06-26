using System;

namespace ServicesAgreement.Exceptions
{
    public class RequiredFieldException : Exception
    {
        public RequiredFieldException(string message): base(message)
        {

        }
    }
}
