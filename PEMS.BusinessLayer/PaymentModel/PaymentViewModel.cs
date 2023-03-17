using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEMS.BusinessLayer.PaymentModel
{
    public class PaymentViewModel
    {
        public int PaymentId { get; set; }
        public string BenId { get; set; }
        public Nullable<int> Installment { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public string Amount { get; set; }
        public string DonorCode { get; set; }
        public string BankCode { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public int PostCodeId { get; set; }
        public string DonorName { get; set; }
        public string BankName { get; set; }
        public string ChequeNo { get; set; }
        public int OwnerId { get; set; }
        public string FullName { get; set; }
    }
    public class PaymentFilesViewModel
    {
        public int FileId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string FileName { get; set; }
        public int EnteredBy { get; set; }

    }
    public class PaymentSummaryViewModel
    {
        public string BenId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string StateName { get; set; }
        public string CItyName { get; set; }
        public int FisrtInstallmentCount { get; set; }
        public int SecondInstallmentCount { get; set; }
        public int ThirdInstallmentCount { get; set; }
        public string DonorCode { get; set; }
        public string DonorName { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }

    }
}
