using LoggingInKibana.Domain.Request;
using LoggingInKibana.Domain.Response;
using Microsoft.AspNetCore.Mvc;

namespace LoggingInKibana.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult<Response<CreateOrderResponse>>> CreateOrder(Request<CreateOrderRequest> request)
        {
            var response = await HandleCreateOrder(request);
            return Ok(response);
        }

        // GET: api/orders/{orderId}
        [HttpGet("{orderId}")]
        public async Task<ActionResult<Response<GetOrderResponse>>> GetOrder(string orderId)
        {
            var response = await HandleGetOrder(orderId);
            return Ok(response);
        }

        #region Private Mock Handlers

        private Task<Response<CreateOrderResponse>> HandleCreateOrder(Request<CreateOrderRequest> request)
        {
            // mock processing
            var payload = new CreateOrderResponse
            {
                OrderId = Guid.NewGuid().ToString(),
                Status = "Created"
            };

            var response = new Response<CreateOrderResponse>
            {
                TransactionId = request.TransactionId,
                ServiceName = request.ServiceName,
                Payload = payload,
                StatusCode = 201
            };

            return Task.FromResult(response);
        }

        private Task<Response<GetOrderResponse>> HandleGetOrder(string orderId)
        {
            // mock data
            var payload = new GetOrderResponse
            {
                OrderId = orderId,
                ProductId = "prod-456",
                Quantity = 3,
                Status = "Shipped"
            };

            var response = new Response<GetOrderResponse>
            {
                TransactionId = Guid.NewGuid().ToString(),
                ServiceName = "MockService",
                Payload = payload,
                StatusCode = 200
            };

            return Task.FromResult(response);
        }

        #endregion
    }
}
