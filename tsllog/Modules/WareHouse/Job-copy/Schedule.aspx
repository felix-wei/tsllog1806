<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Schedule.aspx.cs" Inherits="WareHouse_Job_Schedule" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="/Script/Sea/Acc.js"></script>
    <script type="text/javascript">

        function OnSaveCallBack(v) {
            if (v != null && v.indexOf("Fail") > -1) {
                alert(v);
            }
            else if (v != null && v.indexOf("Success") > -1) {
                alert(v);
            }
        }
        function AfterPopubMultiInv(v) {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            parent.navTab.openTab(v, "/Modules/WareHouse/Job/SoEdit.aspx?no=" + v, {
                title: v, fresh: false, external: true
            });
            grid.Refresh();
        }
        function PopupCreateOrder(id,sales) {
            popubCtr.SetHeaderText('Set Stock');
            popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/SelectSetStock.aspx?SchId=' + id + "&Salesman=" + sales);
            popubCtr.Show();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsWhSchedule" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.WhSchedule" KeyMember="Id" />
            <table>
                <tr>
                    <td>Customer
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Name" Width="100" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" runat="server" Text="Retrieve"
                            OnClick="btn_Sch_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Refresh" Width="110" AutoPostBack="False"
                            UseSubmitBehavior="False">
                            <ClientSideEvents Click="function(s, e) {
                                   grid.Refresh();
                                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btnSelect" runat="server" Text="Add" Width="110" AutoPostBack="False"
                            UseSubmitBehavior="False">
                            <ClientSideEvents Click="function(s, e) {
                                   grid.AddNewRow();
                                    }" />
                        </dxe:ASPxButton>
                    </td>

                </tr>
            </table>
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" DataSourceID="dsWhSchedule" Width="100%"
                KeyFieldName="Id" AutoGenerateColumns="False" OnCustomDataCallback="ASPxGridView1_CustomDataCallback"
                OnInit="ASPxGridView1_Init" OnInitNewRow="ASPxGridView1_InitNewRow" OnRowInserting="ASPxGridView1_RowInserting" OnRowUpdating="ASPxGridView1_RowUpdating" OnRowDeleting="ASPxGridView1_RowDeleting">
                <SettingsEditing Mode="Inline" />
                <SettingsPager PageSize="100" Mode="ShowPager">
                </SettingsPager>
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsBehavior ConfirmDelete="True" />
                <Columns>
                    <dxwgv:GridViewCommandColumn Caption="#" VisibleIndex="0" Width="60">
                        <EditButton Text="Edit" Visible="true"></EditButton>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Id" FieldName="Id" Width="20" VisibleIndex="1" ReadOnly="true" CellStyle-HorizontalAlign="Center">
                        <DataItemTemplate>
                            <%# Eval("Id") %>
                            <div style="display: none">
                                <dxe:ASPxTextBox ID="txt_Id" ClientInstanceName="txt_Id" runat="server" Width="10" Text='<%# Eval("Id") %>'>
                                </dxe:ASPxTextBox>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Customer" FieldName="PartyName" VisibleIndex="1">
                        <DataItemTemplate>
                            <%# Eval("PartyName") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxButtonEdit ID="txt_PartyId" ClientInstanceName="txt_PartyId" runat="server"
                                Width="100%" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("PartyId") %>'>
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                                PopupSalesOrderParty(txt_PartyId,txt_PartyName,txt_PartyContact,null,null,null,null,txt_PartyAdd,null,null,'C');
                             }" />
                            </dxe:ASPxButtonEdit>
                            <div style="display: none">
                                <dxe:ASPxTextBox ID="txt_PartyName" ClientInstanceName="txt_PartyName" runat="server" Text='<%# Bind("PartyName") %>'></dxe:ASPxTextBox>
                                <dxe:ASPxTextBox ID="txt_PartyAdd" ClientInstanceName="txt_PartyAdd" runat="server" Text='<%# Bind("PartyAdd") %>'></dxe:ASPxTextBox>
                                <dxe:ASPxTextBox ID="txt_PartyContact" ClientInstanceName="txt_PartyContact" runat="server" Text='<%# Bind("PartyContact") %>'></dxe:ASPxTextBox>
                            </div>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTimeEditColumn Caption="DateTime" FieldName="DoDateTime" VisibleIndex="2" SortOrder="Ascending">
                        <PropertiesTimeEdit EditFormat="DateTime" DisplayFormatString="yyyy-MM-dd HH:mm:ss"></PropertiesTimeEdit>
                        <EditItemTemplate>
                            <dxe:ASPxDateEdit ID="txt_DoDateTime" runat="server" Value='<%# Bind("DoDateTime") %>' Width="100%" EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm:ss">
                            </dxe:ASPxDateEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTimeEditColumn>
                    <dxwgv:GridViewDataColumn Caption="Doctor" FieldName="DoctorId">
                        <EditItemTemplate>
                            <dxe:ASPxButtonEdit ID="txt_Doctor" ClientInstanceName="txt_Doctor" runat="server" Width="120"
                                HorizontalAlign="Left" AutoPostBack="false" Text='<%# Bind("DoctorId") %>'>
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s,e){PopupPersonInfo(txt_Doctor,null,null,null,null,null,null,null,null,'D');}" />
                            </dxe:ASPxButtonEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Patient" FieldName="Patient">
                        <EditItemTemplate>
                            <dxe:ASPxButtonEdit ID="txt_Patient" ClientInstanceName="txt_Patient" runat="server" Width="120"
                                HorizontalAlign="Left" AutoPostBack="false" Text='<%# Bind("Patient") %>'>
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s,e){PopupPersonInfo(null,txt_Patient,null,null,null,null,null,null,null,'P');}" />
                            </dxe:ASPxButtonEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Sales" FieldName="SalesId">
                        <EditItemTemplate>
                            <dxe:ASPxButtonEdit ID="btn_Sales" ClientInstanceName="btn_Sales" runat="server" Width="120"
                                HorizontalAlign="Left" AutoPostBack="false" Text='<%# Bind("SalesId") %>'>
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s,e){PopupSaleMan(btn_Sales);}" />
                            </dxe:ASPxButtonEdit>
                        </EditItemTemplate>
                        <DataItemTemplate>
                            <dxe:ASPxLabel runat="server" ID="txt_Sales" ClientInstanceName="txt_Sales" Text='<%# Bind("SalesId") %>'></dxe:ASPxLabel>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Status" FieldName="StatusCode">
                        <PropertiesComboBox Width="100">
                            <Items>
                                <dxe:ListEditItem Value="Pending" Text="Pending" />
                                <dxe:ListEditItem Value="Finished" Text="Finished" />
                                <dxe:ListEditItem Value="Rescheduled" Text="Rescheduled" />
                                <dxe:ListEditItem Value="Cancelled" Text="Cancelled" />
                                <dxe:ListEditItem Value="Closed" Text="Closed" />
                            </Items>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataColumn Caption="Order&nbsp;No" FieldName="DoNo">
                        <DataItemTemplate>
                            <%-- <dxe:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="false" Width="80" Text="Create Order"  Visible='<%# SafeValue.SafeString(Eval("DoNo"),"")=="" %>'>
                            <ClientSideEvents Click="function(s,e) {
                        PopupCreateOrder(<%# Bind("SalesId") %>,txt_Sales.GetText());
                        }" />
                        </dxe:ASPxButton>--%>
                            <div style="display: <%# SafeValue.SafeString(Eval("DoNo"),"")==""?"block":"none" %>">
                                 <a onclick='javascript:PopupCreateOrder("<%# Eval("Id") %>","<%# Eval("SalesId") %>");' href="#">Create Order</a>
                             </div>
                            <div style="display: <%# SafeValue.SafeString(Eval("DoNo"),"")==""?"none":"block" %>">
                            <a  href='javascript: parent.navTab.openTab("<%# Eval("DoNo") %>","/Modules/WareHouse/Job/SoEdit.aspx?no=<%# Eval("DoNo") %>",{title:"<%# Eval("DoNo") %>", fresh:false, external:true});'><%# Eval("DoNo") %></a>
                                </div>
                            <div style="display: none">
                                <input type="hidden" id="create" />
                            </div>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxLabel runat="server" ID="lb_Create1" Text='<%# Eval("DoNo") %>'></dxe:ASPxLabel>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>

                </Columns>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="1100" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
