using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEMS.BusinessLayer.InspectionModel
{
    public class InspectionViewModel
    {

        public int InspectionId { get; set; }
        public string BenId { get; set; }
        public Nullable<int> InspectionLevel { get; set; }
        public Nullable<System.DateTime> InspectionDate { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CityId { get; set; }
        public string  CityName { get; set; }
        public int PostCodeId { get; set; }
        public int OwnerId { get; set; }
        public string FullName { get; set; }
    }
    public class InspectionFilesViewModel
    {
        public int FileId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string FileName { get; set; }
        public int EnteredBy { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public int CountRecords { get; set; }

    }
    public class InspectionSummaryViewModel
    {
        public string BenId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string StateName { get; set; }
        public string CItyName { get; set; }
        public int FisrtInspCount { get; set; }
        public int SecondInspCount { get; set; }
        public int ThirdInspCount { get; set; }
    }
}

