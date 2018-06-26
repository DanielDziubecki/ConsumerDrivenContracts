using System.IO;
using System.Text;
using Newtonsoft.Json;
using ServicesAgreement.Config;
using ServicesAgreement.Model.Schemas;

namespace ServicesAgreement.Services
{
    internal class JsonFileAgreementRecorder : IAgreementRecorder
    {
        private readonly IServicesAgreementConfig servicesAgreementConfig;
        private const string JsonExtension = ".json";

        public JsonFileAgreementRecorder(IServicesAgreementConfig servicesAgreementConfig)
        {
            this.servicesAgreementConfig = servicesAgreementConfig;
        }

        public void Record(AgreementSchema schema)
        {
            if (!Directory.Exists(servicesAgreementConfig.AgreementPath))
            {
                Directory.CreateDirectory(servicesAgreementConfig.AgreementPath);
            }
         
            var serializedSchema = JsonConvert.SerializeObject(schema, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, ObjectCreationHandling = ObjectCreationHandling.Replace });
            File.WriteAllText(Path.Combine(servicesAgreementConfig.AgreementPath, $"{schema.Provider}-{schema.Consumer}-{schema.Message.GetType().Name}{JsonExtension}"),serializedSchema,Encoding.UTF8);
        }
    }
}