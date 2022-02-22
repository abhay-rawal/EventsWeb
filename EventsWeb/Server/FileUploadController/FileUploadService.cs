using EventsWeb.Shared.Model;

namespace EventsWeb.Server.FileUploadController
{
	public class FileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileUploadService(IWebHostEnvironment webHostEnvironment)
		{
            _webHostEnvironment = webHostEnvironment;   
		}
		public async Task<string> Create(EventsFileUpload fileUpload)
		{
            var folderDirectory = $"{_webHostEnvironment.WebRootPath}\\images\\category";
            if (!Directory.Exists(folderDirectory))
            {
                Directory.CreateDirectory(folderDirectory);
            }
            var path = Path.Combine(folderDirectory, fileUpload.FileName);

            await using FileStream fs = new FileStream(path, FileMode.Create);
            fs.Write(fileUpload.FileContent, 0, fileUpload.FileContent.Length);
            fs.Close();
            var fullPath = $"/images/category/{fileUpload.FileName}";
            return fullPath;    
        }

		public async Task Delete(string Path)
		{
			if(File.Exists(_webHostEnvironment.WebRootPath + Path))
			{
                File.Delete(_webHostEnvironment.WebRootPath + Path);
			}
		}
	}
}
