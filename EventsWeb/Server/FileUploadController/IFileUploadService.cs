using EventsWeb.Shared.Model;

namespace EventsWeb.Server.FileUploadController
{
	public interface IFileUploadService
	{
		public Task<string> Create(EventsFileUpload fileUpload);
		public Task Delete(string Path);
	}
}
