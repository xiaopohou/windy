using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windy.Service.Data
{
    public abstract class ServiceBase
    {
        protected virtual bool Initialize()
        {
            if (ServiceParam.Instance.IsInitCompleted)
                return true;
            return ServiceParam.Instance.Initialize();
        }
    }
}