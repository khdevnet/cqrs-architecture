using AutoMapper;
using SW.Store.Checkout.Extensibility.Dto;
using SW.Store.Checkout.Extensibility.Messages;

namespace SW.Store.Checkout.WebApi
{
    public class WebApiAutomapperProfile : Profile
    {
        public WebApiAutomapperProfile()
        {
            CreateMap<OrderDto, CreateOrderMessage>();
            CreateMap<OrderLineDto, OrderLineMessage>();
        }
    }
}
