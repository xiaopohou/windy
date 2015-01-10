using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Windy.Common.Libraries;
using Windy.Service.DAL;
using Windy.Service.Data;

namespace Windy.WebMVC.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            if (Session["CurrentEmployee"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public ActionResult test()
        {
            return View();
        }
        public ActionResult Login()
        {
            Session["CurrentEmployee"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Login(Employee emplyee)
        {

            //验证账号密码
            short shRet = SystemContext.Instance.EmployeeService.Exists(emplyee.EmpNo, emplyee.Pwd, ref emplyee);

            if (shRet == ExecuteResult.OK)
            {
                Session["CurrentEmployee"] = emplyee;
                return RedirectToAction("Index");
            }
            else
            {
                string error = string.Empty;
                if (shRet == ExecuteResult.RES_NO_FOUND)
                    error = "账号或密码错误";
                else
                    error = ExecuteResult.GetResultMessage(shRet);
                ModelState.AddModelError("Message", error);
                return View();
            }

        }
        public ActionResult ChangePwd()
        {
            string szPwdOld = Request.Form["pwdold"] != "" ? Request.Form["pwdold"] : "";
            string szPwdNew = Request.Form["pwdnew"] != "" ? Request.Form["pwdnew"] : "";
            string szPwdConfirm = Request.Form["pwdconfirm"] != "" ? Request.Form["pwdconfirm"] : "";
            Employee curUser = Session["CurrentEmployee"] as Employee;
            if (curUser == null)
                return RedirectToAction("Login");
            string writeMsg = "更改失败！";
            if (curUser.Pwd != szPwdOld)
            {
                writeMsg = "原始密码错误，无法更改密码！";
            }
            else {
                curUser.Pwd = szPwdNew;
                short shRet = SystemContext.Instance.EmployeeService.Update(curUser);

                if (shRet == ExecuteResult.OK)
                {
                    writeMsg = "更改成功!";
                }
                else
                {
                    writeMsg = "更改失败!";
                }
            }
            return Content(writeMsg);
        }
        // GET api/admin
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [HttpPost]
        public ActionResult GetEmpMenu()
        {
            Employee CurrentEmployee = Session["CurrentEmployee"] as Employee;
            if (CurrentEmployee == null)
            {
                return RedirectToAction("Login");
            }
            List<Menus> lstMenus = new List<Menus>();
            short shRet = SystemContext.Instance.MenuServices.GetMenuList("", ref lstMenus);
            DataTable dtMenu = GlobalMethods.Table.GetDataTable(lstMenus);
            //得到ID号
            string szCheckItems = string.Empty;

            List<EmpMenu> lstEmpMenu = new List<EmpMenu>();
            string EmpID = CurrentEmployee.ID;
            //EmpID = "10";
            shRet = SystemContext.Instance.MenuServices.GetEmpMenuByEmpID(EmpID, ref lstEmpMenu);
            foreach (EmpMenu item in lstEmpMenu)
            {
                szCheckItems += "," + item.MenuID;
            }

            string strJson = string.Empty;
            if (!string.IsNullOrEmpty(szCheckItems))
            {
                strJson = JsonHelper.GetMenuJsonByTable(dtMenu, "ID", "MenuName", "ParentID", "Url", "MenuType", "Icon", "0", szCheckItems);
                strJson = "{" + strJson + "}";
            }
            else
                strJson = "\"menus\":[]";
            //string strJson = "[{\"id\":\"1\",\"text\":\"hello1\",\"checked\":\"true\",\"state\":\"open\",\"children\":[{\"id\":\"2\",\"text\":\"hello2\",\"state\":\"open\"}]},{\"id\":\"1\",\"text\":\"hello1\",\"state\":\"open\",\"children\":[{\"id\":\"2\",\"text\":\"hello2\",\"state\":\"open\"}]}]";

            return Content(strJson);

        }


    }
}
