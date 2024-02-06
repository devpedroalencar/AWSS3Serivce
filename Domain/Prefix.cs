

using AWSS3Service.Enums;
using AWSS3Service.Helpers;
using AWSS3Service.Models;

namespace AWSS3Service.Domain
{
    public class Prefix
    {
        private readonly Parameters _parametros;
        private string _raiz { get; set; }
        private PrefixHelper _helper;

        public Prefix(Parameters parametros)
        {
            _parametros = parametros;
            _helper = new PrefixHelper();

            if (string.IsNullOrEmpty(_parametros.prefixo)) // Em caso de exlcusão ou visualização prefixo vem do cliente (Sisum).
            {
                _raiz = _helper.GetPrefixoEnum(_parametros.ePrefixoTipo);

                if (_parametros.ePrefixoTipo != EPrefix.ImagemUsuario)
                    _raiz = String.Concat(_raiz, _parametros.IdEmpresa + "/");

            }
        }

        public string getPrefixo()
        {
            if (!string.IsNullOrEmpty(_parametros.prefixo)) // Em caso de exlcusão ou visualização prefixo vem do cliente (Sisum).
                return _parametros.prefixo;

            string retorno = _raiz;
            if (_parametros.ePrefixoTipoFinal.HasValue) //Monta subpastas
            {
                if (_parametros.ePrefixoTipoFinal == EPrefix.ResponsavelLocal ||
                    _parametros.ePrefixoTipoFinal == EPrefix.ResponsavelTecnico ||
                    _parametros.ePrefixoTipoFinal == EPrefix.imagensOS)
                {

                    retorno = String.Concat(retorno, _parametros.idItem + "/");
                    return retorno = String.Concat(retorno, _helper.GetPrefixoEnum(_parametros.ePrefixoTipoFinal.Value));
                }

                retorno = String.Concat(retorno, _helper.GetPrefixoEnum(_parametros.ePrefixoTipoFinal.Value));

                if (_parametros.ePrefixoTipoFinal != EPrefix.Logo &&
                    _parametros.ePrefixoTipoFinal != EPrefix.AssinaturaContratada
                    )
                {
                    retorno = String.Concat(retorno, _parametros.idItem + "/");
                }
            }
            else
            {
                retorno = String.Concat(_raiz, _parametros.idItem + "/");
            }

            return retorno;
        }
    }


}
