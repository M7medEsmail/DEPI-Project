using AutoMapper;
using DEPI_DomainLayer.Entities;
using DEPI_GraduationProject.ViewModels;
using System.Text.RegularExpressions;

namespace DEPI_GraduationProject.Helper
{
    public class Mapping :Profile
    {
        public Mapping()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<RegisterVM, ApplicationUser>()
           .ForMember(dest => dest.UserName, opt => opt.MapFrom(src =>
           Regex.Replace($"{src.FirstName}{src.LastName}".ToLower(), @"[^a-zA-Z0-9]", "")));

            CreateMap<ProductBrandVM, ProductBrand>().ReverseMap();
            CreateMap<ProductTypeVM, ProductType>().ReverseMap();
            
;
            // .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<ProductPictureUrlResolver>()); // Change object to string
        }
    }
}
