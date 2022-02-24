using System.Net.Http.Json;
using EventsWeb.Shared.Model;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using EventsWeb.Client.Helper;
using Microsoft.JSInterop;

namespace EventsWeb.Client.FileUpload
{
	public class FileUploadService : IFileUploadService                
	{
		private readonly HttpClient _http;
		private readonly IJSRuntime _jsRuntime;
	
		public FileUploadService(HttpClient http, IJSRuntime jSRuntime)
		{
			_http = http;
			_jsRuntime = jSRuntime;
		
		}
		/// <summary> Creates a unique fileName, Converts file to a biteArray and call FileUpload Api</summary> 
		/// <param name="file">Uploaded File</param>
		/// <returns>Returns File path </returns>
		public async Task<string> UploadFile(IBrowserFile file)
		{
			EventsFileUpload UploadedFile;
			FileInfo fileInfo = new(file.Name);
			var newFileName = Guid.NewGuid().ToString() + fileInfo.Extension; //Creates Unique FileName with the help of Guid
			using (MemoryStream ms = new()) // Read a file and copy it to Memory Stream
			{
				await file.OpenReadStream().CopyToAsync(ms);
				UploadedFile = new()
				{
					FileName = newFileName,
					FileContent = ms.ToArray()
				};
			}

			var response = await _http.PostAsJsonAsync<EventsFileUpload>("api/FileUpload", UploadedFile); //Call File Upload Api
			var content = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
			{
				return content;
			}
			else
			{
				var error = JsonConvert.DeserializeObject<ErrorMessage>(content);
				await _jsRuntime.ToastrError(error.Message);
				return null;
			}
		}
	}
}
