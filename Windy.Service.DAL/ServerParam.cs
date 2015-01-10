// ***********************************************************
// 数据库访问层系统运行参数管理类,用于调用层来修改运行参数.
// Creator:YangMingkun  Date:2012-3-20
// Copyright : Heren Health Services Co.,Ltd.
// ***********************************************************
using System;
using System.Text;
using System.Collections.Generic;
using Windy.Common.Libraries;
using Windy.Common.Libraries.DbAccess;
using Windy.Common.Libraries.Ftp;
using Windy.Service.DAL;

namespace Windy.Service.DAL
{
    public class ServerParam
    {
        private string m_szWorkPath = string.Empty;
        private string m_szIISAddress = string.Empty;
        private bool m_bAutoClearPool = true;
        private bool m_bThrowExOnError = false;
        //private ConfigAccess m_ConfigAccess = null;
        private FtpAccess m_DocFtpAccess = null;
        private FtpAccess m_UpgradeFtpAccess = null;
        private FtpAccess m_InfoLibFtpAccess = null;
        private DataAccess m_DocDbAccess = null;
        private StorageMode m_eStorageMode = StorageMode.Unknown;


        private static ServerParam m_Instance = null;

        /// <summary>
        /// 获取SystemParam对象实例
        /// </summary>
        public static ServerParam Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new ServerParam();
                return m_Instance;
            }
        }
        
        private ServerParam()
        {
            this.m_DbAccess = new DataAccess();
        }

        private bool m_bThrowException = false;
        /// <summary>
        /// 获取是否将数据访问层的异常向上层抛出
        /// </summary>
        public bool ThrowException
        {
            get { return this.m_bThrowException; }
            set { this.m_bThrowException = value; }
        }

        private DataAccess m_DbAccess = null;
        /// <summary>
        /// 获取数据库访问对象实例
        /// </summary>
        public DataAccess DataAccess
        {
            get { return this.m_DbAccess; }
        }


        /// <summary>
        /// 获取或设置程序工作路径
        /// </summary>
        public string WorkPath
        {
            set
            {
                this.m_szWorkPath = value;
            }

            get
            {
                if (GlobalMethods.Misc.IsEmptyString(this.m_szWorkPath))
                    this.m_szWorkPath = GlobalMethods.Misc.GetWorkingPath();
                return this.m_szWorkPath;
            }
        }
       
        /// <summary>
        /// 获取或设置MDSDBLib的错误日志的备份目录
        /// </summary>
        public string LogFilePath
        {
            get { return LogManager.Instance.LogFilePath; }
            set { LogManager.Instance.LogFilePath = value; }
        }

        /// <summary>
        /// 获取或设置当出现ORA-12571错误时,
        /// 是否执行进行清空缓存池中XDB连接的操作
        /// </summary>
        public bool AutoClearPool
        {
            get
            {
                return this.m_bAutoClearPool;
            }

            set
            {
                this.m_bAutoClearPool = value;
                if (this.m_DocDbAccess != null)
                    this.m_DocDbAccess.ClearPoolEnabled = value;
            }
        }

        /// <summary>
        /// 获取或设置当发生错误时是否抛出异常
        /// </summary>
        public bool ThrowExOnError
        {
            get { return this.m_bThrowExOnError; }
            set { this.m_bThrowExOnError = value; }
        }

       
    }
}
