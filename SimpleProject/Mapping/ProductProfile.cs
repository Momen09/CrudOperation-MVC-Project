using AutoMapper;
using SimpleProject.Models;
using SimpleProject.ViewModels.Products;

namespace SimpleProject.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<AddProductViewModel, Product>();
            CreateMap<Product, GetProductListViewModel>()
                .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr,src.NameEn)));

            CreateMap<Product, UpdateProductViewModel>()
                                .ForMember(des => des.CurrentPaths, opt => opt.MapFrom(src => src.ProductImages.Select(x=>x.Path).ToList()));
            CreateMap<UpdateProductViewModel, Product>();



            //.ForMember(dest => dest.ProductImages, opt => opt.Ignore())
            //.ForMember(dest => dest.Category, opt => opt.Ignore())
            //CreateMap<Category, CategoryViewModel>().ReverseMap();
            //CreateMap<ProductImages, ProductImagesViewModel>().ReverseMap();
        }
    }
    
}
