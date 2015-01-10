﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Windy.Common.Libraries;
using Windy.Service.DAL;
using Windy.Service.Data;

namespace Windy.WebMVC.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        //
        // GET: /Admin/News/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DelData()
        {
            string writeMsg = "删除失败！";

            string selectID = Request.Form["cbx_select"] != "" ? Request.Form["cbx_select"] : "";
            if (selectID != string.Empty && selectID != "0")
            {
                short shRet = SystemContext.Instance.NewsService.Delete(selectID);

                if (shRet == ExecuteResult.OK)
                {
                    writeMsg = string.Format("删除成功");
                }
                else
                {
                    writeMsg = "删除失败！";
                }
            }

            return Content(writeMsg);
        }
        #region 查询数据

        /// <summary>
        /// 查询数据
        /// </summary>
        public ActionResult QueryData()
        {

            int page = Request.Form["page"] != "" ? Convert.ToInt32(Request.Form["page"]) : 0;
            int size = Request.Form["rows"] != "" ? Convert.ToInt32(Request.Form["rows"]) : 0;
            string sort = Request.Form["sort"] != "" ? Request.Form["sort"] : "";
            string order = Request.Form["order"] != "" ? Request.Form["order"] : "";
            if (page < 1) return Content("");

            List<News> lstNews = new List<News>();
            short shRet = SystemContext.Instance.NewsService.GetNewsPageList(size, page, "", ref lstNews);
            JsonHelper json = new JsonHelper();
            string strJson = string.Empty;
            foreach (News item in lstNews)
            {
                json.AddItem("ID", item.ID.ToString());
                json.AddItem("NewsTitle", item.NewsTitle);
                json.AddItem("CategoryName", item.CategoryName);
                //json.AddItem("NewsContent", item.NewsContent);
                json.AddItem("CreateTime", item.CreateTime.ToString());
                json.AddItem("CreateUser", item.CreateUser);
                json.AddItem("ModifyTime", item.ModifyTime.ToString());
                json.AddItem("ModifyUser", item.ModifyUser);
                json.ItemOk();
            }
            int totalCount = 0;
            shRet = SystemContext.Instance.NewsService.GetNewsTotalCount("", ref totalCount);
            json.totlalCount = totalCount;
            if (json.totlalCount > 0)
            {
                strJson = json.ToEasyuiGridJsonString();
            }
            else
            {
                strJson = @"[]";
            }
            // json = "{ \"rows\":[ { \"ID\":\"48\",\"NewsTitle\":\"mr\",\"NewsContent\":\"mrsoft\",\"CreateTime\":\"2013-12-23\",\"CreateUser\":\"ceshi\",\"ModifyTime\":\"2013-12-23\",\"ModifyUser\":\"ceshi\"} ],\"total\":3}";
            return Content(strJson);
        }
        #endregion
    }
}
