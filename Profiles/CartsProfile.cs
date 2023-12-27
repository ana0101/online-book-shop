using AutoMapper;
using OnlineBookShop.Models;

namespace OnlineBookShop.Profiles
{
    public class CartsProfile : Profile
    {
        public CartsProfile()
        {
            CreateMap<Cart, CartDto>().ReverseMap();
        }
    }
}
