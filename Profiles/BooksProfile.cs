using AutoMapper;
using OnlineBookShop.Models;

namespace OnlineBookShop.Profiles
{
    public class BooksProfile : Profile
    {
        public BooksProfile() 
        {
            CreateMap<Book, BookDtoCreate>().ReverseMap();
        }
    }
}
