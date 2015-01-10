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
    public class EmpMenuAccess : DBAccessBase
    {
        private static EmpMenuAccess m_Instance = null;
        /// <summary>
        /// 单实例
        /// </summary>
        public static EmpMenuAccess Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new EmpMenuAccess();
                return m_Instance;
            }
        }

        public short AddEmpMenu(EmpMenu item)
        {
            string szFiled = string.Format("{0},{1}"
                    , ServerData.EmpMenuTable.EmpID
                    , ServerData.EmpMenuTable.MenuID);
            string szValue = string.Format("{0},{1}"
                , item.EmpID
                , item.MenuID);
            string szSql = string.Format("Insert Into {0}({1}) values({2})"
                , ServerData.DataTable.EmpMenu
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
        /// 按员工ID号删除组织信息
        /// </summary>
        /// <param name="szGroupName">配置组</param>
        /// <returns>ExecuteResult</returns>
        public short DeleteByEmpID(string EmpID)
        {
            if (string.IsNullOrEmpty(EmpID))
                return ExecuteResult.PARAM_ERROR;

            string szCondition = string.Format("{0} = {1}"
                , ServerData.EmpMenuTable.EmpID, EmpID);

            string szSQL = string.Format(ServerData.SQL.DELETE, ServerData.DataTable.EmpMenu, szCondition);

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
        /// 按组织ID号删除组织信息
        /// </summary>
        /// <param name="szGroupName">配置组</param>
        /// <returns>ExecuteResult</returns>
        public short DeleteByMenuID(string MenuID)
        {
            if (string.IsNullOrEmpty(MenuID))
                return ExecuteResult.PARAM_ERROR;

            string szCondition = string.Format("{0} = {1}"
                , ServerData.EmpMenuTable.MenuID, MenuID);

            string szSQL = string.Format(ServerData.SQL.DELETE, ServerData.DataTable.EmpMenu, szCondition);

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
        public short GetEmpMenuByEmpID(string EmpID, ref List<EmpMenu> lstEmpMenu)
        {
            if (string.IsNullOrEmpty(EmpID))
                return ExecuteResult.PARAM_ERROR;
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            string szField = string.Format("{0},{1}"
               , ServerData.EmpMenuTable.EmpID
               , ServerData.EmpMenuTable.MenuID);
            string szCondition = string.Format("1=1");
            szCondition = string.Format("{0} = {1}"
                    , ServerData.EmpMenuTable.EmpID, EmpID);
            string szTable = ServerData.DataTable.EmpMenu;
            string szSQL = string.Format(ServerData.SQL.SELECT_WHERE, szField, szTable, szCondition);

            IDataReader dataReader = null;
            try
            {
                dataReader = base.DataAccess.ExecuteReader(szSQL, CommandType.Text);
                if (dataReader == null || dataReader.IsClosed || !dataReader.Read())
                {
                    LogManager.Instance.WriteLog("DocumentAccess.GetEmpMenuByMenuID", new string[] { "szSQL" }, new object[] { szSQL }, "未查询到记录");
                    return ExecuteResult.RES_NO_FOUND;
                }
                if (lstEmpMenu == null)
                    lstEmpMenu = new List<EmpMenu>();
                do
                {
                    EmpMenu item = new EmpMenu();
                    item.EmpID = int.Parse(dataReader.GetValue(0).ToString());
                    item.MenuID = int.Parse(dataReader.GetValue(1).ToString());
                    lstEmpMenu.Add(item);
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
        /// 根据主键查找行
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public short GetEmpMenuByMenuID(string MenuID, ref List<EmpMenu> lstEmpMenu)
        {
            if (string.IsNullOrEmpty(MenuID))
                return ExecuteResult.PARAM_ERROR;
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            string szField = string.Format("{0},{1}"
               , ServerData.EmpMenuTable.EmpID
               , ServerData.EmpMenuTable.MenuID);
            string szCondition = string.Format("1=1");
            szCondition = string.Format("{0} = {1}"
                    , ServerData.EmpMenuTable.MenuID, MenuID);
            string szTable = ServerData.DataTable.EmpMenu;
            string szSQL = string.Format(ServerData.SQL.SELECT_WHERE, szField, szTable, szCondition);

            IDataReader dataReader = null;
            try
            {
                dataReader = base.DataAccess.ExecuteReader(szSQL, CommandType.Text);
                if (dataReader == null || dataReader.IsClosed || !dataReader.Read())
                {
                    LogManager.Instance.WriteLog("DocumentAccess.GetEmpMenuByMenuID", new string[] { "szSQL" }, new object[] { szSQL }, "未查询到记录");
                    return ExecuteResult.RES_NO_FOUND;
                }
                if (lstEmpMenu == null)
                    lstEmpMenu = new List<EmpMenu>();
                do
                {
                    EmpMenu item = new EmpMenu();
                    item.EmpID = int.Parse(dataReader.GetValue(0).ToString());
                    item.MenuID = int.Parse(dataReader.GetValue(1).ToString());
                    lstEmpMenu.Add(item);
                } while (dataReader.Read());
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
