using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Windy.Common.Libraries;

namespace Windy.Service.DAL
{

    [Serializable]
    public class DbTypeBase : Object, ICloneable
    {
        /// <summary>
        /// 获取缺省时间
        /// </summary>
        
        public DateTime DefaultTime
        {
            set { }
            get { return DateTime.Parse("1900-1-1"); }
        }

        public virtual object Clone()
        {
            object instance = Activator.CreateInstance(this.GetType());
            GlobalMethods.Reflect.CopyProperties(this, instance);
            return instance;
        }
    }
}
