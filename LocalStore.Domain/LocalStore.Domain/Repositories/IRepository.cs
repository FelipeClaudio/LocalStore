using System;

namespace LocalStore.Domain.Repositories
{
    public interface IRepository<in T>
    {
        void DeleteById(Guid id);
        void Insert(T entity);
    }
}
