<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectAddress.aspx.cs" Inherits="SelectPage_SelectAddress" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.ClosePopupCtr(); }
        }
        document.onkeydown = keydown;

        var lastType = null;
        function OnTypeChanged(cbb_Type) {
            if (cbb_Location.InCallback())
                lastType = cbb_Type.GetValue().toString();
            else
                cbb_Location.PerformCallback(cbb_Type.GetValue().toString());
        }
        function OnEndCallback(s, e) {
            if (lastType) {
                cbb_Location.PerformCallback(lastType);
                lastType = null;
            }
        }

        function return_values(par, par1) {
            var p = "";
            if (par) {
                p = par.replace(/\r\n/g, "<BR>");
                p = p.replace(/\n/g, "<BR>");
            }
            var p1 = "";
            if (par1) {
                p1 = par1.replace(/\r\n/g, "<BR>");
                p1 = p1.replace(/\n/g, "<BR>");
            }
            parent.PutValue(p, p1);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
                <wilson:DataSource ID="dsUom" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXUom" KeyMember="Id" FilterExpression="1=0" />
        <table>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="Code" />
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txt_Code" Width="80" runat="server">
                    </dxe:ASPxTextBox>
                </td>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="Party" />
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txt_party" Width="80" runat="server">
                    </dxe:ASPxTextBox>
                </td>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Type" />
                </td>
                <td>
                    <dxe:ASPxComboBox ID="cbb_Type" runat="server" Width="80" OnCustomJSProperties="cbb_Type_CustomJSProperties" AutoPostBack="false">
                        <Items>
                            <dxe:ListEditItem Text="Location" Value="Location" />
                            <dxe:ListEditItem Text="Owner" Value="Owner" />
                        </Items>
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { OnTypeChanged(s); }" />
                    </dxe:ASPxComboBox>
                </td>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Location" />
                </td>
                <td>
                    <dxe:ASPxComboBox ID="cbb_Location" Width="90" runat="server" ClientInstanceName="cbb_Location"  OnCallback="cbb_Location_Callback"  EnableIncrementalFiltering="true" TextField="Code" ValueField="Code" ValueType="System.String">
                        <ClientSideEvents EndCallback=" OnEndCallback"/>
                    </dxe:ASPxComboBox>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_Sch" runat="server" Text="Retrieve"
                        OnClick="btn_Sch_Click">
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%"
            KeyFieldName="Id"
            AutoGenerateColumns="False">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                    <DataItemTemplate>
                        <a onclick='parent.PutValue("<%# Eval("Address") %>","<%# Eval("Address1") %>");'>Select</a>
                        <%--<a onclick='return_values("<%# Eval("Address") %>","<%# Eval("Address1") %>");'>Select</a>--%>
                    </DataItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Address" VisibleIndex="1" SortIndex="0" SortOrder="Ascending" Width="150">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Address" FieldName="Address1" VisibleIndex="1" Width="200">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
        </dxwgv:ASPxGridView>
    </form>
</body>
</html>
