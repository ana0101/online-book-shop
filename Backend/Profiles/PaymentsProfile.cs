using AutoMapper;
using OnlineBookShop.Entities;
using OnlineBookShop.Models;

namespace OnlineBookShop.Profiles
{
    public class PaymentsProfile : Profile
    {
        public PaymentsProfile() 
        { 
            CreateMap<Payment, PaymentDto>().ReverseMap();
        }
    }
}
