using LocalStore.Commons;

namespace LocalStore.Domain.Models.OrderAggregate
{
    public class ProductPart : IValueObject, IMeasurable
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
    }
}
