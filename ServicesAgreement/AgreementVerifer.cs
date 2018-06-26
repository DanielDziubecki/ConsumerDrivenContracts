using ServicesAgreement.Exceptions;
using ServicesAgreement.Factories;
using ServicesAgreement.Model.Schemas;
using ServicesAgreement.Services;

namespace ServicesAgreement
{
    public class AgreementVerifer : IAgreementVerifer
    {
        private IAgreementReader agreementReader;
        private readonly RequiredFieldFactory requiredFieldFactory;
        private string providerName;
        private string consumerName;
        private object metaData;
        private object message;
        private string description;

        public AgreementVerifer()
        {
            requiredFieldFactory = new RequiredFieldFactory();
        }

        public IAgreementVerifer Provider(string providerName)
        {
            this.providerName = providerName;
            return this;
        }

        public IAgreementVerifer HasAgreementWith(string consumerName)
        {
            this.consumerName = consumerName;
            return this;
        }

        public IAgreementVerifer DescriptedWith(string description)
        {
            this.description = description;
            return this;
        }

        public IAgreementVerifer WithMetaData(object metaData)
        {
            this.metaData = metaData;
            return this;
        }

        public IAgreementVerifer WithMessage(object message)
        {
            this.message = message;
            return this;
        }

        public IAgreementVerifer WithAgreementDestination(string destination)
        {
            this.agreementReader = new JsonFileAgreementReader(destination);
            return this;
        }

        private void ValidateVerifier()
        {
            if (string.IsNullOrEmpty(providerName))
                throw new RequiredFieldException("Provider name is required. Use Provider method.");

            if (string.IsNullOrEmpty(consumerName))
                throw new RequiredFieldException("Consumer name is required. Use HasAgreementWith method.");

            if (message == null)
                throw new RequiredFieldException("Message object is required. Use WithMessage method.");
        }

        public void Verify()
        {
            ValidateVerifier();
            var schemaRequiredFields = requiredFieldFactory.GetRequiredFields(message.GetType());
            var provierSchema = new AgreementSchema(providerName, consumerName, description, metaData, message, schemaRequiredFields);
            var consumerSchema = agreementReader.Read();
            provierSchema.Equals(consumerSchema);
        }
    }
}