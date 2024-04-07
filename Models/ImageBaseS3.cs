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
        public string IdImage { get; internal set; }
        public string NameImage { get; internal set; }
        public double Size { get; internal set; }
        public string Patch { get; internal set; }

        public ImageBaseS3(S3Object obj)
        {
            string[] explodedString = obj.Key.Split('/');
            int index = 3;
            if (explodedString.Count() > 4)
            {
                index = 4;
            }

            //id_imagem = explodedString[index].Split(':')[0];
            IdImage = explodedString[index];
            //nome_imagem = explodedString[index].Split(':')[1];
            NameImage = explodedString[index];

            Size = obj.Size;
            Patch = obj.Key;
        }
    }
}
