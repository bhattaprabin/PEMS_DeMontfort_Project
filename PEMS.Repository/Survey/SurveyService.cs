using PEMS.BusinessLayer.SurveyModel;
using System;
using System.Collections.Generic;
using PEMS.BusinessEntities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PEMS.Repository.Survey
{
    public class SurveyService : ISurveyService
    {
        public List<SurveyViewModel> GetSurveyList(SurveyViewModel model)
        {
            List<SurveyViewModel> searchlist = new List<SurveyViewModel>();
            using (PEMSDBEntities db = new PEMSDBEntities())
            {
                searchlist = (from sv in db.Surveys
                              join st in db.States on sv.StateId equals st.StateId
                              join ct in db.Cities on sv.CityId equals ct.CityId
                              select new SurveyViewModel()
                              {
                                  StateId = st.StateId,
                                  CityId = ct.CityId,
                                  OwnerFullName = sv.OwnerFirstName + " " + sv.OwnerLastName,
                                  OwnerId = sv.OwnerId,
                                  OwnerPhoneNumber = sv.OwnerPhoneNumber,
                                  SurveyedDate = sv.SurveyedDate.ToString(),
                                  StateName = st.StateName,
                                  CityName = ct.CityName
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
                if (!string.IsNullOrEmpty(model.OwnerFullName))
                {
                    searchlist = searchlist.Where(i => i.OwnerFullName.Contains(model.OwnerFullName)).ToList();
                }
                return searchlist;
            }
        }

        public SurveyViewModel GetSurveyByID(int ownerID)
        {
            SurveyViewModel objModel = new SurveyViewModel();
            using (PEMSDBEntities db = new PEMSDBEntities())
            {
                objModel = (from s in db.Surveys
                            select new SurveyViewModel()
                            {
                                OwnerId = s.OwnerId,
                                OwnerFirstName = s.OwnerFirstName,
                                OwnerLastName = s.OwnerLastName,
                                OwnerPhoneNumber = s.OwnerPhoneNumber,
                                DamageStatus = s.DamageStatus
                            }).FirstOrDefault(x => x.OwnerId == ownerID);
            }
            return objModel;
        }
        public List<SurveyFilesViewModel> GetSurveyUploadedFiles(string fileId)
        {
            List<SurveyFilesViewModel> searchlist = new List<SurveyFilesViewModel>();
            using (PEMSDBEntities db = new PEMSDBEntities())
            {
                if (string.IsNullOrEmpty(fileId))
                {
                    searchlist = (from sf in db.SurveyFilesInfoes
                                  join s in db.Surveys on sf.FileId equals s.FileId
                                  join st in db.States on sf.StateId equals st.StateId
                                  join c in db.Cities on sf.CityId equals c.CityId

                                  select new SurveyFilesViewModel()
                                  {
                                      FileId = sf.FileId,
                                      StateName = st.StateName,
                                      CityName = c.CityName,
                                      FileName = sf.FileName,
                                      CountRecords = db.Surveys.Count(s => s.FileId == sf.FileId)
                                  }).Distinct().ToList();
                }
                else
                {
                    //searchlist = db.GetSurveyList().Where(x => x.MedicineName.ToLower().StartsWith(SearchTerm.ToLower())).ToList();
                }
                return searchlist;
            }
        }

        public int UploadExcelFileInfo(string fileName, int stateId, int CityId, int userID)
        {
            var date = DateTime.Now;
            using (PEMSDBEntities db = new PEMSDBEntities())
            {
                var surveyFilesObj = new SurveyFilesInfo()
                {
                    StateId = stateId,
                    CityId = CityId,
                    FileName = fileName,
                    EnteredDate = date,
                    EnteredBy = userID
                };
                db.SurveyFilesInfoes.Add(surveyFilesObj);
                db.SaveChanges();
                int fileId = surveyFilesObj.FileId;
                return fileId;
            }
        }

        public int UploadSurveyFromExcel(DataTable dt, string userId, int fileID)
        {
            int result = 0;
            var date = DateTime.Now;
            int userID = Int32.Parse(userId.ToString());
            List<PEMS.BusinessEntities.Survey> surveylist = new List<BusinessEntities.Survey>();
            if (dt != null)
            {
                using (PEMSDBEntities db = new PEMSDBEntities())
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            PEMS.BusinessEntities.Survey objSurvey = new PEMS.BusinessEntities.Survey();
                            objSurvey.OwnerId = Int32.Parse(dr["OwnerId"].ToString());
                            objSurvey.OwnerFirstName = dr["OwnerFirstName"].ToString();
                            objSurvey.OwnerLastName = dr["OwnerLastName"].ToString();
                            objSurvey.StateId = Int32.Parse(dr["StateId"].ToString());
                            objSurvey.CityId = Int32.Parse(dr["CityId"].ToString());
                            objSurvey.PostCodeId = Int32.Parse(dr["CityId"].ToString());
                            objSurvey.GenderCode = dr["GenderCode"].ToString();
                            objSurvey.OwnerPhoneNumber = dr["OwnerPhoneNumber"].ToString();
                            objSurvey.DamageStatus = dr["DamageStatus"].ToString();
                            objSurvey.FrontHousePhoto = dr["FrontHousePhoto"].ToString();
                            objSurvey.LeftHousePhoto = dr["LeftHousePhoto"].ToString();
                            objSurvey.RightHousePhoto = dr["RightHousePhoto"].ToString();
                            objSurvey.BackHousePhoto = dr["BackHousePhoto"].ToString();
                            objSurvey.SurveyedBy = userID;
                            objSurvey.SurveyedDate = date;
                            objSurvey.FileId = fileID;
                            surveylist.Add(objSurvey);
                        }
                    }
                    db.Surveys.AddRange(surveylist);
                    result = db.SaveChanges();
                }
            }
            return result;
        }
        public int UpdateSurveyEvaluation(SurveyViewModel model)
        {
            int i = 0;
            PEMS.BusinessEntities.Survey objSurvey = new PEMS.BusinessEntities.Survey();
            using (PEMSDBEntities db = new PEMSDBEntities())
            {
                objSurvey = db.Surveys.Find(model.OwnerId);
                objSurvey.DamageStatus = model.DamageStatus;
                db.Entry(objSurvey).State = System.Data.Entity.EntityState.Modified;
                i = db.SaveChanges();
            }
            return i;
        }

        public List<SurveySummaryReportViewModel> GetSurveySummaryReport(SurveyViewModel model)
        {
            List<SurveySummaryReportViewModel> searchlist = new List<SurveySummaryReportViewModel>();
            using (PEMSDBEntities db = new PEMSDBEntities())
            {
                searchlist = (from sv in db.Surveys
                              join st in db.States on sv.StateId equals st.StateId
                              join ct in db.Cities on sv.CityId equals ct.CityId
                              select new SurveySummaryReportViewModel()
                              {
                                  StateId = st.StateId,
                                  CityId = ct.CityId,
                                  CityName = ct.CityName,
                                  StateName = st.StateName,
                                  MaleCount = db.Surveys.Count(k => k.GenderCode == "M" && k.StateId == sv.StateId && k.CityId == sv.CityId),
                                  FemaleCount = db.Surveys.Count(k => k.GenderCode == "F" && k.StateId == sv.StateId && k.CityId == sv.CityId),
                                  OtherCount = db.Surveys.Count(k => k.GenderCode == "O" && k.StateId == sv.StateId && k.CityId == sv.CityId),
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