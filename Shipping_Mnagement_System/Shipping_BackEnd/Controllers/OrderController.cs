using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Core.DomainModels.OrderModels;
using Shipping.Core.Services.Contracts;
using Shipping.Core.Specification;
using Shipping.Models;

namespace Shipping_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrders([FromQuery] OrderParameters orderParameters)
        {
            var orders = await _orderService.GetOrdersAsync(orderParameters);
            return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderDto>>(orders));
            //return Ok(orders);
        }
    }
}
