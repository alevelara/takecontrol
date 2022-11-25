using MediatR;

namespace takecontrol.Application.Contracts.Logger;

public interface ILog
{
    void Info(string message); 
    void Error(string message);
    void Warn(string message);
}
