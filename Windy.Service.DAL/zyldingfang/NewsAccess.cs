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
    public class NewsAccess : DBAccessBase
    {
        private static NewsAccess m_Instance = null;
        /// <summary>
        /// 单实例
        /// </summary>
        public static NewsAccess Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new NewsAccess();
                return m_Instance;
            }
        }

        public short Add(News news)
        {
       
            string szFiled = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}"
                
                , ServerData.NewsTable.NewsTitle
                , ServerData.NewsTable.NewsContent
                , ServerData.NewsTable.CreateTime
                , ServerData.NewsTable.CreateUser
                , ServerData.NewsTable.ModifyTime
                , ServerData.NewsTable.ModifyUser
                , ServerData.NewsTable.CategoryName
                , ServerData.NewsTable.ID);
            string szValue = string.Format("'{0}','{1}',{2},'{3}',{4},'{5}','{6}','{7}'"
              
                , news.NewsTitle
                , news.NewsContent
                , base.DataAccess.GetSqlTimeFormat(news.CreateTime)
                , news.CreateUser
                , base.DataAccess.GetSqlTimeFormat(news.ModifyTime)
                , news.ModifyUser
                , news.CategoryName
                  , news.ID);
            string szSQL = string.Format("Insert Into {0}({1}) values({2})"
                , ServerData.DataTable.NEWS
                , szFiled
                , szValue);
            try
            {
                base.DataAccess.ExecuteNonQuery(szSQL, CommandType.Text);

            }
            catch (Exception ex)
            {
                return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), szSQL, "SQL语句执行失败!");
            }
            return ExecuteResult.OK;
        }

        /// <summary>
        /// 更新新闻资讯
        /// </summary>
        /// <param name="news">新闻资讯</param>
        /// <returns>ExecuteResult</returns>
        public short Update(News news)
        {
            if (news == null)
                return ExecuteResult.PARAM_ERROR;

            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;

            string szFields = string.Format("{0}='{1}',{2}='{3}',{4}={5},{6}='{7}',{8}='{9}'"
                 , ServerData.NewsTable.NewsTitle, news.NewsTitle
                 , ServerData.NewsTable.NewsContent, news.NewsContent
                 , ServerData.NewsTable.ModifyTime, base.DataAccess.GetSqlTimeFormat(news.ModifyTime)
                 , ServerData.NewsTable.ModifyUser, news.ModifyUser
                 , ServerData.NewsTable.CategoryName, news.CategoryName
                 );
            string szCondition = string.Format("{0}='{1}'"
                , ServerData.NewsTable.ID, news.ID
                );
            string szSQL = string.Format(ServerData.SQL.UPDATE, ServerData.DataTable.NEWS, szFields, szCondition);

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

            string szCondition = string.Format("{0} in ({1})"
                , ServerData.NewsTable.ID, ID);

            string szSQL = string.Format(ServerData.SQL.DELETE, ServerData.DataTable.NEWS, szCondition);

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
        public short GetNewsByID(string ID, ref News news)
        {
            if (string.IsNullOrEmpty(ID))
                return ExecuteResult.PARAM_ERROR;
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            string szField = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}"
               , ServerData.NewsTable.ID, ServerData.NewsTable.NewsTitle
               , ServerData.NewsTable.NewsContent, ServerData.NewsTable.CreateTime
               , ServerData.NewsTable.CreateUser, ServerData.NewsTable.ModifyTime
               , ServerData.NewsTable.ModifyUser, ServerData.NewsTable.CategoryName);
            string szCondition = string.Format("1=1");
            szCondition = string.Format("{0} = '{1}'"
                    , ServerData.NewsTable.ID, ID);
            string szTable = ServerData.DataTable.NEWS;
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

                if (news == null)
                    news = new News();
                news.ID = dataReader.GetString(0);
                news.NewsTitle = dataReader.GetString(1);
                news.NewsContent = dataReader.GetString(2);
                news.CreateTime = dataReader.GetDateTime(3);
                news.CreateUser = dataReader.GetString(4);
                news.ModifyTime = dataReader.GetDateTime(5);
                news.ModifyUser = dataReader.GetString(6);
                news.CategoryName = dataReader.IsDBNull(7)?"":dataReader.GetString(7);
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
        public short GetNewsList(string keyWords, ref List<News> lstNews)
        {
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            string szField = string.Format("{0},{1},{2},{3},{4},{5},{6}"
               , ServerData.NewsTable.ID, ServerData.NewsTable.NewsTitle
               , ServerData.NewsTable.NewsContent, ServerData.NewsTable.CreateTime
               , ServerData.NewsTable.CreateUser, ServerData.NewsTable.ModifyTime
               , ServerData.NewsTable.ModifyUser);
            string szCondition = string.Format("1=1");
            if (!string.IsNullOrEmpty(keyWords))
                szCondition = string.Format("{0} like '%{1}%'"
                    , ServerData.NewsTable.NewsTitle, keyWords);
            string szTable = ServerData.DataTable.NEWS;
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
                if (lstNews == null)
                    lstNews = new List<News>();
                do
                {
                    News news = new News();
                    news.ID = dataReader.GetString(0);
                    news.NewsTitle = dataReader.GetString(1);
                    news.NewsContent = dataReader.GetString(2);
                    news.CreateTime = dataReader.GetDateTime(3);
                    news.CreateUser = dataReader.GetString(4);
                    news.ModifyTime = dataReader.GetDateTime(5);
                    news.ModifyUser = dataReader.GetString(6);
                    lstNews.Add(news);
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
        public short GetNewsPageList(int pageSize, int pageIndex, string szCategoryName, ref List<News> lstNews)
        {
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            if (lstNews == null)
                lstNews = new List<News>();
            string szTableName = ServerData.DataTable.NEWS;
            string szFields = "ID,NewsTitle,NewsContent,CreateTime,CreateUser,ModifyTime,ModifyUser,CategoryName";
            string szOrderName = "CreateTime";
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
                        News news = new News();
                        news.ID = result.Tables[0].Rows[index][0].ToString();
                        news.NewsTitle = result.Tables[0].Rows[index][1].ToString();
                        news.NewsContent = result.Tables[0].Rows[index][2].ToString();
                        news.CreateTime = DateTime.Parse(result.Tables[0].Rows[index][3].ToString());
                        news.CreateUser = result.Tables[0].Rows[index][4] == null ? "" : result.Tables[0].Rows[index][4].ToString();
                        if(result.Tables[0].Rows[index][5].ToString()!="")
                        {
                        news.ModifyTime =  DateTime.Parse(result.Tables[0].Rows[index][5].ToString());
                        }
                        news.ModifyUser = result.Tables[0].Rows[index][6] == null ? "" : result.Tables[0].Rows[index][6].ToString();
                        news.CategoryName = result.Tables[0].Rows[index][7] == null ? "" : result.Tables[0].Rows[index][7].ToString();
                        lstNews.Add(news);
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
        public short GetNewsTotalCount(string szCategoryName, ref int TotalCount)
        {
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            string szTableName = ServerData.DataTable.NEWS;
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

        public short Exists(string strUserName, string strPassWord)
        {
            string szSQL = "Select * from User";
            base.DataAccess.ExecuteReader(szSQL, CommandType.Text);
            return ExecuteResult.OK;
        }
    }
}
