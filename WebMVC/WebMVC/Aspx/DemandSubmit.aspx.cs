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
    public partial class DemandSubmit : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            
            string ID = string.Empty;
            if (!IsPostBack)
            {
                InitDropDownList();
                CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
                _FileBrowser.BasePath = "../Content/JQueryTools/ckfinder/";
                _FileBrowser.SetupCKEditor(CKEditorControl1);
                
            }
            string action = string.Empty;
            if (Request.Form["action"] != null)
            {
                action = Request.Form["action"].ToString();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["action"]))
                action = Request.QueryString["action"];
            if (action != string.Empty)
            {
                switch (action)
                {
                    case "GetDemandList":
                        GetDemandList();
                        break;
                    default:
                        break;
                }
            }

        }

        private void GetDemandList()
        {
            Employee CurrentEmployee = Session["CurrentEmployee"] as Employee;
               string writeMsg = "";
            if (CurrentEmployee != null)
            {
                List<Demand> lstDemand = new List<Demand>();
                short shRet = SystemContext.Instance.DemandService.GetDemandPageList(20, 1, CurrentEmployee.EmpNo, ref lstDemand);
                foreach (Demand item in lstDemand)
                {
                    writeMsg = writeMsg + string.Format(" <li ><a href=\"DemandEdit.aspx?id={0}\"  target=\"_blank\">{1}</a> {2}</li>"
                        , item.ID, item.Title, item.State);

                }
            }
            
            Response.Clear();
            Response.Write(writeMsg);
            Response.End();
        }

        private void InitDropDownList()
        {
            this.ddlProduct.Items.Clear();
            string[] ProductNames = SystemContext.ProductName.GetProductNames();
            foreach (string item in ProductNames)
            {
                this.ddlProduct.Items.Add(item);
            }
            this.ddlProduct.SelectedIndex = 0;
        }

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            Employee employee = Session["CurrentEmployee"] as Employee;
            if (employee == null)
            {
                Response.Write("<script language='javascript'>alert('系统未登录，请重新登陆后提交！');</script>");
                return;
            }
            Demand demand = new Demand();
            if (this.FileAttach.HasFile)
            {
                try
                {
                    this.FileAttach.SaveAs(Server.MapPath(SystemContext.FilePath.Demand) + System.IO.Path.GetFileName(this.FileAttach.FileName));
                    demand.FileAttach = string.Format("{0}{1}",SystemContext.FilePath.Demand.Replace("~",""), this.FileAttach.FileName);
                }
                catch (Exception)
                {

                    Response.Write("<script language='javascript'>alert('附加上传不成功！');</script>");
                    return;
                }
               
            }

            demand.ID = SystemContext.Instance.CreatIDs("Demand");
            demand.Product = this.ddlProduct.Text;
            demand.Creater = employee.EmpNo;
            demand.SubmitTime = DateTime.Now;
            demand.State = SystemContext.DemandState.Submit;
            demand.Question = this.CKEditorControl1.Text;
            demand.Title = this.txtNewsTitle.Text;
            short shRet = SystemContext.Instance.DemandService.Add(demand);
            string msg = ExecuteResult.GetResultMessage(shRet);
            Response.Write("<script language='javascript'>alert('" + msg + "！');</script>");
        }


    }
}