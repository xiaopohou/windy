using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Windy.Common.Libraries;
using Windy.Service.DAL;
 
 
using Windy.Service.DAL;

namespace Windy.Service.DAL.zyldingfang
{
    public class EmployeeAccess : DBAccessBase
    {
        private static EmployeeAccess m_Instance = null;
        /// <summary>
        /// 单实例
        /// </summary>
        public static EmployeeAccess Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new EmployeeAccess();
                return m_Instance;
            }
        }

        public short Add(Employee employee)
        {
            string szFiled = string.Format("{0},{1},{2},{3},{4},{5}"
                    , ServerData.EmployeeTable.EmpNo
                    , ServerData.EmployeeTable.Name
                    , ServerData.EmployeeTable.OrgName
                    , ServerData.EmployeeTable.PassWord
                    , ServerData.EmployeeTable.RoleType
                    , ServerData.EmployeeTable.Tel);
            string szValue = string.Format("'{0}','{1}','{2}','{3}','{4}','{5}'"
                , employee.EmpNo
                , employee.Name
                , employee.OrgName
                , employee.Pwd
                , employee.RoleType
                , employee.Tel);
            string szSql = string.Format("Insert Into {0}({1}) values({2})"
                , ServerData.DataTable.Employee
                , szFiled
                , szValue);
            try
            {
                base.DataAccess.ExecuteNonQuery(szSql, CommandType.Text);

            }
            catch (Exception ex)
            {
                return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), szSql, "SQL语句执行失败!");
            }
            return ExecuteResult.OK;
        }

        /// <summary>
        /// 更新新闻资讯
        /// </summary>
        /// <param name="employee">新闻资讯</param>
        /// <returns>ExecuteResult</returns>
        public short Update(Employee employee)
        {
            if (employee == null)
                return ExecuteResult.PARAM_ERROR;

            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;

            string szFields = string.Format("{0}='{1}',{2}='{3}',{4}='{5}',{6}='{7}',{8}='{9}',{10}='{11}'"
                 , ServerData.EmployeeTable.EmpNo, employee.EmpNo
                 , ServerData.EmployeeTable.Name, employee.Name
                 , ServerData.EmployeeTable.OrgName, employee.OrgName
                 , ServerData.EmployeeTable.RoleType, employee.RoleType
                 , ServerData.EmployeeTable.Tel, employee.Tel
                 , ServerData.EmployeeTable.PassWord, employee.Pwd
                 );
            string szCondition = string.Format("{0}={1}"
                , ServerData.NewsTable.ID, employee.ID
                );
            string szSQL = string.Format(ServerData.SQL.UPDATE, ServerData.DataTable.Employee, szFields, szCondition);

            int nCount = 0;
            try
            {
                nCount = base.DataAccess.ExecuteNonQuery(szSQL, CommandType.Text);
            }
            catch (Exception ex)
            {
                return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), szSQL, "SQL语句执行失败!");
            }
            if (nCount <= 0)
            {
                LogManager.Instance.WriteLog("EmployeeAccess.Update", new string[] { "szSQL" }, new object[] { szSQL }, "SQL语句执行后返回0!");
                return ExecuteResult.ACCESS_ERROR;
            }
            return ExecuteResult.OK;
        }

        /// <summary>
        /// 删除新闻资讯
        /// </summary>
        /// <param name="szGroupName">配置组</param>
        /// <param name="szConfigName">配置项</param>
        /// <returns>ExecuteResult</returns>
        public short Delete(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return ExecuteResult.PARAM_ERROR;

            string szCondition = string.Format("{0} in ({1})"
                , ServerData.EmployeeTable.ID, ID);

            string szSQL = string.Format(ServerData.SQL.DELETE, ServerData.DataTable.Employee, szCondition);

            int count = 0;
            try
            {
                count = base.DataAccess.ExecuteNonQuery(szSQL, CommandType.Text);
            }
            catch (Exception ex)
            {
                return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), szSQL, "SQL语句执行失败!");
            }
            return count > 0 ? ExecuteResult.OK : ExecuteResult.RES_NO_FOUND;
        }
        /// <summary>
        /// 根据主键查找行
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public short GetEmployeeByID(string ID, ref Employee employee)
        {
            if (string.IsNullOrEmpty(ID))
                return ExecuteResult.PARAM_ERROR;
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            string szField = string.Format("{0},{1},{2},{3},{4},{5},{6}"
               , ServerData.EmployeeTable.ID
               , ServerData.EmployeeTable.EmpNo
               , ServerData.EmployeeTable.Name
               , ServerData.EmployeeTable.OrgName
               , ServerData.EmployeeTable.PassWord
               , ServerData.EmployeeTable.RoleType
               , ServerData.EmployeeTable.Tel);
            string szCondition = string.Format("1=1");
            szCondition = string.Format("{0} = {1}"
                    , ServerData.EmployeeTable.ID, ID);
            string szTable = ServerData.DataTable.Employee;
            string szSQL = string.Format(ServerData.SQL.SELECT_WHERE, szField, szTable, szCondition);

            IDataReader dataReader = null;
            try
            {
                dataReader = base.DataAccess.ExecuteReader(szSQL, CommandType.Text);
                if (dataReader == null || dataReader.IsClosed || !dataReader.Read())
                {
                    LogManager.Instance.WriteLog("DocumentAccess.GetDocInfoBySetID", new string[] { "szSQL" }, new object[] { szSQL }, "未查询到记录");
                    return ExecuteResult.RES_NO_FOUND;
                }

                if (employee == null)
                    employee = new Employee();
                employee.ID = dataReader.GetValue(0).ToString();
                employee.EmpNo =dataReader.IsDBNull(1)?"": dataReader.GetString(1);
                employee.Name = dataReader.IsDBNull(2) ? "" : dataReader.GetString(2);
                employee.OrgName = dataReader.IsDBNull(3) ? "" : dataReader.GetString(3);
                employee.Pwd = dataReader.IsDBNull(4) ? "" : dataReader.GetString(4);
                employee.RoleType = dataReader.IsDBNull(5) ? "" : dataReader.GetString(5);
                employee.Tel = dataReader.IsDBNull(6) ? "" : dataReader.GetString(6);
                return ExecuteResult.OK;
            }
            catch (Exception ex)
            {
                return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), szSQL, "SQL语句执行失败!");
            }
            finally { base.DataAccess.CloseConnnection(false); }
        }
        /// <summary>
        /// 根据主键查找行
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public short GetEmployeeByName(string szName, ref Employee employee)
        {
            if (string.IsNullOrEmpty(szName))
                return ExecuteResult.PARAM_ERROR;
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            string szField = string.Format("{0},{1},{2},{3},{4},{5},{6}"
               , ServerData.EmployeeTable.ID
               , ServerData.EmployeeTable.EmpNo
               , ServerData.EmployeeTable.Name
               , ServerData.EmployeeTable.OrgName
               , ServerData.EmployeeTable.PassWord
               , ServerData.EmployeeTable.RoleType
               , ServerData.EmployeeTable.Tel);
            string szCondition = string.Format("1=1");
            szCondition = string.Format("{0} = '{1}'"
                    , ServerData.EmployeeTable.Name, szName);
            string szTable = ServerData.DataTable.Employee;
            string szSQL = string.Format(ServerData.SQL.SELECT_WHERE, szField, szTable, szCondition);

            IDataReader dataReader = null;
            try
            {
                dataReader = base.DataAccess.ExecuteReader(szSQL, CommandType.Text);
                if (dataReader == null || dataReader.IsClosed || !dataReader.Read())
                {
                    LogManager.Instance.WriteLog("EmployeeAccess.GetEmployeeByName", new string[] { "szSQL" }, new object[] { szSQL }, "未查询到记录");
                    return ExecuteResult.RES_NO_FOUND;
                }

                if (employee == null)
                    employee = new Employee();
                employee.ID = dataReader.GetValue(0).ToString();
                employee.EmpNo = dataReader.IsDBNull(1) ? "" : dataReader.GetString(1);
                employee.Name = dataReader.IsDBNull(2) ? "" : dataReader.GetString(2);
                employee.OrgName = dataReader.IsDBNull(3) ? "" : dataReader.GetString(3);
                employee.Pwd = dataReader.IsDBNull(4) ? "" : dataReader.GetString(4);
                employee.RoleType = dataReader.IsDBNull(5) ? "" : dataReader.GetString(5);
                employee.Tel = dataReader.IsDBNull(6) ? "" : dataReader.GetString(6);
                return ExecuteResult.OK;
            }
            catch (Exception ex)
            {
                return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), szSQL, "SQL语句执行失败!");
            }
            finally { base.DataAccess.CloseConnnection(false); }
        }
        /// <summary>
        /// 获取新闻资讯集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetEmployeeList(string IDs,string keyWords,string RoleType, ref List<Employee> lstEmployee)
        {
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            string szField = string.Format("{0},{1},{2},{3},{4},{5},{6}"
               , ServerData.EmployeeTable.ID
               , ServerData.EmployeeTable.EmpNo
               , ServerData.EmployeeTable.Name
               , ServerData.EmployeeTable.OrgName
               , ServerData.EmployeeTable.PassWord
               , ServerData.EmployeeTable.RoleType
               , ServerData.EmployeeTable.Tel);
            string szCondition = string.Format("1=1");
            if (!string.IsNullOrEmpty(IDs))
                szCondition = string.Format("{0} and ID in ({1})",szCondition, IDs);
            if (!string.IsNullOrEmpty(keyWords))
                szCondition = string.Format("{0} and  {1} like '%{2}%'"
                    , szCondition
                    , ServerData.NewsTable.NewsTitle, keyWords);
            if (!string.IsNullOrEmpty(RoleType))
                szCondition = string.Format("{0} and {1} = '{2}'"
                    , szCondition
                    , ServerData.EmployeeTable.RoleType, RoleType);
            string szTable = ServerData.DataTable.Employee;
            string szSQL = string.Format(ServerData.SQL.SELECT_WHERE, szField, szTable, szCondition);

            IDataReader dataReader = null;
            try
            {
                dataReader = base.DataAccess.ExecuteReader(szSQL, CommandType.Text);
                if (dataReader == null || dataReader.IsClosed || !dataReader.Read())
                {
                    LogManager.Instance.WriteLog("EmployeeAccess.GetEmployeeList", new string[] { "szSQL" }, new object[] { szSQL }, "未查询到记录");
                    return ExecuteResult.RES_NO_FOUND;
                }
                if (lstEmployee == null)
                    lstEmployee = new List<Employee>();
                do
                {
                    Employee employee = new Employee();
                    employee.ID = dataReader.GetValue(0).ToString();
                    employee.EmpNo =dataReader.IsDBNull(1)?"": dataReader.GetString(1);
                    employee.Name = dataReader.IsDBNull(2) ? "" : dataReader.GetString(2);
                    employee.OrgName = dataReader.IsDBNull(3) ? "" : dataReader.GetString(3);
                    employee.Pwd = dataReader.IsDBNull(4) ? "" : dataReader.GetString(4);
                    employee.RoleType = dataReader.IsDBNull(5) ? "" : dataReader.GetString(5);
                    employee.Tel = dataReader.IsDBNull(6) ? "" : dataReader.GetString(6);
                    lstEmployee.Add(employee);
                } while (dataReader.Read());
                return ExecuteResult.OK;
            }
            catch (Exception ex)
            {
                return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), szSQL, "SQL语句执行失败!");
            }
            finally { base.DataAccess.CloseConnnection(false); }
        }

        /// <summary>
        /// 获取分页新闻资讯集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetEmployeePageList(int pageSize, int pageIndex, string szCategoryName, ref List<Employee> lstEmployee)
        {
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            if (lstEmployee == null)
                lstEmployee = new List<Employee>();
            string szTableName = ServerData.DataTable.Employee;
            string szFields = string.Format("{0},{1},{2},{3},{4},{5},{6}"
                , ServerData.EmployeeTable.ID
                , ServerData.EmployeeTable.EmpNo
                , ServerData.EmployeeTable.Name
                , ServerData.EmployeeTable.OrgName
                , ServerData.EmployeeTable.PassWord
                , ServerData.EmployeeTable.RoleType
                , ServerData.EmployeeTable.Tel); 
            string szOrderName = "ID";
            string szOrderType = "Desc";
            string strWhere = "1=1";
            if (!String.IsNullOrEmpty(szCategoryName))
                strWhere = string.Format("{0} and {1} ='{2}'"
                    , strWhere
                    , ServerData.NewsTable.CategoryName, szCategoryName);
            DataSet result = new DataSet();
            short shRet = base.GetPageList(szTableName, szFields, szOrderName, pageSize, pageIndex, false, szOrderType, strWhere, ref  result);
            if (shRet == ExecuteResult.OK)
            {
                try
                {
                    for (int index = 0; index < result.Tables[0].Rows.Count; index++)
                    {
                        Employee employee = new Employee();
                        employee.ID = result.Tables[0].Rows[index][0].ToString();
                        employee.EmpNo = result.Tables[0].Rows[index][1].ToString();
                        employee.Name = result.Tables[0].Rows[index][2].ToString();
                        employee.OrgName = result.Tables[0].Rows[index][3].ToString();
                        employee.Pwd = result.Tables[0].Rows[index][4].ToString();
                        employee.RoleType = result.Tables[0].Rows[index][5].ToString();
                        employee.Tel = result.Tables[0].Rows[index][6].ToString();
                        lstEmployee.Add(employee);
                    }
                }
                catch (Exception ex)
                {
                    return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "获取分页新闻资讯集合失败");
                }
            }
            return shRet;

        }
        /// <summary>
        /// 获取分页新闻资讯集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetEmployeeTotalCount(string szCategoryName, ref int TotalCount)
        {
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            string szTableName = ServerData.DataTable.Employee;
            string strWhere = "1=1";
            if (!string.IsNullOrEmpty(szCategoryName))
                strWhere = string.Format("{0} and {1} ='{2}'"
                    , strWhere
                    , ServerData.NewsTable.CategoryName, szCategoryName);
            DataSet result = new DataSet();
            short shRet = base.GetPageList(szTableName, null, null, 0, 0, true, null, strWhere, ref  result);
            if (shRet == ExecuteResult.OK)
            {
                TotalCount = int.Parse(result.Tables[0].Rows[0][0].ToString());
            }
            return shRet;

        }

        /// <summary>
        /// 执行指定的SQL语句查询
        /// </summary>
        /// <param name="sql">查询的SQL语句</param>
        /// <param name="result">查询返回的结果集</param>
        /// <returns>ExecuteResult</returns>
        public short ExecuteQuery(string sql, out DataSet result)
        {
            result = null;
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;

            try
            {
                result = base.DataAccess.ExecuteDataSet(sql, CommandType.Text);
            }
            catch (Exception ex)
            {
                return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), sql, "SQL语句执行失败!");
            }
            return ExecuteResult.OK;
        }

        /// <summary>
        /// 根据主键查找行
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public short Exsit(string szName,string szPassWard, ref Employee employee)
        {
            if (string.IsNullOrEmpty(szName) || string.IsNullOrEmpty(szPassWard))
                return ExecuteResult.PARAM_ERROR;
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            string szField = string.Format("{0},{1},{2},{3},{4},{5},{6}"
               , ServerData.EmployeeTable.ID
               , ServerData.EmployeeTable.EmpNo
               , ServerData.EmployeeTable.Name
               , ServerData.EmployeeTable.OrgName
               , ServerData.EmployeeTable.PassWord
               , ServerData.EmployeeTable.RoleType
               , ServerData.EmployeeTable.Tel);
            string szCondition = string.Format("1=1");
            szCondition = string.Format("{0} = '{1}' and {2}='{3}'"
                    , ServerData.EmployeeTable.EmpNo, szName
                    , ServerData.EmployeeTable.PassWord,szPassWard);
            string szTable = ServerData.DataTable.Employee;
            string szSQL = string.Format(ServerData.SQL.SELECT_WHERE, szField, szTable, szCondition);

            IDataReader dataReader = null;
            try
            {
                dataReader = base.DataAccess.ExecuteReader(szSQL, CommandType.Text);
                if (dataReader == null || dataReader.IsClosed || !dataReader.Read())
                {
                    LogManager.Instance.WriteLog("EmployeeAccess.Exsit", new string[] { "szSQL" }, new object[] { szSQL }, "未查询到记录");
                    return ExecuteResult.RES_NO_FOUND;
                }

                if (employee == null)
                    employee = new Employee();
                employee.ID = dataReader.GetValue(0).ToString();
                employee.EmpNo = dataReader.IsDBNull(1) ? "" : dataReader.GetString(1);
                employee.Name = dataReader.IsDBNull(2) ? "" : dataReader.GetString(2);
                employee.OrgName = dataReader.IsDBNull(3) ? "" : dataReader.GetString(3);
                employee.Pwd = dataReader.IsDBNull(4) ? "" : dataReader.GetString(4);
                employee.RoleType = dataReader.IsDBNull(5) ? "" : dataReader.GetString(5);
                employee.Tel = dataReader.IsDBNull(6) ? "" : dataReader.GetString(6);
                return ExecuteResult.OK;
            }
            catch (Exception ex)
            {
                return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), szSQL, "SQL语句执行失败!");
            }
            finally { base.DataAccess.CloseConnnection(false); }
        }
    }
}
