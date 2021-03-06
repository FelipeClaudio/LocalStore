﻿using LocalStore.Commons.Definitions;
using System.Collections.Generic;

namespace LocalStore.Domain.Models.ProductAggregate
{
    public class ProductPart : ValueObject, IMeasurable
    {
        public ProductPart(string name, string measuringUnit, decimal quantity, ICollection<Material> materials)
        {
            this.MeasuringUnit = measuringUnit;
            this.Quantity = quantity;
            this.Name = name;
            this.Materials = materials;
        }

        public string Name { get; }

        public string MeasuringUnit { get; }

        public decimal Quantity { get; }

        public ICollection<Material> Materials { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time

            yield return Name;
            yield return MeasuringUnit;
            yield return Quantity;
            yield return Materials;
        }
    }
}
