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
    public partial class DemandEdit : System.Web.UI.Page
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
                _FileBrowser.SetupCKEditor(CKEditorControl2);
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    ID = Request.QueryString["id"].ToString();
                 
                     Demand   demand = new Demand();
                    short shRet = SystemContext.Instance.DemandService.GetDemandByID(ID, ref demand);
                    if (shRet == ExecuteResult.OK)
                    {
                        this.ddlProduct.Text = demand.Product;
                        this.txtCreater.Text = demand.Creater;
                        this.txtSubmitTime.Text = demand.SubmitTime.ToString();
                        this.txtOwener.Text = demand.Owener;
                        this.txtSoluteTime.Text = demand.SoluteTime.ToString();
                        this.ddlState.Text = demand.State;
                        this.CKEditorControl1.Text = demand.Question;
                        this.txtTitle.Text = demand.Title;
                        this.linkFileAttach.NavigateUrl=demand.FileAttach;
                        this.linkFileAttach.Text = demand.FileAttach;

                        this.txtExpense.Text = demand.Expense;
                        this.CKEditorControl2.Text = demand.Solution;
                        this.hfID.Value = ID;
                        this.hfState.Value = demand.State;
                    }
                }
                else
                {
                    GetStateMap();
                    this.CKEditorControl1.Text = string.Empty;
                    this.txtOwener.Text = string.Empty;
                }

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
                    case "GetStateMap":
                        GetStateMap();
                        break;
                    default:
                        break;
                }
            }

        }

        private void GetStateMap()
        {
            Employee CurrentEmployee = Session["CurrentEmployee"] as Employee;
               string writeMsg = "";
               string template = "<ol class=\"num4\"><li class=\"{0}\"> <span class=\" first  \"> 已提交</span></li>";
               template += "<li class=\"{1}\"> <span class=\" step2  \"> <s></s>处理中</span></li>";
               template += "<li class=\"{2}\"> <span class=\" step3  \"> <s></s>已解决</span></li>";
               template += "<li class=\"{3}\"> <span class=\" step4  \"> <s></s>已确认</span></li></ol>";
            if (CurrentEmployee != null)
            {
                switch (this.hfState.Value)
	            {
                    case "已提交":
                        writeMsg=string.Format(template,"current","next","","");
                        break;
                    case "处理中":
                        writeMsg = string.Format(template, "done", "current", "next", "");
                        break;
                    case "已解决":
                        writeMsg = string.Format(template, "done", "done", "current", "next");
                        break;
                    case "已确认":
                        writeMsg = string.Format(template, "done", "done", "done", "current");
                        break;
                    default:
                        writeMsg = string.Format(template, "", "", "", "");
                        break;
	            }
            }
            this.div_state.InnerHtml = writeMsg;
            
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

            this.ddlState.Items.Clear();
            string[] DemandStates = SystemContext.DemandState.GetDemandStates();
            foreach (string item in DemandStates)
            {
                this.ddlState.Items.Add(item);
            }
            this.ddlState.SelectedIndex = 0;
            //用户
            List<Employee> lstEmployee=new List<Employee>();
            short shRet = SystemContext.Instance.EmployeeService.GetEmployeeList("", "", "", ref lstEmployee);
            this.txtCreater.Items.Clear();
            this.txtOwener.Items.Clear();
            foreach (Employee item in lstEmployee)
            {
                this.txtCreater.Items.Add(item.EmpNo);
                this.txtOwener.Items.Add(item.EmpNo);
            }
        }

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            Employee employee = Session["CurrentEmployee"] as Employee;
            Demand demand = new Demand();
            if (employee == null)
            {
                Response.Write("<script language='javascript'>alert('系统未登录，请重新登陆后提交！');</script>");
                return;
            }


            if (this.FileAttach.HasFile)
            {
                try
                {
                    this.FileAttach.SaveAs(Server.MapPath(SystemContext.FilePath.Demand) + System.IO.Path.GetFileName(this.FileAttach.FileName));
                    demand.FileAttach = string.Format("{0}{1}", SystemContext.FilePath.Demand.Replace("~", ""), this.FileAttach.FileName);
                }
                catch (Exception)
                {

                    Response.Write("<script language='javascript'>alert('附加上传不成功！');</script>");
                    return;
                }

            }
            else
            {
                demand.FileAttach = this.linkFileAttach.Text;
            }
            if (string.IsNullOrEmpty(this.txtCreater.Text))
                demand.Creater = employee.EmpNo;
            else
                demand.Creater = this.txtCreater.Text;
            demand.Product = this.ddlProduct.Text;
            if (!string.IsNullOrEmpty(this.txtSubmitTime.Text.Trim()))
                demand.SubmitTime = DateTime.Parse(this.txtSubmitTime.Text.Trim());
            demand.Owener = this.txtOwener.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtSoluteTime.Text.Trim()))
                demand.SoluteTime = DateTime.Parse(this.txtSoluteTime.Text);
            demand.State = this.ddlState.Text;
            demand.Question = this.CKEditorControl1.Text;
            demand.Title = this.txtTitle.Text;
            demand.Expense = this.txtExpense.Text;
            demand.Solution = this.CKEditorControl2.Text;
            this.hfState.Value = demand.State;
            short shRet;
            if (string.IsNullOrEmpty(this.hfID.Value))
            {
                demand.SubmitTime= DateTime.Now;
                demand.ID = SystemContext.Instance.CreatIDs("Demand");
                shRet = SystemContext.Instance.DemandService.Add(demand);
                this.hfID.Value = demand.ID.ToString();
                this.hfState.Value = demand.State;
            }
            else
            {
                demand.ID = hfID.Value;
                shRet = SystemContext.Instance.DemandService.Update(demand);

            }
            string msg = ExecuteResult.GetResultMessage(shRet);
            Response.Write("<script language='javascript'>alert('" + msg + "！');</script>");
            if(shRet==ExecuteResult.OK)
            {
                this.linkFileAttach.Text = demand.FileAttach;
                this.linkFileAttach.NavigateUrl = demand.FileAttach;
                GetStateMap();
            }
            
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.hfID.Value))
            {
                Response.Write("<script language='javascript'>alert('未添加新需求，无法撤销');</script>");
                return;
            }
            string ids = "'" + this.hfID.Value + "'";
            short shRet = SystemContext.Instance.DemandService.Delete(ids);
            Response.Write("<script language='javascript'>alert('"+ExecuteResult.GetResultMessage(shRet)+"');</script>");
            if (shRet == ExecuteResult.OK)
            {
                this.hfID.Value = string.Empty;
            }
            return;
        }


    }
}