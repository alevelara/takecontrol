using takecontrol.Domain.Primitives;

namespace takecontrol.Application.Exceptions;

public sealed class NotFoundException : BaseException
{
	public NotFoundException(DomainError error) : base(error)
	{

	}
}
