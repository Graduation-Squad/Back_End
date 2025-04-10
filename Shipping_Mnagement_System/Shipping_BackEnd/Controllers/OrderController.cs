﻿using AutoMapper;
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
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrders([FromQuery] OrderParameters orderParameters)
        {
            var orders = await _orderService.GetOrdersAsync(orderParameters);
            return Ok(_mapper.Map<IReadOnlyList<OrderDto>>(orders));
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder(OrderCreateDto dto)
        {
            var order = await _orderService.CreateOrderAsync(dto);
            return Ok(_mapper.Map<OrderDto>(order));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(_mapper.Map<OrderDto>(order));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, OrderUpdateDto dto)
        {
            await _orderService.UpdateOrderAsync(id, dto);
            return NoContent();
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, OrderStatusUpdateDto dto)
        {
            await _orderService.UpdateOrderStatusAsync(id, dto);
            return NoContent();
        }

        [HttpPost("{id}/assign")]
        public async Task<IActionResult> Assign(int id, [FromQuery] int deliveryManId)
        {
            await _orderService.AssignOrderToDeliveryManAsync(id, deliveryManId);
            return Ok();
        }

        [HttpGet("merchant/{id}")]
        public async Task<IActionResult> GetByMerchant(int id, [FromQuery] OrderParameters parameters)
        {
            var orders = await _orderService.GetOrdersByMerchantAsync(id, parameters);
            return Ok(_mapper.Map<IReadOnlyList<OrderDto>>(orders));
        }

        [HttpGet("agent/{id}")]
        public async Task<IActionResult> GetByAgent(int id, [FromQuery] OrderParameters parameters)
        {
            var orders = await _orderService.GetOrdersByDeliveryManAsync(id, parameters);
            return Ok(_mapper.Map<IReadOnlyList<OrderDto>>(orders));
        }
    }

}
