using AWSS3Service.Enums;

namespace AWSS3Service.Helpers
{
    internal class PrefixHelper
    {
        public string GetPrefixoEnum(EPrefix eTtipoPrefixo)
        {
            var membro = typeof(EPrefix).GetMember(eTtipoPrefixo.ToString());
            var atributo = (PrefixAttribute)Attribute.GetCustomAttribute(membro[0], typeof(PrefixAttribute));

            return atributo?.Prefixo ?? string.Empty;
        }
    }
    internal class PrefixAttribute : Attribute
    {
        public string Prefixo { get; }

        public PrefixAttribute(string prefixo)
        {
            Prefixo = prefixo;
        }
    }
}
