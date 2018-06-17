<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenCodeList.aspx.cs" Inherits="SelectPage_GenCodeList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.ClosePopupCtr(); }
        }
        document.onkeydown = keydown;
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
                    <dxe:ASPxTextBox ID="txt_Name" Width="1" runat="server" ReadOnly="true" Border-BorderStyle="None">
                    </dxe:ASPxTextBox> 
                    <%--<dxe:ASPxButton ID="ASPxButton1" type="hidden" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                        <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
                    </dxe:ASPxButton>--%>
        <wilson:DataSource ID="dsGenCode" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXGenCode" KeyMember="Code" FilterExpression="1=1" />
        <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%" DataSourceID="dsGenCode"
             KeyFieldName="Code" AutoGenerateColumns="False">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                    <DataItemTemplate>
                        <a onclick='parent.PutValue("<%# Eval("Code") %>","<%# Eval("Description") %>");'>Select</a>
                    </DataItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="1" SortIndex="0" SortOrder="Ascending" Width="100">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
       </Columns>
        </dxwgv:ASPxGridView>           
    </div>
    </form>
</body>
</html>
