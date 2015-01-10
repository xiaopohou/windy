using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Linq;
using System.Web;


namespace Windy.Common.Libraries.Taobao
{
    public class taobaoApi
    {
        public const string VERSION = "1.0";

        private string _sessionID;

        #region 构造函数
        public taobaoApi()
        {
        }
        public taobaoApi(string sessionID)
        {
            _sessionID = sessionID;
        }
        #endregion

        #region 属性
        //获取session
        public string SessionID
        {
            get
            {
                if (string.IsNullOrEmpty(HttpContext.Current.Session["sip_sessionid"].ToString()))
                {
                    HttpContext.Current.Session["sip_sessionid"] = Guid.NewGuid().ToString();
                }
                return HttpContext.Current.Session["sip_sessionid"].ToString();
            }
        }

        #endregion

        #region 商品接口




        /// <summary>
        /// 获取某会员商品列表达式
        /// </summary>
        /// <param name="q"></param>
        /// <param name="fields"></param>
        /// <param name="page_no"></param>
        /// <param name="page_size"></param>
        /// <param name="nicks"></param>
        /// <returns></returns>
        public XmlDocument taobao_items_get(string q, string fields, int page_no, int page_size, string nicks)
        {

            Util.ParamsBuild pb = new Util.ParamsBuild(SessionID, "taobao.items.get");

            pb.AddParam("fields", fields);//"iid,delist_time"
            pb.AddParam("v", VERSION);
            if (q != string.Empty)
                pb.AddParam("q", q);
            if (page_no != 0)
                pb.AddParam("page_no", page_no);
            if (page_size != 0)
                pb.AddParam("page_size", page_size);
            // / 
            pb.AddParam("nicks", nicks);
            //if(order_by != "")
            //    pb.AddParam("order_by", order_by);
            string data = pb.GetURL();

            return Util.HttpRequest(data);
        }

        /// <summary>
        /// 获取宝贝列表的返回值
        /// </summary>
        /// <param name="q"></param>
        /// <param name="fields"></param>
        /// <param name="page_no"></param>
        /// <param name="page_size"></param>
        /// <param name="nicks"></param>
        /// <returns></returns>
        public string taobao_items_get2(string q, string fields, int page_no, int page_size, string nicks)
        {

            Util.ParamsBuild pb = new Util.ParamsBuild(SessionID, "taobao.items.get");

            pb.AddParam("fields", fields);//"iid,delist_time"
            pb.AddParam("v", VERSION);
            if (q != string.Empty)
                pb.AddParam("q", q);
            if (page_no != 0)
                pb.AddParam("page_no", page_no);
            if (page_size != 0)
                pb.AddParam("page_size", page_size);
            // / 
            pb.AddParam("nicks", nicks);
            //if(order_by != "")
            //    pb.AddParam("order_by", order_by);
            string data = pb.GetURL();

            return Util.HttpRequest2(data);
        }




        /// <summary>
        ///获取当前会话用户（必须为卖家）的出售中商品列表。支持分页。
        /// </summary>
        /// <param name="q">搜索字段</param>
        /// <param name="fields">商品对象字段</param>
        /// <param name="page_no">页码</param>
        /// <param name="page_size">页显示数量</param>
        /// <param name="has_discount">是否参与会员折扣</param>
        /// <param name="has_showcase">是否橱窗推荐</param>
        /// <returns></returns>
        public HttpWebResponse taobao_items_onsale_get(string q, string fields, int page_no, int page_size, bool has_discount, bool has_showcase, string orderby, string cid, string seller_cids)
        {

            Util.ParamsBuild pb = new Util.ParamsBuild(SessionID, "taobao.items.onsale.get");

            pb.AddParam("fields", fields);//"iid,delist_time"
            pb.AddParam("v", VERSION);
            if (q != string.Empty)
                pb.AddParam("q", q);
            if (page_no != 0)
                pb.AddParam("page_no", page_no);
            if (page_size != 0)
                pb.AddParam("page_size", page_size);
            if (has_discount)
            {
                pb.AddParam("has_discount", "true");
            }
            if (has_showcase)
            {
                pb.AddParam("has_showcase", "true");
            }
            if (orderby.Length != 0)
                pb.AddParam("order_by", orderby);
            if (cid.Length != 0)
                pb.AddParam("cid", cid);
            if (seller_cids.Length != 0)
                pb.AddParam("seller_cids", seller_cids);
            //pb.AddParam("sip_usertoken", token);
            string data = pb.GetURL();
            return Util.HttpRequest3(data);
        }







        #endregion

        #region 用户接口

        #endregion

        #region 店铺接口
        /// <summary>
        /// 
        /// </summary>

        /// <param name="fields"></param>
        /// <param name="nicks"></param>
        /// <returns></returns>
        public XmlDocument taobao_shop_get(string fields, string nicks)
        {

            Util.ParamsBuild pb = new Util.ParamsBuild(SessionID, "taobao.shop.get");

            pb.AddParam("fields", fields);//nick,sex,buyer_credit,seller_credit,location.city,created,real_name,shop_id
            pb.AddParam("v", VERSION);
            pb.AddParam("nick", nicks);

            string data = pb.GetURL();

            return Util.HttpRequest(data);
        }


        //taobao.shop.update
        public XmlDocument taobao_shop_update(string _title, string _bulletin, string _desc)
        {
            Util.ParamsBuild pb = new Util.ParamsBuild(SessionID, "taobao.shop.update");

            pb.AddParam("title", _title);//nick,sex,buyer_credit,seller_credit,location.city,created,real_name,shop_id
            pb.AddParam("v", VERSION);
            pb.AddParam("bulletin", _bulletin);
            pb.AddParam("desc", _desc);
            string data = pb.GetURL();

            return Util.HttpRequest(data);
        }
        #endregion



        #region 类目属性接口



        //taobao.itemcats.get.v2 ok
        /// <summary>
        /// 获取后台供卖家发布商品的标准商品类目(获取品牌型号)

        /// </summary>
        /// <param name="parent_cid"></param>
        /// <param name="cids"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public HttpWebResponse taobao_itemcats_get_v2(string parent_cid, string cids, string fields)
        {
            Util.ParamsBuild pb = new Util.ParamsBuild(SessionID, "taobao.itemcats.get.v2");

            if (parent_cid.Length != 0 || cids.Length != 0)
            {
                if (parent_cid != "")
                {
                    pb.AddParam("parent_cid", parent_cid);//

                }
                if (cids != "")
                {
                    pb.AddParam("cids", cids);//

                }
            }
            pb.AddParam("fields", fields);
            pb.AddParam("v", VERSION);
            string data = pb.GetURL();

            return Util.HttpRequest3(data);
        }


        #endregion
    }
}
