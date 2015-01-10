using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Windy.Common.Libraries;
namespace Windy.WebMVC.Areas.Admin.Controllers
{
    public class LogManageController : Controller
    {
        //
        // GET: /Admin/LogManage/

        public ActionResult Index()
        {
            string szDictionary = string.Format("{0}bin\\Logs\\TxtLog",Request.PhysicalApplicationPath);
            //Response.Write(szDictionary);
            FileInfo[] fileInfos= GlobalMethods.IO.GetFiles(szDictionary);
            
            ViewData["fileInfos"] = fileInfos;
            return View();
        }
        public ActionResult Detail(string id)
        {
            string szFileFuleName = string.Format("{0}bin\\Logs\\TxtLog\\{1}.log", Request.PhysicalApplicationPath, id);
            string szTextData=string.Empty;
            GlobalMethods.IO.GetFileText(szFileFuleName,ref szTextData);
            szTextData = szTextData.Replace("\n", "<br/>");
            Response.Write(szTextData);
            return View();
        }
    }
}
