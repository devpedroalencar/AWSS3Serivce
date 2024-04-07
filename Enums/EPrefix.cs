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
        [Prefix("Products/")]
        Stock = 1,
        [Prefix("Files/")]
        ServiceOrder = 2,
        [Prefix("Clients/")]
        Clients = 3,
        [Prefix("Contacts/")]
        Contacts = 4,
        [Prefix("UserImage/")]
        UserImage = 5,
        [Prefix("Budget/")]
        Budget = 6,
        //Daqui pra baixo são subpastas conforme estrutura do s3
        [Prefix("_ContractedSignature/")]
        ContractedSignature = 7,
        [Prefix("_Logo/")]
        Logo = 8,
        [Prefix("TechnicianResponsible/")]
        TechnicianResponsible = 9,
        [Prefix("LocalResponsible/")]
        LocalResponsible = 10,
        [Prefix("Images/")]
        ImagesOS = 11,
        [Prefix("_Employees/")]
        Employees = 12,
        [Prefix("Location/")]
        Location = 13,
        [Prefix("Equipment/")]
        Equipment = 14,
        [Prefix("Component/")]
        Component = 15,
        [Prefix("Part/")]
        Part = 16,
        [Prefix("PlanFiles/")]
        PlanFiles = 17
    }
}
