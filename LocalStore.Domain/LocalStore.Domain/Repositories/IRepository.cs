using System;

namespace LocalStore.Domain.Repositories
{
    public interface IRepository<in T>
    {
        void Delete(Guid id);
        void Insert(T entity);
    }
}
