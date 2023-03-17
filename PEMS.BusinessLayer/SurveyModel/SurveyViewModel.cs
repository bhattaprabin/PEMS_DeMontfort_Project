using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PEMS.BusinessLayer.SurveyModel
{
    public class SurveyViewModel
    {
        public SurveyViewModel()
        {
            ownerFullName = string.Empty;
        }
        public string ownerFullName;
        public int SurveyId { get; set; }
        public int OwnerId { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public string OwnerFullName { get { return ownerFullName; } set { ownerFullName = value; } }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public int PostCodeId { get; set; }
        public string GenderCode { get; set; }
        public string OwnerPhoneNumber { get; set; }
        public string DamageStatus { get; set; }
        public string FrontHousePhoto { get; set; }
        public string LeftHousePhoto { get; set; }
        public string RightHousePhoto { get; set; }
        public string BackHousePhoto { get; set; }
        public Nullable<int> SurveyedBy { get; set; }
        public string SurveyedDate { get; set; }
        public string EnteredBy { get; set; }
        public string EnteredDate { get; set; }
        public int FileId { get; set; }
        public string ReturnMessage { get; set; }
        public string ReportCategory { get; set; }
        public int MaleTotal { get; set; }
        public int FemaleTotal { get; set; }
        public int OtherTotal { get; set; }
    }
    public class SurveyFilesViewModel
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
    public class SurveySummaryReportViewModel
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public Nullable<int> CityId { get; set; }
        public string CityName { get; set; }
        public int MaleCount { get; set; }
        public int FemaleCount { get; set; }
        public int OtherCount { get; set; }

    }
}
