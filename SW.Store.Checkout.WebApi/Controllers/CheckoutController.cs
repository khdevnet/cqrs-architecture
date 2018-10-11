﻿using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SW.Store.Checkout.Domain;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Checkout.Extensibility.Client;
using SW.Store.Checkout.Extensibility.Messages;
using SW.Store.Checkout.Read.Extensibility;
using SW.Store.Core.Messages;

namespace SW.Store.Checkout.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly IMessageSender messageSender;
        private readonly ICustomerRepository customerRepository;
        private readonly IOrderReadRepository orderReadRepository;
        private readonly IMapper mapper;

        public CheckoutController(
            IMessageSender messageSender,
            ICustomerRepository customerRepository,
            IOrderReadRepository orderReadRepository,
            IMapper mapper)
        {
            this.messageSender = messageSender;
            this.customerRepository = customerRepository;
            this.orderReadRepository = orderReadRepository;
            this.mapper = mapper;
        }

        // POST api/Orders
        [HttpPost]
        public void Post([FromBody] CreateOrderRequestModel createOrder)
        {
            messageSender.Send("localhost", "processorder", "processorder", mapper.Map<CreateOrderMessage>(createOrder));
        }

        // POST api/Orders
        [HttpGet]
        [Route("status/{id}")]
        public IActionResult Status([FromRoute] Guid id)
        {
            Read.OrderReadDto order = orderReadRepository.GetById(id);
            if (order != null)
            {
                return Ok(new OrderResponseModel()
                {
                    OrderId = order.OrderId,
                    Status = order.Status.ToString(),
                    Lines = order.Lines.Select(l => new OrderLineResponseModel
                    {
                        ProductName = l.ProductName,
                        ProductNumber = l.ProductNumber,
                        Quantity = l.Quantity,
                        Status = l.Status
                    })
                });
            }
            return NotFound();
        }
    }
}
