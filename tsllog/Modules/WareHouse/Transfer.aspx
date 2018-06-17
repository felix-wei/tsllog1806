<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="Transfer.aspx.cs" Inherits="WareHouse_Transfer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Transfer</title>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <script type="text/javascript">
        var clientId = null;
        var clientName = null;
        var clientType = null;
        var clientLc = null;
        function PutWhValue(s, name, wh, lc) {
            if (clientId != null) {
                clientId.SetText(s);
            }
            if (clientName != null) {
                clientName.SetText(name);
            }
            if (clientType != null) {
                clientType.SetText(wh);
            }
            if (clientLc != null) {
                clientLc.SetText(lc);
            }
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
        }
        function OnCallBack(v) {
            if (v == "Success") {
                alert("Action Success!");
                detailGrid.Refresh();
            }
            else if (v == "Fail")
                alert("Action Fail,please try again!");
            else if (v == "Permit")
                alert("Please enter Permit info !");
            else if (v == "LotNo")
                alert("Please enter LotNo !");
        }
        function AddItem() {
            //grid_det.AddNewRow();
            popubCtr.SetContentUrl('SelectPage/transferProduct.aspx?no=' + txt_TransferNo.GetText() + '&PartyId=' + txt_PartyId.GetText());
            popubCtr.SetHeaderText('SKU List');
            popubCtr.Show();
        }
        function Print() {
            window.open('/Modules/WareHouse/PrintView.aspx?document=Transfer&master=' + txt_TransferNo.GetText() + "&house=0");
        }
        function ClosePopupCtr() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            grid_det.Refresh();
        }
    </script>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <wilson:DataSource ID="dsTransfer" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.WhTransfer" KeyMember="Id" />
    <wilson:DataSource ID="dsTransferDet" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.WhTransferDet" KeyMember="Id" />
    <wilson:DataSource ID="dsWhMastData" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.WhMastData" KeyMember="Id" FilterExpression="Type='Attribute'" />
    <form id="form1" runat="server">
        <div>
            <table style="width: 700px">
                <tr>
                    <td>Transfer No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Name" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="Label1" runat="server" Text="From">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="Label2" runat="server" Text="To">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" Width="100" runat="server" Text="Retrieve" OnClick="btn_Sch_Click">
                            <ClientSideEvents Click="function(s,e){
                        window.location='Transfer.aspx?name='+txt_Name.GetText();
                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                                detailGrid.AddNewRow();
                                }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <div>
                <dxwgv:ASPxGridView ID="grid" ClientInstanceName="detailGrid" runat="server" OnCustomDataCallback="grid_CustomDataCallback"
                    KeyFieldName="Id" Width="100%" OnCustomCallback="grid_CustomCallback" AutoGenerateColumns="False" OnInit="grid_Init" Theme="DevEx" DataSourceID="dsTransfer" OnHtmlEditFormCreated="grid_HtmlEditFormCreated" OnInitNewRow="grid_InitNewRow" OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating" OnRowDeleting="grid_RowDeleting">
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <SettingsEditing Mode="EditForm" />
                    <SettingsCustomizationWindow Enabled="True" />
                    <SettingsBehavior ConfirmDelete="True" />
                    <Columns>
                        <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="50">
                            <EditButton Visible="true"></EditButton>
                            <DeleteButton Visible="true" Text="Delete"></DeleteButton>
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="Id" VisibleIndex="1" Visible="false">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="TransferNo" FieldName="TransferNo" VisibleIndex="2" Width="150">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Request Person" FieldName="RequestPerson" VisibleIndex="2" Width="150">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Request Date" FieldName="RequestDate" VisibleIndex="2" Width="150">
                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Transfer Date" FieldName="TransferDate" VisibleIndex="2" Width="150">
                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="StatusCode" FieldName="StatusCode" VisibleIndex="2" Width="150">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="CreateBy" FieldName="CreateBy" VisibleIndex="4" Width="100">
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="CreateDateTime" FieldName="CreateDateTime" VisibleIndex="5" Width="100">
                            <EditFormSettings Visible="False" />
                            <DataItemTemplate><%# SafeValue.SafeDateStr(Eval("CreateDateTime")) %></DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <EditForm>
                            <table style="text-align: left; padding: 2px 2px 2px 2px; width: 900px">
                                <tr>
                                    <td width="80%"></td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_Save" Width="100" runat="server" Text="Save" AutoPostBack="false"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'>
                                            <ClientSideEvents Click="function(s,e) {
                                            detailGrid.UpdateEdit();
                                            }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_CloseJob" Width="90" runat="server" Text="Close Job" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                                detailGrid.GetValuesOnCustomCallback('CloseJob',OnCallBack);
                                                                    }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_Void" ClientInstanceName="btn_Void" runat="server" Width="100" Text="Void" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CLS" %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                                    if(confirm('Confirm '+btn_Void.GetText()+' Job?'))
                                                                    {
                                                                        detailGrid.GetValuesOnCustomCallback('Void',OnCallBack);                 
                                                                    }
                                        }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_Cancle" ClientInstanceName="btn_Cancle" runat="server" Width="100" Text="Cancle" AutoPostBack="False"
                                            UseSubmitBehavior="false">
                                            <ClientSideEvents Click="function(s, e) {
                                                 detailGrid.CancelEdit();
                                        }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_Print" runat="server" Width="100" Text="Print" AutoPostBack="False"
                                            UseSubmitBehavior="false" >
                                            <ClientSideEvents Click="function(s, e) {
                                                                   Print();
                                        }" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                            <div style="padding: 2px 2px 2px 2px">


                                <div style="display: none">
                                    <dxe:ASPxTextBox runat="server" Width="60" ID="txt_Id" ClientInstanceName="txt_Id"
                                        Text='<%# Eval("Id") %>'>
                                    </dxe:ASPxTextBox>
                                </div>
                                <table border="0" width="900">
                                    <tr>
                                        <td width="100"></td>
                                        <td width="180"></td>
                                        <td width="100"></td>
                                        <td width="180"></td>
                                        <td width="100"></td>
                                        <td width="180"></td>
                                    </tr>
                                    <tr>
                                        <td width="100">Transfer No
                                        </td>
                                        <td>
                                            <dxe:ASPxTextBox runat="server" Width="150" ID="txt_TransferNo" ClientInstanceName="txt_TransferNo" ReadOnly="true" BackColor="Control" Text='<%# Eval("TransferNo")%>'>
                                            </dxe:ASPxTextBox>
                                        </td>
                                        <td width="100">Request Person</td>
                                        <td>
                                            <dxe:ASPxTextBox ID="txt_RequestPerson" runat="server" ClientInstanceName="txt_PartyRefNo" Width="165px" Text='<%# Eval("RequestPerson")%>'>
                                            </dxe:ASPxTextBox>
                                        </td>
                                        <td>Request Date
                                        </td>
                                        <td>
                                            <dxe:ASPxDateEdit ID="de_RequestDate" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" Value='<%# Eval("RequestDate") %>' Width="120">
                                            </dxe:ASPxDateEdit>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Customer</td>
                                        <td colspan="3">
                                            <table cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButtonEdit ID="txt_PartyId" ClientInstanceName="txt_PartyId" runat="server" Text='<%# Eval("PartyId")%>' Width="80" HorizontalAlign="Left" AutoPostBack="False">
                                                            <Buttons>
                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                            </Buttons>
                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                               PopupParty(txt_PartyId,txt_PartyName);
                                                                    }" />
                                                        </dxe:ASPxButtonEdit>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox runat="server" Width="385px" BackColor="Control" ID="txt_PartyName"
                                                            ReadOnly="true" ClientInstanceName="txt_PartyName" Text='<%# Eval("PartyName")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>Transfer Date</td>
                                        <td>
                                            <dxe:ASPxDateEdit ID="de_TransferDate" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" Value='<%# Eval("TransferDate") %>' Width="120px">
                                            </dxe:ASPxDateEdit>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PIC
                                        </td>
                                        <td>
                                            <dxe:ASPxTextBox ID="txt_Pic" runat="server" Text='<%# Eval("Pic") %>' Width="150">
                                            </dxe:ASPxTextBox>
                                        </td>
                                        <td>Confirm Person</td>
                                        <td>
                                            <dxe:ASPxTextBox ID="txt_ConfirmPerson" runat="server" Text='<%# Eval("ConfirmPerson") %>' Width="165px">
                                            </dxe:ASPxTextBox>
                                        </td>
                                        <td>Confirm Date</td>
                                        <td>
                                            <dxe:ASPxDateEdit ID="de_ConfirmDate" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" Value='<%# Eval("ConfirmDate") %>' Width="120px">
                                            </dxe:ASPxDateEdit>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <hr>
                                            <table>
                                                <tr>
                                                    <td style="width: 80px;">Creation</td>
                                                    <td style="width: 160px"><%# Eval("CreateBy")%> @ <%# SafeValue.SafeDateStr( Eval("CreateDateTime"))%></td>
                                                    <td style="width: 100px;">Last Updated</td>
                                                    <td style="width: 160px; text-align: center"><%# Eval("UpdateBy")%> @ <%# SafeValue.SafeDateStr(Eval("UpdateDateTime"))%></td>
                                                    <td style="width: 100px;">Status</td>
                                                    <td><%# Eval("StatusCode")%> </td>
                                                </tr>
                                            </table>
                                            <hr>
                                        </td>
                                    </tr>
                                </table>

                            </div>
                            <table>
                                <tr>
                                    <td></td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_DetAdd" runat="server" Text="Add Item" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE"  %>' AutoPostBack="false" UseSubmitBehavior="false">
                                            <ClientSideEvents Click="function(s,e){
                                         AddItem();
                            }" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                            <div>
                                <%--style="WIDTH: 1050px; overflow-y: scroll;"--%>

                                <dxwgv:ASPxGridView ID="grid_det" ClientInstanceName="grid_det"
                                    runat="server" DataSourceID="dsTransferDet" KeyFieldName="Id"
                                    OnBeforePerformDataSelect="grid_det_BeforePerformDataSelect" OnRowUpdating="grid_det_RowUpdating"
                                    OnRowInserting="grid_det_RowInserting" OnInitNewRow="grid_det_InitNewRow" OnInit="grid_det_Init" OnRowDeleting="grid_det_RowDeleting"
                                    Width="1000"
                                    AutoGenerateColumns="False">
                                    <SettingsPager Mode="ShowAllRecords">
                                    </SettingsPager>
                                    <SettingsEditing Mode="Inline" />
                                    <SettingsCustomizationWindow Enabled="True" />
                                    <SettingsBehavior ConfirmDelete="True" />
                                    <Columns>
                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40px">
                                            <DataItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_DoDet_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                ClientSideEvents-Click='<%# "function(s) { grid_det.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_DoDet_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_det.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </DataItemTemplate>
                                            <EditItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_DoDet_edit" runat="server" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                                Text="Update" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { grid_det.UpdateEdit(); }"  %>'>
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_DoDet_cancle" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                ClientSideEvents-Click='<%# "function(s) { grid_det.CancelEdit(); }"  %>'>
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EditItemTemplate>
                                        </dxwgv:GridViewDataColumn>
                                        <dxwgv:GridViewDataTextColumn
                                            Caption="Product" FieldName="Product" VisibleIndex="1" Width="60">
                                            <PropertiesTextEdit Width="60" />
                                            <EditItemTemplate>
                                                <%# Eval("Product")%>
                                                <%--<dxe:ASPxButtonEdit ID="btn_Product" ClientInstanceName="btn_Product" runat="server" Text='<%# Bind("Product")%>' Width="60">
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                              PopupProductToWareHouse(btn_Product,btn_FromLoc,txt_LotNo,txt_Uom1,txt_Uom2,txt_Uom3,spin_QtyPack,spin_QtyWhole,spin_PackQty,spin_Price);
                                                                   }" />
                                                </dxe:ASPxButtonEdit>--%>
                                                <div style="display: none">
                                                    <dxe:ASPxTextBox runat="server" ID="txt_DoInId" Text='<%# Eval("DoInId")%>'></dxe:ASPxTextBox>
                                                    <dxe:ASPxTextBox runat="server" ID="txt_DoOutNo" Text='<%# Eval("DoOutId")%>'></dxe:ASPxTextBox>
                                                </div>
                                            </EditItemTemplate>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="LotNo" FieldName="LotNo" VisibleIndex="1" Width="100">
                                            <PropertiesTextEdit Width="100" />
                                            <EditItemTemplate>
                                                <%# Eval("LotNo")%>
                                                <%--<dxe:ASPxTextBox ID="txt_LotNo" runat="server" BackColor="Control" Width="80" ReadOnly="true" Text='<%# Bind("LotNo")%>' ClientInstanceName="txt_LotNo"></dxe:ASPxTextBox>--%>
                                            </EditItemTemplate>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Des1" VisibleIndex="1" Width="120">                                            
                                            <PropertiesTextEdit Width="100" />
                                            <EditItemTemplate>
                                                <%# Eval("Des1")%>
                                                <%--<dxe:ASPxTextBox ID="txt_Des" runat="server" BackColor="Control" Width="80" ReadOnly="true" Text='<%# Bind("Des1")%>' ClientInstanceName="txt_LotNo"></dxe:ASPxTextBox>--%>
                                            </EditItemTemplate>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataSpinEditColumn Caption="P.Qty" FieldName="Qty1" VisibleIndex="10" Width="40">
                                            <EditItemTemplate>
                                                <dxe:ASPxSpinEdit ID="spin_PackQty" runat="server" Width="40" ClientInstanceName="spin_PackQty" Value='<%# Bind("Qty1") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                </dxe:ASPxSpinEdit>
                                            </EditItemTemplate>
                                        </dxwgv:GridViewDataSpinEditColumn>
                                        <dxwgv:GridViewDataSpinEditColumn Caption="W.Qty" FieldName="Qty2" VisibleIndex="11" Width="40">
                                            <PropertiesSpinEdit NumberType="Integer" Increment="0" SpinButtons-ShowIncrementButtons="false" Width="40">
                                            </PropertiesSpinEdit>
                                        </dxwgv:GridViewDataSpinEditColumn>
                                        <dxwgv:GridViewDataSpinEditColumn Caption="L.Qty" FieldName="Qty3" VisibleIndex="12" Width="40">
                                            <PropertiesSpinEdit NumberType="Integer" Increment="0" SpinButtons-ShowIncrementButtons="false" Width="40">
                                            </PropertiesSpinEdit>
                                        </dxwgv:GridViewDataSpinEditColumn>

                                        <dxwgv:GridViewDataTextColumn Caption="Packing" ReadOnly="true" VisibleIndex="17" Width="100">
                                            <PropertiesTextEdit Width="100" />
                                            <EditItemTemplate>
                                                <%# Eval("Packing") %>
                                            </EditItemTemplate>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="From Location" ReadOnly="true" FieldName="FromLocId" VisibleIndex="17" Width="100">
                                            <PropertiesTextEdit Width="100" />
                                            <EditItemTemplate>
                                                <%# Eval("FromLocId") %>
                                            </EditItemTemplate>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="To Location" FieldName="ToLocId" VisibleIndex="17" Width="100">
                                            <EditItemTemplate>
                                                <dxe:ASPxButtonEdit ID="btn_ToLoc" ClientInstanceName="btn_ToLoc" runat="server" Text='<%# Bind("ToLocId")%>' Width="100">
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                              PopupToLo(btn_ToLoc,null);
                                                                   }" />
                                                </dxe:ASPxButtonEdit>
                                            </EditItemTemplate>
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                </dxwgv:ASPxGridView>
                            </div>
                        </EditForm>
                    </Templates>
                </dxwgv:ASPxGridView>
                <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                    HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                    AllowResize="True" Width="1000" EnableViewState="False">
                </dxpc:ASPxPopupControl>
            </div>
        </div>
    </form>
</body>
</html>
