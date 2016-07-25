using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GenericBackend.DataModels.Plan;
using GenericBackend.DataModels.Actual;
using GenericBackend.DataModels.Document;
using GenericBackend.Excel;
using GenericBackend.Helpers;
using GenericBackend.Repository;
using GenericBackend.UnitOfWork.GoodNightMedical;

namespace GenericBackend.Controllers
{
    public class FileUploadController : ApiController
    {
        private readonly IMongoRepository<PlanSheet> _planSheetRepository;
        private readonly IMongoRepository<ActualSheet> _actualSheetRepository;
        private readonly IMongoRepository<DocumentInfo> _documentInfoRepository;

        public FileUploadController(IUnitOfWork unitOfWork)
        {
            _planSheetRepository = unitOfWork.PlanSheets;
            _documentInfoRepository = unitOfWork.DocumentsInfo;
        }

        [HttpPost]
        [AllowAnonymous]
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
                        var filename = sPath + Path.GetFileName("temp" + DateTime.Now.Millisecond + hpf.FileName);
                        hpf.SaveAs(filename);
                        iUploadedCnt = iUploadedCnt + 1;
                        var parser = new ParsePlanActual(filename);
                        var plan = parser.ParsePlanSheet();
                        var actual = parser.ParseActualSheet();

                        var documentInfo = new DocumentInfo
                        {
                            DateOfPost = DateTime.Now,
                            Name = hpf.FileName,
                            Type = "PlanActual",
                            User = "demouser@example.com",
                            Plan = plan,
                            Actual = actual
                        };

                        _documentInfoRepository.Add(documentInfo);
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
