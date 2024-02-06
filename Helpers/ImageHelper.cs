using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO.Compression;
using System.Text;

namespace AWSS3Service.Helpers
{
    internal class ImageHelper
    {
        public string GetMimeType(Stream imageStream, string fileName)
        {
            try
            {
                var format = Image.DetectFormat(imageStream);
                if (format != null)
                {
                    switch (format.Name.ToLower())
                    {
                        case "jpeg":
                            return "image/jpeg";
                        case "png":
                            return "image/png";
                        case "gif":
                            return "image/gif";
                        default:
                            return "image/unknown";
                    }
                }
                else
                {
                    string extension = Path.GetExtension(fileName).ToLower();
                    switch (extension)
                    {
                        case ".jpeg":
                        case ".jpg":
                            return "image/jpeg";
                        case ".png":
                            return "image/png";
                        case ".gif":
                            return "image/gif";
                        default:
                            return "image/unknown";
                    }
                }
            }
            catch (Exception)
            {
                return "image/unknown";
            }
        }

        public byte[] ResizeImage(Image imagem, Size tamanho)
        {
            imagem.Mutate(x => x.Resize(tamanho.Width, tamanho.Height));
            using (MemoryStream ms = new MemoryStream())
            {
                imagem.SaveAsJpeg(ms);
                return ms.ToArray();
            }
        }

        public string RemoveAccentuation(string text)
        {
            System.Text.EncodingProvider provider = System.Text.CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(provider);

            return
                System.Web.HttpUtility.UrlDecode(
                    System.Web.HttpUtility.UrlEncode(
                       text, System.Text.Encoding.GetEncoding("iso-8859-7")));
        }

        public string ConvertStreamToString(Stream stream)
        {
            byte[] arqUp;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                byte[] arq = memoryStream.ToArray();
                arqUp = new byte[arq.Length];

                using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress))
                {
                    gzipStream.Write(arq, 0, arq.Length);
                }

                arqUp = memoryStream.ToArray();

                Buffer.BlockCopy(arq, 0, arqUp, 0, arq.Length);
                return Convert.ToBase64String(arqUp);
            }
        }
    }
}
