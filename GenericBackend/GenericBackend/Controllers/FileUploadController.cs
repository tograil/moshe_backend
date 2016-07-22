using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GenericBackend.Controllers
{
    public class FileUploadController : ApiController
    {
        [HttpPost]
        [Authorize(Roles = "SuperUser")]
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
                        hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
                        iUploadedCnt = iUploadedCnt + 1;
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
