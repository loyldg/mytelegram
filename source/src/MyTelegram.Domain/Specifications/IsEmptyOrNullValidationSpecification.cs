namespace MyTelegram.Domain.Specifications;

public class IsEmptyOrNullValidationSpecification : Specification<string>
{
    protected override IEnumerable<string> IsNotSatisfiedBecause(string obj)
    {
        if (string.IsNullOrEmpty(obj))
        {
            yield return $"{nameof(obj)} can not be empty";
        }
    }
}
