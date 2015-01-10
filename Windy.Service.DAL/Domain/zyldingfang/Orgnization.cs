using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
 

namespace Windy.Service.DAL
{
    
    public class Orgnization : DbTypeBase, ICloneable
    {

        private int _ID = 0;
        
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _OrgName = string.Empty;
        
        public string OrgName
        {
            get { return _OrgName; }
            set { _OrgName = value; }
        }

        private string _ParentID = string.Empty;
        
        public string ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        private string _ParentName = string.Empty;
        
        public string ParentName
        {
            get { return _ParentName; }
            set { _ParentName = value; }
        }
        private string _Describe = string.Empty;
        
        public string Describe
        {
            get { return _Describe; }
            set { _Describe = value; }
        }
        private string _RoleType = string.Empty;
        
        public string RoleType
        {
            get { return _RoleType; }
            set { _RoleType = value; }
        }

        public Orgnization()
        {
        }
    }
}
