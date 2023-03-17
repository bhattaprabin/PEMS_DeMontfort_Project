using PEMS.BusinessLayer.DonorModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEMS.Repository.Donor
{
    public interface IDonorService
    {
        int RegisterDonor(DonorViewModel user);
        List<DonorViewModel> GetDonorList(string serchTerm);
    }
}
