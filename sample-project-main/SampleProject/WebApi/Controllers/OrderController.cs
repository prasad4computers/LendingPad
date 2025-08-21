using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using Core.Services.Orders;
using WebApi.Models.Orders;


namespace WebApi.Controllers
{
    [RoutePrefix("orders")]
    public class OrderController : BaseApiController
    {
        private readonly ICreateOrderService _createOrderService;
        private readonly IDeleteOrderService _deleteOrderService;
        private readonly IGetOrderService _getOrderService;
        private readonly IUpdateOrderService _updateOrderService;

        public OrderController(ICreateOrderService createOrderService, IDeleteOrderService deleteOrderService, IGetOrderService getOrderService, IUpdateOrderService updateOrderService)
        {
            _createOrderService = createOrderService;
            _deleteOrderService = deleteOrderService;
            _getOrderService = getOrderService;
            _updateOrderService = updateOrderService;
        }

        [Route("{orderId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateOrder(Guid orderId, [FromBody] OrderModel model)
        {
            if (model == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Request body cannot be null." });
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

            try
            {
                var order = _createOrderService.Create(orderId, model.OrderDate, model.Status, model.TotalAmount, model.CustomerId, model.ProductIds, model.Tags);
                return Found(new OrderData(order));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Exception while creating order: {ex.Message}");
            }
        }

        [Route("{orderId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateOrder(Guid orderId, [FromBody] OrderModel model)
        {
            if (model == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Request body cannot be null." });
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

            try
            {
                var order = _getOrderService.GetOrder(orderId);
                if (order == null)
                    return DoesNotExist();

                _updateOrderService.Update(order, model.OrderDate, model.Status, model.TotalAmount, model.CustomerId, model.ProductIds, model.Tags);
                return Found(new OrderData(order));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error while updating order: {ex.Message}");
            }
        }

        [Route("{orderId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteOrder(Guid orderId)
        {
            try
            {
                var order = _getOrderService.GetOrder(orderId);
                if (order == null)
                    return DoesNotExist();

                _deleteOrderService.Delete(order);
                return Found();
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error while deleting an order: {ex.Message}");
            }
        }

        [Route("{orderId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetOrder(Guid orderId)
        {
            try
            {
                var order = _getOrderService.GetOrder(orderId);
                return Found(new OrderData(order));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error while retrieving order details: {ex.Message}");
            }
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetOrders(int skip, int take, OrderStatus? status = null, string customerId = null)
        {
            try
            {
                var orders = _getOrderService.GetOrders(status, customerId)
                    .Skip(skip).Take(take)
                    .Select(o => new OrderData(o))
                    .ToList();
                return Found(orders);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error while retrieving orders list: {ex.Message}");
            }
        }

        [Route("clear")]
        [HttpDelete]
        public HttpResponseMessage DeleteAllOrders()
        {
            try
            {
                _deleteOrderService.DeleteAll();
                return Found();
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error while deleting all orders data: {ex.Message}");
            }
        }

    }
}