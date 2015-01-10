using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
 
using System.Text;
using Windy.Common.Libraries;
 
 
 
using Windy.Service.DAL;
using Windy.Service.DAL.Task;
using Windy.Service.DAL.zyldingfang;
using Windy.Service.Data;

namespace Windy.Service.Data.Task
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“DemandService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 DemandService.svc 或 DemandService.svc.cs，然后开始调试。
    public class DemandService : ServiceBase
    {
        private DemandAccess m_demandAccess = null;
        protected override bool Initialize()
        {
            if (!base.Initialize())
                return false;
            if (this.m_demandAccess == null)
                this.m_demandAccess = new DemandAccess();
            return true;
        }

        public void DoWork()
        {
        }
        public short Add(Demand item)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            
            return DemandAccess.Instance.Add(item);
        }

        /// <summary>
        /// 更新员工信息
        /// </summary>
        /// <param name="news">新闻资讯</param>
        /// <returns>ServerData.ExecuteResult</returns>
        public short Update(Demand item)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return DemandAccess.Instance.Update(item);
        }

        /// <summary>
        /// 删除员工信息
        /// </summary>
        /// <param name="szGroupName">配置组</param>
        /// <param name="szConfigName">配置项</param>
        /// <returns>ServerData.ExecuteResult</returns>
        public short Delete(string ID)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return DemandAccess.Instance.Delete(ID);
        }

        /// <summary>
        /// 获取员工信息集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetDemandList(string IDs,string szCreater,string szOwener, ref List<Demand> lstDemand)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return DemandAccess.Instance.GetDemandList(IDs,szCreater,szOwener, ref lstDemand);
        }

        /// <summary>
        /// 获取分页新闻资讯集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetDemandPageList(int pageSize, int pageIndex, string szCreater, ref List<Demand> lstDemand)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return DemandAccess.Instance.GetDemandPageList(pageSize, pageIndex, szCreater, ref lstDemand);

        }
        /// <summary>
        /// 获取分页新闻资讯集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetDemandTotalCount(string szCategoryName, ref int TotalCount)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return DemandAccess.Instance.GetDemandTotalCount(szCategoryName, ref  TotalCount);
        }



        public short GetDemandByID(string ID, ref Demand item)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return DemandAccess.Instance.GetDemandByID(ID, ref  item);
        }
        public short GetDemandByName(string Name, ref Demand item)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return DemandAccess.Instance.GetDemandsByName(Name, ref  item);
        }


        public short ExecuteUpdate(bool isProc, params string[] sqlarray)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return CommonAccess.Instance.ExecuteUpdate(isProc, sqlarray);
        }
    }
}
