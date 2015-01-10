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
    public class OrgnizationController : Controller
    {
        //
        // GET: /Admin/Menu/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QueryEmployee()
        {
            string OrgID = Request.Form["OrgID"] != "" ? Request.Form["OrgID"].ToString() : "";
            //查询所有用户信息
            List<Employee> lstEmployee = new List<Employee>();
            short shRet = SystemContext.Instance.EmployeeService.GetEmployeeList("", "", "", ref lstEmployee);
            List<EmpOrg> lstEmpOrg = new List<EmpOrg>();
            shRet = SystemContext.Instance.OrgnizationServices.GetEmpOrgByOrgID(OrgID, ref lstEmpOrg);
            string writeMsg = "";
            foreach (Employee item in lstEmployee)
            {
                bool b = false;
                foreach (EmpOrg item1 in lstEmpOrg)
                {
                    if (item1.EmpID.ToString() == item.ID)
                    {
                        b = true;
                        break;
                    }
                }
                if (b)
                    writeMsg = writeMsg + string.Format("<input type=\"checkbox\"  checked=true id=\"{0}\" class=\"empid\" />{1}&nbsp", item.ID, item.EmpNo + "|" + item.Name);
                else
                    writeMsg = writeMsg + string.Format("<input type=\"checkbox\"  id=\"{0}\" class=\"empid\"  />{1}&nbsp", item.ID, item.EmpNo + "|" + item.Name);
            }
            return Content(writeMsg);
        }

        public ActionResult SaveByOrgID()
        {
            string OrgID = Request.Form["OrgID"] != "" ? Request.Form["OrgID"].ToString() : "";
            string EmpID = Request.Form["EmpID"] != "" ? Request.Form["EmpID"].ToString() : "";
            string[] arrEmpID = EmpID.Split(',');
            string writeMsg = "保存成功！";
            if (EmpID == "")
                writeMsg = "保存失败！";
            else
            {
                List<EmpOrg> lstEmpRog = new List<EmpOrg>();
                foreach (string item in arrEmpID)
                {
                    if (string.IsNullOrEmpty(item))
                        continue;
                    EmpOrg model = new EmpOrg();
                    model.EmpID = int.Parse(item);
                    model.OrgID = int.Parse(OrgID);
                    lstEmpRog.Add(model);
                }
                short shRet = SystemContext.Instance.OrgnizationServices.SaveByOrgID(lstEmpRog, OrgID);
                if (shRet != ExecuteResult.OK)
                    writeMsg = "保存失败";
            }
            return Content(writeMsg);
        }

        public ActionResult GetEmpName()
        {
            string result = "";
            string szOrgID = Request.Form["OrgID"] == null ? Request.QueryString["OrgID"].ToString() : Request.Form["OrgID"];
            List<EmpOrg> lstEmpOrg = new List<EmpOrg>();
            short shRet = SystemContext.Instance.OrgnizationServices.GetEmpOrgByOrgID(szOrgID, ref lstEmpOrg);
            string szEmpIDs = string.Empty;
            if (lstEmpOrg.Count > 0)
            {
                foreach (EmpOrg item in lstEmpOrg)
                {
                    if (szEmpIDs != "")
                        szEmpIDs = "," + szEmpIDs;
                    szEmpIDs = item.EmpID + szEmpIDs;
                }
                List<Employee> lstEmployee = new List<Employee>();
                shRet = SystemContext.Instance.EmployeeService.GetEmployeeList(szEmpIDs, "", "", ref lstEmployee);
                foreach (Employee item in lstEmployee)
                {
                    if (result != "")
                        result = "," + result;
                    result = item.Name + result;
                }
            }
            return Content(result);
        }

        public ActionResult SaveByEmpID()
        {
            string OrgID = Request.Form["OrgID"] != "" ? Request.Form["OrgID"].ToString() : "";
            string EmpID = Request.Form["EmpID"] != "" ? Request.Form["EmpID"].ToString() : "";
            string[] arrOrgID = OrgID.Split(',');
            string writeMsg = "保存成功！";
            if (EmpID == "")
                writeMsg = "保存失败！";
            else
            {
                List<EmpOrg> lstEmpRog = new List<EmpOrg>();
                foreach (string item in arrOrgID)
                {
                    if (string.IsNullOrEmpty(item))
                        continue;
                    EmpOrg model = new EmpOrg();
                    model.EmpID = int.Parse(EmpID);
                    model.OrgID = int.Parse(item);
                    lstEmpRog.Add(model);
                }
                short shRet = SystemContext.Instance.OrgnizationServices.SaveByEmpID(lstEmpRog, EmpID);
                if (shRet != ExecuteResult.OK)
                    writeMsg = "保存失败";
            }
            return Content(writeMsg);
        }

        public ActionResult GetOrgTree()
        {

            List<Orgnization> lstOrgnization = new List<Orgnization>();
            short shRet = SystemContext.Instance.OrgnizationServices.GetOrgnizationList("", ref lstOrgnization);
            DataTable dtOrgnization = GlobalMethods.Table.GetDataTable(lstOrgnization);
            //得到ID号
            string szCheckItems = string.Empty;
            if (Request.QueryString["EmpID"] != null && Request.QueryString["EmpID"] != "")
            {
                List<EmpOrg> lstEmpOrg = new List<EmpOrg>();
                string EmpID = Request.QueryString["EmpID"];
                shRet = SystemContext.Instance.OrgnizationServices.GetEmpOrgByEmpID(EmpID, ref lstEmpOrg);
                foreach (EmpOrg item in lstEmpOrg)
                {
                    szCheckItems += "," + item.OrgID;
                }
            }
            string strJson = JsonHelper.GetTreeJsonByTable(dtOrgnization, "ID", "OrgName", "ParentID", "", szCheckItems);
            //string strJson = "[{\"id\":\"1\",\"text\":\"hello1\",\"checked\":\"true\",\"state\":\"open\",\"children\":[{\"id\":\"2\",\"text\":\"hello2\",\"state\":\"open\"}]},{\"id\":\"1\",\"text\":\"hello1\",\"state\":\"open\",\"children\":[{\"id\":\"2\",\"text\":\"hello2\",\"state\":\"open\"}]}]";
            return Content(strJson);
        }
        #region 查询数据
        /// <summary>
        /// 获取指定ID的数据
        /// </summary>
        public ActionResult QueryOneData()
        {
            string id = Request.Form["id"] != "" ? Request.Form["id"] : "0";
            Orgnization item = new Orgnization();
            short shRet = SystemContext.Instance.OrgnizationServices.GetOrgnizationByID(id, ref item);
            JsonHelper json = new JsonHelper();
            string strJson = string.Empty;
            if (shRet == ExecuteResult.OK)
            {
                json.AddItem("ID", item.ID.ToString());
                json.AddItem("OrgName", item.OrgName);
                json.AddItem("ParentID", item.ParentID);
                json.AddItem("ParentName", item.ParentName);
                json.AddItem("Description", item.Describe);
                json.AddItem("RoleType", item.RoleType);
                json.ItemOk();
            }
            strJson = json.ToEasyuiListJsonString();
            return Content(strJson);
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        public ActionResult QueryData()
        {
            List<Orgnization> lstOrgnization = new List<Orgnization>();
            short shRet = SystemContext.Instance.OrgnizationServices.GetOrgnizationList("", ref lstOrgnization);
            JsonHelper json = new JsonHelper();
            string strJson = string.Empty;
            foreach (Orgnization item in lstOrgnization)
            {
                json.AddItem("id", item.ID.ToString());
                json.AddItem("name", item.OrgName);
                json.AddItem("pid", item.ParentID);
                //json.AddItem("NewsContent", item.NewsContent);
                json.AddItem("ParentName", item.ParentName);
                json.AddItem("Description", item.Describe);
                json.AddItem("RoleType", item.RoleType);
                json.ItemOk();
            }
            if (lstOrgnization.Count > 0)
            {
                strJson = json.ToEasyuiListJsonString();
            }
            else
            {
                strJson = @"[]";
            }
            // json = "{ \"rows\":[ { \"ID\":\"48\",\"NewsTitle\":\"mr\",\"NewsContent\":\"mrsoft\",\"CreateTime\":\"2013-12-23\",\"CreateUser\":\"ceshi\",\"ModifyTime\":\"2013-12-23\",\"ModifyUser\":\"ceshi\"} ],\"total\":3}";
            return Content(strJson);
        }
        #endregion

        #region 添加或修改提交的数据
        /// <summary>
        /// 添加或修改数据
        /// </summary>
        public ActionResult UpdateData()
        {
            int id = Request.Form["id"] != "" ? Convert.ToInt32(Request.Form["id"]) : 0;
            Orgnization model = GetData(id);

            string writeMsg = "操作失败！";
            if (model != null)
            {
                if (id < 1)
                {
                    short shRet = SystemContext.Instance.OrgnizationServices.Add(model);
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
                    short shRet = SystemContext.Instance.OrgnizationServices.Update(model);
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
        private Orgnization GetData(int id)
        {
            Orgnization model = new Orgnization();
            if (id > 0)
            {
                SystemContext.Instance.OrgnizationServices.GetOrgnizationByID(id.ToString(), ref model);
            }
            model.ParentID = Request.Form["ParentID"] != "" ? Request.Form["ParentID"] : "";
            model.ParentName = Request.Form["ParentName"] != "" ? Request.Form["ParentName"] : "";
            model.OrgName = Request.Form["OrgName"] != "" ? Request.Form["OrgName"] : "";
            model.Describe = Request.Form["Description"] != "" ? Request.Form["Description"] : "";

            return model;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        public ActionResult DelData()
        {
            string writeMsg = "删除失败！";

            string selectID = Request.Form["id"] != "" ? Request.Form["id"] : "";
            if (selectID != string.Empty && selectID != "0")
            {
                short shRet = SystemContext.Instance.OrgnizationServices.Delete(selectID);

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
    }
}
