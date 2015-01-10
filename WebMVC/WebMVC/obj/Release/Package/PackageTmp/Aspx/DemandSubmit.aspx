<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DemandSubmit.aspx.cs" Inherits="Windy.WebMVC.DemandSubmit" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="~/Content/JQueryTools/EasyUI/themes/metro/easyui.css" rel="stylesheet" />
     
    <link href="../Content/JQueryTools/EasyUI/themes/icon.css" rel="stylesheet" />
    <link href="../Content/admin.css" rel="stylesheet" />
    <script src="../Content/JQueryTools/EasyUI/jquery.min.js"></script>
    <script src="../Content/JQueryTools/EasyUI/jquery.easyui.min.js"></script>
    <script src="../Content/JQueryTools/ckeditor/ckeditor.js"></script>
    <script src="../Content/JQueryTools/EasyUI/locale/easyui-lang-zh_CN.js"></script>
</head>
<body>
   
    <form id="form1" runat="server">
         
        <div id="div_submit" style="float:left;width:750px;">
             <div style="color:red;font-size:large">
     提交您使用系统过程中的问题，或者提出对系统改进有帮助的需求。<br />请注意关注问题的处理状态.....
        </div>
            <table>
                <tr>
                    <td>标题:<asp:TextBox ID="txtNewsTitle" runat="server" Width="300px"></asp:TextBox></td>
                    
                </tr>
                <tr>
                    <td>项目名:<asp:DropDownList ID="ddlProduct" runat="server"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>
                        <CKEditor:CKEditorControl ID="CKEditorControl1"  Height="160px" runat="server"></CKEditor:CKEditorControl>
                    </td>
                </tr>
                <tr>
                    <td>
                        附件：<asp:FileUpload ID="FileAttach" runat="server" />
                    </td>
                </tr>
            </table>
             <div id="edit-buttons">
                 <asp:LinkButton ID="lbtnSubmit" class="easyui-linkbutton" runat="server" OnClick="lbtnSubmit_Click">提交</asp:LinkButton>
                 <a href="javascript:;"
                    class="easyui-linkbutton" onclick="closedialog()">取消</a>
            </div>
            
        </div>
        <%-- 问题列表，只显示前二十条 --%>
        <div class="easyui-panel"  style="float:right;height:450px;width:330px;">
            <span style="color:red; font-size:large">历史提交记录</span>
            <ul id="list" >
                <li ><a   onclick="showDetail(1)">问题一</a> 状态</li> 
                <li ><a   onclick="showDetail(2)">问题一</a> 状态</li> 
                <li ><a  onclick="showDetail(3)">问题一</a> 状态</li> 

            </ul>
        </div>
    </form>
    <script>
        $(function () {
            $.post(location.href, { "action": "GetDemandList", }, function (data) {
                $("#list").html(data);
            });
        });
        function showDetail(id) {
            location.href("DemandEdit.aspx?id=" + id,"");
        }
        function closedialog()
        {
           
            window.parent.d_close();
            return false;
        }
    </script>
</body>
</html>
