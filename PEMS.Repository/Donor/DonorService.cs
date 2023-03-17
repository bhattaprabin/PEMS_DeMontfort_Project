using PEMS.BusinessEntities;
using PEMS.BusinessLayer.DonorModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEMS.Repository.Donor
{
    public class DonorService : IDonorService
    {
        public List<DonorViewModel> GetDonorList(string searchTerm)
        {
            List<DonorViewModel> searchlist = new List<DonorViewModel>();
            using (PEMSDBEntities db = new PEMSDBEntities())
            {
                if (string.IsNullOrEmpty(searchTerm))
                {
                    searchlist = (from d in db.Donors
                                  select new DonorViewModel()
                                  {
                                      DonorCode = d.DonorCode,
                                      DonorName = d.DonorName
                                  }).ToList();
                }
                else
                {
                    //searchlist = db.GetSurveyList().Where(x => x.MedicineName.ToLower().StartsWith(SearchTerm.ToLower())).ToList();
                }
                return searchlist;
            }
        }

        public int RegisterDonor(DonorViewModel model)
        {
            int result = 0;
            using(PEMSDBEntities db = new PEMSDBEntities())
            {
                PEMS.BusinessEntities.Donor donorModel = new PEMS.BusinessEntities.Donor();
                donorModel.DonorCode = model.DonorCode;
                donorModel.DonorName = model.DonorName;
                donorModel.Email = model.Email;
                donorModel.PhoneNumber = model.PhoneNumber;
                donorModel.WebsiteURL = model.WebsiteURL;
                db.Donors.Add(donorModel);
               result = db.SaveChanges();
            }
           return result;
        }
    }
}
