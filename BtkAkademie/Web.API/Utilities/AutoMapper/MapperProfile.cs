using AutoMapper;
using Entities.Concrete;
using Entities.DTOs;

namespace Web.API.Utilities.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<BookForUpdateDTO, Book>().ReverseMap();
            CreateMap<Book, BookDTO>();
            CreateMap<BookForInsertionDTO, Book>();
        }
    }
}
