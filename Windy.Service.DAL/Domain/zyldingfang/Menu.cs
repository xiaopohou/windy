using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
 

namespace Windy.Service.DAL
{
    
    public class Menus : DbTypeBase, ICloneable
    {

        private int _ID = 0;
        
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _MenuName = string.Empty;
        
        public string MenuName
        {
            get { return _MenuName; }
            set { _MenuName = value; }
        }
        private string _Url = string.Empty;
        
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }
        private string _ParentID = "0";
        
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
        private string _Icon = string.Empty;
        
        public string Icon
        {
            get { return _Icon; }
            set { _Icon = value; }
        }
        private string _Description = string.Empty;
        
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private string _MenuType = string.Empty;
        
        public string MenuType
        {
            get { return _MenuType; }
            set { _MenuType = value; }
        }
        public Menus()
        {
        }
    }
}
