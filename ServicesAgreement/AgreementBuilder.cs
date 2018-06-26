using ServicesAgreement.Config;
using ServicesAgreement.Exceptions;
using ServicesAgreement.Factories;
using ServicesAgreement.Model.Schemas;
using ServicesAgreement.Services;

namespace ServicesAgreement
{
    public class AgreementBuilder : IAgreementBuilder
    {
        private readonly IServicesAgreementConfig config;
        private readonly IAgreementRecorder agreementRecorder;
        private readonly RequiredFieldFactory requiredFieldFactory;
        private string providerName;
        private string consumerName;
        private object metaData;
        private object message;
        private string description;

        public AgreementBuilder()
        {
            config = new ServicesAgreementDefaultConfig();
            agreementRecorder = new JsonFileAgreementRecorder(config);
            requiredFieldFactory = new RequiredFieldFactory();
        }

        public AgreementBuilder(IServicesAgreementConfig config)
        {
            this.config = config;
            agreementRecorder = new JsonFileAgreementRecorder(this.config);
            requiredFieldFactory = new RequiredFieldFactory();
        }


        public AgreementBuilder(IAgreementRecorder agreementRecorder)
        {
            config = new ServicesAgreementDefaultConfig();
            this.agreementRecorder = agreementRecorder;
            requiredFieldFactory = new RequiredFieldFactory();
        }

        public AgreementBuilder(IServicesAgreementConfig config, IAgreementRecorder agreementRecorder)
        {
            this.config = config;
            this.agreementRecorder = agreementRecorder;
            requiredFieldFactory = new RequiredFieldFactory();
        }

        public IAgreementBuilder Consumer(string consumerName)
        {
            this.consumerName = consumerName;
            return this;
        }

        public IAgreementBuilder ExpectsMessage(object message)
        {
            this.message = message;
            return this;
        }

        public IAgreementBuilder ExpectsMetaData(object metaData)
        {
            this.metaData = metaData;
            return this;
        }

        public IAgreementBuilder HasAgreementWith(string providerName)
        {
            this.providerName = providerName;
            return this;
        }

        public IAgreementBuilder DescriptedWith(string description)
        {
            this.description = description;
            return this;
        }

        public void Build()
        {
            ValidateSchema();
            var schemaRequiredFields = requiredFieldFactory.GetRequiredFields(message.GetType());
            var schema = new AgreementSchema(providerName, consumerName, description, metaData, message, schemaRequiredFields);
            agreementRecorder.Record(schema);
        }

        private void ValidateSchema()
        {
            if (string.IsNullOrEmpty(providerName))
                throw new RequiredFieldException("Provider name is required. Use HasAgreementWith method.");

            if (string.IsNullOrEmpty(consumerName))
                throw new RequiredFieldException("Consumer name is required. Use Consumer method.");

            if (message == null)
                throw new RequiredFieldException("Message object is required. Use ExpectsMessage method.");
        }
    }
}