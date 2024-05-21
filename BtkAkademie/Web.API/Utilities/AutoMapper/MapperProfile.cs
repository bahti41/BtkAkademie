using AutoMapper;
using Entities.Concrete;
using Entities.DTOs;
using Entities.DTOs.Category;

namespace Web.API.Utilities.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<BookForUpdateDTO, Book>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<BookForInsertionDTO, Book>().ReverseMap();

            CreateMap<UserForRegisterDTO, User>().ReverseMap();

            CreateMap<CategoryDorUpdateDTO, Category>().ReverseMap();
            CreateMap<CategoryForInsertionDTO, Category>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
