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
    public class MenuService : ServiceBase
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
        public short Add(Menus item)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return MenuAccess.Instance.Add(item);
        }

        /// <summary>
        /// 更新员工信息
        /// </summary>
        /// <param name="news">新闻资讯</param>
        /// <returns>ServerData.ExecuteResult</returns>
        public short Update(Menus item)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return MenuAccess.Instance.Update(item);
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
            return MenuAccess.Instance.Delete(ID);
        }

        /// <summary>
        /// 获取员工信息集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetMenuList(string keyWords, ref List<Menus> lstMenu)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;

            return MenuAccess.Instance.GetMenuList(keyWords, ref lstMenu);
        }

        /// <summary>
        /// 获取分页新闻资讯集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetMenuPageList(int pageSize, int pageIndex, string szCategoryName, ref List<Menus> lstMenu)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;

            short shRet= MenuAccess.Instance.GetMenuPageList(pageSize, pageIndex, szCategoryName, ref lstMenu);
            return shRet;

        }
        /// <summary>
        /// 获取分页新闻资讯集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetMenuTotalCount(string szCategoryName, ref int TotalCount)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return MenuAccess.Instance.GetMenuTotalCount(szCategoryName, ref  TotalCount);
        }

        public short Exists(string strUserName, string strPassWord, ref News admin)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            NewsAccess adminAccess = new NewsAccess();
            DataSet ds = new DataSet();
            MenuAccess.Instance.ExecuteQuery("Select * from Users", out ds);
            return ExecuteResult.OK;
        }


        public short GetMenuByID(string ID, ref Menus item)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            short shRet = MenuAccess.Instance.GetMenuByID(ID, ref  item);
            if (shRet != ExecuteResult.OK)
                return shRet;
            //取得上级机构的名称
            Menus parentMenu = new Menus();
            MenuAccess.Instance.GetMenuByID(item.ParentID, ref  parentMenu);
            item.ParentName = parentMenu.MenuName;
            return shRet;

        }


        public short Exists(string strUserName, string strPassWord, ref ExamPlace admin)
        {
            throw new NotImplementedException();
        }

        public short SaveByEmpID(List<EmpMenu> lstEmpMenu, string EmpID)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            //启动事务
            if (!ServerParam.Instance.DataAccess.BeginTransaction(IsolationLevel.ReadCommitted))
                return ExecuteResult.EXCEPTION;
            //先是删除
            short shRet = EmpMenuAccess.Instance.DeleteByEmpID(EmpID);
            //如果出现异常则终止事务
            if (shRet != ExecuteResult.OK && shRet != ExecuteResult.RES_NO_FOUND)
            {
                ServerParam.Instance.DataAccess.AbortTransaction();
                return shRet;
            }
            //插入数据
            foreach (EmpMenu item in lstEmpMenu)
            {
                shRet = EmpMenuAccess.Instance.AddEmpMenu(item);
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
        public short SaveByMenuID(List<EmpMenu> lstEmpMenu, string MenuID)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            //启动事务
            if (!ServerParam.Instance.DataAccess.BeginTransaction(IsolationLevel.ReadCommitted))
                return ExecuteResult.EXCEPTION;
            //先是删除
            short shRet = EmpMenuAccess.Instance.DeleteByMenuID(MenuID);
            //如果出现异常则终止事务
            if (shRet != ExecuteResult.OK && shRet != ExecuteResult.RES_NO_FOUND)
            {
                ServerParam.Instance.DataAccess.AbortTransaction();
                return shRet;
            }
            //插入数据
            foreach (EmpMenu item in lstEmpMenu)
            {
                shRet = EmpMenuAccess.Instance.AddEmpMenu(item);
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
        public short GetEmpMenuByEmpID(string EmpID, ref List<EmpMenu> lstEmpMenu)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return EmpMenuAccess.Instance.GetEmpMenuByEmpID(EmpID, ref lstEmpMenu);
        }

        public short GetEmpMenuByMenuID(string MenuID, ref List<EmpMenu> lstEmpMenu)
        {
            if (!this.Initialize())
                return ExecuteResult.PARAM_ERROR;
            return EmpMenuAccess.Instance.GetEmpMenuByMenuID(MenuID, ref lstEmpMenu);
        }


        public short DeleteByEmpID(string EmpID)
        {
            throw new NotImplementedException();
        }

        public short DeleteByMenuID(string MenuID)
        {
            throw new NotImplementedException();
        }
    }
}
