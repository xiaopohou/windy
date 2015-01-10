using System;
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
    public class EmployeeService : ServiceBase
    {
        private EmployeeAccess m_employeeAccess = null;

        protected override bool Initialize()
        {
            if (!base.Initialize())
                return false;
            if (this.m_employeeAccess == null)
                this.m_employeeAccess = new EmployeeAccess();
            return true;
        }

        public void DoWork()
        {
        }
        public short Add(Employee employee)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return EmployeeAccess.Instance.Add(employee);
        }

        /// <summary>
        /// 更新员工信息
        /// </summary>
        /// <param name="news">新闻资讯</param>
        /// <returns>ServerData.ExecuteResult</returns>
        public short Update(Employee employee)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return EmployeeAccess.Instance.Update(employee);
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
            return EmployeeAccess.Instance.Delete(ID);
        }

        /// <summary>
        /// 获取员工信息集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetEmployeeList(string IDs,string keyWords,string RoleType, ref List<Employee> lstEmployee)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return EmployeeAccess.Instance.GetEmployeeList(IDs,keyWords,RoleType, ref lstEmployee);
        }

        /// <summary>
        /// 获取分页新闻资讯集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetEmployeePageList(int pageSize, int pageIndex, string szCategoryName, ref List<Employee> lstEmployee)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return EmployeeAccess.Instance.GetEmployeePageList(pageSize, pageIndex, szCategoryName, ref lstEmployee);

        }
        /// <summary>
        /// 获取分页新闻资讯集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetEmployeeTotalCount(string szCategoryName, ref int TotalCount)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return EmployeeAccess.Instance.GetEmployeeTotalCount(szCategoryName, ref  TotalCount);
        }

        public short Exists(string szUserName, string szPassWord, ref Employee admin)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return EmployeeAccess.Instance.Exsit(szUserName, szPassWord, ref admin);
        }


        public short GetEmployeeByID(string ID, ref Employee employee)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return EmployeeAccess.Instance.GetEmployeeByID(ID, ref  employee);
        }
        public short GetEmployeeByName(string Name, ref Employee employee)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return EmployeeAccess.Instance.GetEmployeeByName(Name, ref  employee);
        }


        public short ExecuteUpdate(bool isProc, params string[] sqlarray)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return CommonAccess.Instance.ExecuteUpdate(isProc, sqlarray);
        }
    }
}
