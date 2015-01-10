/**
 * 提供通用的sql查询访问方法
 * **/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Windy.Common.Libraries;
using Windy.Common.Libraries.DbAccess;

namespace Windy.Service.DAL
{
    public class CommonAccess : DBAccessBase
    {
        private static CommonAccess m_Instance = null;
        /// <summary>
        /// 单实例
        /// </summary>
        public static CommonAccess Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new CommonAccess();
                return m_Instance;
            }
        }
        /// <summary>
        /// 获取数据库服务器时间
        /// </summary>
        /// <param name="dtSysDate">数据库服务器时间</param>
        /// <returns>ExecuteResult</returns>
        public short GetServerTime(ref DateTime dtSysDate)
        {
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;

            string szSQL = null;
            if (base.DataAccess.DatabaseType == DatabaseType.SQLSERVER)
            {
                szSQL = string.Format(ServerData.SQL.SELECT, "CONVERT(VARCHAR(20), GETDATE(), 20)");
            }
            else if (base.DataAccess.DatabaseType == DatabaseType.ORACLE)
            {
                szSQL = string.Format(ServerData.SQL.SELECT_FROM, "SYSDATE", "DUAL");
            }
            else
            {
                dtSysDate = DateTime.Now;
                return ExecuteResult.OK;
            }
            object oRet = null;
            try
            {
                oRet = base.DataAccess.ExecuteScalar(szSQL, CommandType.Text);
            }
            catch (Exception ex)
            {
                return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), szSQL, "SQL语句执行失败!");
            }
            try
            {
                dtSysDate = DateTime.Parse(oRet.ToString());
                return ExecuteResult.OK;
            }
            catch (Exception ex)
            {
                string error = string.Format("无法将“{0}”转换为DateTime!", oRet);
                return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), szSQL, error);
            }
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
        /// 执行指定的SQL语句查询,已绑定变量的方式
        /// </summary>
        /// <param name="sqlInfo">查询的SqlInfo对象</param>
        /// <param name="result">查询返回的结果集</param>
        /// <returns>ExecuteResult</returns>
        public short ExecuteQuery(SqlInfo sqlInfo, out DataSet result)
        {
            result = null;
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            try
            {

                if (sqlInfo.Args == null)
                    result = base.DataAccess.ExecuteDataSet(sqlInfo.SQL, CommandType.Text);
                else
                {
                    DbParameter[] pmi = new DbParameter[sqlInfo.Args.Length];
                    for (int i = 0; i < sqlInfo.Args.Length; i++)
                    {
                        pmi[i] = new DbParameter(i.ToString(), sqlInfo.Args[i]);
                    }
                    result = base.DataAccess.ExecuteDataSet(sqlInfo.SQL, CommandType.Text, ref pmi);
                }
            }
            catch (Exception ex)
            {
                return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), sqlInfo.SQL, "SQL语句执行失败!");
            }
            return ExecuteResult.OK;
        }

        /// <summary>
        /// 执行指定的一系列的SQL语句更新
        /// </summary>
        /// <param name="isProc">传入的SQL是否是存储过程</param>
        /// <param name="sqlarray">查询的SQL语句集合</param>
        /// <returns>ExecuteResult</returns>
        public short ExecuteUpdate(bool isProc, params string[] sqlarray)
        {
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;

            if (!base.DataAccess.BeginTransaction(IsolationLevel.ReadCommitted))
                return ExecuteResult.EXCEPTION;

            if (sqlarray == null)
                sqlarray = new string[0];
            foreach (string sql in sqlarray)
            {
                try
                {
                    if (!isProc)
                        base.DataAccess.ExecuteNonQuery(sql, CommandType.Text);
                    else
                        base.DataAccess.ExecuteNonQuery(sql, CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    base.DataAccess.AbortTransaction();
                    return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), sql, "SQL语句执行失败!");
                }
            }
            base.DataAccess.CommitTransaction(true);
            return ExecuteResult.OK;
        }
    
    }
}
