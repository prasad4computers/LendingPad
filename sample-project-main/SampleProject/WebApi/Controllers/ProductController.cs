using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using Core.Services.Products;
using WebApi.Models.Products;


namespace WebApi.Controllers
{
    [RoutePrefix("products")]
    public class ProductController : BaseApiController
    {
        private readonly ICreateProductService _createProductService;
        private readonly IDeleteProductService _deleteProductService;
        private readonly IGetProductService _getProductService;
        private readonly IUpdateProductService _updateProductService;

        public ProductController(ICreateProductService createProductService, IDeleteProductService deleteProductService, IGetProductService getProductService, IUpdateProductService updateProductService)
        {
            _createProductService = createProductService;
            _deleteProductService = deleteProductService;
            _getProductService = getProductService;
            _updateProductService = updateProductService;
        }

        [Route("{productId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateProduct(Guid productId, [FromBody] ProductModel model)
        {
            if (model == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Request body cannot be null." });
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

            try
            {
                var product = _createProductService.Create(productId, model.Name, model.Price, model.Description);
                return Found(new ProductData(product));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Exception while creating product: {ex.Message}");
            }
        }

        [Route("{productId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateProduct(Guid productId, [FromBody] ProductModel model)
        {
            if (model == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Request body cannot be null." });
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

            try
            {
                var product = _getProductService.GetProduct(productId);
                if (product == null)
                    return DoesNotExist();

                _updateProductService.Update(product, model.Name, model.Price, model.Description);
                return Found(new ProductData(product));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error while updating product: {ex.Message}");
            }
        }

        [Route("{productId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteProduct(Guid productId)
        {
            try
            {
                var product = _getProductService.GetProduct(productId);
                if (product == null)
                    return DoesNotExist();

                _deleteProductService.Delete(product);
                return Found();
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error while deleting a product: {ex.Message}");
            }
        }

        [Route("{productId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetProduct(Guid productId)
        {
            try
            {
                var product = _getProductService.GetProduct(productId);
                return Found(new ProductData(product));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error while retrieving product details: {ex.Message}");
            }
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetProducts(int skip, int take, string name = null, decimal? minPrice = null)
        {
            try
            {
                var products = _getProductService.GetProducts(name, minPrice)
                    .Skip(skip).Take(take)
                    .Select(p => new ProductData(p))
                    .ToList();
                return Found(products);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error while retrieving products list: {ex.Message}");
            }
        }

        [Route("clear")]
        [HttpDelete]
        public HttpResponseMessage DeleteAllProducts()
        {
            try
            {
                _deleteProductService.DeleteAll();
                return Found();
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error while deleting all products data: {ex.Message}");
            }
        }

    }
}