using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
namespace Windy.Service.DAL
{
    
    public class EmpMenu : DbTypeBase, ICloneable
    {

        private int _EmpID = 0;
        
        public int EmpID
        {
            get { return _EmpID; }
            set { _EmpID = value; }
        }
        private int _MenuID = 0;
        
        public int MenuID
        {
            get { return _MenuID; }
            set { _MenuID = value; }
        }

        public EmpMenu()
        {
        }
    }
}
