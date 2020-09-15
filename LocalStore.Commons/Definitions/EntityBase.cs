using LocalStore.Commons.Services;
using System;

namespace LocalStore.Commons.Definitions
{
    public abstract class EntityBase : IEntity
    {
        protected EntityBase(IDateTimeService dateTimeService)
        {
            this.CreationDate = dateTimeService.GetCurrentDateTime();
        }

        public Guid Id { get; }

        public DateTime CreationDate { get; }

        protected EntityBase(Guid? id = null)
        {
            this.Id = id ?? Guid.NewGuid();
        }
 
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj.GetType() != this.GetType()) 
            {
                return false;
            }

            return obj.GetHashCode() == this.GetHashCode();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.GetType().Name, Id);
        }
    }
}
