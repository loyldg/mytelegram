namespace MyTelegram.Domain;

public static class Specs
{
    public static ISpecification<IAggregateRoot> AggregateIsCreated { get; } = new AggregateIsCreatedSpecification();
    public static ISpecification<IAggregateRoot> AggregateIsNew { get; } = new AggregateIsNewSpecification();

    //public static ISpecification<ChatAdminRights> HasChangeInfoRights { get; } = new AdminRightsSpecification();
    public static ISpecification<string> IsEmailAddress { get; } = new IsEmailAddressSpecification();
    public static ISpecification<string> IsNotEmptyOrNull { get; } = new IsEmptyOrNullValidationSpecification();

    private class AggregateIsCreatedSpecification : Specification<IAggregateRoot>
    {
        protected override IEnumerable<string> IsNotSatisfiedBecause(IAggregateRoot obj)
        {
            if (obj.IsNew)
            {
                yield return $"Aggregate '{obj.Name}' with ID '{obj.GetIdentity()}' is new";
            }
        }
    }
}
