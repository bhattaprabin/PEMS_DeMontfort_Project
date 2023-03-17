using PEMS.BusinessEntities;
using PEMS.BusinessLayer.SurveyModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEMS.Repository.Survey
{
    public interface ISurveyService
    {
        List<SurveyViewModel> GetSurveyList(SurveyViewModel model);
        SurveyViewModel GetSurveyByID(int ownerID);
        int UploadSurveyFromExcel(DataTable dt, string userId, int fileID);
        int UploadExcelFileInfo(string fileName, int stateId, int CityId, int userID);
        List<SurveyFilesViewModel> GetSurveyUploadedFiles(string fileId);
        int UpdateSurveyEvaluation(SurveyViewModel model);
        List<SurveySummaryReportViewModel> GetSurveySummaryReport(SurveyViewModel model);
    }
}
