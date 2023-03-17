using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEMS.WEBUI.Models
{
    public class SurveyViewModel
    {
        public int SurveyId { get; set; }
        public int OwnerId { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
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
    }
    public class SurveyFilesViewModel
    {
        public int FileId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string FileName { get; set; }
        public int EnteredBy { get; set; }

    }
}