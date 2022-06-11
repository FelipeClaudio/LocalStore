using AutoMapper;
using LocalStore.Domain.Models.ProductAggregate;
using System.Linq;

namespace LocalStore.Infrastructure.Database
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Domain.Models.OrderAggregate.OrderItem, Infrastructure.Database.Orders.Models.OrderItem>();
            CreateMap<Infrastructure.Database.Orders.Models.OrderItem, Domain.Models.OrderAggregate.OrderItem>();
            CreateMap<Domain.Models.OrderAggregate.Order, Infrastructure.Database.Orders.Models.Order>();
            CreateMap<Infrastructure.Database.Orders.Models.Order, Domain.Models.OrderAggregate.Order>();

            CreateMap<Domain.Models.ProductAggregate.Material, Infrastructure.Database.Products.Models.Material>();
            CreateMap<Infrastructure.Database.Products.Models.Material, Domain.Models.ProductAggregate.Material>();
            CreateMap<Domain.Models.ProductAggregate.ProductPart, Infrastructure.Database.Products.Models.ProductPart>();
            CreateMap<Infrastructure.Database.Products.Models.ProductPart, Domain.Models.ProductAggregate.ProductPart>();
            CreateMap<Domain.Models.ProductAggregate.Product, Infrastructure.Database.Products.Models.Product>();
            CreateMap<Infrastructure.Database.Products.Models.Product, Domain.Models.ProductAggregate.Product>()
                .ForMember(product => product.ProductParts, opt => opt.MapFrom(product => product.ProductParts.Select(productPart =>
                    new ProductPart(productPart.Name, productPart.MeasuringUnit, productPart.Quantity,
                    productPart.ProductPartMaterials.Select(productPartMaterial => 
                        new Material(productPartMaterial.Material.Name, productPartMaterial.Material.Description, productPartMaterial.Material.Price)).ToList()
                ))));
        }
    }
}
