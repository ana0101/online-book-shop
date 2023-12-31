using AutoMapper;
using OnlineBookShop.Entities;
using OnlineBookShop.Models;

namespace OnlineBookShop.Profiles
{
    public class BookOrdersProfile : Profile
    {
        public BookOrdersProfile() 
        {
            CreateMap<BookOrder, BookOrderDto>().ReverseMap();
        }
    }
}
