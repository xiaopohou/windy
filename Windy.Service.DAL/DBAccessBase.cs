// ***********************************************************
// 数据库访问层数据访问基类,本基类负责提供一些共享变量与方法.
// 用于防止重复实例化,提高效率
// Creator:YangMingkun  Date:2012-3-20
// Copyright : Heren Health Services Co.,Ltd.
// ***********************************************************
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Windy.Common.Libraries;
using Windy.Common.Libraries.DbAccess;
using Windy.Common.Libraries.Ftp;
using Windy.Service.DAL;
using Windy.Service.DAL;

namespace Windy.Service.DAL
{
    public abstract class DBAccessBase
    {
        private DataAccess m_DbAccess = null;
        private FtpAccess m_FtpAccess = null;
        private StorageMode m_StorageMode = StorageMode.Unknown;

        public DBAccessBase()
        {
        }


        /// <summary>
        /// 获取数据库访问对象实例
        /// </summary>
        protected DataAccess DataAccess
        {
            get { return ServerParam.Instance.DataAccess; }
        }

        /// <summary>
        /// 获取病历及模板的存储模式
        /// </summary>
        protected StorageMode StorageMode
        {
            get
            {
                //if (this.m_StorageMode == StorageMode.Unknown)
                //    this.m_StorageMode = ServerParam.Instance.GetStorageMode();
                return this.m_StorageMode;
            }
        }

        /// <summary>
        /// 获取FTP服务器访问对象实例
        /// </summary>
        protected FtpAccess FtpAccess
        {
            get
            {
                //if (this.m_FtpAccess == null)
                //    this.m_FtpAccess = ServerParam.Instance.GetDocFtpAccess();
                return this.m_FtpAccess;
            }
        }

        /// <summary>
        /// 处理接口产生的异常,主要记录异常日志,以及决定是否向调用层抛出
        /// </summary>
        /// <param name="ex">异常信息</param>
        /// <param name="method">异常方法</param>
        /// <param name="param">方法参数</param>
        /// <param name="error">其他错误信息</param>
        /// <returns>ExecuteResult</returns>
        protected short HandleException(Exception ex, MethodBase method, string param, string error)
        {
            string szMethodDesc = string.Empty;
            if (method != null)
                szMethodDesc = method.ToString();
            LogManager.Instance.WriteLog(szMethodDesc, new string[] { "param", "db-info", "ftp-info" }
                , new object[] { param, this.m_DbAccess, this.m_FtpAccess }, error, ex);

            if (ServerParam.Instance.ThrowExOnError)
                throw ex;
            return ExecuteResult.EXCEPTION;
        }

        /// <summary>
        /// 分页算法（支持Access、SQLServer、Oracle）
        /// </summary>
        /// <param name="szTableName">表名</param>
        /// <param name="szFields">需要返回的列 默认为 *</param>
        /// <param name="szOrderName">排序的字段名</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="isGetCount">返回记录总数,非 0 值则返回</param>
        /// <param name="szOrderType">设置排序类型,Asc 表示升序 Desc 降序</param>
        /// <param name="strWhere">默认为 1=1 </param>
        /// <returns></returns>
        public  short GetPageList(string szTableName, string szFields, string szOrderName, int pageSize, int pageIndex, bool isGetCount, string szOrderType, string strWhere,ref DataSet result)
        {
            if (this.DataAccess.DatabaseType == DatabaseType.ACCESS)
            {
                string szSQL = string.Empty;
                if (isGetCount)
                {
                    szSQL = string.Format("Select count(*) from {0} where {1}"
                            , szTableName
                            , strWhere);
                }
                else
                { 
                    if(pageIndex==1)
                    {
                        szSQL = string.Format("Select Top {0} {1} from {2} where {3} order by {4} {5} "
                                , pageSize.ToString()
                                , szFields
                                , szTableName
                                , strWhere
                                , szOrderName
                                , szOrderType);
                        
                    }
                    else{
                        if (szOrderType == "ASC")
                        {
                            szSQL = "select top " + pageSize + " " + szFields + " from " + szTableName + " where " + strWhere + " and " + szOrderName + " > (select max(" + szOrderName + ") from (Select top " + (pageSize * (pageIndex-1)).ToString() + " * from " + szTableName + " where " + strWhere + " order by " + szOrderName + " " + szOrderType + ") as T)order by  " + szOrderName + " " + szOrderType + ";";
                        }
                        else
                        {
                            szSQL = "select top " + pageSize + " " + szFields + " from " + szTableName + " where " + strWhere + " and " + szOrderName + " < (select min(" + szOrderName + ") from (Select top " + (pageSize * (pageIndex-1)).ToString() + " * from " + szTableName + " where " + strWhere + " order by " + szOrderName + " " + szOrderType + ") as T)order by  " + szOrderName + " " + szOrderType + ";";
                        }
                        
                    }
                }

                try
                {
                    if (szSQL == string.Empty)
                        return ExecuteResult.PARAM_ERROR;
                    result = this.DataAccess.ExecuteDataSet(szSQL, CommandType.Text);
                }
                catch (Exception ex)
                {
                    this.DataAccess.AbortTransaction();
                    return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), szSQL, "分页存储过程保存执行失败!");
                }
                finally { this.DataAccess.CloseConnnection(false); }

                return ExecuteResult.OK;
            }
            else if(this.DataAccess.DatabaseType==DatabaseType.SQLSERVER)
            {
                DbParameter[] pmi = new DbParameter[8];
                pmi[0] = new DbParameter("@tblName", szTableName);
                pmi[1] = new DbParameter("@strGetFields", szFields);
                pmi[2] = new DbParameter("@fldName", szOrderName);
                pmi[3] = new DbParameter("@PageSize", pageSize);
                pmi[4] = new DbParameter("@PageIndex", pageIndex);
                pmi[5] = new DbParameter("@@doCount", isGetCount ? 1 : 0);
                pmi[6] = new DbParameter("@OrderType", szOrderType=="Asc" ? 1 : 0);
                pmi[7] = new DbParameter("@strWhere", strWhere);

                string szSQL = "pro_pageList";
                try
                {
                    result = this.DataAccess.ExecuteDataSet(szSQL, CommandType.StoredProcedure, ref pmi);
                }
                catch (Exception ex)
                {
                    this.DataAccess.AbortTransaction();
                    return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), szSQL, "分页存储过程保存执行失败!");
                }
                this.DataAccess.CommitTransaction(true);
                return ExecuteResult.OK;
            }
            else if (this.DataAccess.DatabaseType == DatabaseType.MYSQL)
            {
                string szSQL = string.Empty;
                if (isGetCount)
                {
                    szSQL = string.Format("Select count(*) from {0} where {1}"
                            , szTableName
                            , strWhere);
                }
                else
                {
                    szSQL = "select " + szFields + " from "+szTableName+" where "+strWhere+" order by  "+szOrderName+" "+szOrderType+" limit "+pageSize*(pageIndex-1)+" , "+pageSize+";";
                        
                       
                }

                try
                {
                    if (szSQL == string.Empty)
                        return ExecuteResult.PARAM_ERROR;
                    result = this.DataAccess.ExecuteDataSet(szSQL, CommandType.Text);
                }
                catch (Exception ex)
                {
                    this.DataAccess.AbortTransaction();
                    return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), szSQL, "分页存储过程保存执行失败!");
                }
                finally { this.DataAccess.CloseConnnection(false); }

                return ExecuteResult.OK;
            }
            return ExecuteResult.OK;
           
        }
    }
}
