using System.IO;

namespace ServicesAgreement.Config
{
    public class ServicesAgreementDefaultConfig : IServicesAgreementConfig
    {
        private const string DefaultDir = "Agreements";

        public string AgreementPath => Path.Combine(Directory.GetCurrentDirectory(), DefaultDir);
    }
}
