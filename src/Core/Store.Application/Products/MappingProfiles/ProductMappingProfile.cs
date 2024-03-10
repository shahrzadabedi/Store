using AutoMapper;
using Store.Application.Products.Dtos;
using Store.Domain.Entities;

namespace Store.Application.Products.MappingProfiles;
public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, GetProductByIdResponse>();
    }
}
