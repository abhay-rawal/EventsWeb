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
        /// <summary>
        ///     Uploads a file to server
        /// </summary>
        /// <param name="fileUpload">Contains a byte array of File</param>
        /// <returns>Returns a file Path of the Uploaded File</returns>
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

        /// <summary>
        /// Deletes a file if Path is Found
        /// </summary>
        /// <param name="Path"></param>

		public async Task Delete(string Path)
		{
			if(File.Exists(_webHostEnvironment.WebRootPath + Path))
			{
                File.Delete(_webHostEnvironment.WebRootPath + Path);
			}
		}
	}
}
