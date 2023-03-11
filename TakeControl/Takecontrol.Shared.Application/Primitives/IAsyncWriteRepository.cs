﻿using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Shared.Application.Contracts.Persitence.Primitives;

public interface IAsyncWriteRepository<T>
    where T : BaseDomainModel
{
    Task<T> AddAsync(T entity);

    T UpdateAsync(T entity);

    void DeleteAsync(T entity);
}