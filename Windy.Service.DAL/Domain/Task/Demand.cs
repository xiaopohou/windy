/*
 * 系统需求管理类
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
namespace Windy.Service.DAL
{
    
    public class Demand:  DbTypeBase, ICloneable
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

        private string _Product;

        /// <summary>
        /// 项目
        /// </summary>
        
        public string Product
        {
            get { return this._Product; }
            set { this._Product = value; }
        }

        private string _Version;

        /// <summary>
        /// 版本号
        /// </summary>
        
        public string Version
        {
            get { return this._Version; }
            set { this._Version = value; }
        }

        private string _Creater;

        /// <summary>
        /// 提交人
        /// </summary>
        
        public string Creater
        {
            get { return this._Creater; }
            set { this._Creater = value; }
        }

        private DateTime _SubmitTime;

        /// <summary>
        /// 提交时间
        /// </summary>
        
        public DateTime SubmitTime
        {
            get { return this._SubmitTime; }
            set { this._SubmitTime = value; }
        }

        private string _Owener;

        /// <summary>
        /// 负责人
        /// </summary>
        
        public string Owener
        {
            get { return this._Owener; }
            set { this._Owener = value; }
        }

        private DateTime _SoluteTime;

        /// <summary>
        /// 姓名
        /// </summary>
        
        public DateTime SoluteTime
        {
            get { return this._SoluteTime; }
            set { this._SoluteTime = value; }
        }

        private string _State;

        /// <summary>
        /// 状态：1 提交，2 处理中，3 已解决 4已确认
        /// </summary>
        
        public string State
        {
            get { return this._State; }
            set { this._State = value; }
        }

        private string _Question;

        /// <summary>
        /// 问题或需求描述
        /// </summary>
        
        public string Question
        {
            get { return this._Question; }
            set { this._Question = value; }
        }

        private string _Title;

        /// <summary>
        /// 标题
        /// </summary>
        
        public string Title
        {
            get { return this._Title; }
            set { this._Title = value; }
        }

        private string _FileAttach;

        /// <summary>
        /// 附件
        /// </summary>
        
        public string FileAttach
        {
            get { return this._FileAttach; }
            set { this._FileAttach = value; }
        }

        private string _Expense;

        /// <summary>
        /// 用时
        /// </summary>
        
        public string Expense
        {
            get { return this._Expense; }
            set { this._Expense = value; }
        }
        private string _Solution;

        /// <summary>
        /// 解决方案
        /// </summary>
        
        public string Solution
        {
            get { return this._Solution; }
            set { this._Solution = value; }
        }
        public Demand(){
            this._SoluteTime = base.DefaultTime;
            this._SubmitTime = base.DefaultTime;
         }

    }
}
