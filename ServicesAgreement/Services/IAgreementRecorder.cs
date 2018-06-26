using ServicesAgreement.Model.Schemas;

namespace ServicesAgreement.Services
{
    public interface IAgreementRecorder
    {
        void Record(AgreementSchema schema);
    }
}
