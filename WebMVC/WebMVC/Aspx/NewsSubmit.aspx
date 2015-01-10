<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsSubmit.aspx.cs" Inherits="Windy.WebMVC.NewsSubmit" %>

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
<body style="font-size:12px;"> 
    <form id="form1" runat="server">
            <table>
                <tr>
                    <td>标题:<asp:TextBox ID="txtNewsTitle" runat="server"></asp:TextBox></td>

                </tr>
                <tr>
                    <td>类别:<asp:DropDownList ID="txtCategoryName" runat="server"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>
                        <CKEditor:CKEditorControl ID="CKEditorControl1" runat="server"></CKEditor:CKEditorControl>
                    </td>
                </tr>
            </table>
            <div id="edit-buttons">
                <asp:LinkButton ID="lbtnSubmit" class="easyui-linkbutton" runat="server" OnClick="lbtnSubmit_Click">提交</asp:LinkButton>
                <a href="javascript:;"
                    class="easyui-linkbutton" onclick="closedialog()">取消</a>
            </div>
        <asp:HiddenField ID="hfID" runat="server" />
    </form>
    <script>
        function closedialog() {

            window.parent.d_close();
            return false;
        }
    </script>
</body>
</html>
