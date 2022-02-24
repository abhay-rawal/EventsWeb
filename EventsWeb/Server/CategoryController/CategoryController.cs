using Events_Repository.Repository.IRepository;
using EventsWeb.Server.FileUploadController;
using EventsWeb.Shared.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventsWeb.Server.CategoryController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IFileUploadService _fileUploadService; 

        public CategoryController(ICategoryService categoryRepository, IFileUploadService fileUploadService)
        {
            _categoryService = categoryRepository;
            _fileUploadService = fileUploadService;
        }

        //Calls a service to Get all Categories
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                //fetch categories
                var categories = await _categoryService.GetAll();
                //Check if categories is null
                //if null send bad request
                if (categories == null)
                {
                    return BadRequest(new ErrorMessage()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Not Found"
                    });
                }
                return Ok(categories);
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

        //Calls a service to Get a category by Id
        [Route("Get/{categoryId}")]
        [HttpGet("categoryId")]
        public async Task<IActionResult> Get(int? categoryId)
        {
            try
            {
                //Check if Id is null
                //if null send bad request
                if (categoryId == null || categoryId == 0)
                {
                    return BadRequest(new ErrorMessage()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = $"Invalid Id : {categoryId}"
                    });
                }

                //fetch category
                var category = await _categoryService.Get(categoryId.Value);
                //Check if returned category is Null
                //if null send bad request
                if (category == null)
                {
                    return BadRequest(new ErrorMessage()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = $"No Category With Id: {categoryId}"
                    });
                }
                //Return Category
                return Ok(category);

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

        //Calls a service to Creates a new Category
        [HttpPost]
        public async Task<IActionResult> Create(EventsCategory category)
        {
            try
            {
                //Create If ID == 0
                var categoryFromDb = await _categoryService.Create(category);
                //Check if Id has been Updated
                //If Not updated send bad request
                if (categoryFromDb.Id == 0)
                {
                    return BadRequest(new ErrorMessage()
                    {
                        Message = "Couldn't create category",
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

        //Calls a service to Updates Category by Id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(EventsCategory category, int id)
        {
            try
            {
                await _categoryService.Update(category);
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

        //Calls a service to Deletes Category by Id
        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                //fetch category to check if it exist
                var category = await _categoryService.Get(id);
                if (id == 0)
                {
                    return BadRequest(new ErrorMessage()
                    {
                        Message = "Category Doesn't Exist",
                        StatusCode = StatusCodes.Status500InternalServerError
                    });
                }
                //If category exist : Delete
                var result = await _categoryService.Delete(id);
                //If result < 0 return Bad request
                if (result == 0)
                {
                    return BadRequest(new ErrorMessage()
                    {
                        Message = "Couldn't Delete category",
                        StatusCode = StatusCodes.Status500InternalServerError
                    });
                }
                return Ok(result);
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

        //Calls a service to Deletes Image associated with Image
        [Route("DeleteImage/{filepath}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string filepath)
        {
            try
            {
                var categoryFilePath = "/images/category/"+filepath ;
                await _fileUploadService.Delete(categoryFilePath);
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

