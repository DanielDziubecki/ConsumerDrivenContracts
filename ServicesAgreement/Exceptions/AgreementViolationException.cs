using System;

namespace ServicesAgreement.Exceptions
{
    public class AgreementViolationException : Exception
    {
        public AgreementViolationException(string message) : base(message)
        {
        }
    }
}
