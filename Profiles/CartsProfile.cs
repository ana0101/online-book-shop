using AutoMapper;
using OnlineBookShop.Entities;
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
