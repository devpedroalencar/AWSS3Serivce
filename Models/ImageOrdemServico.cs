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

        public int id_imagem_ordem_servico { get; set; }
        public int id_ordem_servico { get; set; }
        public string status { get; set; }
        public string progress { get; set; }
        public string tipo { get; set; }
    }
}
