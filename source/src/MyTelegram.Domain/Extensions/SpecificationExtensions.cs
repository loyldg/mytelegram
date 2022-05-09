using EventFlow.Exceptions;

namespace MyTelegram.Domain.Extensions;

public static class SpecificationExtensions
{
    public static void ThrowFirstDomainErrorIfNotSatisfied<T>(this ISpecification<T> specification,
        T obj)
    {
        if (specification == null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var whyIsNotSatisfiedBy = specification.WhyIsNotSatisfiedBy(obj).ToList();
        var firstDomainError = whyIsNotSatisfiedBy.FirstOrDefault();
        if (firstDomainError != null)
        {
            throw DomainError.With(firstDomainError);
        }
    }
}
