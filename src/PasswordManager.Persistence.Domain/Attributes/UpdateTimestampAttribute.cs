namespace PasswordManager.Persistence.Domain.Attributes;

[AttributeUsage(AttributeTargets.Property)]
internal sealed class UpdateTimestampAttribute(bool onInsert, bool onUpdate, bool useUtc) : Attribute
{
    public bool OnInsert => onInsert;

    public bool OnUpdate => onUpdate;
}