using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
namespace Windy.Service.DAL
{
    [Serializable]
    public class Employee : DbTypeBase, ICloneable
    {
        private string _id;

        /// <summary>
        /// 编号
        /// </summary>
        
        public string ID
        {
            get { return this._id; }
            set { this._id = value; }
        }

        private string _EmpNo;

        /// <summary>
        /// 员工账号
        /// </summary>
        
        public string EmpNo
        {
            get { return this._EmpNo; }
            set { this._EmpNo = value; }
        }

        private string _Pwd;

        /// <summary>
        /// 密码
        /// </summary>
        
        public string Pwd
        {
            get { return this._Pwd; }
            set { this._Pwd = value; }
        }

        private string _Name;

        /// <summary>
        /// 姓名
        /// </summary>
        
        public string Name
        {
            get { return this._Name; }
            set { this._Name = value; }
        }

        private string _RoleType;

        /// <summary>
        /// 角色
        /// </summary>
        
        public string RoleType
        {
            get { return this._RoleType; }
            set { this._RoleType = value; }
        }

        private string _OrgName;

        /// <summary>
        /// 机构名
        /// </summary>
        
        public string OrgName
        {
            get { return this._OrgName; }
            set { this._OrgName = value; }
        }

        private string _Tel;

        /// <summary>
        /// 修改人
        /// </summary>
        
        public string Tel
        {
            get { return this._Tel; }
            set { this._Tel = value; }
        }

        public Employee()
        {
        }
    }
}
