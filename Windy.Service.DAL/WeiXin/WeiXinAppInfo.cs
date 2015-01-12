/******************************************
 * 微信应用信息
 * ****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Windy.Service.DAL.WeiXin
{
    public class WeiXinAppInfo
    {
        public string AppID { get; set; }
        public string AppSecret { get; set; }
        public string OpenID { get; set; }
        public string AccessToken { get; set; }
        
    }
}
