using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GenericBackend.DataModels.Plan;
using GenericBackend.Excel;
using GenericBackend.Helpers;
using GenericBackend.Repository;
using GenericBackend.UnitOfWork.GoodNightMedical;

namespace GenericBackend.Controllers
{
    public class FileUploadController : ApiController
    {
        private readonly IMongoRepository<PlanSheet> _planSheetRepository;

        public FileUploadController(IUnitOfWork unitOfWork)
        {
            _planSheetRepository = unitOfWork.PlanSheets;
        }
        [HttpPost]
        [AuthorizeUser(AccessLevel = "SuperUser")]
        public string UploadFiles()
        {
            int iUploadedCnt = 0;
            
            string sPath = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Start/ExcelDocuments/");

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                    {
                        var filename = sPath + Path.GetFileName(hpf.FileName);
                        hpf.SaveAs(filename);
                        iUploadedCnt = iUploadedCnt + 1;
                        var parser = new ParsePlanActual(filename);
                        var result = parser.ParsePlanSheet();

                        _planSheetRepository.Add(result);
                    }
                }
            }
            
            if (iUploadedCnt > 0)
            {
                return iUploadedCnt + " Files Uploaded Successfully";
            }
            else
            {
                return "Upload Failed";
            }
        }
    }
}
