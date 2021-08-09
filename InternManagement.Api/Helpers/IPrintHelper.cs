using InternManagement.Api.Enums;

namespace InternManagement.Api.Helpers
{
    public interface IPrintHelper
    {
        string PrintCancel(eGender g);
        string PrintCertificate(eGender g);
        string PrintDecision(eGender g);
    }
}