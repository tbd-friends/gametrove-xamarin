using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Gametrove.Core.Services.Actions
{
    public class UploadImageForGameAction : IApiAction<bool>
    {
        private readonly Guid _id;
        private readonly Stream _image;
        private readonly string _fileName;

        public UploadImageForGameAction(Guid id, Stream image, string fileName)
        {
            _id = id;
            _image = image;
            _fileName = fileName;
        }

        public async Task<bool> DoAsync(APIActionService service)
        {
            HttpContent fileStreamContent = new StreamContent(_image);

            fileStreamContent.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("form-data")
                {
                    Name = "file",
                    FileName = _fileName
                };

            fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(fileStreamContent);

                var response = await service.Client.PostAsync($"images/{_id}", formData);

                return response.IsSuccessStatusCode;
            }
        }
    }
}