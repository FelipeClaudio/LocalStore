﻿using System;

namespace LocalStore.Commons.Definitions
{
    public abstract class EntityBase : IEntity
    {
        protected EntityBase(Guid? id)
        {
            this.Id = id ?? Guid.NewGuid();
            this.CreationTime = DateTime.Now;
        }

        public Guid Id { get; }

        public DateTime CreationTime { get; }

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
