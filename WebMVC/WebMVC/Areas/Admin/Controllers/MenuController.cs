using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Windy.Common.Libraries;
using Windy.Service.DAL;
using Windy.Service.Data;

namespace Windy.WebMVC.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /Admin/Menu/

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetGridList()
        {
            //Employee CurrentEmployee = Session["curUser"] as Employee;
            //if (CurrentEmployee == null)
            //{
            //    return RedirectToAction("Login");
            //}
            int page = Request.Form["page"] != "" ? Convert.ToInt32(Request.Form["page"]) : 0;
            int size = Request.Form["rows"] != "" ? Convert.ToInt32(Request.Form["rows"]) : 0;
            string sort = Request.Form["sort"] != "" ? Request.Form["sort"] : "";
            string order = Request.Form["order"] != "" ? Request.Form["order"] : "";
            if (page < 1) 
                return Content("");

            List<Menus> lstMenus = new List<Menus>();
            short shRet = SystemContext.Instance.MenuServices.GetMenuPageList(size, page, "", ref lstMenus);
            JsonHelper json = new JsonHelper();
            string strJson = string.Empty;
            foreach (Menus item in lstMenus)
            {
                json.AddItem("id", item.ID.ToString());
                json.AddItem("MenuName", item.MenuName);
                json.AddItem("Url", item.Url);
                //json.AddItem("NewsContent", item.NewsContent);
                if (item.ParentID != "" && item.ParentID != "0")
                    json.AddItem("_parentId", item.ParentID);
                json.AddItem("ParentName", item.ParentName);
                json.AddItem("Icon", item.Icon);
                json.AddItem("MenuType", item.MenuType);
                json.AddItem("Description", item.Description);
                json.ItemOk();
            }
            int totalCount = 0;
            shRet = SystemContext.Instance.MenuServices.GetMenuTotalCount("", ref totalCount);
            json.totlalCount = totalCount;
            if (json.totlalCount > 0)
            {
                strJson = json.ToEasyuiGridJsonString();
            }
            else
            {
                strJson = @"[]";
            }
            // json = "{ \"rows\":[ { \"ID\":\"48\",\"NewsTitle\":\"mr\",\"NewsContent\":\"mrsoft\",\"CreateTime\":\"2013-12-23\",\"CreateUser\":\"ceshi\",\"ModifyTime\":\"2013-12-23\",\"ModifyUser\":\"ceshi\"} ],\"total\":3}";
            //strJson = "{\"total\":2,\"rows\":[{\"id\":\"2\",\"MenuName\":\"ff\",\"Url\":\"ff\",\"ParentName\":\"\",\"Icon\":\"ff\",\"MenuType\":\"\"},{\"id\":\"1\",\"MenuName\":\"日\",\"Url\":\"1\",\"_parentId\":\"2\",\"ParentName\":\"\",\"Icon\":\"2\",\"MenuType\":\"\"}]}";
            return Content(strJson);
        }

        public ActionResult GetComboTree()
        {
            List<Menus> lstMenus = new List<Menus>();
            short shRet = SystemContext.Instance.MenuServices.GetMenuList("", ref lstMenus);
            DataTable dtMenus = GlobalMethods.Table.GetDataTable(lstMenus);
            DataRow dataRow = dtMenus.Rows.Add();
            dataRow["ID"] = "0";
            dataRow["ParentID"] = "";
            dataRow["MenuName"] = "父节点";
            string strJson = JsonHelper.GetTreeJsonByTable(dtMenus, "ID", "MenuName", "ParentID", "", "");
            //string strJson = "[{\"id\":\"1\",\"text\":\"hello1\",\"checked\":\"true\",\"state\":\"open\",\"children\":[{\"id\":\"2\",\"text\":\"hello2\",\"state\":\"open\"}]},{\"id\":\"1\",\"text\":\"hello1\",\"state\":\"open\",\"children\":[{\"id\":\"2\",\"text\":\"hello2\",\"state\":\"open\"}]}]";
            return Content(strJson);
        }
        public ActionResult QueryOneData()
        {
            string id = Request.Form["id"] != "" ? Request.Form["id"] : "0";
            Menus item = new Menus();
            short shRet = SystemContext.Instance.MenuServices.GetMenuByID(id, ref item);
            JsonHelper json = new JsonHelper();
            string strJson = string.Empty;
            if (shRet == ExecuteResult.OK)
            {
                json.AddItem("ID", item.ID.ToString());
                json.AddItem("MenuName", item.MenuName);
                json.AddItem("Url", item.Url);
                json.AddItem("ParentID", item.ParentID);
                json.AddItem("ParentName", item.ParentName);
                json.AddItem("Icon", item.Icon);
                json.AddItem("Description", item.Description);
                json.AddItem("MenuType", item.MenuType);
                json.ItemOk();
            }
            strJson = json.ToEasyuiOneModelJsonString();
            // strJson = "[{\"ID\":\"81\",\"EmpNo\":\"jxdhlgljp\",\"Name\":\"hello\",\"Pwd\":\"111111\",\"Tel\":\"18720081979\"}]";
            return Content(strJson);
        }
        public ActionResult Update()
        {
            bool blResult = false;
            int id = Request.Form["id"] != "" ? Convert.ToInt32(Request.Form["id"]) : 0;
            Menus model = GetData(id);

            string writeMsg = "操作失败！";
            if (model != null)
            {
                if (id < 1)
                {
                    short shRet = SystemContext.Instance.MenuServices.Add(model);

                    if (shRet == ExecuteResult.OK)
                    {
                        writeMsg = "增加成功!";
                    }
                    else
                    {
                        writeMsg = "增加失败!";
                    }
                }
                else
                {
                    short shRet = SystemContext.Instance.MenuServices.Update(model);
                    if (shRet == ExecuteResult.OK)
                    {
                        writeMsg = "更新成功!";
                    }
                    else
                    {
                        writeMsg = "更新失败!";
                    }

                }
            }
            return Content(writeMsg);
        }
        public ActionResult DelData()
        {
            string writeMsg = "删除失败！";

            string selectID = Request.Form["cbx_select"] != "" ? Request.Form["cbx_select"] : "";
            if (selectID != string.Empty && selectID != "0")
            {
                short shRet = SystemContext.Instance.MenuServices.Delete(selectID);

                if (shRet == ExecuteResult.OK)
                {
                    writeMsg = string.Format("删除成功");
                }
                else
                {
                    writeMsg = "删除失败！";
                }
            }

            return Content(writeMsg);
        }
        /// <summary>
        /// 取得数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Menus GetData(int id)
        {
            Menus model = new Menus();
            model.ParentID = "0";
            if (id > 0)
            {
                SystemContext.Instance.MenuServices.GetMenuByID(id.ToString(), ref model);

            }
            //model.Name = Request.Form["ipt_Name"] != "" ? Request.Form["ipt_Name"] : "";
            //model.Url = Request.Form["ipt_Url"] != "" ? Request.Form["ipt_Url"] : "";
            model.MenuName = Request.Form["MenuName"] != "" ? Request.Form["MenuName"] : "";
            model.Url = Request.Form["Url"] != "" ? Request.Form["Url"] : "";
            model.ParentID = Request.Form["ParentID"] != "" ? Request.Form["ParentID"] : "0";
            model.Icon = Request.Form["Icon"] != "" ? Request.Form["Icon"] : "";
            model.Description = Request.Form["Description"] != "" ? Request.Form["Description"] : "";
            model.MenuType = Request.Form["MenuType"] != "" ? Request.Form["MenuType"] : "";
            return model;
        }

        public ActionResult GetEmpList()
        {
            List<Employee> lstEmployee = new List<Employee>();
            short shRet = SystemContext.Instance.EmployeeService.GetEmployeeList("", "", "", ref lstEmployee);
            JsonHelper json = new JsonHelper();
            foreach (Employee item in lstEmployee)
            {
                json.AddItem("ID", item.ID);
                json.AddItem("EmpNo", item.EmpNo);
                json.AddItem("Name", item.Name);
                json.ItemOk();
            }

            json.totlalCount = lstEmployee.Count;
            string strJson = json.ToEasyuiGridJsonString();
            return Content(strJson);
        }
        public ActionResult SaveByEmpID()
        {
            string MenuID = Request.Form["MenuID"] != "" ? Request.Form["MenuID"].ToString() : "";
            string EmpID = Request.Form["EmpID"] != "" ? Request.Form["EmpID"].ToString() : "";
            string[] arrMenuID = MenuID.Split(',');
            string writeMsg = "保存成功！";
            if (EmpID == "")
                writeMsg = "保存失败！";
            else
            {
                List<EmpMenu> lstEmpMenu = new List<EmpMenu>();
                foreach (string item in arrMenuID)
                {
                    if (string.IsNullOrEmpty(item))
                        continue;
                    EmpMenu model = new EmpMenu();
                    model.EmpID = int.Parse(EmpID);
                    model.MenuID = int.Parse(item);
                    lstEmpMenu.Add(model);
                }
                short shRet = SystemContext.Instance.MenuServices.SaveByEmpID(lstEmpMenu, EmpID);
                if (shRet != ExecuteResult.OK)
                    writeMsg = "保存失败";
            }
            return Content(writeMsg);
        }

        public ActionResult GetMenuTree()
        {

            List<Menus> lstMenus = new List<Menus>();
            short shRet = SystemContext.Instance.MenuServices.GetMenuList("", ref lstMenus);
            DataTable dtMenu = GlobalMethods.Table.GetDataTable(lstMenus);
            //得到ID号
            string szCheckItems = string.Empty;
            if (Request.QueryString["EmpID"] != null && Request.QueryString["EmpID"] != "" && Request.QueryString["EmpID"] != "0")
            {
                List<EmpMenu> lstEmpMenu = new List<EmpMenu>();
                string EmpID = Request.QueryString["EmpID"];
                shRet = SystemContext.Instance.MenuServices.GetEmpMenuByEmpID(EmpID, ref lstEmpMenu);
                foreach (EmpMenu item in lstEmpMenu)
                {
                    szCheckItems += "," + item.MenuID;
                }
            }
            string strJson = JsonHelper.GetTreeJsonByTable(dtMenu, "ID", "MenuName", "ParentID", "0", szCheckItems);
            //string strJson = "[{\"id\":\"1\",\"text\":\"hello1\",\"checked\":\"true\",\"state\":\"open\",\"children\":[{\"id\":\"2\",\"text\":\"hello2\",\"state\":\"open\"}]},{\"id\":\"1\",\"text\":\"hello1\",\"state\":\"open\",\"children\":[{\"id\":\"2\",\"text\":\"hello2\",\"state\":\"open\"}]}]";
            return Content(strJson);
        }
    }
}
