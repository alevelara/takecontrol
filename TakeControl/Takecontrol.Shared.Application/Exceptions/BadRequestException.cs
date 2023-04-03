using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Shared.Application.Exceptions;

public sealed class BadRequestException : BaseException
{
    public BadRequestException(DomainError error) : base(error)
    {
    }
}
