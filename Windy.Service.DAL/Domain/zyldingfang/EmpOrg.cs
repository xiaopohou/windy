using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Windy.Service.DAL
{
    
    public class EmpOrg : DbTypeBase, ICloneable
    {

        private int _EmpID = 0;
        
        public int EmpID
        {
            get { return _EmpID; }
            set { _EmpID = value; }
        }
        private int _OrgID = 0;
        
        public int OrgID
        {
            get { return _OrgID; }
            set { _OrgID = value; }
        }

        public EmpOrg()
        {
        }
    }
}
