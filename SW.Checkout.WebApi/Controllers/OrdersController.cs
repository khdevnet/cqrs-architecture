using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SW.Checkout.Read.Extensibility;
using SW.Checkout.Read.ReadView;
using SW.Checkout.WebApi.Models;

namespace SW.Checkout.WebApi.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IOrderReadRepository orderReadRepository;

        public OrdersController(IMapper mapper,
            IOrderReadRepository orderReadRepository)
        {
            this.mapper = mapper;
            this.orderReadRepository = orderReadRepository;
        }

        ///GET api/orders/
        [HttpGet]
        [Route("getbyid/{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            OrderReadView order = orderReadRepository.GetById(id);

            if (order != null)
            {
                return Ok(new OrdersSummaryModel
                {
                    LastOrders = mapper.Map<IEnumerable<OrderReadModel>>(new[] { order })
                });
            }

            return Ok(new OrdersSummaryModel
            {
                LastOrders = new OrderReadModel[0]
            });
        }

        ///GET api/orders/
        [HttpGet]
        [Route("customer/{id}/last/{limit}")]
        public IActionResult GetLast([FromRoute] int id, int limit)
        {
            return Ok(new OrdersSummaryModel
            {
                LastOrders = mapper.Map<IEnumerable<OrderReadModel>>(orderReadRepository.GetByCustomer(id).TakeLast(limit)),
                Count = orderReadRepository.GetCountByCustomer(id)
            });
        }
    }
}
