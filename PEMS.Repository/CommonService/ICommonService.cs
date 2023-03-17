using PEMS.BusinessLayer.BankModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PEMS.Repository.CommonService
{
    public interface ICommonService
    {
        int SaveEmailsForAlert(string Emailid);
        List<SelectListItem> GetBanksinDropDown();
    }
}
