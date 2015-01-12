using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Windy.Common.Libraries;
using Windy.Common.Libraries.DbAccess;
using Windy.Service.DAL;
using Windy.Service.DAL.WeiXin;

namespace Windy.Service.Data
{
    public class ServiceParam
    {
        private static ServiceParam m_instance = null;
        /// <summary>
        /// 获取ServiceParam对象实例
        /// </summary>
        public static ServiceParam Instance 
        {
            get 

            {
                if (m_instance == null)
                    m_instance = new ServiceParam();
                return m_instance;
            }
        }
        private ServiceParam()
        {
        }
        
        private bool m_bInitCompleted = false;
        /// <summary>
        /// 获取当前web服务运行参数是否已经初始化
        /// </summary>
        public bool IsInitCompleted
        {
            get { return this.m_bInitCompleted; }
        }

        public bool Initialize()
        {
            string szDbType = null, szProvider = null, szConnString = null;
            try
            {
                //初始化IIS服务bin工作路径
                string szWorkPath = AppDomain.CurrentDomain.BaseDirectory;
                if (!szWorkPath.EndsWith("\\")) szWorkPath += "\\";
                if (!szWorkPath.EndsWith("\\bin\\", StringComparison.OrdinalIgnoreCase))
                    szWorkPath += "\\bin\\";

                if (string.IsNullOrEmpty(szWorkPath))
                    szWorkPath = ConfigurationManager.AppSettings["WorkPath"];

                ServerParam.Instance.WorkPath = szWorkPath;
                ServerParam.Instance.ThrowException = true;

                //初始化系统日志记录程序
                if (ConfigurationManager.AppSettings["system.log.type"] == "text")
                    LogManager.Instance.TextLogOnly = true;
                LogManager.Instance.LogFilePath = szWorkPath + "\\Logs";

                //初始化数据访问层数据库参数
                szDbType = ConfigurationManager.AppSettings["zyldingfang.db.type"];
                szProvider = ConfigurationManager.AppSettings["zyldingfang.db.provider"];
                szConnString = ConfigurationManager.AppSettings["zyldingfang.db.connection"];

                //初始化微信App信息
                WeiXinAppInfo weiXinAppInfo = new WeiXinAppInfo();
                weiXinAppInfo.AppID = ConfigurationManager.AppSettings["weixin.app_id"];
                weiXinAppInfo.AppSecret = ConfigurationManager.AppSettings["weixin.app_secret"];
                weiXinAppInfo.OpenID = ConfigurationManager.AppSettings["weixin.open_id"];
                weiXinAppInfo.AccessToken = ConfigurationManager.AppSettings["weixin.access_token"];
                ServerParam.Instance.WeiXinAppInfo = weiXinAppInfo;
            }
            catch (Exception ex)
            {
                LogManager.Instance.WriteLog("ServiceParam.Initialize", ex);
                return false;
            }
            this.m_bInitCompleted = this.SetMainDbParams(szDbType, szProvider, szConnString);
            return this.m_bInitCompleted;
        }

        /// <summary>
        /// 设置主数据库访问参数
        /// </summary>
        /// <param name="dbtype">数据库类型</param>
        /// <param name="provider">提供程序类型</param>
        /// <param name="connString">连接字符串</param>
        /// <returns>是否设置成功</returns>
        private bool SetMainDbParams(string dbtype, string provider, string connString)
        {
            ServerParam.Instance.DataAccess.ClearPoolEnabled = true;
            if (string.IsNullOrEmpty(connString))
            {
                LogManager.Instance.WriteLog("ServiceParam.SetMainDbParams"
                    , new string[] { "dbtype", "provider", "connString" }
                    , new string[] { dbtype, provider, connString }, "数据库连接串未设置!");
                return false;
            }
            ServerParam.Instance.DataAccess.ConnectionString = connString;

            if (dbtype == null) dbtype = string.Empty;
            dbtype = dbtype.Trim().ToLower();

            if (dbtype == "oracle")
            {
                ServerParam.Instance.DataAccess.DatabaseType = DatabaseType.ORACLE;
            }
            else if (dbtype == "sqlserver")
            {
                ServerParam.Instance.DataAccess.DatabaseType = DatabaseType.SQLSERVER;
            }
            else if (dbtype == "access")
            {
                ServerParam.Instance.DataAccess.DatabaseType = DatabaseType.ACCESS;
            }
            else if(dbtype=="mysql")
            {
                ServerParam.Instance.DataAccess.DatabaseType = DatabaseType.MYSQL;
            }
            else
            {
                LogManager.Instance.WriteLog("ServiceParam.SetMainDbParams"
                    , new string[] { "dbtype", "provider", "connString" }
                    , new string[] { dbtype, provider, connString }, "不支持的数据库类型!");
                return false;
            }

            if (provider == null) provider = string.Empty;
            provider = provider.Trim().ToLower();

            if (provider == "oledb")
            {
                ServerParam.Instance.DataAccess.DataProvider = DataProvider.OleDb;
            }
            else if (provider == "odpnet")
            {
                ServerParam.Instance.DataAccess.DataProvider = DataProvider.ODPNET;
            }
            else if (provider == "mysqlclient")
            {
                ServerParam.Instance.DataAccess.DataProvider = DataProvider.MySqlClient;
            }
            else if (provider == "odbc")
            {
                ServerParam.Instance.DataAccess.DataProvider = DataProvider.Odbc;
            }
            else if (provider == "oracleclient")
            {
                ServerParam.Instance.DataAccess.DataProvider = DataProvider.OracleClient;
            }
            else if (provider == "sqlclient")
            {
                ServerParam.Instance.DataAccess.DataProvider = DataProvider.SqlClient;
            }
            else
            {
                LogManager.Instance.WriteLog("ServiceParam.SetMainDbParams"
                    , new string[] { "dbtype", "provider", "connString" }
                    , new string[] { dbtype, provider, connString }, "不支持的数据库驱动提供程序!");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 默认密码 六个1，得到考生登陆默认密码
        /// </summary>
        /// <param name="Tel"></param>
        /// <returns></returns>
        public string GetPwd(string Tel)
        {
            if (Tel == null)
                return string.Empty;
            if (Tel.Length >= 6)
            {
                // return Tel.Substring(Tel.Length - 6, 6);
                return "111111";  //改成默认密码 六个1
            }
            return string.Empty;
        }
       
    }
}