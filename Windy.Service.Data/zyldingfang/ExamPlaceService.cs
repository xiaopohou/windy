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
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“EmployeeService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 EmployeeService.svc 或 EmployeeService.svc.cs，然后开始调试。
    public class ExamPlaceService : ServiceBase
    {
        private ExamPlaceAccess m_examPlaceAccess = null;
        protected override bool Initialize()
        {
            if (!base.Initialize())
                return false;
            if (this.m_examPlaceAccess == null)
                this.m_examPlaceAccess = new ExamPlaceAccess();
            return true;
        }

        public void DoWork()
        {
        }
        public short Add(ExamPlace examPlace)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            short shRet= ExamPlaceAccess.Instance.Add(examPlace);
            List<ExamPlace> lstExamPlace=new List<ExamPlace>();
            ExamPlaceAccess.Instance.GetExamPlaceList("", "", "", ref lstExamPlace);
            SystemContext.Instance.CacheExamPlace = lstExamPlace;
            return shRet;
            
        }

        /// <summary>
        /// 更新员工信息
        /// </summary>
        /// <param name="news">新闻资讯</param>
        /// <returns>ServerData.ExecuteResult</returns>
        public short Update(ExamPlace examPlace)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            short shRet= ExamPlaceAccess.Instance.Update(examPlace);
            List<ExamPlace> lstExamPlace = new List<ExamPlace>();
            ExamPlaceAccess.Instance.GetExamPlaceList("", "", "", ref lstExamPlace);
            SystemContext.Instance.CacheExamPlace = lstExamPlace;
            return shRet;
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
            short shRet= ExamPlaceAccess.Instance.Delete(ID);
            List<ExamPlace> lstExamPlace = new List<ExamPlace>();
            ExamPlaceAccess.Instance.GetExamPlaceList("", "", "", ref lstExamPlace);
            SystemContext.Instance.CacheExamPlace = lstExamPlace;
            return shRet;
        }

        /// <summary>
        /// 获取员工信息集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetExamPlaceList(string keyWords,string szPlaceType,string ParentID, ref List<ExamPlace> lstExamPlace)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return ExamPlaceAccess.Instance.GetExamPlaceList(keyWords,szPlaceType,ParentID, ref lstExamPlace);
        }

        /// <summary>
        /// 获取分页新闻资讯集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetExamPlacePageList(int pageSize, int pageIndex, string szCategoryName, ref List<ExamPlace> lstExamPlace)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return ExamPlaceAccess.Instance.GetExamPlacePageList(pageSize, pageIndex, szCategoryName, ref lstExamPlace);

        }
        /// <summary>
        /// 获取分页新闻资讯集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetExamPlaceTotalCount(string szCategoryName, ref int TotalCount)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return ExamPlaceAccess.Instance.GetExamPlaceTotalCount(szCategoryName, ref  TotalCount);
        }

        public short Exists(string strUserName, string strPassWord, ref News admin)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            NewsAccess adminAccess = new NewsAccess();
            DataSet ds = new DataSet();
            this.m_examPlaceAccess.ExecuteQuery("Select * from Users", out ds);
            return ExecuteResult.OK;
        }


        public short GetExamPlaceByID(string ID, ref ExamPlace examPlace)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            short shRet= ExamPlaceAccess.Instance.GetExamPlaceByID(ID, ref  examPlace);
            if (shRet != ExecuteResult.OK)
                return shRet;
            //取得上级机构的名称
            ExamPlace parent = new ExamPlace();
            shRet= ExamPlaceAccess.Instance.GetExamPlaceByID(examPlace.ParentID, ref  parent);
            examPlace.ParentName = parent.PlaceName;
            return shRet;
        }

        public short SaveExamPlace(string szCityID, string szExamPlace)
        {
            ExamPlace examPlace = new ExamPlace();
            short shRet = ExamPlaceAccess.Instance.GetExamPlaceByNameAndParentID(szCityID, szExamPlace, ref examPlace);
            //考生提交的考点不存在于数据库
            if (shRet == ExecuteResult.RES_NO_FOUND)
            {
                examPlace.ParentID = szCityID;
                examPlace.PlaceName = szExamPlace;
                examPlace.PlaceType = SystemContext.PlaceType.SCHOOL;
                examPlace.Description = string.Empty;
                shRet = ExamPlaceAccess.Instance.Add(examPlace);

                List<ExamPlace> lstExamPlace = new List<ExamPlace>();
                ExamPlaceAccess.Instance.GetExamPlaceList("", "", "", ref lstExamPlace);
                SystemContext.Instance.CacheExamPlace = lstExamPlace;

                if (shRet != ExecuteResult.OK)
                    return shRet;
            }
            return ExecuteResult.OK;
        }

        public short Exists(string strUserName, string strPassWord, ref ExamPlace admin)
        {
            throw new NotImplementedException();
        }
    }
}
