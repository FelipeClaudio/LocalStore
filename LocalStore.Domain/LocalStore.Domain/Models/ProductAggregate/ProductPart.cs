using LocalStore.Commons;
using LocalStore.Domain.Models.OrderAggregate;
using System.Collections.Generic;

namespace LocalStore.Domain.Models.ProductAggregate
{
    public class ProductPart : ValueObject, IMeasurable
    {
        public ProductPart(string name, string measuringUnit, decimal quantity, Material material)
        {
            this.MeasuringUnit = measuringUnit;
            this.Quantity = quantity;
            this.Name = name;
            this.Material = material;
        }

        public string Name { get; }

        public string MeasuringUnit { get; }

        public decimal Quantity { get; }

        public Material Material { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time

            yield return Name;
            yield return MeasuringUnit;
            yield return Quantity;
            yield return Material;
        }
    }
}
