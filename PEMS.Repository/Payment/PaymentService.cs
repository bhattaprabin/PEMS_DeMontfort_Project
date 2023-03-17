using PEMS.BusinessEntities;
using PEMS.BusinessLayer.PaymentModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEMS.Repository.Payment
{
    public class PaymentService : IPaymentService
    {
        public List<PaymentViewModel> GetPaymentList(PaymentViewModel model)
        {
            List<PaymentViewModel> searchlist = new List<PaymentViewModel>();
            using (PEMSDBEntities db = new PEMSDBEntities())
            {
                searchlist = (from p in db.Payments
                              join d in db.Donors on p.DonorCode equals d.DonorCode
                              join b in db.Banks on p.BankCode equals b.BankCode
                              select new PaymentViewModel()
                              {
                                  BenId = p.BenId,
                                  Installment = p.Installment,
                                  Amount = p.Amount,
                                  DonorName = d.DonorName,
                                  BankName = b.BankName,
                                  ChequeNo = p.ChequeNo,
                                  BankCode = p.BankCode,
                                  //FullName = p.BeneficiaryName
                              }).Distinct().ToList();
                if (model.StateId > 0)
                {
                    searchlist = searchlist.Where(i => i.StateId == model.StateId).ToList();
                }
                if (model.CityId > 0)
                {
                    searchlist = searchlist.Where(i => i.CityId == model.CityId).ToList();
                }
                if (model.OwnerId > 0)
                {
                    searchlist = searchlist.Where(i => i.OwnerId == model.OwnerId).ToList();
                }
                if (!string.IsNullOrEmpty(model.FullName))
                {
                    searchlist = searchlist.Where(i => i.FullName.Contains(model.FullName)).ToList();
                }
                if (!string.IsNullOrEmpty(model.BenId))
                {
                    searchlist = searchlist.Where(i => i.BenId.Contains(model.BenId)).ToList();
                }
                if (!string.IsNullOrEmpty(model.BankCode))
                {
                    searchlist = searchlist.Where(i => i.BankCode.Contains(model.BankCode)).ToList();
                }
                return searchlist;
            }
        }

        public List<PaymentSummaryViewModel> GetPaymentSummaryReport(PaymentViewModel model)
        {
            List<PaymentSummaryViewModel> searchlist = new List<PaymentSummaryViewModel>();
            using (PEMSDBEntities db = new PEMSDBEntities())
            {
                searchlist = (from p in db.Payments
                              join d in db.Donors on p.DonorCode equals d.DonorCode
                              join b in db.Banks on p.BankCode equals b.BankCode
                              select new PaymentSummaryViewModel()
                              {
                                  BenId = p.BenId,
                                  FisrtInstallmentCount = db.Payments.Count(i=> i.Installment == 1 && i.DonorCode == d.DonorCode && i.BankCode == b.BankCode),
                                  SecondInstallmentCount = db.Payments.Count(i => i.Installment == 2 && i.DonorCode == d.DonorCode && i.BankCode == b.BankCode),
                                  ThirdInstallmentCount = db.Payments.Count(i => i.Installment == 3 && i.DonorCode == d.DonorCode && i.BankCode == b.BankCode),
                                  DonorCode = p.DonorCode,
                                  DonorName = d.DonorName,
                                  BankName = b.BankName,
                                  BankCode = p.BankCode
                              }).Distinct().ToList();
                if (model.StateId > 0)
                {
                    searchlist = searchlist.Where(i => i.StateId == model.StateId).ToList();
                }
                if (model.CityId > 0)
                {
                    searchlist = searchlist.Where(i => i.CityId == model.CityId).ToList();
                }
                if (!string.IsNullOrEmpty(model.DonorCode))
                {
                    searchlist = searchlist.Where(i => i.DonorCode.Contains(model.DonorCode)).ToList();
                }
                if (!string.IsNullOrEmpty(model.BenId))
                {
                    searchlist = searchlist.Where(i => i.BenId.Contains(model.BenId)).ToList();
                }
                if (!string.IsNullOrEmpty(model.BankCode))
                {
                    searchlist = searchlist.Where(i => i.BankCode.Contains(model.BankCode)).ToList();
                }
                return searchlist;
            }
        }

        public List<PaymentFilesInfo> GetPaymentUploadedFiles(string fileId)
        {
            List<PEMS.BusinessEntities.PaymentFilesInfo> searchlist = new List<PEMS.BusinessEntities.PaymentFilesInfo>();
            using (PEMSDBEntities db = new PEMSDBEntities())
            {
                if (string.IsNullOrEmpty(fileId))
                {
                    searchlist = db.PaymentFilesInfoes.ToList();
                }
                else
                {
                    //searchlist = db.GetSurveyList().Where(x => x.MedicineName.ToLower().StartsWith(SearchTerm.ToLower())).ToList();
                }
                return searchlist;
            }
        }

        public int UploadPaymentFileInfo(string fileName, int stateId, int CityId)
        {
            using (PEMSDBEntities db = new PEMSDBEntities())
            {
                var paymentFilesObj = new PaymentFilesInfo()
                {
                    StateId = stateId,
                    CityId = CityId,
                    FileName = fileName
                };
                db.PaymentFilesInfoes.Add(paymentFilesObj);
                db.SaveChanges();
                int fileId = paymentFilesObj.FileId;
                return fileId;
            }
        }

        public int UploadPaymentFromExcel(DataTable dt, string userId, int fileID)
        {
            int result = 0;
            var date = DateTime.Now;
            int userID = Int32.Parse(userId.ToString());
            List<PEMS.BusinessEntities.Payment> paymentList = new List<BusinessEntities.Payment>();
            if (dt != null)
            {
                using (PEMSDBEntities db = new PEMSDBEntities())
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            PEMS.BusinessEntities.Payment objPayment = new PEMS.BusinessEntities.Payment();
                            objPayment.BenId = dr["BenId"].ToString();
                            objPayment.Installment = Int32.Parse(dr["Installment"].ToString());
                            objPayment.PaymentDate = DateTime.Now;
                            objPayment.Amount = dr["Amount"].ToString();
                            objPayment.DonorCode = dr["DonorCode"].ToString();
                            objPayment.BankCode = dr["BankCode"].ToString();
                            objPayment.ChequeNo = dr["ChequeNo"].ToString();
                            objPayment.EnteredBy = userID;
                            objPayment.FileId = fileID;
                            paymentList.Add(objPayment);
                        }
                    }
                    db.Payments.AddRange(paymentList);
                    result = db.SaveChanges();
                }
            }
            return result;
        }
    }
}
