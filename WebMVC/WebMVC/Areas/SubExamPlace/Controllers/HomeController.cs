using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Windy.Common.Libraries;
using Windy.Service.DAL;
using Windy.Service.Data;

namespace Windy.WebMVC.Areas.SubExamPlace.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /SubExamPlace/Home/

        public ActionResult Index()
        {
            if (Session["CurrentUser"] == null)
                return RedirectToAction("Login");
            return RedirectToAction("UserInfo");
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Users users)
        { //验证账号密码
            short shRet = SystemContext.Instance.UsersService.Exists(users.Tel, users.PassWord, ref users);

            if (shRet == ExecuteResult.OK)
            {
                Session["CurrentUser"] = users;
                return RedirectToAction("UserInfo");
            }
            else
            {
                string error = string.Empty;
                if (shRet == ExecuteResult.RES_NO_FOUND)
                    error = "账号或密码错误";
                else
                    error = ExecuteResult.GetResultMessage(shRet);
                ModelState.AddModelError("Message", error);
                return View();
            }
        }
        public ActionResult examplace()
        {
            if (Session["CurrentUser"] == null)
                return RedirectToAction("Login");
            return View();
        }
        [HttpPost]
        public ActionResult examplace(FormCollection collection)
        {
            string szchkOther = collection["chkOther"];
            string szPlace = collection["Place"];
            string szExamPlace = collection["ddlSchool"];

            if (Session["CurrentUser"] == null)
                return RedirectToAction("Login");
            Users curUser = Session["CurrentUser"] as Users;
            if (szchkOther == "on")
            {
                curUser.ExamPlace = szPlace;
            }
            else
                curUser.ExamPlace = szExamPlace;
            short shRet = SystemContext.Instance.UsersService.Update(curUser);
            if (shRet != ExecuteResult.OK)
            {
                ViewData["chkOther"] = szchkOther;
                ViewData["Place"] = szPlace;
                ViewData["ddlSchool"] = szExamPlace;
                Response.Write("<script>alert('考点提交失败!')</script>");
                return View();
            }
            Response.Write("<script>alert('提交考点成功!')</script>");
            Session["CurrentUser"] = curUser;
            return RedirectToAction("UserInfo");
        }
        public ActionResult UserInfo()
        {
            if (Session["CurrentUser"] == null)
                return RedirectToAction("Login");
            Users curUser=Session["CurrentUser"] as Users;
            //查询室友
            if(string.IsNullOrEmpty(curUser.Room)||string.IsNullOrEmpty(curUser.Hotel))
                return View();
            List<Users> roomies=new List<Users>();
            short shRet = SystemContext.Instance.UsersService.GetUsersListByRoom(curUser.Room, curUser.Hotel, ref roomies);
            if (shRet == ExecuteResult.OK)
                ViewData["roomies"] = roomies;
            return View();
        }
        public ActionResult RoomieInfo(string id)
        {
            if (Session["CurrentUser"] == null)
                return RedirectToAction("Login");
            if (string.IsNullOrEmpty(id))
                return View();
            Users user = new Users();
            short shRet = SystemContext.Instance.UsersService.GetUsersByID(id,  ref user);
            if (shRet == ExecuteResult.OK)
                ViewData["roomie"] = user;
            return View();
        }
        [HttpPost]
        public ActionResult UserInfo(FormCollection collection)
        {
            if (Session["CurrentUser"] == null)
                return RedirectToAction("Login");
            string szName = collection["txtName"];
            string szGender = collection["txtGender"];
            string szSchool = collection["txtSchool"];
            string szPayPlace = collection["txtPayPlace"];
            string szTemplate = collection["txtTemplate"];
            string szExamSchool = collection["txtExamSchool"];
            string szExamPlace = collection["txtExamPlace"];
            string szExceptRoomie = collection["txtExceptRoomie"];
            string szBaks = collection["txtBaks"];
            Users curUser = Session["CurrentUser"] as Users;
            curUser.Name = szName;
            curUser.Gender = szGender;
            curUser.School = szSchool;
            curUser.PayPlace = szPayPlace;
            curUser.Template = szTemplate;
            curUser.ExamSchool = szExamSchool;
            curUser.ExceptRoomie = szExceptRoomie;
            curUser.ExamPlace = szExamPlace;
            curUser.Bak = szBaks;
            short shRet = SystemContext.Instance.UsersService.Update(curUser);
            if (shRet != ExecuteResult.OK)
            {
                Response.Write("<script>alert('考生个人信息修改失败!')</script>");
                return View();
            }
            Response.Write("<script>alert('考生个人信息修改成功!')</script>");
            Session["CurrentUser"] = curUser;
            return View();
        }
        public ActionResult GetProvinceList()
        {
            List<ExamPlace> lstExamPlace = new List<ExamPlace>();
            //short shRet = SystemContext.Instance.ExamPlaceServices.GetExamPlaceList("", SystemContext.PlaceType.PROVINCE, "", ref lstExamPlace);
            foreach (ExamPlace item in SystemContext.Instance.CacheExamPlace)
            {
                if (item.PlaceType == SystemContext.PlaceType.PROVINCE)
                {
                    lstExamPlace.Add(item);
                }
            }

            return Json(lstExamPlace, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCityList(int id)
        {
            if (!Request.IsAjaxRequest())
            {
                return Content("请不要非法方法,这是不道德的行为！");
            }
            List<ExamPlace> lstExamPlace = new List<ExamPlace>();
            foreach (ExamPlace item in SystemContext.Instance.CacheExamPlace)
            {
                if (item.PlaceType == SystemContext.PlaceType.CITY
                    && id.ToString()==item.ParentID)
                {
                    lstExamPlace.Add(item);
                }
            }
            //short shRet = SystemContext.Instance.ExamPlaceServices.GetExamPlaceList("", SystemContext.PlaceType.CITY, id.ToString(), ref lstExamPlace);
            
            return Json(lstExamPlace, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取某［城市］的所有［市区］数据
        /// </summary>
        public ActionResult GetSchoolList(int id)
        {
            if (!Request.IsAjaxRequest())
            {
                return Content("请不要非法方法,这是不道德的行为！");
            }

            List<ExamPlace> lstExamPlace = new List<ExamPlace>();
            //short shRet = SystemContext.Instance.ExamPlaceServices.GetExamPlaceList("", SystemContext.PlaceType.SCHOOL, id.ToString(), ref lstExamPlace);
            foreach (ExamPlace item in SystemContext.Instance.CacheExamPlace)
            {
                if (item.PlaceType == SystemContext.PlaceType.SCHOOL
                    && id.ToString() == item.ParentID)
                {
                    lstExamPlace.Add(item);
                }
            }
            return Json(lstExamPlace, JsonRequestBehavior.AllowGet);
        }

    }
}
