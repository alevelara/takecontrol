﻿using MediatR;

namespace Takecontrol.Application.Abstractions.Mediatr;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
}
