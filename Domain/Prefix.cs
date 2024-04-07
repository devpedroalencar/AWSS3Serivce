

using AWSS3Service.Enums;
using AWSS3Service.Helpers;
using AWSS3Service.Models;

namespace AWSS3Service.Domain
{
    public class Prefix
    {
        private readonly Parameters _parameters;
        private string _root { get; set; }
        private PrefixHelper _helper;

        public Prefix(Parameters parameters)
        {
            _parameters = parameters;
            _helper = new PrefixHelper();

            if (string.IsNullOrEmpty(_parameters.Prefix))
            {
                _root = _helper.GetPrefixoEnum(_parameters.eTypePrefix);

                if (_parameters.eTypePrefix != EPrefix.UserImage)
                    _root = String.Concat(_root, _parameters.IdCompany + "/");

            }
        }

        public string getPrefix()
        {
            if (!string.IsNullOrEmpty(_parameters.Prefix)) 
                return _parameters.Prefix;

            string retorno = _root;
            if (_parameters.eTypePrefixEnd.HasValue) //Build subfolders
            {
                if (_parameters.eTypePrefixEnd == EPrefix.LocalResponsible ||
                    _parameters.eTypePrefixEnd == EPrefix.TechnicianResponsible ||
                    _parameters.eTypePrefixEnd == EPrefix.ImagesOS)
                {

                    retorno = String.Concat(retorno, _parameters.idItem + "/");
                    return retorno = String.Concat(retorno, _helper.GetPrefixoEnum(_parameters.eTypePrefixEnd.Value));
                }

                retorno = String.Concat(retorno, _helper.GetPrefixoEnum(_parameters.eTypePrefixEnd.Value));

                if (_parameters.eTypePrefixEnd != EPrefix.Logo &&
                    _parameters.eTypePrefixEnd != EPrefix.ContractedSignature
                    )
                {
                    retorno = String.Concat(retorno, _parameters.idItem + "/");
                }
            }
            else
            {
                retorno = String.Concat(_root, _parameters.idItem + "/");
            }

            return retorno;
        }
    }


}
