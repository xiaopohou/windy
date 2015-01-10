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
    public class OrgnizationService : ServiceBase
    {

        protected override bool Initialize()
        {
            if (!base.Initialize())
                return false;
            return true;
        }

        public void DoWork()
        {
        }
        public short Add(Orgnization orgnization)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return OrgnizationAccess.Instance.Add(orgnization);
        }

        /// <summary>
        /// 更新员工信息
        /// </summary>
        /// <param name="news">新闻资讯</param>
        /// <returns>ServerData.ExecuteResult</returns>
        public short Update(Orgnization orgnization)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return OrgnizationAccess.Instance.Update(orgnization);
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
            return OrgnizationAccess.Instance.Delete(ID);
        }

        /// <summary>
        /// 获取员工信息集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetOrgnizationList(string keyWords, ref List<Orgnization> lstOrgnization)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return OrgnizationAccess.Instance.GetOrgnizationList(keyWords, ref lstOrgnization);
        }

        /// <summary>
        /// 获取分页新闻资讯集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetOrgnizationPageList(int pageSize, int pageIndex, string szCategoryName, ref List<Orgnization> lstOrgnization)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return OrgnizationAccess.Instance.GetOrgnizationPageList(pageSize, pageIndex, szCategoryName, ref lstOrgnization);

        }
        /// <summary>
        /// 获取分页新闻资讯集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetOrgnizationTotalCount(string szCategoryName, ref int TotalCount)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return OrgnizationAccess.Instance.GetOrgnizationTotalCount(szCategoryName, ref  TotalCount);
        }

        public short Exists(string strUserName, string strPassWord, ref News admin)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            NewsAccess adminAccess = new NewsAccess();
            DataSet ds = new DataSet();
            OrgnizationAccess.Instance.ExecuteQuery("Select * from Users", out ds);
            return ExecuteResult.OK;
        }


        public short GetOrgnizationByID(string ID, ref Orgnization orgnization)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            short shRet = OrgnizationAccess.Instance.GetOrgnizationByID(ID, ref  orgnization);
            if (shRet != ExecuteResult.OK)
                return shRet;
            //取得上级机构的名称
            Orgnization parentOrg = new Orgnization();
            OrgnizationAccess.Instance.GetOrgnizationByID(orgnization.ParentID, ref  parentOrg);
            orgnization.ParentName = parentOrg.OrgName;
            return shRet;

        }


        public short Exists(string strUserName, string strPassWord, ref ExamPlace admin)
        {
            throw new NotImplementedException();
        }


        public short SaveByEmpID(List<EmpOrg> lstEmpOrg, string EmpID)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            //启动事务
            if (!ServerParam.Instance.DataAccess.BeginTransaction(IsolationLevel.ReadCommitted))
                return ExecuteResult.EXCEPTION;
            //先是删除
            short shRet= EmpOrgAccess.Instance.DeleteByEmpID(EmpID);
            //如果出现异常则终止事务
            if (shRet != ExecuteResult.OK && shRet != ExecuteResult.RES_NO_FOUND)
            {
                ServerParam.Instance.DataAccess.AbortTransaction();
                return shRet;
            }
            //插入数据
            foreach (EmpOrg item in lstEmpOrg)
            {
                shRet = EmpOrgAccess.Instance.AddEmpOrg(item);
                if(shRet!=ExecuteResult.OK)
                {
                    ServerParam.Instance.DataAccess.AbortTransaction();
                    return shRet;
                }
            }
            //提交事务
            if (!ServerParam.Instance.DataAccess.CommitTransaction(true))
                return ExecuteResult.EXCEPTION;
            return ExecuteResult.OK;
        }
        public short SaveByOrgID(List<EmpOrg> lstEmpOrg, string OrgID)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            //启动事务
            if (!ServerParam.Instance.DataAccess.BeginTransaction(IsolationLevel.ReadCommitted))
                return ExecuteResult.EXCEPTION;
            //先是删除
            short shRet = EmpOrgAccess.Instance.DeleteByOrgID(OrgID);
            //如果出现异常则终止事务
            if (shRet != ExecuteResult.OK && shRet != ExecuteResult.RES_NO_FOUND)
            {
                ServerParam.Instance.DataAccess.AbortTransaction();
                return shRet;
            }
            //插入数据
            foreach (EmpOrg item in lstEmpOrg)
            {
                shRet = EmpOrgAccess.Instance.AddEmpOrg(item);
                if (shRet != ExecuteResult.OK)
                {
                    ServerParam.Instance.DataAccess.AbortTransaction();
                    return shRet;
                }
            }
            //提交事务
            if (!ServerParam.Instance.DataAccess.CommitTransaction(true))
                return ExecuteResult.EXCEPTION;
            return ExecuteResult.OK;
        }
        public short GetEmpOrgByEmpID(string EmpID, ref List<EmpOrg> lstEmpOrg)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return EmpOrgAccess.Instance.GetEmpOrgByEmpID(EmpID, ref lstEmpOrg);
        }

        public short GetEmpOrgByOrgID(string OrgID, ref List<EmpOrg> lstEmpOrg)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return EmpOrgAccess.Instance.GetEmpOrgByOrgID(OrgID, ref lstEmpOrg);
        }


        public short DeleteByEmpID(string EmpID)
        {
            throw new NotImplementedException();
        }

        public short DeleteByOrgID(string OrgID)
        {
            throw new NotImplementedException();
        }
    }
}
