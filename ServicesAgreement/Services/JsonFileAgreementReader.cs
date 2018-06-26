using System.IO;
using Newtonsoft.Json;
using ServicesAgreement.Model.Schemas;

namespace ServicesAgreement.Services
{
    internal class JsonFileAgreementReader : IAgreementReader
    {
        private readonly string path;

        public JsonFileAgreementReader(string path)
        {
            this.path = path;
        }
        public AgreementSchema Read()
        {
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<AgreementSchema>(json, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, ObjectCreationHandling = ObjectCreationHandling.Replace });
        }
    }
}