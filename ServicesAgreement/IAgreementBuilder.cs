using System;

namespace ServicesAgreement
{
    public interface IAgreementBuilder
    {
        IAgreementBuilder Consumer(string consumerName);
        IAgreementBuilder HasAgreementWith(string providerName);
        IAgreementBuilder DescriptedWith(string description);
        IAgreementBuilder ExpectsMetaData(object metaData);
        IAgreementBuilder ExpectsMessage<T>(T message) where T: class;
        IAgreementBuilder ExpectsMessageWithSpecificFields(object message);
        void Build();
    }
}