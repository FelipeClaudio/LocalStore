using LocalStore.Commons.Definitions;
using System.Collections.Generic;

namespace LocalStore.Domain.Models.ProductAggregate
{
    public class Material : ValueObject
    {
        public string Name { get; }

        public string Description { get; }

        public decimal Price { get; set; }

        public Material(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time

            yield return Name;
            yield return Description;
            yield return Price;
        }
    }
}
