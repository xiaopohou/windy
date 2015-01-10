using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Windy.Service.DAL
{

    
    public class ExamPlace : DbTypeBase, ICloneable
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

        private string _PlaceName;

        /// <summary>
        /// 考点名称
        /// </summary>
        
        public string PlaceName
        {
            get { return this._PlaceName; }
            set { this._PlaceName = value; }
        }

        private string _ParentID;

        /// <summary>
        /// 上级
        /// </summary>
        
        public string ParentID
        {
            get { return this._ParentID; }
            set { this._ParentID = value; }
        }

        private string _ParentName;

        /// <summary>
        /// 姓名
        /// </summary>
        
        public string ParentName
        {
            get { return this._ParentName; }
            set { this._ParentName = value; }
        }

        private string _Description;

        /// <summary>
        /// 角色
        /// </summary>
        
        public string Description
        {
            get { return this._Description; }
            set { this._Description = value; }
        }

        private string _PlaceType;

        /// <summary>
        /// 机构名
        /// </summary>
        
        public string PlaceType
        {
            get { return this._PlaceType; }
            set { this._PlaceType = value; }
        }

       

        public ExamPlace()
        {
        }
    }
}
