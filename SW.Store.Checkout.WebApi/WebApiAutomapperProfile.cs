using AutoMapper;
using SW.Store.Checkout.Read.ReadView;

namespace SW.Store.Checkout.WebApi
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
