using AWSS3Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSS3Service.Models.Requests
{
    public class ServiceRequest
    {
        public int? IdCompany { get; set; }
        public int? idItem { get; set; }
        public EPrefix eTypePrefix { get; set; }//PASTAS RAIZ
                                                //(1 - Materiais/
                                                // 2 - Arquivos/
                                                // 3 - Clientes
                                                // 4 - Contratos
                                                // 5 - ImagemUsuario
                                                // 6 - Orcamentos
        public EPrefix? eTypePrefixEnd { get; set; }//SUBPASTAS
                                                    // 7 - _AssinaturaContratada/
                                                    // 8 - _Logo
                                                    // 9 - ResponsavelTecnico
                                                    // 10 - ResponsavelLocal
                                                    // 11 - Imagens)
        public EInvoke eTypeCall { get => (EInvoke)eTypeCallInt; }
        public int eTypeCallInt { get; set; }

        public string sImg { get; set; }

        public byte[] image
        {
            get
            {
                return sImg != null ? Convert.FromBase64String(sImg) : new byte[0];
            }
        }
        public string ContentType { get; set; } = string.Empty;
        public string DescriptionImage { get; set; } = string.Empty;
        public string Prefix { get; set; } // Esse campo vem para ser excluido ou para visualização de imagem ou setado manualmente usando o prefixoLista
        public List<string>? ListPrefix { get; set; } // Esse campo vem para listar imagens de diferentes diretorios
        public List<string>? TypeFile { get; set; }
    }
}
