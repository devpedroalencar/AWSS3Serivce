using AWSS3Service.Domain;
using AWSS3Service.Helpers;
using AWSS3Service.Models;
using SixLabors.ImageSharp;

namespace AWSS3Service.Repositories
{
    internal class ImageRepository : s3Repository
    {
        private readonly string _prefix;

        public ImageRepository(Prefix prefix) : base(prefix)
        {
            _prefix = prefix.getPrefix();
        }

        public async Task<ResponseService> saveFile(byte[] imagem, string descricaoImagem, string contentType)
        {
            try
            {
                var helper = new ImageHelper();
                Stream fileStream = new MemoryStream(imagem);

                descricaoImagem = helper.RemoveAccentuation(descricaoImagem).ToString().ToUpper();

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
                    contentType = helper.GetMimeType(fileStream, descricaoImagem);
                }
                string key = _prefix + descricaoImagem;
                UploadFileBucket(fileStream, key, contentType);
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