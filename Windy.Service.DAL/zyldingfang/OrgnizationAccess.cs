﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Windy.Common.Libraries;
using Windy.Service.DAL;
 
 
using Windy.Service.DAL;

namespace Windy.Service.DAL.zyldingfang
{
    public class OrgnizationAccess : DBAccessBase
    {
        private static OrgnizationAccess m_Instance = null;
        /// <summary>
        /// 单实例
        /// </summary>
        public static OrgnizationAccess Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new OrgnizationAccess();
                return m_Instance;
            }
        }

        public short Add(Orgnization orgnization)
        {
            string szFiled = string.Format("{0},{1},{2},{3}"
                    , ServerData.OrgnizationTable.OrgName
                    , ServerData.OrgnizationTable.ParentID
                    , ServerData.OrgnizationTable.Description
                    , ServerData.OrgnizationTable.RoleType);
            string szValue = string.Format("'{0}','{1}','{2}','{3}'"
                , orgnization.OrgName
                , orgnization.ParentID
                , orgnization.Describe
                , orgnization.RoleType);
            string szSql = string.Format("Insert Into {0}({1}) values({2})"
                , ServerData.DataTable.Orgnization
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
        public short Update(Orgnization orgnization)
        {
            if (orgnization == null)
                return ExecuteResult.PARAM_ERROR;

            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;

            string szFields = string.Format("{0}='{1}',{2}='{3}',{4}='{5}',{6}='{7}'"
                 , ServerData.OrgnizationTable.OrgName, orgnization.OrgName
                 , ServerData.OrgnizationTable.ParentID, orgnization.ParentID
                 , ServerData.OrgnizationTable.Description, orgnization.Describe
                 , ServerData.OrgnizationTable.RoleType, orgnization.RoleType
                 );
            string szCondition = string.Format("{0}={1}"
                , ServerData.OrgnizationTable.ID, orgnization.ID
                );
            string szSQL = string.Format(ServerData.SQL.UPDATE, ServerData.DataTable.Orgnization, szFields, szCondition);

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
                , ServerData.OrgnizationTable.ID, ID);

            string szSQL = string.Format(ServerData.SQL.DELETE, ServerData.DataTable.Orgnization, szCondition);

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
        public short GetOrgnizationByID(string ID, ref Orgnization orgnization)
        {
            if (string.IsNullOrEmpty(ID))
                return ExecuteResult.PARAM_ERROR;
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            string szField = string.Format("{0},{1},{2},{3},{4}"
               , ServerData.OrgnizationTable.ID
               , ServerData.OrgnizationTable.OrgName
               , ServerData.OrgnizationTable.ParentID
               , ServerData.OrgnizationTable.Description
               , ServerData.OrgnizationTable.RoleType);
            string szCondition = string.Format("1=1");
            szCondition = string.Format("{0} = {1}"
                    , ServerData.OrgnizationTable.ID, ID);
            string szTable = ServerData.DataTable.Orgnization;
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

                if (orgnization == null)
                    orgnization = new Orgnization();
                orgnization.ID = int.Parse(dataReader.GetValue(0).ToString());
                orgnization.OrgName = dataReader.IsDBNull(1)?"": dataReader.GetString(1);
                orgnization.ParentID = dataReader.IsDBNull(2) ? "" : dataReader.GetString(2);
                orgnization.Describe = dataReader.IsDBNull(3) ? "" : dataReader.GetString(3);
                orgnization.RoleType = dataReader.IsDBNull(4) ? "" : dataReader.GetString(4);
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
        public short GetOrgnizationList(string keyWords, ref List<Orgnization> lstOrgnization)
        {
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            string szField = string.Format("{0},{1},{2},{3},{4}"
               , ServerData.OrgnizationTable.ID
               , ServerData.OrgnizationTable.OrgName
               , ServerData.OrgnizationTable.ParentID
               , ServerData.OrgnizationTable.Description
               , ServerData.OrgnizationTable.RoleType);
            string szCondition = string.Format("1=1");
            if (!string.IsNullOrEmpty(keyWords))
                szCondition = string.Format("{0} like '%{1}%'"
                    , ServerData.OrgnizationTable.OrgName, keyWords);
            string szTable = ServerData.DataTable.Orgnization;
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
                if (lstOrgnization == null)
                    lstOrgnization = new List<Orgnization>();
                do
                {
                    Orgnization item = new Orgnization();
                    item.ID = int.Parse(dataReader.GetValue(0).ToString());
                    item.OrgName = dataReader.GetString(1);
                    item.ParentID = dataReader.IsDBNull(2)? "":dataReader.GetString(2);
                    item.Describe = dataReader.IsDBNull(3) ? "" : dataReader.GetString(3);
                    item.RoleType = dataReader.IsDBNull(4) ? "" : dataReader.GetString(4);
                    lstOrgnization.Add(item);
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
        public short GetOrgnizationPageList(int pageSize, int pageIndex, string szCategoryName, ref List<Orgnization> lstOrgnization)
        {
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            if (lstOrgnization == null)
                lstOrgnization = new List<Orgnization>();
            string szTableName = ServerData.DataTable.Orgnization;
            string szFields = string.Format("{0},{1},{2},{3},{4}"
                , ServerData.OrgnizationTable.ID
                , ServerData.OrgnizationTable.OrgName
                , ServerData.OrgnizationTable.ParentID
                , ServerData.OrgnizationTable.Description
                , ServerData.OrgnizationTable.RoleType); 
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
                        Orgnization item = new Orgnization();
                        item.ID = int.Parse(result.Tables[0].Rows[index][0].ToString());
                        item.OrgName = result.Tables[0].Rows[index][1].ToString();
                        item.ParentID = result.Tables[0].Rows[index][2].ToString();
                        item.Describe = result.Tables[0].Rows[index][3].ToString();
                        item.RoleType = result.Tables[0].Rows[index][4].ToString();
                        lstOrgnization.Add(item);
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
        public short GetOrgnizationTotalCount(string szCategoryName, ref int TotalCount)
        {
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            string szTableName = ServerData.DataTable.Orgnization;
            string strWhere = "1=1";
            if (!string.IsNullOrEmpty(szCategoryName))
                strWhere = string.Format("{0} and {1} ='{2}'"
                    , strWhere
                    , ServerData.OrgnizationTable.OrgName, szCategoryName);
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
