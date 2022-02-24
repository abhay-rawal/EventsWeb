using Events_Repository.Repository.IRepository;
using EventsWeb.Server.FileUploadController;
using EventsWeb.Shared.Model;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventsWeb.Server.CategoryController
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileUploadService _fileUploadService;
        public FileUploadController(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }
        //Calls a service to Upload A file
        [HttpPost]
        public async Task<IActionResult> Create(EventsFileUpload fileUpload)
        {
            try
            {
                string fullPath = await _fileUploadService.Create(fileUpload);
                return Ok(fullPath);
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
        //Calls a service to Delete a file
        [HttpDelete("{filepath}")]
        public async Task<IActionResult> Delete(string filepath)
        {
            try
            {
                await _fileUploadService.Delete(filepath);
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

