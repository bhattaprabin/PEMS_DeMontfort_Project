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
    using System.Collections.Generic;
    
    public partial class PaymentFilesInfo
    {
        public int FileId { get; set; }
        public Nullable<int> StateId { get; set; }
        public Nullable<int> CityId { get; set; }
        public string FileName { get; set; }
        public Nullable<int> EnteredBy { get; set; }
        public Nullable<System.DateTime> EnteredDate { get; set; }
    
        public virtual City City { get; set; }
        public virtual State State { get; set; }
    }
}