using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Core.DomainModels.OrderModels;
using Shipping.Core.Permissions;
using Shipping.Core.Services.Contracts;
using Shipping.Core.Specification;
using Shipping.Models;
using Shipping_APIs.Attributes;

namespace Shipping_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderController(IOrderService orderService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _orderService = orderService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Permission(Permissions.Orders.View)]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrders([FromQuery] OrderParameters orderParameters)
        {
            var orders = await _orderService.GetOrdersAsync(orderParameters);
            return Ok(_mapper.Map<IReadOnlyList<OrderDto>>(orders));
        }

        [HttpPost]
        [Permission(Permissions.Orders.Create)]
        [Authorize(Roles = "Merchant")]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] OrderCreateDto dto)
        {
            var userEmail = _httpContextAccessor.HttpContext.User?.Identity?.Name;

            if (string.IsNullOrEmpty(userEmail))
                return Unauthorized("Invalid token.");

            var order = await _orderService.CreateOrderAsync(dto, userEmail);
            return Ok(_mapper.Map<OrderDto>(order));
        }


        [HttpGet("{id}")]
        [Permission(Permissions.Orders.View)]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(_mapper.Map<OrderDto>(order));
        }

        [HttpPut("{id}")]
        [Permission(Permissions.Orders.Edit)]
        public async Task<IActionResult> UpdateOrder(int id, OrderUpdateDto dto)
        {
            await _orderService.UpdateOrderAsync(id, dto);
            return NoContent();
        }

        [HttpPut("{id}/status")]
        [Permission(Permissions.Orders.UpdateStatus)]
        public async Task<IActionResult> UpdateStatus(int id, OrderStatusUpdateDto dto)
        {
            await _orderService.UpdateOrderStatusAsync(id, dto);
            return NoContent();
        }

        [HttpPost("{id}/assign")]
        [Permission(Permissions.Orders.Assign)]
        public async Task<IActionResult> Assign(int id, [FromQuery] int deliveryManId)
        {
            await _orderService.AssignOrderToDeliveryManAsync(id, deliveryManId);
            return Ok();
        }

        [HttpGet("merchant/{id}")]
        [Permission(Permissions.Orders.View)]
        public async Task<IActionResult> GetByMerchant(int id, [FromQuery] OrderParameters parameters)
        {
            var orders = await _orderService.GetOrdersByMerchantAsync(id, parameters);
            return Ok(_mapper.Map<IReadOnlyList<OrderDto>>(orders));
        }

        [HttpGet("agent/{id}")]
        [Permission(Permissions.Orders.View)]
        public async Task<IActionResult> GetByAgent(int id, [FromQuery] OrderParameters parameters)
        {
            var orders = await _orderService.GetOrdersByDeliveryManAsync(id, parameters);
            return Ok(_mapper.Map<IReadOnlyList<OrderDto>>(orders));
        }
    }

}
