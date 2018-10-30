using AutoMapper;
using SW.Checkout.Read.ReadView;
using SW.Checkout.WebApi.Models;

namespace SW.Checkout.WebApi
{
    public class WebApiAutomapperProfile : Profile
    {
        public WebApiAutomapperProfile()
        {
            CreateMap<OrderReadView, OrderReadModel>();
            CreateMap<OrderLineReadView, OrderLineReadModel>();
        }
    }
}
