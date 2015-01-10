using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Windy.Common.Libraries;
using Windy.Service.DAL;
 
 
 
using Windy.Service.DAL;

namespace Windy.Service.DAL.Task
{
    public class DemandAccess : DBAccessBase
    {
        private static DemandAccess m_Instance = null;
        /// <summary>
        /// 单实例
        /// </summary>
        public static DemandAccess Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new DemandAccess();
                return m_Instance;
            }
        }

        public short Add(Demand item)
        {
            string szFiled = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}"
                    , ServerData.DemandTable.Product
                    , ServerData.DemandTable.Version
                    , ServerData.DemandTable.Creater
                    , ServerData.DemandTable.SubmitTime
                    , ServerData.DemandTable.Owener
                    , ServerData.DemandTable.SoluteTime
                    , ServerData.DemandTable.State
                    , ServerData.DemandTable.Question
                    , ServerData.DemandTable.Title
                    , ServerData.DemandTable.FileAttach
                    , ServerData.DemandTable.Expense
                    , ServerData.DemandTable.Solution
                    , ServerData.DemandTable.ID);
            string szValue = string.Format("'{0}','{1}','{2}',{3},'{4}',{5},'{6}','{7}','{8}','{9}','{10}','{11}','{12}'"
                , item.Product
                 , item.Version
                 , item.Creater
                 , base.DataAccess.GetSqlTimeFormat(item.SubmitTime)
                 , item.Owener
                 , base.DataAccess.GetSqlTimeFormat(item.SoluteTime)
                 , item.State
                 , item.Question
                 , item.Title
                 , item.FileAttach
                 , item.Expense
                 , item.Solution
                 , item.ID);
            string szSql = string.Format("Insert Into {0}({1}) values({2})"
                , ServerData.DataTable.Demand
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
        /// <param name="item">新闻资讯</param>
        /// <returns>ExecuteResult</returns>
        public short Update(Demand item)
        {
            if (item == null)
                return ExecuteResult.PARAM_ERROR;

            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;

            string szFields = string.Format("{0}='{1}',{2}='{3}',{4}='{5}',{6}={7},{8}='{9}',{10}={11},{12}='{13}',{14}='{15}',{16}='{17}',{18}='{19}',{20}='{21}',{22}='{23}'"
                 , ServerData.DemandTable.Product, item.Product
                 , ServerData.DemandTable.Version, item.Version
                 , ServerData.DemandTable.Creater, item.Creater
                 , ServerData.DemandTable.SubmitTime, base.DataAccess.GetSqlTimeFormat(item.SubmitTime)
                 , ServerData.DemandTable.Owener, item.Owener
                 , ServerData.DemandTable.SoluteTime, base.DataAccess.GetSqlTimeFormat(item.SoluteTime)
                 , ServerData.DemandTable.State, item.State
                 , ServerData.DemandTable.Question, item.Question
                 , ServerData.DemandTable.Title, item.Title
                 , ServerData.DemandTable.FileAttach, item.FileAttach
                 , ServerData.DemandTable.Expense, item.Expense
                 , ServerData.DemandTable.Solution,item.Solution
                 );
            string szCondition = string.Format("{0}='{1}'"
                , ServerData.DemandTable.ID, item.ID
                );
            string szSQL = string.Format(ServerData.SQL.UPDATE, ServerData.DataTable.Demand, szFields, szCondition);

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
                LogManager.Instance.WriteLog("DemandAccess.Update", new string[] { "szSQL" }, new object[] { szSQL }, "SQL语句执行后返回0!");
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
                , ServerData.DemandTable.ID, ID);

            string szSQL = string.Format(ServerData.SQL.DELETE, ServerData.DataTable.Demand, szCondition);

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
        public short GetDemandByID(string ID, ref Demand item)
        {
            if (string.IsNullOrEmpty(ID))
                return ExecuteResult.PARAM_ERROR;
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            string szField = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}"
               , ServerData.DemandTable.ID
               , ServerData.DemandTable.Product
                , ServerData.DemandTable.Version
                , ServerData.DemandTable.Creater
                , ServerData.DemandTable.SubmitTime
                , ServerData.DemandTable.Owener
                , ServerData.DemandTable.SoluteTime
                , ServerData.DemandTable.State
                , ServerData.DemandTable.Question
                , ServerData.DemandTable.Title
                , ServerData.DemandTable.FileAttach
                , ServerData.DemandTable.Expense
                , ServerData.DemandTable.Solution);
            string szCondition = string.Format("1=1");
            szCondition = string.Format("{0} = '{1}'"
                    , ServerData.DemandTable.ID, ID);
            string szTable = ServerData.DataTable.Demand;
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
                    item = new Demand();
                item.ID = dataReader.GetString(0);
                item.Product =dataReader.IsDBNull(1)?"": dataReader.GetString(1);
                item.Version = dataReader.IsDBNull(2) ? "" : dataReader.GetString(2);
                item.Creater = dataReader.IsDBNull(3) ? "" : dataReader.GetString(3);
                if(!dataReader.IsDBNull(4))
                    item.SubmitTime =   dataReader.GetDateTime(4);
                item.Owener = dataReader.IsDBNull(5) ? "" : dataReader.GetString(5);
                if (!dataReader.IsDBNull(6))
                    item.SubmitTime = dataReader.GetDateTime(6);
                item.State = dataReader.IsDBNull(7) ? "" : dataReader.GetString(7);
                item.Question = dataReader.IsDBNull(8) ? "" : dataReader.GetString(8);
                item.Title = dataReader.IsDBNull(9) ? "" : dataReader.GetString(9);
                item.FileAttach = dataReader.IsDBNull(10) ? "" : dataReader.GetString(10);
                item.Expense = dataReader.IsDBNull(11) ? "" : dataReader.GetString(11);
                item.Solution = dataReader.IsDBNull(12) ? "" : dataReader.GetString(12);

                return ExecuteResult.OK;
            }
            catch (Exception ex)
            {
                return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), szSQL, "SQL语句执行失败!");
            }
            finally { base.DataAccess.CloseConnnection(false); }
        }
        /// <summary>
        /// 查找用户创建的需求
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public short GetDemandsByName(string szCreater, ref Demand item)
        {
            if (string.IsNullOrEmpty(szCreater))
                return ExecuteResult.PARAM_ERROR;
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            string szField = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}"
               , ServerData.DemandTable.ID
               , ServerData.DemandTable.Product
                , ServerData.DemandTable.Version
                , ServerData.DemandTable.Creater
                , ServerData.DemandTable.SubmitTime
                , ServerData.DemandTable.Owener
                , ServerData.DemandTable.SoluteTime
                , ServerData.DemandTable.State
                , ServerData.DemandTable.Question
                , ServerData.DemandTable.Title
                , ServerData.DemandTable.FileAttach
                , ServerData.DemandTable.Expense
                , ServerData.DemandTable.Solution);
            string szCondition = string.Format("1=1");
            szCondition = string.Format("{0} = '{1}'"
                    , ServerData.DemandTable.Creater, szCreater);
            string szTable = ServerData.DataTable.Demand;
            string szSQL = string.Format(ServerData.SQL.SELECT_WHERE, szField, szTable, szCondition);

            IDataReader dataReader = null;
            try
            {
                dataReader = base.DataAccess.ExecuteReader(szSQL, CommandType.Text);
                if (dataReader == null || dataReader.IsClosed || !dataReader.Read())
                {
                    LogManager.Instance.WriteLog("DemandAccess.GetDemandByName", new string[] { "szSQL" }, new object[] { szSQL }, "未查询到记录");
                    return ExecuteResult.RES_NO_FOUND;
                }

                if (item == null)
                    item = new Demand();
                item.ID = dataReader.GetString(0);
                item.Product = dataReader.IsDBNull(1) ? "" : dataReader.GetString(1);
                item.Version = dataReader.IsDBNull(2) ? "" : dataReader.GetString(2);
                item.Creater = dataReader.IsDBNull(3) ? "" : dataReader.GetString(3);
                if (!dataReader.IsDBNull(4))
                    item.SubmitTime = dataReader.GetDateTime(4);
                item.Owener = dataReader.IsDBNull(5) ? "" : dataReader.GetString(5);
                if (!dataReader.IsDBNull(6))
                    item.SubmitTime = dataReader.GetDateTime(6);
                item.State = dataReader.IsDBNull(7) ? "" : dataReader.GetString(7);
                item.Question = dataReader.IsDBNull(8) ? "" : dataReader.GetString(8);
                item.Title = dataReader.IsDBNull(9) ? "" : dataReader.GetString(9);
                item.FileAttach = dataReader.IsDBNull(10) ? "" : dataReader.GetString(10);
                item.Expense = dataReader.IsDBNull(11) ? "" : dataReader.GetString(11);
                item.Solution = dataReader.IsDBNull(12) ? "" : dataReader.GetString(12);
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
        public short GetDemandList(string IDs,string szCreater,string szOwener, ref List<Demand> lstDemand)
        {
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            string szField = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}"
                , ServerData.DemandTable.ID
                , ServerData.DemandTable.Product
                 , ServerData.DemandTable.Version
                 , ServerData.DemandTable.Creater
                 , ServerData.DemandTable.SubmitTime
                 , ServerData.DemandTable.Owener
                 , ServerData.DemandTable.SoluteTime
                 , ServerData.DemandTable.State
                 , ServerData.DemandTable.Question
                 , ServerData.DemandTable.Title
                 , ServerData.DemandTable.FileAttach
                 , ServerData.DemandTable.Expense
                 , ServerData.DemandTable.Solution);
            string szCondition = string.Format("1=1");
            if (!string.IsNullOrEmpty(IDs))
                szCondition = string.Format("{0} and ID in ({1})",szCondition, IDs);
            if (!string.IsNullOrEmpty(szCreater))
                szCondition = string.Format("{0} and  {1} like '%{2}%'"
                    , szCondition
                    , ServerData.DemandTable.Creater, szCreater);
            if (!string.IsNullOrEmpty(szOwener))
                szCondition = string.Format("{0} and {1} = '{2}'"
                    , szCondition
                    , ServerData.DemandTable.Owener, szOwener);
            string szTable = ServerData.DataTable.Demand;
            string szSQL = string.Format(ServerData.SQL.SELECT_WHERE, szField, szTable, szCondition);

            IDataReader dataReader = null;
            try
            {
                dataReader = base.DataAccess.ExecuteReader(szSQL, CommandType.Text);
                if (dataReader == null || dataReader.IsClosed || !dataReader.Read())
                {
                    LogManager.Instance.WriteLog("DemandAccess.GetDemandList", new string[] { "szSQL" }, new object[] { szSQL }, "未查询到记录");
                    return ExecuteResult.RES_NO_FOUND;
                }
                if (lstDemand == null)
                    lstDemand = new List<Demand>();
                do
                {
                    Demand item = new Demand();
                    item.ID = dataReader.GetString(0);
                    item.Product = dataReader.IsDBNull(1) ? "" : dataReader.GetString(1);
                    item.Version = dataReader.IsDBNull(2) ? "" : dataReader.GetString(2);
                    item.Creater = dataReader.IsDBNull(3) ? "" : dataReader.GetString(3);
                    if (!dataReader.IsDBNull(4))
                        item.SubmitTime = dataReader.GetDateTime(4);
                    item.Owener = dataReader.IsDBNull(5) ? "" : dataReader.GetString(5);
                    if (!dataReader.IsDBNull(6))
                        item.SubmitTime = dataReader.GetDateTime(6);
                    item.State = dataReader.IsDBNull(7) ? "" : dataReader.GetString(7);
                    item.Question = dataReader.IsDBNull(8) ? "" : dataReader.GetString(8);
                    item.Title = dataReader.IsDBNull(9) ? "" : dataReader.GetString(9);
                    item.FileAttach = dataReader.IsDBNull(10) ? "" : dataReader.GetString(10);
                    item.Expense = dataReader.IsDBNull(11) ? "" : dataReader.GetString(11);
                    item.Solution = dataReader.IsDBNull(12) ? "" : dataReader.GetString(12);
                    lstDemand.Add(item);
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
        public short GetDemandPageList(int pageSize, int pageIndex, string szCreater, ref List<Demand> lstDemand)
        {
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            if (lstDemand == null)
                lstDemand = new List<Demand>();
            string szTableName = ServerData.DataTable.Demand;
            string szField = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}"
                 , ServerData.DemandTable.ID
                 , ServerData.DemandTable.Product
                  , ServerData.DemandTable.Version
                  , ServerData.DemandTable.Creater
                  , ServerData.DemandTable.SubmitTime
                  , ServerData.DemandTable.Owener
                  , ServerData.DemandTable.SoluteTime
                  , ServerData.DemandTable.State
                  , ServerData.DemandTable.Question
                  , ServerData.DemandTable.Title
                  , ServerData.DemandTable.FileAttach
                  , ServerData.DemandTable.Expense
                  , ServerData.DemandTable.Solution);
            string szOrderName = "SubmitTime";
            string szOrderType = "Desc";
            string strWhere = "1=1";
            if (!String.IsNullOrEmpty(szCreater))
                strWhere = string.Format("{0} and {1} ='{2}'"
                    , strWhere
                    , ServerData.DemandTable.Creater, szCreater);
            DataSet result = new DataSet();
            short shRet = base.GetPageList(szTableName, szField, szOrderName, pageSize, pageIndex, false, szOrderType, strWhere, ref  result);
            if (shRet == ExecuteResult.OK)
            {
                try
                {
                    for (int index = 0; index < result.Tables[0].Rows.Count; index++)
                    {
                        Demand item = new Demand();
                        item.ID = result.Tables[0].Rows[index][0].ToString();
                        item.Product = result.Tables[0].Rows[index][1].ToString();
                        item.Version = result.Tables[0].Rows[index][2].ToString();
                        item.Creater = result.Tables[0].Rows[index][3].ToString();
                        if(!result.Tables[0].Rows[index].IsNull(4))
                            item.SubmitTime = DateTime.Parse(result.Tables[0].Rows[index][4].ToString());
                        item.Owener = result.Tables[0].Rows[index][5].ToString();
                        if (!result.Tables[0].Rows[index].IsNull(6))
                            item.SoluteTime = DateTime.Parse(result.Tables[0].Rows[index][6].ToString());

                        item.State = result.Tables[0].Rows[index][7].ToString();
                        item.Question = result.Tables[0].Rows[index][8].ToString();
                        item.Title = result.Tables[0].Rows[index][9].ToString();
                        item.FileAttach = result.Tables[0].Rows[index][10].ToString();
                        item.Expense = result.Tables[0].Rows[index][11].ToString();
                        item.Solution = result.Tables[0].Rows[index][12].ToString();
                        lstDemand.Add(item);
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
        public short GetDemandTotalCount(string szCategoryName, ref int TotalCount)
        {
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            string szTableName = ServerData.DataTable.Demand;
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
    }
}
