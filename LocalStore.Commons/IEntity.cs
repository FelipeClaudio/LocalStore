using System;

namespace LocalStore.Commons
{
    public interface IEntity
    {
        Guid Id { get; }
        DateTime CreationDate { get; }
    }
}
