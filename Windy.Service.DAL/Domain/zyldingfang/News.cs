using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
 

namespace Windy.Service.DAL
{
    
    public class News : DbTypeBase, ICloneable
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

        private string _NewsTitle;

        /// <summary>
        /// 新闻标题
        /// </summary>
        
        public string NewsTitle
        {
            get { return this._NewsTitle; }
            set { this._NewsTitle = value; }
        }

        private string _NewsContent;

        /// <summary>
        /// 新闻内容
        /// </summary>
        
        public string NewsContent
        {
            get { return this._NewsContent; }
            set { this._NewsContent = value; }
        }

        private string _CategoryName;

        /// <summary>
        /// 类别
        /// </summary>
        
        public string CategoryName
        {
            get { return this._CategoryName; }
            set { this._CategoryName = value; }
        }

        private DateTime _CreateTime;

        /// <summary>
        /// 创建时间
        /// </summary>
        
        public DateTime CreateTime
        {
            get { return this._CreateTime; }
            set { this._CreateTime = value; }
        }

        private string _CreateUser;

        /// <summary>
        /// 创建人
        /// </summary>
        
        public string CreateUser
        {
            get { return this._CreateUser; }
            set { this._CreateUser = value; }
        }

        private DateTime _ModifyTime=DateTime.Now;

        /// <summary>
        /// 修改时间
        /// </summary>
        
        public DateTime ModifyTime
        {
            get { return this._ModifyTime; }
            set { this._ModifyTime = value; }
        }

        private string _ModifyUser;

        /// <summary>
        /// 修改人
        /// </summary>
        
        public string ModifyUser
        {
            get { return this._ModifyUser; }
            set { this._ModifyUser = value; }
        }

        public News()
        {
          
            this._CreateTime = base.DefaultTime;
            this._ModifyTime = base.DefaultTime;
        }
    }
}
