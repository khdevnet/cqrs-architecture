using AutoMapper;
using SW.Store.Checkout.Extensibility.Client;
using SW.Store.Checkout.Extensibility.Messages;

namespace SW.Store.Checkout.WebApi
{
    public class WebApiAutomapperProfile : Profile
    {
        public WebApiAutomapperProfile()
        {
            CreateMap<CreateOrderRequestModel, CreateOrderMessage>();
        }
    }
}
