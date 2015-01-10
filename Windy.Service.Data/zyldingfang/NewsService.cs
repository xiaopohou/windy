using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Windy.Common.Libraries;
using Windy.Service.DAL;
using Windy.Service.DAL.zyldingfang;
using Windy.Service.Data;
namespace Windy.Service.Data.zyldingfang
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“AdminService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 AdminService.svc 或 AdminService.svc.cs，然后开始调试。
    public class NewsService :  ServiceBase
    {
        
        private NewsAccess m_adminAccess = null;
        protected override bool Initialize()
        {
            if (!base.Initialize())
                return false;
            if (this.m_adminAccess == null)
                this.m_adminAccess = new NewsAccess();
            return true;
        }

        public void DoWork()
        {
        }
        public short Add(News news)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return NewsAccess.Instance.Add(news);
        }

        /// <summary>
        /// 更新新闻资讯
        /// </summary>
        /// <param name="news">新闻资讯</param>
        /// <returns>ServerData.ExecuteResult</returns>
        public short Update(News news)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return NewsAccess.Instance.Update(news);
        }

        /// <summary>
        /// 删除新闻资讯
        /// </summary>
        /// <param name="szGroupName">配置组</param>
        /// <param name="szConfigName">配置项</param>
        /// <returns>ServerData.ExecuteResult</returns>
        public short Delete(string ID)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return NewsAccess.Instance.Delete(ID);
        }

        /// <summary>
        /// 获取新闻资讯集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetNewsList(string keyWords, ref List<News> lstNews)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return NewsAccess.Instance.GetNewsList(keyWords,ref lstNews);
        }

        /// <summary>
        /// 获取分页新闻资讯集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetNewsPageList(int pageSize, int pageIndex, string szCategoryName, ref List<News> lstNews)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return NewsAccess.Instance.GetNewsPageList(pageSize, pageIndex, szCategoryName, ref lstNews);

        }
        /// <summary>
        /// 获取分页新闻资讯集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetNewsTotalCount(string szCategoryName, ref int TotalCount)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return NewsAccess.Instance.GetNewsTotalCount(szCategoryName, ref  TotalCount);
        } 

        public short Exists(string strUserName, string strPassWord,ref News admin)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            NewsAccess adminAccess = new NewsAccess();
            DataSet ds =new DataSet();
            this.m_adminAccess.ExecuteQuery("Select * from Users", out ds);
            return ExecuteResult.OK;
        }
        public short Exists4(string strUserName, string strPassWord, ref News admin)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            NewsAccess adminAccess = new NewsAccess();
            DataSet ds = new DataSet();
            this.m_adminAccess.ExecuteQuery("Select * from USERS", out ds);
            return ExecuteResult.OK;
        }

        public short GetNewsByID(string ID, ref News news)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return NewsAccess.Instance.GetNewsByID(ID, ref  news);
        }
    }
}
