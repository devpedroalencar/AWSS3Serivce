using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSS3Service.Models
{
    public abstract class ImageBaseS3
    {
        public string id_imagem { get; internal set; }
        public string nome_imagem { get; internal set; }
        public double tamanho { get; internal set; }
        public string caminho_imagem { get; internal set; }

        public ImageBaseS3(S3Object obj)
        {
            string[] explodedString = obj.Key.Split('/');
            int index = 3;
            if (explodedString.Count() > 4)
            {
                index = 4;
            }

            //id_imagem = explodedString[index].Split(':')[0];
            id_imagem = explodedString[index];
            //nome_imagem = explodedString[index].Split(':')[1];
            nome_imagem = explodedString[index];

            tamanho = obj.Size;
            caminho_imagem = obj.Key;
        }
    }
}
