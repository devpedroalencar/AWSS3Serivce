using AWSS3Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSS3Service.Enums
{
    public enum EPrefix
    {
        //PASTAS RAIZ
        [Prefix("Materiais/")]
        Estoque = 1,
        [Prefix("Arquivos/")]
        OrdemSevico = 2,
        [Prefix("Clientes/")]
        Clientes = 3,
        [Prefix("Contratos/")]
        Contratos = 4,
        [Prefix("ImagemUsuario/")]
        ImagemUsuario = 5,
        [Prefix("Orcamentos/")]
        Orcamentos = 6,
        //Daqui pra baixo são subpastas conforme estrutura do s3
        [Prefix("_AssinaturaContratada/")]
        AssinaturaContratada = 7,
        [Prefix("_Logo/")]
        Logo = 8,
        [Prefix("ResponsavelTecnico/")]
        ResponsavelTecnico = 9,
        [Prefix("ResponsavelLocal/")]
        ResponsavelLocal = 10,
        [Prefix("Imagens/")]
        imagensOS = 11,
        [Prefix("_Funcionarios/")]
        Funcionarios = 12,
        [Prefix("Localizacao/")]
        Localizacao = 13,
        [Prefix("Equipamento/")]
        Equipamento = 14,
        [Prefix("Componente/")]
        Componente = 15,
        [Prefix("Peca/")]
        Peca = 16,
        [Prefix("ArquivosPlano/")]
        Planos = 17
    }
}
