using LocalStore.Commons;

namespace LocalStore.Domain.Models.OrderAggregate
{
    public class Material : IValueObject
    {
        public string Name { get; set; }
    }
}
