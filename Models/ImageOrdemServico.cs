using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSS3Service.Models
{
    internal class ImageOrdemServico : ImageBaseS3
    {
        public ImageOrdemServico(S3Object obj) : base(obj)
        {
        }

        public int Id { get; set; }
        public int IdOrdemServico { get; set; }
        public string Status { get; set; }
        public string Progress { get; set; }
        public string Tyoe { get; set; }
    }
}
