using System;

namespace LocalStore.Commons
{
    public abstract class EntityBase : IEntity
    {
        public DateTime CreationDate => DateTime.Now;

        public Guid Id { get; }

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
