using PEMS.BusinessLayer.InspectionModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEMS.Repository.Inspection
{
    public interface IInspectionService 
    {
        List<InspectionViewModel> GetInspectionList(InspectionViewModel model);
        int UploadInspectionFromExcel(DataTable dt, string userId, int fileID);
        int UploadInspectionFileInfo(string fileName, int stateId, int CityId);
        List<InspectionFilesViewModel> GetInspectionUploadedFiles(string fileId);
        List<InspectionSummaryViewModel> GetInspectionSummaryReport(InspectionViewModel model);
    }
}
