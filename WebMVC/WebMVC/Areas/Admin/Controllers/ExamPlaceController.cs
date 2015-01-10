using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Windy.Common.Libraries;
using Windy.Service.DAL;
using Windy.Service.Data;

namespace Windy.WebMVC.Areas.Admin.Controllers
{
    public class ExamPlaceController : Controller
    {
        //
        // GET: /Admin/ExamPlace/

        public ActionResult Index()
        {
            return View();
        }
        #region 查询数据
        /// <summary>
        /// 获取指定ID的数据
        /// </summary>
        public ActionResult QueryOneData()
        {
            string id = Request.Form["id"] != "" ? Request.Form["id"] : "0";
            ExamPlace item = new ExamPlace();
            short shRet = SystemContext.Instance.ExamPlaceServices.GetExamPlaceByID(id, ref item);
            JsonHelper json = new JsonHelper();
            string strJson = string.Empty;
            if (shRet == ExecuteResult.OK)
            {
                json.AddItem("ID", item.ID.ToString());
                json.AddItem("PlaceName", item.PlaceName);
                json.AddItem("ParentID", item.ParentID);
                json.AddItem("ParentName", item.ParentName);
                json.AddItem("Description", item.Description);
                json.ItemOk();
            }
            strJson = json.ToEasyuiListJsonString();
            return Content(strJson);
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        public ActionResult QueryData()
        {
            List<ExamPlace> lstExamPlace = new List<ExamPlace>();
            short shRet = SystemContext.Instance.ExamPlaceServices.GetExamPlaceList("", "", "", ref lstExamPlace);
            JsonHelper json = new JsonHelper();
            string strJson = string.Empty;
            foreach (ExamPlace item in lstExamPlace)
            {
                json.AddItem("id", item.ID.ToString());
                json.AddItem("name", item.PlaceName);
                json.AddItem("pid", item.ParentID);
                //json.AddItem("NewsContent", item.NewsContent);
                json.AddItem("ParentName", item.ParentName);
                json.AddItem("Description", item.Description);
                json.ItemOk();
            }
            if (lstExamPlace.Count > 0)
            {
                strJson = json.ToEasyuiListJsonString();
            }
            else
            {
                strJson = @"[]";
            }
            // json = "{ \"rows\":[ { \"ID\":\"48\",\"NewsTitle\":\"mr\",\"NewsContent\":\"mrsoft\",\"CreateTime\":\"2013-12-23\",\"CreateUser\":\"ceshi\",\"ModifyTime\":\"2013-12-23\",\"ModifyUser\":\"ceshi\"} ],\"total\":3}";
            return Content(strJson);
        }
        #endregion

        #region 添加或修改提交的数据
        /// <summary>
        /// 添加或修改数据
        /// </summary>
        public ActionResult UpdateData()
        {
            int id = Request.Form["id"] != "" ? Convert.ToInt32(Request.Form["id"]) : 0;
            ExamPlace model = GetData(id);

            string writeMsg = "操作失败！";
            if (model != null)
            {
                if (id < 1)
                {
                    short shRet = SystemContext.Instance.ExamPlaceServices.Add(model);
                    if (shRet == ExecuteResult.OK)
                    {
                        writeMsg = "增加成功!";
                    }
                    else
                    {
                        writeMsg = "增加失败!";
                    }
                }
                else
                {
                    short shRet = SystemContext.Instance.ExamPlaceServices.Update(model);
                    if (shRet == ExecuteResult.OK)
                    {
                        writeMsg = "更新成功!";
                    }
                    else
                    {
                        writeMsg = "更新失败!";
                    }

                }
            }
            return Content(writeMsg);
        }

        /// <summary>
        /// 取得数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private ExamPlace GetData(int id)
        {
            ExamPlace model = new ExamPlace();
            if (id > 0)
            {
                SystemContext.Instance.ExamPlaceServices.GetExamPlaceByID(id.ToString(), ref model);
            }
            model.ParentID = Request.Form["ParentID"] != "" ? Request.Form["ParentID"] : "";
            model.ParentName = Request.Form["ParentName"] != "" ? Request.Form["ParentName"] : "";
            model.PlaceName = Request.Form["PlaceName"] != "" ? Request.Form["PlaceName"] : "";
            model.Description = Request.Form["Description"] != "" ? Request.Form["Description"] : "";

            return model;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        public ActionResult DelData()
        {
            string writeMsg = "删除失败！";

            string selectID = Request.Form["id"] != "" ? Request.Form["id"] : "";
            if (selectID != string.Empty && selectID != "0")
            {
                short shRet = SystemContext.Instance.ExamPlaceServices.Delete(selectID);

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
        #endregion
    }
}
