using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Windy.Common.Libraries.Taobao
{
    public class Util
    {

        #region 测试用 返回字符串结果集
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="image"></param>
        /// <param name="uploadfile"></param>
        /// <param name="fileFormName"></param>
        /// <param name="contenttype"></param>
        /// <returns></returns>
        public static HttpWebResponse HttpRequest5(string data, byte[] image, string uploadfile, string fileFormName, string contenttype)
        {
            ////ASCIIEncoding encoding = new ASCIIEncoding();
            //byte[] postdata = System.Text.Encoding.UTF8.GetBytes(data);//所有要传参数拼装
            //// Prepare web request...
            ////目前阿里软件的服务集成平台（SIP）的接口测试地址是：http://sipdev.alisoft.com/sip/rest，生产环境地址是：http://sip.alisoft.com/sip/rest,
            ////这里使用测试接口先，到正式上线时需要做切换
            //string url = System.Configuration.ConfigurationManager.AppSettings["APPURL"];
            //HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            //myRequest.Method = "POST";
            //myRequest.ContentType = "application/x-www-form-urlencoded";
            //myRequest.ContentLength = postdata.Length;
            //Stream newStream = myRequest.GetRequestStream();
            //// Send the data.
            //newStream.Write(postdata, 0, postdata.Length);
            //newStream.Close();
            //// Get response
            //HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            //return myResponse;

            string url = System.Configuration.ConfigurationManager.AppSettings["APPURL"];

            Uri uri = new Uri(url + data);


            string boundary = "----------" + DateTime.Now.Ticks.ToString("x");
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(uri);
            webrequest.ContentType = "text/plain";
            webrequest.Method = "POST";

            // Build up the post message header
            if (uploadfile != null && uploadfile.Length > 0)
            {
                webrequest.ContentType = "multipart/form-data; boundary=" + boundary;
                StringBuilder sb = new StringBuilder();
                sb.Append("--");
                sb.Append(boundary);
                sb.Append("\r\n");
                sb.Append("Content-Disposition: form-data; name=\"");
                sb.Append(fileFormName);
                sb.Append("\"; filename=\"");
                sb.Append("test.jpg");
                sb.Append("\"");
                sb.Append("\r\n");
                sb.Append("Content-Type: ");
                sb.Append(contenttype);
                sb.Append("\r\n");
                sb.Append("\r\n");

                string postHeader = sb.ToString();
                byte[] postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);

                // Build the trailing boundary string as a byte array

                // ensuring the boundary appears on a line by itself

                byte[] boundaryBytes =
                       Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");


                long length = postHeaderBytes.Length + image.Length +
                                                       boundaryBytes.Length;
                webrequest.ContentLength = length;

                Stream requestStream = webrequest.GetRequestStream();

                // Write out our post header

                requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);


                // Write out the file contents
                requestStream.Write(image, 0, image.Length);

                // Write out the trailing boundary

                requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
            }
            HttpWebResponse responce = (HttpWebResponse)webrequest.GetResponse();
            //Stream s = responce.GetResponseStream();
            //StreamReader sr = new StreamReader(s);

            //return sr.ReadToEnd();
            return responce;

        }

        /// 
        /// <summary>
        /// 上传图片时用
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static HttpWebResponse HttpRequest4(string data)
        {
            //ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] postdata = System.Text.Encoding.UTF8.GetBytes(data);//所有要传参数拼装
            // Prepare web request...
            //目前阿里软件的服务集成平台（SIP）的接口测试地址是：http://sipdev.alisoft.com/sip/rest，生产环境地址是：http://sip.alisoft.com/sip/rest,
            //这里使用测试接口先，到正式上线时需要做切换
            string url = System.Configuration.ConfigurationManager.AppSettings["APPURL"];
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = postdata.Length;
            Stream newStream = myRequest.GetRequestStream();
            // Send the data.
            newStream.Write(postdata, 0, postdata.Length);
            newStream.Close();
            // Get response
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            return myResponse;


        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static HttpWebResponse HttpRequest3(string data)
        {
            //ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] postdata = System.Text.Encoding.UTF8.GetBytes(data);//所有要传参数拼装
            // Prepare web request...
            //目前阿里软件的服务集成平台（SIP）的接口测试地址是：http://sipdev.alisoft.com/sip/rest，生产环境地址是：http://sip.alisoft.com/sip/rest,
            //这里使用测试接口先，到正式上线时需要做切换
            string url = System.Configuration.ConfigurationManager.AppSettings["APPURL"];
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = postdata.Length;
            Stream newStream = myRequest.GetRequestStream();
            // Send the data.
            newStream.Write(postdata, 0, postdata.Length);
            newStream.Close();
            // Get response
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();

            return myResponse;


        }


        /// <summary>
        /// 测试用 返回字符串结果集
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string HttpRequest2(string data)
        {
            //ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] postdata = System.Text.Encoding.UTF8.GetBytes(data);//所有要传参数拼装
            // Prepare web request...
            //目前阿里软件的服务集成平台（SIP）的接口测试地址是：http://sipdev.alisoft.com/sip/rest，生产环境地址是：http://sip.alisoft.com/sip/rest,
            //这里使用测试接口先，到正式上线时需要做切换
            string url = System.Configuration.ConfigurationManager.AppSettings["APPURL"];
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = postdata.Length;
            Stream newStream = myRequest.GetRequestStream();
            // Send the data.
            newStream.Write(postdata, 0, postdata.Length);
            newStream.Close();
            // Get response
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            string content = reader.ReadToEnd();
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.LoadXml(content);
            //XmlNode node = xmlDoc.SelectSingleNode("/error_rsp/code");
            //if (node != null && node.InnerText != string.Empty)
            //{

            //    throw new ApiException(node.InnerText, xmlDoc.SelectSingleNode("/error_rsp/msg").InnerText);
            //}
            return content;

        }

        #endregion


        #region 正式用 返回Xml结果集

        public static XmlDocument HttpRequest(string data)
        {
            //ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] postdata = System.Text.Encoding.UTF8.GetBytes(data);//所有要传参数拼装
            // Prepare web request...
            //目前阿里软件的服务集成平台（SIP）的接口测试地址是：http://sipdev.alisoft.com/sip/rest，生产环境地址是：http://sip.alisoft.com/sip/rest,
            //这里使用测试接口先，到正式上线时需要做切换
            string url = System.Configuration.ConfigurationManager.AppSettings["APPURL"];
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = postdata.Length;
            Stream newStream = myRequest.GetRequestStream();
            // Send the data.
            newStream.Write(postdata, 0, postdata.Length);
            newStream.Close();
            // Get response
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            string content = reader.ReadToEnd();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(content);
            XmlNode node = xmlDoc.SelectSingleNode("/error_rsp/code");
            if (node != null && node.InnerText != string.Empty)
            {

                // throw new ApiException(node.InnerText, xmlDoc.SelectSingleNode("/error_rsp/msg").InnerText);
            }
            return xmlDoc;

        }

        #endregion


        public static string HttpRequest(string data, string xPath)
        {
            XmlDocument doc = HttpRequest(data);
            return doc.SelectSingleNode(xPath).InnerText;
        }



        #region MD5加密
        /// <summary>
        /// md5加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string MD5(string data)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(data))).Replace("-", "");
        }


        #endregion

        #region 参数创建类
        public class ParamsBuild
        {
            string _code { get; set; }
            SortedList _mySL = new SortedList();

            public ParamsBuild()
                : this(Util.GetCode)
            {

            }
            public ParamsBuild(string code)
            {
                _code = code;
            }
            public ParamsBuild(System.Web.HttpContext content, string apiName)
                : this(content.Session.SessionID, apiName)
            {

            }
            public ParamsBuild(string sip_sessionid, string apiName)
                : this(Util.GetCode)
            {
                AddParam("sip_appkey", Util.GetAppID);
                AddParam("sip_apiname", apiName);
                AddParam("sip_timestamp", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                AddParam("sip_sessionid", sip_sessionid);
            }
            public void AddParam(string name, string value)
            {
                _mySL.Add(name, value);
            }
            public void AddParam(string name, int value)
            {
                _mySL.Add(name, value);
            }
            public string GetURL()
            {
                StringBuilder orgin = new StringBuilder();
                orgin.Append(_code); //将安全编码放字符串首位
                //对list里的参数进行拼装，参数名+参数值，按自然排序，即所有参数字母排序
                StringBuilder _url = new StringBuilder();
                foreach (DictionaryEntry Item in _mySL)
                {
                    // ListItem newListItem = new ListItem();
                    orgin.Append(Item.Key.ToString());
                    if (Item.Value != null)
                    {
                        orgin.Append(Item.Value.ToString());
                    }
                    _url.AppendFormat("{0}={1}&", Item.Key, Item.Value);

                }
                _url.AppendFormat("sip_sign={0}", Util.MD5(orgin.ToString()));
                return _url.ToString();
            }


            /// <summary>
            /// 获取签名
            /// </summary>
            /// <returns></returns>
            public string GetSign()
            {
                StringBuilder orgin = new StringBuilder();
                orgin.Append(Util.GetCode); //将安全编码放字符串首位
                //对list里的参数进行拼装，参数名+参数值，按自然排序，即所有参数字母排序
                StringBuilder _url = new StringBuilder();
                foreach (DictionaryEntry Item in _mySL)
                {
                    // ListItem newListItem = new ListItem();
                    orgin.Append(Item.Key.ToString());
                    if (Item.Value != null)
                    {
                        orgin.Append(Item.Value.ToString());
                    }
                }
                return Util.MD5(orgin.ToString());

            }
        }

        #endregion

        #region 获取APPID
        public static string GetAppID
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["AppID"];
            }
        }

        #endregion

        #region 获取APPcode

        public static string GetCode
        {
            get
            {
                //软件注册时获得
                return System.Configuration.ConfigurationManager.AppSettings["AppCode"];
            }
        }

        #endregion

        #region 根据软件互联平台的用户ID，获取用户的阿里巴巴中文站（或淘宝）帐号。

        /// <summary>
        /// 根据软件互联平台的用户ID，获取用户的阿里巴巴中文站（或淘宝）帐号。
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appUserId"></param>
        /// <returns></returns>
        public static string getDomainLoginId(string appId, string appUserId)
        {
            string code = GetCode;
            ParamsBuild pb = new ParamsBuild(code);
            pb.AddParam("sip_appkey", appId);
            pb.AddParam("sip_apiname", "alisoft.udb.getDomainLoginId");
            pb.AddParam("sip_timestamp", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            pb.AddParam("domainid", "1");
            pb.AddParam("userid", appUserId);
            string data = pb.GetURL();



            //解析接口返回值，这里选用XML格式的解析，接口默认返回是XML格式
            XmlDocument xmlDoc = Util.HttpRequest(data);

            XmlNode xn = xmlDoc.SelectSingleNode("StringResult/code");
            XmlNode xn2 = xmlDoc.SelectSingleNode("StringResult/result");
            if (xn.InnerText != "0")
            {
                return appUserId;
            }
            else
            {
                return xn2.InnerText;
            }
        }
        #endregion
    }
}
