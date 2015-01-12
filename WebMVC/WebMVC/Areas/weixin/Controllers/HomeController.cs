using Senparc.Weixin.MP.CommonAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Windy.Service.DAL;
using Windy.Service.DAL.WeiXin;

namespace Windy.WebMVC.Areas.weixin.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /weixin/Home/

        public ActionResult Index()
        {
            WeiXinAppInfo weiXinAppInfo = ServerParam.Instance.WeiXinAppInfo;
            AccessTokenContainer.Register(ServerParam.Instance.WeiXinAppInfo.AppID, ServerParam.Instance.WeiXinAppInfo.AppSecret);
            var accessToken = AccessTokenContainer.GetToken(ServerParam.Instance.WeiXinAppInfo.AppID);

            var result = Senparc.Weixin.MP.AdvancedAPIs.User.Info(accessToken, ServerParam.Instance.WeiXinAppInfo.OpenID);

            return View(result);
        }

    }
}
