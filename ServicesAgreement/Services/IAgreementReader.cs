using ServicesAgreement.Model.Schemas;

namespace ServicesAgreement.Services
{
    internal interface IAgreementReader
    {
        AgreementSchema Read();
    }
}