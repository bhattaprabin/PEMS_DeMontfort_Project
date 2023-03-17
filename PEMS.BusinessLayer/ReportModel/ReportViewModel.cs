using PEMS.BusinessLayer.BankModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PEMS.BusinessLayer.ReportModel
{
    public class ReportViewModel
    {
        public ReportViewModel()
        {
            FullName = string.Empty;
            BankList = new List<BankViewModel>();
        }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public int OwnerId { get; set; }
        public string FullName { get; set; }
        public string DetailReportPage { get; set; }
        public string DetailReportTitle { get; set; }
        public string SummaryReportPage { get; set; }
        public string SummaryReportTitle { get; set; }
        public string ReportCategory { get; set; }
        public string ReportFrom { get; set; }
        public string ParentPageName { get; set; }
        public string ChildPageName { get; set; }
        public string BeneficiaryId { get; set; }
        public string BankCode { get; set; }
        public string ReportType { get; set; }
        public List<SelectListItem> ListItems { get; set; }
        public List<BankViewModel> BankList { get; set; }
    }
}
