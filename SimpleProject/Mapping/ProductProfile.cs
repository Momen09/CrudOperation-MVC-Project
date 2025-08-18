using AutoMapper;
using SimpleProject.Models;

namespace SimpleProject.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductViewModel, Product>(); //.ForMember(dest => dest.ProductImages, opt => opt.Ignore())
                //.ForMember(dest => dest.Category, opt => opt.Ignore())
            //CreateMap<Category, CategoryViewModel>().ReverseMap();
            //CreateMap<ProductImages, ProductImagesViewModel>().ReverseMap();
        }
    }    
}
