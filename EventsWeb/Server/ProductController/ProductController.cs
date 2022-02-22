using Events_Repository.Repository.IRepository;
using EventsWeb.Shared.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventsWeb.Server.ProductController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                //fetch Product
                var products = await _productService.GetAll();
                //Check if products is null
                //if null send bad request
                if (products == null)
                {
                    return BadRequest(new ErrorMessage()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Not Found"
                    });
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }

        [Route("Get/{categoryId}")]
        [HttpGet("productId")]
        public async Task<IActionResult> Get(int? productId)
        {
            try
            {
                //Check if Id is null
                //if null send bad request
                if (productId == null || productId == 0)
                {
                    return BadRequest(new ErrorMessage()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = $"Invalid Id : {productId}"
                    });
                }

                //fetch Product
                var product = await _productService.Get(productId.Value);
                //Check if returned Product is Null
                //if null send bad request
                if (product == null)
                {
                    return BadRequest(new ErrorMessage()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = $"No product With Id: {productId}"
                    });
                }
                //Return Category
                return Ok(product);

            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(EventsProduct product)
        {
            try
            { 

                var productFromDb = await _productService.Create(product);
                //Check if Id has been Updated
                //If Not updated send bad request
                if (productFromDb.Id == 0)
                {
                    return BadRequest(new ErrorMessage()
                    {
                        Message = "Couldn't create product",
                        StatusCode = StatusCodes.Status500InternalServerError
                    });

                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                //fetch product to check if it exist
                var product = await _productService.Get(id);
                if (product == null)
                {
                    return BadRequest(new ErrorMessage()
                    {
                        Message = "Product Doesn't Exist",
                        StatusCode = StatusCodes.Status500InternalServerError
                    });
                }
                //If product exist : Delete
                var result = await _productService.Delete(id);
                //If result < 0 return Bad request
                if (result == 0)
                {
                    return BadRequest(new ErrorMessage()
                    {
                        Message = "Couldn't Delete Product",
                        StatusCode = StatusCodes.Status500InternalServerError
                    });
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(EventsProduct product, int id)
        {
            try
            {
                await _productService.Update(product);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }

    }
}

