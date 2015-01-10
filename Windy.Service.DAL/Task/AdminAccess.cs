using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Windy.Common.Libraries;
 
 
using Windy.Service.DAL;

namespace Windy.Service.DAL.Task
{
    public class AdminAccess : DBAccessBase
    {
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
