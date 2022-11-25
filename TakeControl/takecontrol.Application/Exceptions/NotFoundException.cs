namespace takecontrol.Application.Exceptions;

public sealed class NotFoundException : ApplicationException
{
	public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) has not been found.")
	{

	}
}
