using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Windy.Common.Libraries;
using Windy.Service.DAL;
using Windy.Service.DAL.zyldingfang;
using Windy.Service.Data;

namespace Windy.Service.Data.zyldingfang
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“EmployeeService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 EmployeeService.svc 或 EmployeeService.svc.cs，然后开始调试。
    public class UsersService : ServiceBase
    {

        protected override bool Initialize()
        {
            if (!base.Initialize())
                return false;
            return true;
        }

        public void DoWork()
        {
        }
        public short Add(Users item)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return UsersAccess.Instance.Add(item);
        }

        /// <summary>
        /// 更新员工信息
        /// </summary>
        /// <param name="news">新闻资讯</param>
        /// <returns>ServerData.ExecuteResult</returns>
        public short Update(Users item)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return UsersAccess.Instance.Update(item);
        }
        /// <summary>
        /// 更新员工信息
        /// </summary>
        /// <param name="news">新闻资讯</param>
        /// <returns>ServerData.ExecuteResult</returns>
        public short UpdateByTel(Users item)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return UsersAccess.Instance.UpdateByTel(item);
        }
        /// <summary>
        /// 删除员工信息
        /// </summary>
        /// <param name="szGroupName">配置组</param>
        /// <param name="szConfigName">配置项</param>
        /// <returns>ServerData.ExecuteResult</returns>
        public short Delete(string ID)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return UsersAccess.Instance.Delete(ID);
        }

        /// <summary>
        /// 获取员工信息集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetUsersList(string keyWords, string EmployeeIDs, ref List<Users> lstUsers)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;

            short shRet = UsersAccess.Instance.GetUsersList(keyWords, EmployeeIDs, ref lstUsers);
            if (shRet != ExecuteResult.OK)
                return shRet;
            List<Employee> lstEmployee = new List<Employee>();
            shRet = EmployeeAccess.Instance.GetEmployeeList("", "", "", ref lstEmployee);
            Hashtable htEmployee = new Hashtable();
            foreach (var item in lstEmployee)
            {
                htEmployee.Add(item.ID, item.Name);
            }

            foreach (Users item in lstUsers)
            {
                if (htEmployee[item.EmployeeID] == null)
                    continue;
                item.EmployeeName = htEmployee[item.EmployeeID].ToString();
            }
            return shRet;
        }
        /// <summary>
        /// 通过房间号获取室友和自己
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetUsersListByRoom(string Room, string Hotel, ref List<Users> lstUsers)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;

            short shRet = UsersAccess.Instance.GetUsersListByRoom(Room, Hotel, ref lstUsers);
            if (shRet != ExecuteResult.OK)
                return shRet;
            List<Employee> lstEmployee = new List<Employee>();
            shRet = EmployeeAccess.Instance.GetEmployeeList("", "", "", ref lstEmployee);
            Hashtable htEmployee = new Hashtable();
            foreach (var item in lstEmployee)
            {
                htEmployee.Add(item.ID, item.Name);
            }

            foreach (Users item in lstUsers)
            {
                item.EmployeeName = htEmployee[item.EmployeeID].ToString();
            }
            return shRet;
        }

        /// <summary>
        /// 获取分页新闻资讯集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetUsersPageList(int pageSize, int pageIndex, string Name, string EmployeeID, string Tel, ref List<Users> lstUsers)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            short shRet = UsersAccess.Instance.GetUsersPageList(pageSize, pageIndex, Name, EmployeeID, Tel, ref lstUsers);
            if (shRet != ExecuteResult.OK)
                return shRet;
            List<Employee> lstEmployee = new List<Employee>();
            shRet = EmployeeAccess.Instance.GetEmployeeList("", "", "", ref lstEmployee);
            Hashtable htEmployee = new Hashtable();
            foreach (var item in lstEmployee)
            {
                htEmployee.Add(item.ID, item.Name);
            }
            try
            {
                foreach (Users item in lstUsers)
                {
                    if (item.EmployeeID != "0")
                        item.EmployeeName = htEmployee[item.EmployeeID].ToString();
                }
            }
            catch (Exception ex)
            {
                LogManager.Instance.WriteLog("GetUsersPageList"
                        , new string[] { "EmployeeID" }, new string[] { EmployeeID }, "获取考生数据失败!", ex);
            }
           
            return shRet;
        }
        /// <summary>
        /// 获取分页新闻资讯集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetUsersTotalCount(string Name, string EmployeeID,string Tel, ref int TotalCount)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return UsersAccess.Instance.GetUsersTotalCount(Name, EmployeeID,Tel, ref  TotalCount);
        }

        public short Exists(string strUserName, string strPassWord, ref News admin)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            NewsAccess adminAccess = new NewsAccess();
            DataSet ds = new DataSet();
            OrgnizationAccess.Instance.ExecuteQuery("Select * from Users", out ds);
            return ExecuteResult.OK;
        }


        public short GetUsersByID(string ID, ref Users item)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            short shRet = UsersAccess.Instance.GetUsersByID(ID, ref  item);
            if (shRet != ExecuteResult.OK)
                return shRet;
            Employee employee = new Employee();
            short shRet2 = EmployeeAccess.Instance.GetEmployeeByID(item.EmployeeID, ref employee);
            if (shRet2 == ExecuteResult.OK)
                item.EmployeeName = employee.Name;
            return shRet;
        }

        public short GetUsersByTel(string Tel, ref Users item)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            short shRet = UsersAccess.Instance.GetUsersByTel(Tel, ref  item);
            if (shRet != ExecuteResult.OK)
                return shRet;
            Employee employee = new Employee();
            short shRet2 = EmployeeAccess.Instance.GetEmployeeByID(item.EmployeeID, ref employee);
            if (shRet2 == ExecuteResult.OK)
                item.EmployeeName = employee.Name;
            return shRet;
        }

        public short Exists(string strUserName, string strPassWord, ref ExamPlace admin)
        {
            throw new NotImplementedException();
        }


        public short GetOrgnizationList(string keyWords, ref List<Users> lstUsers)
        {
            throw new NotImplementedException();
        }

        public short Exists(string szTel, string szPassWord, ref Users users)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return UsersAccess.Instance.Exists(szTel, szPassWord, ref users);
        }


        public short Import(string filePath, string szTemplateValue)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            if (string.IsNullOrEmpty(szTemplateValue))
                return ExecuteResult.PARAM_ERROR;
            try
            {

                List<Employee> lstEmployee = new List<Employee>();
                short shRet = EmployeeAccess.Instance.GetEmployeeList("", "", "", ref lstEmployee);
                Hashtable htEmployee = new Hashtable();
                foreach (var item in lstEmployee)
                {
                    if (htEmployee.Contains(item.Name))
                        continue;
                    htEmployee.Add(item.Name, item.ID);
                }

                string[] values = szTemplateValue.Split(',');

                Windy.Common.Libraries.ExcelProvider excelProvider = Windy.Common.Libraries.ExcelProvider.Create(filePath, "Sheet1");
                foreach (Windy.Common.Libraries.ExcelRow row in excelProvider)
                {

                    int ID = 0;
                    Users user = new Users();
                    for (int index = 0; index < values.Length; index++)
                    {
                        switch (values[index])
                        {
                            case "网报序号":
                                if (!string.IsNullOrEmpty(row.GetString("网报序号")))
                                    user.ID = int.Parse(row.GetString(values[index]));
                                break;
                            case "多退少补":
                                user.MoneyBack = row.GetString(values[index]);
                                break;
                            case "酒店房价":
                                user.HotelExpense = row.GetString(values[index]);
                                break;
                            case "酒店":
                                user.Hotel = row.GetString(values[index]);
                                break;
                            case "房号":
                                user.Room = row.GetString(values[index]);
                                break;
                            case "已交款额":
                                user.PayMoney = row.GetString(values[index]);
                                break;
                            case "备注":
                                user.Bak = row.GetString(values[index]);
                                break;
                            case "联系方式":
                                user.Tel = row.GetString(values[index]);
                                break;
                            case "网报密码":
                                user.PassWord = row.GetString(values[index]);
                                break;
                            case "所在学校":
                                user.School = row.GetString(values[index]);
                                break;
                            case "性别":
                                user.Gender = row.GetString(values[index]);
                                break;
                            case "报名次序":
                                user.Sequence = row.GetString(values[index]);
                                break;
                            case "业务员":
                                user.EmployeeName = row.GetString(values[index]);
                                break;
                            case "意向同住人":
                                user.ExceptRoomie = row.GetString(values[index]);
                                break;
                            case "报考类型":
                                user.Template = row.GetString(values[index]);
                                break;
                            case "收缴余款所在地":
                                user.PayPlace = row.GetString(values[index]);
                                break;
                            case "所报学校":
                                user.ExamSchool = row.GetString(values[index]);
                                break;
                            case "姓名":
                                user.Name = row.GetString(values[index]);
                                break;
                            case "提交考点":
                                user.ExamPlace = row.GetString(values[index]);
                                break;
                            default:
                                break;
                        }
                    }
                    if (string.IsNullOrEmpty(user.Tel))
                        continue;
                    object szEmployeeID = htEmployee[user.EmployeeName];
                    user.EmployeeID = szEmployeeID == null ? "0" : szEmployeeID.ToString();
                    //判断考生是否已导入
                    Users queryUser = new Users();
                    shRet = UsersAccess.Instance.GetUsersByTel(user.Tel, ref queryUser);

                    if (shRet == ExecuteResult.OK)
                    {
                        //重复导入时密码重置为111111，excel有网报序号时密码无需重置
                        if (user.ID.ToString() == "" || user.ID == 0)
                        {
                            user.PassWord = ServiceParam.Instance.GetPwd(user.Tel);
                        }

                        shRet = UsersAccess.Instance.UpdateByTel(user);
                    }
                    else
                    {
                        user.PassWord = ServiceParam.Instance.GetPwd(user.Tel);

                        shRet = UsersAccess.Instance.Add(user);
                    }
                    if (shRet != ExecuteResult.OK)
                        return shRet;
                }
            }
            catch (Exception ex)
            {

                Windy.Common.Libraries.LogManager.Instance.WriteLog("UsersService.Import", new string[] { "param", "db-info", "ftp-info" }
               , new object[] { "", "", "" }, "", ex);
                if (ex.ToString().Contains("关键字"))
                    return ExecuteResult.FILE_FORMAT_ERROR;
                return ExecuteResult.EXCEPTION;
            }

            return ExecuteResult.OK;
        }
    }
}
