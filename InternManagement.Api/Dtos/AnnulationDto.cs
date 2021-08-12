using System;

namespace InternManagement.Api.Dtos
{
    public class AnnulationDto
    {
        public string Template { get; set; }
        public int CurrentYear { get; set; } = DateTime.Now.Year;
        public DateTime DecisionDate { get; set; }
        public string DecisionCode { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime AnnulationDate { get; set; }


    }
}