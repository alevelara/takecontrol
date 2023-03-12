namespace takecontrol.Application.Contracts.Persitence.Emails;

public interface IEmailUnitOfWork : IDisposable
{
    IEmailWriteRepository EmailWriteRepository();

    Task<int> CompleteAsync();
}
