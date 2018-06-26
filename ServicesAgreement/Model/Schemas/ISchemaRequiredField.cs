namespace ServicesAgreement.Model.Schemas
{
    public interface ISchemaRequiredField
    {
        string Name { get; }
        int Level { get; }
        string Type { get; }
    }
}
