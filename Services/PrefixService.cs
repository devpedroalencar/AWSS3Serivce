using AWSS3Service.Enums;
using AWSS3Service.Helpers;
using AWSS3Service.Models.Requests;

namespace AWSS3Service.Services
{
    public class PrefixService
    {
        private readonly RequestService _RequestService;
        private string _root { get; set; } = default!;
        private PrefixHelper _helper;

        public PrefixService(RequestService RequestService)
        {
            _RequestService = RequestService;
            _helper = new PrefixHelper();

            if (string.IsNullOrEmpty(_RequestService.Prefix))
            {
                _root = _helper.GetPrefixoEnum(_RequestService.eTypePrefix);

                if (_RequestService.eTypePrefix != EPrefix.UserImage)
                    _root = string.Concat(_root, _RequestService.IdCompany + "/");

            }
        }

        public string getPrefix()
        {
            if (!string.IsNullOrEmpty(_RequestService.Prefix))
                return _RequestService.Prefix;

            string retorno = _root;
            if (_RequestService.eTypePrefixEnd.HasValue) //Build subfolders
            {
                if (_RequestService.eTypePrefixEnd == EPrefix.LocalResponsible ||
                    _RequestService.eTypePrefixEnd == EPrefix.TechnicianResponsible ||
                    _RequestService.eTypePrefixEnd == EPrefix.ImagesOS)
                {

                    retorno = string.Concat(retorno, _RequestService.idItem + "/");
                    return string.Concat(retorno, _helper.GetPrefixoEnum(_RequestService.eTypePrefixEnd.Value));
                }

                retorno = string.Concat(retorno, _helper.GetPrefixoEnum(_RequestService.eTypePrefixEnd.Value));

                if (_RequestService.eTypePrefixEnd != EPrefix.Logo &&
                    _RequestService.eTypePrefixEnd != EPrefix.ContractedSignature
                    )
                {
                    retorno = string.Concat(retorno, _RequestService.idItem + "/");
                }
            }
            else
            {
                retorno = string.Concat(_root, _RequestService.idItem + "/");
            }

            return retorno;
        }
    }


}
