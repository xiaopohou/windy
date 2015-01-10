/**
 *系统缓存区
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windy.Service.DAL;
using Windy.Service.DAL.zyldingfang;
using Windy.Service.Data.Task;
using Windy.Service.Data.zyldingfang;

namespace Windy.Service.Data
{
    public class SystemContext
    {
        private static SystemContext m_instance = null;

        /// <summary>
        /// 获取系统运行上下文实例
        /// </summary>
        public static SystemContext Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new SystemContext();
                return m_instance;
            }
        }

        private SystemContext()
        {
        }
        private List<ExamPlace> m_lstExamPlace = null;
        /// <summary>
        /// 缓存考点数据，提高访问效率
        /// </summary>
        public List<ExamPlace> CacheExamPlace
        {
            get
            {
                if (this.m_lstExamPlace == null||this.m_lstExamPlace.Count<=0)
                {
                    this.m_lstExamPlace = new List<ExamPlace>();
                    if (this.m_ExamPlaceService == null)
                        this.m_ExamPlaceService = new ExamPlaceService();
                    short shRet = this.m_ExamPlaceService.GetExamPlaceList("", "", "", ref this.m_lstExamPlace);
                }
                return this.m_lstExamPlace;
            }
            set
            {
                this.m_lstExamPlace = value;
            }
        }
        private EmployeeService m_EmployeeService = null;
        public EmployeeService EmployeeService
        {
            get {
                if (this.m_EmployeeService == null)
                    this.m_EmployeeService = new EmployeeService();
                return this.m_EmployeeService;
            }
        }
        private MenuService m_MenuService = null;
        public MenuService MenuServices
        {
            get
            {
                if (this.m_MenuService == null)
                    this.m_MenuService = new MenuService();
                return this.m_MenuService;
            }
        }
        private OrgnizationService m_OrgnizationService = null;
        public OrgnizationService OrgnizationServices
        {
            get
            {
                if (this.m_OrgnizationService == null)
                    this.m_OrgnizationService = new OrgnizationService();
                return this.m_OrgnizationService;
            }
        }
        private ExamPlaceService m_ExamPlaceService = null;
        public ExamPlaceService ExamPlaceServices
        {
            get
            {
                if (this.m_ExamPlaceService == null)
                    this.m_ExamPlaceService = new ExamPlaceService();
                return this.m_ExamPlaceService;
            }
        }
        private UsersService m_UsersService = null;
        public UsersService UsersService
        {
            get
            {
                if (this.m_UsersService == null)
                    this.m_UsersService = new UsersService();
                return this.m_UsersService;
            }
        }
        private DemandService m_DemandService = null;
        public DemandService DemandService
        {
            get
            {
                if (this.m_DemandService == null)
                    this.m_DemandService = new DemandService();
                return this.m_DemandService;
            }
        }
        private NewsService m_NewsService = null;
        public NewsService NewsService
        {
            get
            {
                if (this.m_NewsService == null)
                    this.m_NewsService = new NewsService();
                return this.m_NewsService;
            }
        }
        private string m_szWorkPath = string.Empty;
        /// <summary>
        /// 获取或设置程序工作路径
        /// </summary>
        public string WorkPath
        {
            set { this.m_szWorkPath = value; }
            get
            {
                if (string.IsNullOrEmpty(this.m_szWorkPath))
                {

                    string szDllPath = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                    this.m_szWorkPath = szDllPath;
                    return m_szWorkPath;
                }
                return this.m_szWorkPath;
            }
        }
        /// <summary>
        /// 新闻资讯类别配置
        /// </summary>
        public struct NewsCategory
        {
            /// <summary>
            /// 新闻中心 News_新闻中心
            /// </summary>
            public const string NewsCenter = "新闻中心";

            /// <summary>
            /// 考研快讯
            /// </summary>
            public const string QuickMessage = "考研快讯";

            /// <summary>
            /// 状元乐公告
            /// </summary>
            public const string Notice = "状元乐公告";


            public static string[] GetCategoryNames()
            {
                return new string[] { 
                    "新闻中心", 
                    "考研快讯", 
                    "状元乐公告"};
            }
        }
        /// <summary>
        /// 项目名
        /// </summary>
        public struct ProductName
        {
            /// <summary>
            /// 状元乐后台管理
            /// </summary>
            public const string ZYLManage = "状元乐后台管理";

            /// <summary>
            /// 状元乐手机应用
            /// </summary>
            public const string AppPhone = "状元乐手机应用";

            /// <summary>
            /// 状元乐网站
            /// </summary>
            public const string ZYLWebSit = "状元乐网站";


            public static string[] GetProductNames()
            {
                return new string[] { 
                    "状元乐后台管理", 
                    "状元乐手机应用", 
                    "状元乐网站"};
            }
        }
        /// <summary>
        /// 需求状态
        /// </summary>
        public struct DemandState
        {
            /// <summary>
            /// 已提交
            /// </summary>
            public const string Submit = "已提交";

            /// <summary>
            /// 处理中
            /// </summary>
            public const string Process = "处理中";

            /// <summary>
            /// 已解决
            /// </summary>
            public const string Solute = "已解决";
            /// <summary>
            /// 已确认
            /// </summary>
            public const string Complete = "已确认";


            public static string[] GetDemandStates()
            {
                return new string[] { 
                    "已提交", 
                    "处理中", 
                    "已解决",
                    "已确认"};
            }
        }
        public string CreatIDs(string szTableName)
        {
            Random rand = new Random();
            return string.Format("{0}_{1}{2}", szTableName, DateTime.Now.ToString("yyyyMMddHHmmss"), rand.Next(0, 9999).ToString().PadLeft(4, '0'));
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
        public struct Template
        {
            public const string SiFa = "业务员,报名次序,姓名,性别,收缴余款所在地,所在学校,报考类型,网报序号,网报密码,联系方式,备注,意向同住人,已交款额,提交考点,房号,酒店,酒店房价,多退少补";
            public const string YanjiuSheng = "业务员,报名次序,姓名,性别,所在学校,所报学校,报考类型,网报序号,网报密码,联系方式,备注,意向同住人,已交款额,提交考点,房号,酒店,酒店房价,多退少补";
            public static string GetTemplate(string Name)
            {
                if (Name == "司法考试")
                    return SiFa;
                else if (Name == "研究生考试")
                    return YanjiuSheng;
                else
                    return string.Empty;
            }
            public static string[] GetArrTemplate()
            {
                return new string[] { 
                    "司法考试", 
                    "研究生考试"
                };
            }
        }
        /// <summary>
        /// 用户角色
        /// </summary>
        public struct RoleType
        {
            /// <summary>
            /// 总公司 
            /// </summary>
            public const string Company = "总公司";

            /// <summary>
            /// 省份
            /// </summary>
            public const string Province = "省份";

            /// <summary>
            /// 城市
            /// </summary>
            public const string City = "城市";

            /// <summary>
            /// 高校
            /// </summary>
            public const string School = "高校";


            public static string[] GetRoleTypes()
            {
                return new string[] { 
                    "总公司", 
                    "省份", 
                    "城市",
                    "高校"
                };
            }
        }
        /// <summary>
        /// 考点类型
        /// </summary>
        public struct PlaceType
        {

            /// <summary>
            /// 考点
            /// </summary>
            public const string SCHOOL = "考点";
            /// <summary>
            /// 城市
            /// </summary>
            public const string CITY = "城市";
            /// <summary>
            /// 省份
            /// </summary>
            public const string PROVINCE = "省份";

            public static string[] GetPlaceTypes()
            {
                return new string[] { 
                    "省份", 
                    "城市",
                    "考点"
                };
            }

        }
        public struct FilePath
        {
            public const string Excel = "~/Content/temp/excel/";
            public const string Demand = "~/Content/temp/demand/";
            public const string News = "~/Content/temp/news/";
        }
    }
}