using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Windy.Common.Libraries;
using Windy.Service.DAL;
using Windy.Service.Data;

namespace Windy.WebMVC.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        //
        // GET: /Admin/Users/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FileUpload()
        {
            return View();
        }

        public ActionResult QueryTemplate()
        {
            JsonHelper json = new JsonHelper();
            string strJson = string.Empty;
            string[] arrTemplate = SystemContext.Template.GetArrTemplate();
            foreach (string item in arrTemplate)
            {
                json.AddItem("id", item);
                json.AddItem("text", item);
                json.ItemOk();
            }
            strJson = json.ToEasyuiListJsonString();
            return Content(strJson);
        }

        public ActionResult QueryEmployeeIDs()
        {
            Employee CurrentEmployee = Session["CurrentEmployee"] as Employee;
            if (CurrentEmployee == null)
            {
                return RedirectToAction("Admin/Home/Login");
            }
            JsonHelper json = new JsonHelper();
            string strJson = string.Empty;
            json.AddItem("id", "");
            json.AddItem("text", "所有");
            json.ItemOk();
            if (CurrentEmployee.RoleType == SystemContext.RoleType.School)
            {
                json.AddItem("id", CurrentEmployee.ID.ToString());
                json.AddItem("text", CurrentEmployee.Name);
                json.ItemOk();
            }
            else
            {
                List<Employee> lstEmployee = new List<Employee>();
                this.GetEmployeeIDs(CurrentEmployee, ref lstEmployee);
                foreach (Employee item in lstEmployee)
                {
                    json.AddItem("id", item.ID.ToString());
                    json.AddItem("text", item.Name);
                    json.ItemOk();
                }

            }
            strJson = json.ToEasyuiListJsonString();
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
                short shRet = SystemContext.Instance.UsersService.Delete(selectID);

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
            Users model = GetData(id);

            string writeMsg = "操作失败！";
            if (model != null)
            {
                if (id < 1)
                {
                    string szEmployeeID = "0";
                    Employee CurrentEmployee = Session["CurrentEmployee"] as Employee;
                    if (CurrentEmployee != null)
                    {
                        if (CurrentEmployee.RoleType == SystemContext.RoleType.School)
                        {
                            model.EmployeeID = CurrentEmployee.ID;
                            short shRet = SystemContext.Instance.UsersService.Add(model);

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
                            writeMsg = "只能用业务员账号添加考生";
                        }
                    }


                }
                else
                {
                    short shRet = SystemContext.Instance.UsersService.Update(model);
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
        private Users GetData(int id)
        {
            Users model = new Users();
            if (id > 0)
            {
                SystemContext.Instance.UsersService.GetUsersByID(id.ToString(), ref model);

            }
            //model.Name = Request.Form["ipt_Name"] != "" ? Request.Form["ipt_Name"] : "";
            //model.Url = Request.Form["ipt_Url"] != "" ? Request.Form["ipt_Url"] : "";
            model.Name = Request.Form["Name"] != "" ? Request.Form["Name"] : "";
            model.School = Request.Form["School"] != "" ? Request.Form["School"] : "";
            model.ExamSchool = Request.Form["ExamSchool"] != "" ? Request.Form["ExamSchool"] : "";
            model.Sequence = Request.Form["Sequences"] != "" ? Request.Form["Sequences"] : "";
            model.Tel = Request.Form["Tel"] != "" ? Request.Form["Tel"] : "";
            model.Bak = Request.Form["Baks"] != "" ? Request.Form["Baks"] : "";
            model.PassWord = Request.Form["Pwd"] != "" ? Request.Form["Pwd"] : "111111";
            model.PayMoney = Request.Form["PayMoney"] != "" ? Request.Form["PayMoney"] : "";
            model.ExamPlace = Request.Form["ExamPlace"] != "" ? Request.Form["ExamPlace"] : "";
            model.Room = Request.Form["Room"] != "" ? Request.Form["Room"] : "";
            model.Hotel = Request.Form["Hotel"] != "" ? Request.Form["Hotel"] : "";
            model.HotelExpense = Request.Form["HotelExpense"] != "" ? Request.Form["HotelExpense"] : "";
            model.MoneyBack = Request.Form["MoneyBack"] != "" ? Request.Form["MoneyBack"] : "";
            model.Gender = Request.Form["Gender"] != "" ? Request.Form["Gender"] : "";
            model.Template = Request.Form["Template"] != "" ? Request.Form["Template"] : "";
            model.PayPlace = Request.Form["PayPlace"] != "" ? Request.Form["PayPlace"] : "";
            model.ExceptRoomie = Request.Form["ExceptRoomie"] != "" ? Request.Form["ExceptRoomie"] : "";
            model.Status = Request.Form["Status"] != "" ? Request.Form["Status"] : "";
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
            Users item = new Users();
            short shRet = SystemContext.Instance.UsersService.GetUsersByID(id, ref item);
            JsonHelper json = new JsonHelper();
            string strJson = string.Empty;
            if (shRet == ExecuteResult.OK)
            {
                json.AddItem("ID", item.ID.ToString());
                json.AddItem("Name", item.Name);
                json.AddItem("School", item.School);
                json.AddItem("ExamSchool", item.ExamSchool);
                json.AddItem("Sequences", item.Sequence);
                json.AddItem("Tel", item.Tel);
                json.AddItem("Baks", item.Bak);
                json.AddItem("Pwd", item.PassWord);
                json.AddItem("PayMoney", item.PayMoney);
                json.AddItem("ExamPlace", item.ExamPlace);
                json.AddItem("Room", item.Room);
                json.AddItem("Hotel", item.Hotel);
                json.AddItem("PassWord", item.PassWord);
                json.AddItem("PayMoney", item.PayMoney);
                json.AddItem("ExamPlace", item.ExamPlace);
                json.AddItem("Room", item.Room);
                json.AddItem("Hotel", item.Hotel);
                json.AddItem("HotelExpense", item.HotelExpense);
                json.AddItem("MoneyBack", item.MoneyBack);
                json.AddItem("EmployeeID", item.EmployeeID);
                json.AddItem("Gender", item.Gender);
                json.AddItem("Template", item.Template);
                json.AddItem("PayPlace", item.PayPlace);
                json.AddItem("ExceptRoomie", item.ExceptRoomie);
                json.AddItem("Status", item.Status);

                json.ItemOk();
            }
            strJson = json.ToEasyuiOneModelJsonString();
            // strJson = "[{\"ID\":\"81\",\"EmpNo\":\"jxdhlgljp\",\"Name\":\"hello\",\"Pwd\":\"111111\",\"Tel\":\"18720081979\"}]";
            return Content(strJson);
        }
        #endregion

        #region 查询数据

        /// <summary>
        /// 查询数据
        /// </summary>
        public ActionResult QueryData()
        {
            int page = Request.Form["page"] != "" ? Convert.ToInt32(Request.Form["page"]) : 0;
            int size = Request.Form["rows"] != "" ? Convert.ToInt32(Request.Form["rows"]) : 0;
            string sort = Request.Form["sort"] != "" ? Request.Form["sort"] : "";
            string order = Request.Form["order"] != "" ? Request.Form["order"] : "";
            string Template = Request.Form["Template"] != "" ? Request.Form["Template"] : "";
            string szEmployeeIDs = Request.Form["EmployeeID"] != "" ? Request.Form["EmployeeID"] : "";
            string Name = Request.Form["Name"] != "" ? Request.Form["Name"] : "";
            string Tel = Request.Form["Tel"] != "" ? Request.Form["Tel"] : "";
            if (page < 1) return Content("");
            if (Name == "所有")
                Name = string.Empty;
            Employee CurrentEmployee = Session["CurrentEmployee"] as Employee;
            if (CurrentEmployee == null)
                return Content("");
            List<Employee> lstEmployee = new List<Employee>();

            if (szEmployeeIDs == string.Empty || szEmployeeIDs == "所有")
                szEmployeeIDs = this.GetEmployeeIDs(CurrentEmployee, ref lstEmployee);

            List<Users> lstUsers = new List<Users>();
            short shRet = SystemContext.Instance.UsersService.GetUsersPageList(size, page, Name, szEmployeeIDs, Tel, ref lstUsers);
            JsonHelper json = new JsonHelper();
            string strJson = string.Empty;
            try
            {
                foreach (Users item in lstUsers)
                {
                    item.Hotel = item.Hotel.Replace("\t", "");
                    json.AddItem("ID", item.ID.ToString());
                    json.AddItem("Name", item.Name);
                    json.AddItem("School", item.School);
                    json.AddItem("ExamSchool", item.ExamSchool);
                    json.AddItem("Sequences", item.Sequence);
                    json.AddItem("Tel", item.Tel);
                    json.AddItem("Baks", item.Bak);
                    json.AddItem("Pwd", item.PassWord);
                    json.AddItem("PayMoney", item.PayMoney);
                    json.AddItem("ExamPlace", item.ExamPlace);
                    json.AddItem("Room", item.Room);
                    json.AddItem("Hotel", item.Hotel);
                    json.AddItem("PassWord", item.PassWord);
                    json.AddItem("PayMoney", item.PayMoney);
                    json.AddItem("ExamPlace", item.ExamPlace);
                    json.AddItem("Room", item.Room);
                    json.AddItem("Hotel", item.Hotel);
                    json.AddItem("HotelExpense", item.HotelExpense);
                    json.AddItem("MoneyBack", item.MoneyBack);
                    json.AddItem("EmployeeID", item.EmployeeID);
                    json.AddItem("EmployeeName", item.EmployeeName);
                    json.AddItem("Gender", item.Gender);
                    json.AddItem("Template", item.Template);
                    json.AddItem("PayPlace", item.PayPlace);
                    json.AddItem("ExceptRoomie", item.ExceptRoomie);
                    json.AddItem("Status", item.Status);
                    json.ItemOk();
                }
            }
            catch (Exception ex)
            {
                 LogManager.Instance.WriteLog("UserController.QueryData"
                        , new string[] { "" }, new string[] { "" }, "拼接考生json数据失败!", ex);
            }
            
            int totalCount = 0;
            shRet = SystemContext.Instance.UsersService.GetUsersTotalCount(Name, szEmployeeIDs,Tel, ref totalCount);
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
        public string GetEmployeeIDs(Employee CurrentEmployee, ref List<Employee> lstEmployee)
        {
            lstEmployee = new List<Employee>();
            if (CurrentEmployee.RoleType == SystemContext.RoleType.School)
            {
                lstEmployee.Add(CurrentEmployee);
                return CurrentEmployee.ID.ToString();
            }
            else
            {
                //先查找当前用户被授权的组织机构
                List<EmpOrg> lstEmpOrg = new List<EmpOrg>();
                short shRet = SystemContext.Instance.OrgnizationServices.GetEmpOrgByEmpID(CurrentEmployee.ID.ToString(), ref lstEmpOrg);
                //得到用户管理机构关联的业务员
                string OrgIDs = string.Empty;
                foreach (EmpOrg item in lstEmpOrg)
                {
                    if (OrgIDs == string.Empty)
                        OrgIDs = item.OrgID.ToString();
                    else
                        OrgIDs = OrgIDs + "," + item.OrgID;
                }
                if (OrgIDs == string.Empty)
                {
                    return string.Empty;
                }
                shRet = SystemContext.Instance.OrgnizationServices.GetEmpOrgByOrgID(OrgIDs, ref lstEmpOrg);
                string EmpIDs = string.Empty;
                foreach (EmpOrg item in lstEmpOrg)
                {
                    if (EmpIDs == string.Empty)
                        EmpIDs = item.EmpID.ToString();
                    else
                        EmpIDs = EmpIDs + "," + item.EmpID;
                }

                shRet = SystemContext.Instance.EmployeeService.GetEmployeeList(EmpIDs, "", SystemContext.RoleType.School, ref lstEmployee);
                return EmpIDs;
            }
        }
        /// <summary>
        /// 導出試題
        /// </summary>
        /// <param name="filePath">導出的位置</param>
        /// <param name="subjects">導出的題目</param>
        public short Exports(string filePath, List<Users> lstUser, string templateValue)
        {
            ExcelHelper excelOpr = new ExcelHelper();
            string[] columnName = templateValue.Split(',');
            List<ArrayList> values = new List<ArrayList>();
            for (int i = 0; i < lstUser.Count; i++)
            {
                Users user = lstUser[i];
                ArrayList value = new ArrayList();
                for (int j = 0; j < columnName.Length; j++)
                {
                    value.Add(GetColumnValue(user, columnName[j]));
                }
                value.Add("");
                values.Add(value);
            }
            try
            {
                excelOpr.ToExcel(filePath, columnName, values);
            }
            catch (Exception)
            {
                return ExecuteResult.OTHER_ERROR;
            }
            return ExecuteResult.OK;
        }

        private static string GetColumnValue(Users user, string columnName)
        {
            switch (columnName)
            {
                case "备注":
                    return user.Bak;
                case "业务员":
                    return user.EmployeeName;
                case "网报序号":
                    return user.ID.ToString();
                case "报名次序":
                    return user.Sequence;
                case "姓名":
                    return user.Name;
                case "性别":
                    return user.Gender;
                case "所在学校":
                    return user.School;
                case "报考类型":
                    return user.Template;
                case "联系方式":
                    return user.Tel;
                case "意向同住人":
                    return user.ExceptRoomie;
                case "提交考点":
                    return user.ExamPlace;
                case "已交款额":
                    return user.PayMoney;
                case "房号":
                    return user.Room;
                case "酒店":
                    return user.Hotel;
                case "酒店房价":
                    return user.HotelExpense;
                case "多退少补":
                    return user.MoneyBack;
                case "所报学校":
                    return user.ExamSchool;
                case "网报密码":
                    return user.PassWord;
                case "收缴余款所在地":
                    return user.PayPlace;
                default:
                    break;
            }
            return string.Empty;
        }
        public ActionResult Upload()
        {
            HttpFileCollectionBase files = Request.Files;//这里只能用<input type="file" />才能有效果,因为服务器控件是HttpInputFile类型
            string msg = string.Empty;
            string error = string.Empty;
            string imgurl = string.Empty;
             string szTemplate = Request.Form["TemplateType"] == null ? "研究生考试" : Request.Form["TemplateType"].ToString();
            if (files.Count < 0)
                return Content("");
            string res = string.Empty;
            if (files[0].FileName == "")
            {
                error = "未选择文件";
                res = "{ error:'" + error + "', msg:'" + msg + "',imgurl:'" + imgurl + "'}";
            }
            else
            {
                files[0].SaveAs(Server.MapPath(SystemContext.FilePath.Excel) + System.IO.Path.GetFileName(files[0].FileName));
                msg = " 导入成功!请关闭窗口查看导入结果";
                imgurl = "/" + files[0].FileName;

                //处理数据导入
                try
                {
                    short shRet = SystemContext.Instance.UsersService.Import(Server.MapPath(SystemContext.FilePath.Excel) + System.IO.Path.GetFileName(files[0].FileName), SystemContext.Template.GetTemplate(szTemplate));
                    if (shRet != ExecuteResult.OK)
                    {
                        error = ExecuteResult.GetResultMessage(shRet);
                    }

                }
                catch (Exception ex)
                {
                    error = "导入失败";

                }
                res = "{ error:'" + error + "', msg:'" + msg + "',imgurl:'" + imgurl + "'}";
            }
            return Content(res);
        }
        public ActionResult Export()
        {
            //SysAccount sysAccount = Session["SysAccount"] as SysAccount;
            string jsons = "";
            JsonHelper jsonHelper = new JsonHelper();
            var Template = Request.Form["Template"];
            var Name = Request.Form["Name"];
            var szEmployeeID = Request.Form["EmployeeID"];

            string filePath = string.Format("{0}{1}"
                , Server.MapPath(SystemContext.FilePath.Excel)
                , "test.xls");
            Employee CurrentEmployee = Session["CurrentEmployee"] as Employee ;
            if (CurrentEmployee == null)
                return Content("");
            List<Employee> lstEmployee = new List<Employee>();
            if (szEmployeeID == string.Empty || szEmployeeID == "所有")
                szEmployeeID = this.GetEmployeeIDs(CurrentEmployee, ref lstEmployee);
            List<Users> lstUser = new List<Users>();
           
            try
            {
                short shRet = SystemContext.Instance.UsersService.GetUsersList("", szEmployeeID, ref lstUser);
                string templateValue = SystemContext.Template.GetTemplate(Template);
                shRet = this.Exports(filePath, lstUser, templateValue);
                if (shRet == ExecuteResult.OK)
                {
                    jsons = "[{\"success\":true,msg:\"导出成功\",filePath:\""
                        + string.Format("{0}/{1}"
                        , SystemContext.FilePath.Excel
                        , "test.xls")
                        + "\"}]";
                }
                else
                {
                    jsons = "[{\"success\":true,msg:\"导出失败\"}]";
                }
            }
            catch (Exception ex)
            {
                LogManager.Instance.WriteLog("Fileupload.Export", "导出Excel失败", ex);

                jsons = "[{\"success\":true,msg:\"导出失败\"}]";
            }
            return Content(jsons);

        }
        #endregion
    }
}
