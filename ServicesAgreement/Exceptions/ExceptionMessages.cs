namespace ServicesAgreement.Exceptions
{
    public static class ExceptionMessages
    {
        private static readonly string SchemaRequires = "Consumer schema requires {0}";
        private static readonly string ButGet = "but get {1}";

        public static readonly string InvalidProviderName = $"{SchemaRequires} provider name, {ButGet}";
        public static readonly string InvalidConsumerName = $"{SchemaRequires} consumer name, {ButGet}";
        public static readonly string InvalidMetadata = $"{SchemaRequires} metadata, {ButGet}";
        public static readonly string ProviderSchemaBreaksAgreement = "Provider schema breaks agreement because {0} required field was not found";
    }
}
