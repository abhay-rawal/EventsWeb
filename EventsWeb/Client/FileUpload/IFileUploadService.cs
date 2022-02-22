using Microsoft.AspNetCore.Components.Forms;

namespace EventsWeb.Client.FileUpload
{
    public interface IFileUploadService
    {
        public Task<string> UploadFile(IBrowserFile file);
        public Task DeleteImage(string Path);

    }
}
