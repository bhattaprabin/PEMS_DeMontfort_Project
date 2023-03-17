using PEMS.BusinessEntities;
using PEMS.BusinessLayer.BankModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEMS.Repository.Bank
{
    public class BankService : IBankService
    {
        public List<BankViewModel> GetBanksinDropDown()
        {
            List<BankViewModel> bankLists = new List<BankViewModel>();
            using (PEMSDBEntities db = new PEMSDBEntities())
            {
                bankLists = (from b in db.Banks
                             select new BankViewModel()
                             {
                                 BankCode = b.BankCode,
                                 BankName = b.BankName
                             }).Distinct().ToList();
            }
            return bankLists;
        }
    }
}
