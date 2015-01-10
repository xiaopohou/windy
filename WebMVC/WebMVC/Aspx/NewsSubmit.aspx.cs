using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Windy.Common.Libraries;
using Windy.Service.DAL;
using Windy.Service.Data;

namespace Windy.WebMVC
{
    public partial class NewsSubmit : System.Web.UI.Page
    {
        private News _news = new News();
        protected void Page_Load(object sender, EventArgs e)
        {
            string ID = string.Empty;
            if (!IsPostBack)
            {
                InitDropDownList();
                CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
                _FileBrowser.BasePath = "../Content/JQueryTools/ckfinder/";
                _FileBrowser.SetupCKEditor(CKEditorControl1);
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    ID = Request.QueryString["id"].ToString();
                    if (_news == null)
                        _news = new News();
                    short shRet = SystemContext.Instance.NewsService.GetNewsByID(ID, ref _news);
                    if (shRet == ExecuteResult.OK)
                    {
                        this.txtNewsTitle.Text = _news.NewsTitle;
                        this.txtCategoryName.Text = _news.CategoryName;
                        this.CKEditorControl1.Text = _news.NewsContent;
                        this.hfID.Value = ID;
                    }
                }
                else
                {
                    this.hfID.Value = string.Empty;
                    this.txtNewsTitle.Text = string.Empty;
                    this.txtCategoryName.Text=string.Empty;
                    this.CKEditorControl1.Text = string.Empty;
                }
            }

        }

        private void InitDropDownList()
        {
            this.txtCategoryName.Items.Clear();
            string[] CategoryNames = SystemContext.NewsCategory.GetCategoryNames();
            foreach (string item in CategoryNames)
            {
                this.txtCategoryName.Items.Add(item);
            }
            this.txtCategoryName.SelectedIndex = 0;
        }

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            if (this._news == null)
                this._news = new News();
            
            this._news.NewsTitle = this.txtNewsTitle.Text;
            if (this.txtCategoryName.SelectedItem != null)
                this._news.CategoryName = this.txtCategoryName.SelectedItem.Text;
            else
                this._news.CategoryName = string.Empty;
            this._news.NewsContent = this.CKEditorControl1.Text;
            this._news.ModifyTime = DateTime.Now;
            short shRet = ExecuteResult.OK;
            if (string.IsNullOrEmpty(this.hfID.Value))
            {
                this._news.ID = SystemContext.Instance.CreatIDs("News");
                this._news.CreateTime = DateTime.Now;
                shRet = SystemContext.Instance.NewsService.Add(this._news);
                this.hfID.Value = this._news.ID.ToString();
            }
            else
            {
                this._news.ID = hfID.Value;
                shRet = SystemContext.Instance.NewsService.Update(this._news);
                
            }
            if(shRet==ExecuteResult.OK)
            {
                //本地缓存，供客户端浏览
                string szFilePath=string.Format("{0}/{1}.html",Server.MapPath(SystemContext.FilePath.News),this._news.ID);
                //bool result= GlobalMethods.IO.CreateFile(szFilePath);
                //if(result)
                bool result= GlobalMethods.IO.WriteFileText(szFilePath, this._news.NewsContent);
                if(!result)
                    Response.Write("<script language='javascript'>alert('提交成功,文件缓存失败！');</script>");
                else
                    Response.Write("<script language='javascript'>alert('提交成功！');</script>");

            }
               
            else
                Response.Write("<script language='javascript'>alert('提交失败！');</script>");
        }


    }
}