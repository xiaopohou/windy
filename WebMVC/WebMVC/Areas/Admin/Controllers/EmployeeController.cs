using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Windy.Common.Libraries;
using Windy.Service.DAL;
using Windy.Service.Data;

namespace Windy.WebMVC.Areas.Admin.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Admin/Employee/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QueryData()
        {
            int page = Request.Form["page"] != "" ? Convert.ToInt32(Request.Form["page"]) : 0;
            int size = Request.Form["rows"] != "" ? Convert.ToInt32(Request.Form["rows"]) : 0;
            string sort = Request.Form["sort"] != "" ? Request.Form["sort"] : "";
            string order = Request.Form["order"] != "" ? Request.Form["order"] : "";


            if (page < 1) return Content("");

            List<Employee> lstEmployee = new List<Employee>();
            short shRet = SystemContext.Instance.EmployeeService.GetEmployeePageList(size, page, "", ref lstEmployee);
            JsonHelper json = new JsonHelper();
            string strJson = string.Empty;
            foreach (Employee item in lstEmployee)
            {
                json.AddItem("ID", item.ID.ToString());
                json.AddItem("EmpNo", item.EmpNo);
                json.AddItem("Pwd", item.Pwd);
                //json.AddItem("NewsContent", item.NewsContent);
                json.AddItem("Name", item.Name);
                json.AddItem("Tel", item.Tel);
                json.AddItem("RoleType", item.RoleType);
                json.ItemOk();
            }
            int totalCount = 0;
            shRet = SystemContext.Instance.EmployeeService.GetEmployeeTotalCount("", ref totalCount);
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
            return Content(strJson);
        }

        #region 删除指定ID 的数据
        /// <summary>
        /// 删除数据
        /// </summary>
        public ActionResult DelData()
        {
            string writeMsg = "删除失败！";

            string selectID = Request.Form["cbx_select"] != "" ? Request.Form["cbx_select"] : "";
            if (selectID != string.Empty && selectID != "0")
            {
                short shRet = SystemContext.Instance.EmployeeService.Delete(selectID);

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
        #endregion

        #region 添加或修改提交的数据
        /// <summary>
        /// 添加或修改数据
        /// </summary>
        public ActionResult UpdateData()
        {
            bool blResult = false;
            int id = Request.Form["id"] != "" ? Convert.ToInt32(Request.Form["id"]) : 0;
            Employee model = GetData(id);

            string writeMsg = "操作失败！";
            if (model != null)
            {
                if (id < 1)
                {
                    short shRet = SystemContext.Instance.EmployeeService.Add(model);

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
                    short shRet = SystemContext.Instance.EmployeeService.Update(model);
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

        /// <summary>
        /// 取得数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Employee GetData(int id)
        {
            Employee model = new Employee();
            if (id > 0)
            {
                SystemContext.Instance.EmployeeService.GetEmployeeByID(id.ToString(), ref model);

            }
            //model.Name = Request.Form["ipt_Name"] != "" ? Request.Form["ipt_Name"] : "";
            //model.Url = Request.Form["ipt_Url"] != "" ? Request.Form["ipt_Url"] : "";
            model.EmpNo = Request.Form["EmpNo"] != "" ? Request.Form["EmpNo"] : "";
            model.Pwd = Request.Form["Pwd"] != "" ? Request.Form["Pwd"] : "";
            model.Name = Request.Form["Name"] != "" ? Request.Form["Name"] : "";
            model.Tel = Request.Form["Tel"] != "" ? Request.Form["Tel"] : "";
            model.RoleType = Request.Form["RoleType"] != "" ? Request.Form["RoleType"] : "";
            return model;
        }
        #endregion

        #region 查询指定ID 的数据
        /// <summary>
        /// 获取指定ID的数据
        /// </summary>
        public ActionResult QueryOneData()
        {
            string id = Request.Form["id"] != "" ? Request.Form["id"] : "0";
            Employee item = new Employee();
            short shRet = SystemContext.Instance.EmployeeService.GetEmployeeByID(id, ref item);
            JsonHelper json = new JsonHelper();
            string strJson = string.Empty;
            if (shRet == ExecuteResult.OK)
            {
                json.AddItem("ID", item.ID.ToString());
                json.AddItem("EmpNo", item.EmpNo);
                json.AddItem("Pwd", item.Pwd);
                json.AddItem("Name", item.Name);
                json.AddItem("Tel", item.Tel);
                json.AddItem("RoleType", item.RoleType);
                json.ItemOk();
            }
            strJson = json.ToEasyuiOneModelJsonString();
            // strJson = "[{\"ID\":\"81\",\"EmpNo\":\"jxdhlgljp\",\"Name\":\"hello\",\"Pwd\":\"111111\",\"Tel\":\"18720081979\"}]";
            return Content(strJson);
        }
        #endregion
       
    }
}
