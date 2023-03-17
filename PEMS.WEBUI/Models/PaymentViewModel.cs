using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEMS.WEBUI.Models
{
    public class PaymentViewModel
    {
        public int PaymentId { get; set; }
        public string BenId { get; set; }
        public Nullable<int> Installment { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public string Amount { get; set; }
        public string DonorCode { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public int PostCodeId { get; set; }
    }
    public class PaymentFilesViewModel
    {
        public int FileId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string FileName { get; set; }
        public int EnteredBy { get; set; }

    }
}