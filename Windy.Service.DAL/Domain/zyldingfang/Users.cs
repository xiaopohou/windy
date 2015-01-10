using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
 

namespace Windy.Service.DAL
{
    [Serializable]
    public class Users : DbTypeBase, ICloneable
    {
        private int _ID = 0;
        
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _Name = string.Empty;
        
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Gender = string.Empty;
        
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }
        private string _School = string.Empty;
        
        public string School
        {
            get { return _School; }
            set { _School = value; }
        }
        private string _ExamSchool = string.Empty;
        
        public string ExamSchool
        {
            get { return _ExamSchool; }
            set { _ExamSchool = value; }
        }
        private string _EmployeeID = string.Empty;
        
        public string EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; }
        }
        private string _EmployeeName = string.Empty;
        
        public string EmployeeName
        {
            get { return _EmployeeName; }
            set { _EmployeeName = value; }
        }
        private string _Sequence = "0";
        
        public string Sequence
        {
            get { return _Sequence; }
            set { _Sequence = value; }
        }
        private string _Tel = string.Empty;
        
        public string Tel
        {
            get { return _Tel; }
            set { _Tel = value; }
        }
        private string _Bak = string.Empty;
        
        public string Bak
        {
            get { return _Bak; }
            set { _Bak = value; }
        }

        private string _PassWord = string.Empty;
        
        public string PassWord
        {
            get { return _PassWord; }
            set { _PassWord = value; }
        }

        private string _PayMoney = string.Empty;
        
        public string PayMoney
        {
            get { return _PayMoney; }
            set { _PayMoney = value; }
        }
        private string _ExamPlace = string.Empty;
        
        public string ExamPlace
        {
            get { return _ExamPlace; }
            set { _ExamPlace = value; }
        }
        private string _Room = string.Empty;
        
        public string Room
        {
            get { return _Room; }
            set { _Room = value; }
        }
        private string _Hotel = string.Empty;
        
        public string Hotel
        {
            get { return _Hotel; }
            set { _Hotel = value; }
        }
        private string _HotelExpense = string.Empty;
        
        public string HotelExpense
        {
            get { return _HotelExpense; }
            set { _HotelExpense = value; }
        }
        private string _MoneyBack = string.Empty;
        
        public string MoneyBack
        {
            get { return _MoneyBack; }
            set { _MoneyBack = value; }
        }
        private string _ExceptRoomie = string.Empty;
        
        public string ExceptRoomie
        {
            get { return _ExceptRoomie; }
            set { _ExceptRoomie = value; }
        }
        private string _Template = string.Empty;
        /// <summary>
        /// 考试类型及导出模板
        /// </summary>
        
        public string Template
        {
            get { return _Template; }
            set { _Template = value; }
        }
        private string _PayPlace = string.Empty;
        /// <summary>
        /// 收缴余款所在地
        /// </summary>
        
        public string PayPlace
        {
            get { return _PayPlace; }
            set { _PayPlace = value; }
        }
        private string _Status = string.Empty;
        /// <summary>
        /// 状态 0:已删除 1:未删除 空:未删除
        /// </summary>
        
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public Users()
        {


        }
    }
}
