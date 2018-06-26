namespace ServicesAgreement
{
    public interface IAgreementVerifer
    {
        IAgreementVerifer Provider(string providerName);
        IAgreementVerifer HasAgreementWith(string consumerName); 
        IAgreementVerifer DescriptedWith(string description);
        IAgreementVerifer WithMetaData(object metaData);
        IAgreementVerifer WithMessage(object message);
        IAgreementVerifer WithAgreementDestination(string destination);
        void Verify();
    }
}
