using PEMS.BusinessEntities;
using PEMS.BusinessLayer.InspectionModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEMS.Repository.Inspection
{
    public class InspectionService : IInspectionService
    {
        public List<InspectionViewModel> GetInspectionList(InspectionViewModel model)
        {
            List<InspectionViewModel> searchlist = new List<InspectionViewModel>();
            using (PEMSDBEntities db = new PEMSDBEntities())
            {
                    searchlist = (from i in db.Inspections  
                                  join b in db.BeneficiaryDetails on i.BenId equals b.BenId
                                  join st in db.States on b.StateId equals st.StateId
                                  join ct in db.Cities on b.CityId equals ct.CityId
                                  select new InspectionViewModel()
                                  {
                                      BenId = i.BenId,
                                      InspectionLevel = i.InspectionLevel,
                                      StateId = st.StateId,
                                      StateName = st.StateName,
                                      CityId = ct.CityId,
                                      CityName = ct.CityName,
                                      FullName = b.BeneficiaryName
                                  }).Distinct().ToList();
                if (model.StateId > 0)
                {
                    searchlist = searchlist.Where(i => i.StateId == model.StateId).ToList();
                }
                if (model.CityId > 0)
                {
                    searchlist = searchlist.Where(i => i.CityId == model.CityId).ToList();
                }
                if (model.InspectionLevel > 0)
                {
                    searchlist = searchlist.Where(i => i.InspectionLevel == model.InspectionLevel).ToList();
                }
                if (!string.IsNullOrEmpty(model.BenId))
                {
                    searchlist = searchlist.Where(i => i.BenId.Contains(model.BenId)).ToList();
                }
                return searchlist;
            }
        }

        public List<InspectionFilesViewModel> GetInspectionUploadedFiles(string fileId)
        {
            List<InspectionFilesViewModel> searchlist = new List<InspectionFilesViewModel>();
            using (PEMSDBEntities db = new PEMSDBEntities())
            {
                if (string.IsNullOrEmpty(fileId))
                {
                     searchlist = (from sf in db.InspectionFilesInfoes
                                    join s in db.Inspections on sf.FileId equals s.FileId
                                    join st in db.States on sf.StateId equals st.StateId
                                    join c in db.Cities on sf.CityId equals c.CityId

                                    select new InspectionFilesViewModel()
                                    {
                                        FileId = sf.FileId,
                                        StateName = st.StateName,
                                        CityName = c.CityName,
                                        FileName = sf.FileName,
                                        CountRecords = db.Inspections.Count(s => s.FileId == sf.FileId)
                                    }).Distinct().ToList();
                }
                else
                {
                    //searchlist = db.GetSurveyList().Where(x => x.MedicineName.ToLower().StartsWith(SearchTerm.ToLower())).ToList();
                }
                return searchlist;
            }
        }

        public int UploadInspectionFileInfo(string fileName, int stateId, int CityId)
        {
            using (PEMSDBEntities db = new PEMSDBEntities())
            {
                var inspectionFilesObj = new PEMS.BusinessEntities.InspectionFilesInfo()
                {
                    StateId = stateId,
                    CityId = CityId,
                    FileName = fileName
                    
                };
                db.InspectionFilesInfoes.Add(inspectionFilesObj);
                db.SaveChanges();
                int fileId = inspectionFilesObj.FileId;
                return fileId;
            }
        }

        public int UploadInspectionFromExcel(DataTable dt, string userId, int fileID)
        {
            int result = 0;
            var date = DateTime.Now;
            int userID = Int32.Parse(userId.ToString());
            List<PEMS.BusinessEntities.Inspection> inspectionList = new List<BusinessEntities.Inspection>();
            if (dt != null)
            {
                using (PEMSDBEntities db = new PEMSDBEntities())
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            PEMS.BusinessEntities.Inspection objInspection = new PEMS.BusinessEntities.Inspection();
                            objInspection.BenId = dr["BenId"].ToString();
                            objInspection.InspectionLevel = Int32.Parse(dr["InspectionLevel"].ToString());
                            objInspection.InspectionDate = DateTime.Now;
                            objInspection.PhotoPath = dr["PhotoPath"].ToString();
                            objInspection.InspectedBy = Int32.Parse(dr["InspectionLevel"].ToString());
                            objInspection.EnteredBy = userID;
                            objInspection.FileId = fileID;
                            inspectionList.Add(objInspection);
                        }
                    }
                    db.Inspections.AddRange(inspectionList);
                    result = db.SaveChanges();
                }
            }
            return result;
        }
        public List<InspectionSummaryViewModel> GetInspectionSummaryReport(InspectionViewModel model)
        {
            List<InspectionSummaryViewModel> searchlist = new List<InspectionSummaryViewModel>();
            using (PEMSDBEntities db = new PEMSDBEntities())
            {
                searchlist = (from i in db.Inspections
                              join b in db.BeneficiaryDetails on i.BenId equals b.BenId
                              join st in db.States on b.StateId equals st.StateId
                              join ct in db.Cities on b.CityId equals ct.CityId
                              select new InspectionSummaryViewModel()
                              {
                                  FisrtInspCount = db.Inspections.Count(i => i.InspectionLevel == 1 && b.StateId == st.StateId && b.CityId == ct.CityId),
                                  SecondInspCount = db.Inspections.Count(i => i.InspectionLevel == 2 && b.StateId == st.StateId && b.CityId == ct.CityId),
                                  ThirdInspCount = db.Inspections.Count(i => i.InspectionLevel == 3 && b.StateId == st.StateId && b.CityId == ct.CityId),
                                  StateId = st.StateId,
                                  StateName = st.StateName,
                                  CityId = ct.CityId,
                                  CItyName = ct.CityName
                              }).Distinct().ToList();
                if (model.StateId > 0)
                {
                    searchlist = searchlist.Where(i => i.StateId == model.StateId).ToList();
                }
                if (model.CityId > 0)
                {
                    searchlist = searchlist.Where(i => i.CityId == model.CityId).ToList();
                }
                return searchlist;
            }
        }

    }
}
