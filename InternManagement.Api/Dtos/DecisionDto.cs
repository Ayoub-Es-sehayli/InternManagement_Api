using System;

namespace InternManagement.Api.Dtos
{
    public class DecisionDto
    {
        public string Template { get; set; }
        public int CurrentYear { get; set; } = DateTime.Now.Year;
        public string Responsable { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


    }
}