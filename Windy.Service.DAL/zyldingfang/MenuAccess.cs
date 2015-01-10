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
    public class MenuAccess : DBAccessBase
    {
        private static MenuAccess m_Instance = null;
        /// <summary>
        /// 单实例
        /// </summary>
        public static MenuAccess Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new MenuAccess();
                return m_Instance;
            }
        }

        public short Add(Menus item)
        {
            string szFiled = string.Format("{0},{1},{2},{3},{4},{5}"
                    , ServerData.MenuTable.MenuName
                    , ServerData.MenuTable.Url
                    , ServerData.MenuTable.ParentID
                    , ServerData.MenuTable.Icon
                    , ServerData.MenuTable.Description
                    , ServerData.MenuTable.MenuType);
            string szValue = string.Format("'{0}','{1}',{2},'{3}','{4}','{5}'"
                , item.MenuName
                ,item.Url
                , item.ParentID
                , item.Icon
                , item.Description
                , item.MenuType);
            string szSql = string.Format("Insert Into {0}({1}) values({2})"
                , ServerData.DataTable.Menu
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
        /// 更新考点
        /// </summary>
        /// <param name="employee">考点对</param>
        /// <returns>ExecuteResult</returns>
        public short Update(Menus item)
        {
            if (item == null)
                return ExecuteResult.PARAM_ERROR;

            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;

            string szFields = string.Format("{0}='{1}',{2}='{3}',{4}={5},{6}='{7}',{8}='{9}',{10}='{11}'"
                 , ServerData.MenuTable.MenuName, item.MenuName
                 , ServerData.MenuTable.Url, item.Url
                 , ServerData.MenuTable.ParentID, item.ParentID
                 , ServerData.MenuTable.Icon,item.Icon
                 , ServerData.MenuTable.Description, item.Description
                 , ServerData.MenuTable.MenuType, item.MenuType
                 );
            string szCondition = string.Format("{0}={1}"
                , ServerData.MenuTable.ID, item.ID
                );
            string szSQL = string.Format(ServerData.SQL.UPDATE, ServerData.DataTable.Menu, szFields, szCondition);

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
                LogManager.Instance.WriteLog("NurShiftAccess.UpdateShiftWardStatus", new string[] { "szSQL" }, new object[] { szSQL }, "SQL语句执行后返回0!");
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

            string szCondition = string.Format("{0}={1}"
                , ServerData.MenuTable.ID, ID);

            string szSQL = string.Format(ServerData.SQL.DELETE, ServerData.DataTable.Menu, szCondition);

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
        public short GetMenuByID(string ID, ref Menus item)
        {
            if (string.IsNullOrEmpty(ID))
                return ExecuteResult.PARAM_ERROR;
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            string szField = string.Format("{0},{1},{2},{3},{4},{5},{6}"
               , ServerData.MenuTable.ID
               , ServerData.MenuTable.MenuName
               , ServerData.MenuTable.Url
               , ServerData.MenuTable.ParentID
               , ServerData.MenuTable.Icon
               , ServerData.MenuTable.Description
               , ServerData.MenuTable.MenuType);
            string szCondition = string.Format("1=1");
            szCondition = string.Format("{0} = {1}"
                    , ServerData.MenuTable.ID, ID);
            string szTable = ServerData.DataTable.Menu;
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

                if (item == null)
                    item = new Menus();
                item.ID = int.Parse(dataReader.GetValue(0).ToString());
                item.MenuName = dataReader.IsDBNull(1)?"": dataReader.GetString(1);
                item.Url = dataReader.IsDBNull(2) ? "" : dataReader.GetValue(2).ToString();
                item.ParentID = dataReader.IsDBNull(3) ? "" : dataReader.GetValue(3).ToString();
                item.Icon = dataReader.IsDBNull(4) ? "" : dataReader.GetString(4);
                item.Description = dataReader.IsDBNull(5) ? "" : dataReader.GetString(5);
                item.MenuType = dataReader.IsDBNull(6) ? "" : dataReader.GetString(6);
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
        public short GetMenuList(string keyWords, ref List<Menus> lstMenu)
        {
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            string szField = string.Format("{0},{1},{2},{3},{4},{5},{6}"
               , ServerData.MenuTable.ID
               , ServerData.MenuTable.MenuName
               , ServerData.MenuTable.Url
               , ServerData.MenuTable.ParentID
               , ServerData.MenuTable.Icon
               , ServerData.MenuTable.Description
               , ServerData.MenuTable.MenuType);
            string szCondition = string.Format("1=1");
            if (!string.IsNullOrEmpty(keyWords))
                szCondition = string.Format("{0} like '%{1}%'"
                    , ServerData.MenuTable.MenuName, keyWords);
            string szTable = ServerData.DataTable.Menu;
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
                if (lstMenu == null)
                    lstMenu = new List<Menus>();
                do
                {
                    Menus item = new Menus();
                    item.ID = int.Parse(dataReader.GetValue(0).ToString());
                    item.MenuName = dataReader.IsDBNull(1) ? "" : dataReader.GetString(1);
                    item.Url = dataReader.IsDBNull(2) ? "" : dataReader.GetValue(2).ToString();
                    item.ParentID = dataReader.IsDBNull(3) ? "" : dataReader.GetValue(3).ToString();
                    item.Icon = dataReader.IsDBNull(4) ? "" : dataReader.GetString(4);
                    item.Description = dataReader.IsDBNull(5) ? "" : dataReader.GetString(5);
                    item.MenuType = dataReader.IsDBNull(6) ? "" : dataReader.GetString(6);
                    lstMenu.Add(item);
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
        /// 获取组织级别
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetMenuPageList(int pageSize, int pageIndex, string szCategoryName, ref List<Menus> lstMenu)
        {
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            if (lstMenu == null)
                lstMenu = new List<Menus>();
            string szTableName = ServerData.DataTable.Menu;
            string szField = string.Format("{0},{1},{2},{3},{4},{5},{6}"
               , ServerData.MenuTable.ID
               , ServerData.MenuTable.MenuName
               , ServerData.MenuTable.Url
               , ServerData.MenuTable.ParentID
               , ServerData.MenuTable.Icon
               , ServerData.MenuTable.Description
               , ServerData.MenuTable.MenuType); 
            string szOrderName = "ID";
            string szOrderType = "Desc";
            string strWhere = "1=1";
            if (!String.IsNullOrEmpty(szCategoryName))
                strWhere = string.Format("{0} and {1} ='{2}'"
                    , strWhere
                    , ServerData.NewsTable.CategoryName, szCategoryName);
            DataSet result = new DataSet();
            short shRet = base.GetPageList(szTableName, szField, szOrderName, pageSize, pageIndex, false, szOrderType, strWhere, ref  result);
            if (shRet == ExecuteResult.OK)
            {
                try
                {
                    for (int index = 0; index < result.Tables[0].Rows.Count; index++)
                    {
                        Menus item = new Menus();
                        item.ID = int.Parse(result.Tables[0].Rows[index][0].ToString());
                        item.MenuName = result.Tables[0].Rows[index][1].ToString();
                        item.Url = result.Tables[0].Rows[index][2].ToString();
                        item.ParentID = result.Tables[0].Rows[index][3].ToString();
                        item.Icon = result.Tables[0].Rows[index][4].ToString();
                        item.Description = result.Tables[0].Rows[index][5].ToString();
                        item.MenuType = result.Tables[0].Rows[index][6].ToString();

                        lstMenu.Add(item);
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
        /// 获取页码总数
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetMenuTotalCount(string szCategoryName, ref int TotalCount)
        {
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            string szTableName = ServerData.DataTable.Menu;
            string strWhere = "1=1";
            if (!string.IsNullOrEmpty(szCategoryName))
                strWhere = string.Format("{0} and {1} ='{2}'"
                    , strWhere
                    , ServerData.MenuTable.MenuName, szCategoryName);
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

        public short Exists(string strUserName, string strPassWord)
        {
            string szSQL = "Select * from User";
            base.DataAccess.ExecuteReader(szSQL, CommandType.Text);
            return ExecuteResult.OK;
        }
    }
}
