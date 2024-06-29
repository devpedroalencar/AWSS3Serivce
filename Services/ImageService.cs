using AWSS3Service.Helpers;
using AWSS3Service.Models.Requests;
using AWSS3Service.Models.Responses;
using AWSS3Service.Repositories;
using SixLabors.ImageSharp;


namespace AWSS3Service.Services
{
    public class ImageService
    {
        private readonly s3Repository _s3Repository;

        public ImageService(RequestService request)
        {
             _s3Repository = new s3Repository(
                    new PrefixService(request)
                    );
        }

        public async Task<ResponseService> saveFile(byte[] bImage, string imageDescription, string contentType)
        {
            try
            {
                var helper = new ImageHelper();
                Stream fileStream = new MemoryStream(bImage);

                imageDescription = helper.RemoveAccentuation(imageDescription).ToString().ToUpper();

                if (!string.IsNullOrEmpty(contentType) && (contentType == "image/jpeg" || contentType == "image/png"))
                {
                    var tamanho = new Size(500, 500);
                    using (var image = SixLabors.ImageSharp.Image.Load(fileStream))
                    {

                        if (image.Width > tamanho.Width && image.Height > image.Height)
                        {
                            var img = helper.ResizeImage(image, tamanho);
                            fileStream = new MemoryStream(img);
                        }
                    }
                }
                else if (string.IsNullOrEmpty(contentType))
                {
                    contentType = helper.GetMimeType(fileStream, imageDescription);
                }
                //string key = _prefix + imageDescription;
                _s3Repository.UploadFileBucket(fileStream, imageDescription, contentType);
                return new ResponseService("Arquivo/Imagem salvo com sucesso.", true);
            }
            catch (Exception ex)
            {
                return new ResponseService(ex.Message, false);
                throw;
            }
        }
    }
}
