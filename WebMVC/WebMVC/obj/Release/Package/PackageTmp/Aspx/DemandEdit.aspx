<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DemandEdit.aspx.cs" Inherits="Windy.WebMVC.DemandEdit" %>

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
    <link href="../Content/step.css" rel="stylesheet" />
    <style>
        .body{
            font-size:12px;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <div style="height:40px;width:1000px;font-size:12px;">
            
             <div id="div_state" class="flowsteps" runat="server">

            <ol class="num4">
                <li class="done">
                    <span class=" first  ">已提交
                    </span>
                </li>
                <li class="current">
                    <span class=" step2  ">处理中
                    </span>
                </li>
                <li class="next">
                    <span class=" step3    ">
                        <s></s>
                        已解决
                    </span>
                </li>
                <li>
                    <span class=" step4   last ">
                        <s></s>
                        已确认
                    </span>
                </li>
            </ol>

        </div>
        </div>
        <%-- 状态进度条 --%>
       

        <div class="easyui-panel" style="width: 1000px; height: 390px; padding: 0px;">
            <div class="easyui-layout" data-options="fit:true">

                <div data-options="region:'west',split:true" style="width: 500px; height: 255px; padding: 0px">

                    <table style="font-size:12px;">
                        <tr>
                            <td>标题:<asp:TextBox ID="txtTitle" class="easyui-textbox" runat="server"></asp:TextBox></td>
                            <td>提交人:<asp:DropDownList ID="txtCreater" class="easyui-textbox" runat="server"></asp:DropDownList></td>

                        </tr>
                        <tr>
                            <td>提交时间:<asp:TextBox ID="txtSubmitTime" runat="server" class="easyui-datetimebox"></asp:TextBox></td>

                            <td>项目名:<asp:DropDownList ID="ddlProduct" runat="server"></asp:DropDownList></td>

                        </tr>

                        <tr>
                            <td colspan="2">
                                <CKEditor:CKEditorControl
                                    ID="CKEditorControl1" Width="450px" Height="200px" runat="server"  ToolbarStartupExpanded="False"></CKEditor:CKEditorControl>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">附件：<asp:FileUpload ID="FileAttach" runat="server" />
                                <asp:HyperLink ID="linkFileAttach" runat="server"></asp:HyperLink>
                            </td>
                        </tr>
                    </table>


                </div>

                <div data-options="region:'center'" style="padding: 0px; width: 500px">
                    <%-- 解决方案 --%>


                    <table style="font-size:12px;">
                        <tr>
                            <td>负责人:<asp:DropDownList ID="txtOwener" class="easyui-textbox" runat="server"></asp:DropDownList></td>

                            <td>修复工作量：
                                <asp:TextBox ID="txtExpense" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>

                            <td>解决时间：
                               <asp:TextBox ID="txtSoluteTime" runat="server" class="easyui-datetimebox"></asp:TextBox>
                            </td>
                            <td>状态：
                                <asp:DropDownList ID="ddlState" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <CKEditor:CKEditorControl
                                    ToolbarStartupExpanded="False" ID="CKEditorControl2" Width="450px" Height="200px" runat="server"></CKEditor:CKEditorControl>
                            </td>
                        </tr>

                    </table>



                </div>
                <div data-options="region:'south'" style="padding: 0px;">
                    <div id="edit-buttons">
                        <asp:LinkButton ID="lbtnSubmit" class="easyui-linkbutton" runat="server" OnClick="lbtnSubmit_Click">提交</asp:LinkButton>
                         <asp:LinkButton ID="lbtnDelete" class="easyui-linkbutton" runat="server" OnClick="lbtnDelete_Click">撤销</asp:LinkButton>
                       
                    </div>
                </div>
            </div>
        </div>


        <asp:HiddenField ID="hfState" runat="server" />
        <asp:HiddenField ID="hfID" runat="server" />
    </form>
    <script>
        $(function () {
            //$.post(location.href, { "action": "GetStateMap", }, function (data) {
            //     $("#div_state").html(data);
            //});
        });
        function showDetail(id) {
            alert(id);
        }
        function closedialog() {

            window.parent.d_close();
            return false;
        }
    </script>
</body>
</html>
