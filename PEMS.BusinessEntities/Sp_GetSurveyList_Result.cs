//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PEMS.BusinessEntities
{
    using System;
    
    public partial class Sp_GetSurveyList_Result
    {
        public int SurveyId { get; set; }
        public int OwnerId { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public Nullable<int> DistCd { get; set; }
        public Nullable<int> MunCd { get; set; }
        public Nullable<int> Ward { get; set; }
        public string GenderCode { get; set; }
        public string OwnerPhoneNumber { get; set; }
        public string DamageStatus { get; set; }
        public string FrontHousePhoto { get; set; }
        public string LeftHousePhoto { get; set; }
        public string RightHousePhoto { get; set; }
        public string BackHousePhoto { get; set; }
        public Nullable<int> SurveyedBy { get; set; }
        public Nullable<System.DateTime> SurveyedDate { get; set; }
        public Nullable<int> EnteredBy { get; set; }
        public Nullable<System.DateTime> EnteredDate { get; set; }
    }
}
