﻿using MediatR;

namespace Takecontrol.Application.Abstractions.Mediatr;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
