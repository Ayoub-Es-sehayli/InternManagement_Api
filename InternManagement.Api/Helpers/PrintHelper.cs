using System;
using System.IO;
using InternManagement.Api.Enums;

namespace InternManagement.Api.Helpers
{
    public class PrintHelper : IPrintHelper
    {
        public string PrintDecision(eGender g)
        {
            if (g == eGender.Female)
            {
                string path = "Templates/TemplateDecisionFemale.pug";
                return File.ReadAllText(path);
            }
            else
            {
                string path = "Templates/TemplateDecisionMale.pug";
                return File.ReadAllText(path);
            }
        }
        public string PrintCancel(eGender g)
        {
            if (g == eGender.Female)
            {
                string path = "Templates/TemplateAnnulationFemale.pug";
                return File.ReadAllText(path);
            }
            else
            {
                string path = "Templates/TemplateAnnulationMale.pug";
                return File.ReadAllText(path);
            }
        }
        public string PrintCertificate(eGender g)
        {
            if (g == eGender.Female)
            {
                string path = "Templates/TemplateAttestationFemale.pug";
                return File.ReadAllText(path);
            }
            else
            {
                string path = "Templates/TemplateAttestationMale.pug";
                return File.ReadAllText(path);
            }
        }
    }
}