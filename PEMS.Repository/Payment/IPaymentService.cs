using PEMS.BusinessLayer.PaymentModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEMS.Repository.Payment
{
    public interface IPaymentService
    {
        List<PaymentViewModel> GetPaymentList(PaymentViewModel model);
        int UploadPaymentFromExcel(DataTable dt, string userId, int fileID);
        int UploadPaymentFileInfo(string fileName, int stateId, int CityId);
        List<PEMS.BusinessEntities.PaymentFilesInfo> GetPaymentUploadedFiles(string fileId);
        List<PaymentSummaryViewModel> GetPaymentSummaryReport(PaymentViewModel model);
    }
}
