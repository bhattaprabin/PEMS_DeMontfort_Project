using PEMS.BusinessEntities;
using PEMS.BusinessLayer.BankModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PEMS.Repository.CommonService
{
    public class CommonService : ICommonService
    {
        public int SaveEmailsForAlert(string Emailid)
        {
            int result = 0;
            using (PEMSDBEntities db = new PEMSDBEntities())
            {
                var emailAlertMessageModel = new EmailsForAlertMessage()
                {
                    EmailId = Emailid
                };
                db.EmailsForAlertMessages.Add(emailAlertMessageModel);
                result = db.SaveChanges();
            }
            return result;
        }
        public List<SelectListItem> GetBanksinDropDown()
        {
            List<SelectListItem> bankLists = new List<SelectListItem>();
            bankLists.Add(new SelectListItem { Text = "--Select--", Value = "0"});
            using (PEMSDBEntities db = new PEMSDBEntities())
            {
              var  bankLists1 = (from b in db.Banks
                             select new SelectListItem()
                             {
                                 Text = b.BankName,
                                 Value = b.BankCode
                             }).Distinct().ToList();
                bankLists.AddRange(bankLists1);
            }
            return bankLists.OrderBy(i=> i.Value).ToList();
        }
    }
}
