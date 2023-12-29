using AutoMapper;
using OnlineBookShop.Entities;
using OnlineBookShop.Models;

namespace OnlineBookShop.Profiles
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile() 
        {
            CreateMap<Order, OrderDto>().ReverseMap();
        }
    }
}
