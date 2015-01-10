// ***********************************************************
// ���ݿ���ʲ�ϵͳ���в���������,���ڵ��ò����޸����в���.
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
        /// ��ȡSystemParam����ʵ��
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
        /// ��ȡ�Ƿ����ݷ��ʲ���쳣���ϲ��׳�
        /// </summary>
        public bool ThrowException
        {
            get { return this.m_bThrowException; }
            set { this.m_bThrowException = value; }
        }

        private DataAccess m_DbAccess = null;
        /// <summary>
        /// ��ȡ���ݿ���ʶ���ʵ��
        /// </summary>
        public DataAccess DataAccess
        {
            get { return this.m_DbAccess; }
        }


        /// <summary>
        /// ��ȡ�����ó�����·��
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
        /// ��ȡ������MDSDBLib�Ĵ�����־�ı���Ŀ¼
        /// </summary>
        public string LogFilePath
        {
            get { return LogManager.Instance.LogFilePath; }
            set { LogManager.Instance.LogFilePath = value; }
        }

        /// <summary>
        /// ��ȡ�����õ�����ORA-12571����ʱ,
        /// �Ƿ�ִ�н�����ջ������XDB���ӵĲ���
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
        /// ��ȡ�����õ���������ʱ�Ƿ��׳��쳣
        /// </summary>
        public bool ThrowExOnError
        {
            get { return this.m_bThrowExOnError; }
            set { this.m_bThrowExOnError = value; }
        }

       
    }
}
