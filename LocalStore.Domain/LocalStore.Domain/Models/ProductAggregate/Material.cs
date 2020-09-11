using LocalStore.Commons;
using System.Collections.Generic;

namespace LocalStore.Domain.Models.ProductAggregate
{
    public class Material : ValueObject
    {
        public string Name { get; }

        public string Description { get; }

        public Material(string name, string description)
        {
            Name = name;
            Description = description;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time

            yield return Name;
            yield return Description;
        }
    }
}
