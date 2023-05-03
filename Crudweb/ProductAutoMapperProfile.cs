using AutoMapper;
using CRUD.Domain.Employee;
using CRUDWeb.Models;
using System.Text;

namespace CRUDWeb
{
    public class ProductAutoMapperProfile : Profile
    {
        public ProductAutoMapperProfile()
        {
            CreateEmployeeMap();       
        }
        private const string Folder = "images";
        private void CreateEmployeeMap()
        {
            CreateMap<ProductRequestModel, Product>();
            CreateMap<Product, ProductRequestModel>()
                .ForMember(x => x.Image, o => o.MapFrom(x => !string.IsNullOrEmpty(x.ImageURL) ? new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("")), 0, 0, Path.GetFileName(x.ImageURL), Path.GetFileName(x.ImageURL)) : null))
                .ForMember(x => x.ImageName, o => o.MapFrom(x => !string.IsNullOrEmpty(x.ImageURL) ? Path.Combine(Folder, Path.GetFileName(x.ImageURL)): null));
        }
    }
}
