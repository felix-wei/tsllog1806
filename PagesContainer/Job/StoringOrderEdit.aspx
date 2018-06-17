<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="StoringOrderEdit.aspx.cs" Inherits="PagesContainer_Job_StoringOrderEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Storing Order</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Script/pages.js"></script>
    <script type="text/javascript" src="../../Script/Basepages.js"></script>
    <script type="text/javascript" src="../../Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="../../Script/Sea/Acc.js"></script>
    <script>
        //close job
        function OnCloseCallBack(v) {
            if (v == "Success") {
                alert("Action Success!");
                detailGrid.Refresh();
            }
            else if (v == "Fail")
                alert("Action Fail,please try again!");
        }
        ////////////////for dropdown list
        function RowClickHandler(s, e) {
            SetLookupKeyValue(e.visibleIndex);
            DropDownEdit.HideDropDown();
        }
        function SetLookupKeyValue(rowIndex) {

            DropDownEdit.SetText(GridView.cpContN[rowIndex]);
            txt_ContainerType.SetText(GridView.cpContType[rowIndex]);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsTransport" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.ContAsset" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsEvent" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.ContAssetEvent" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsReturnType" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXUom" KeyMember="Code" FilterExpression="CodeType='ReturnType'" />
            <wilson:DataSource ID="dsDepot" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXUom" KeyMember="Code" FilterExpression="CodeType='DepotCode'" />
            <wilson:DataSource ID="dsStatus" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXUom" KeyMember="Code" FilterExpression="CodeType='TankState'" />
            <%--<wilson:DataSource ID="dsRefCont" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.ContAssetEvent" KeyMember="Id"  FilterExpression="1=0" />--%>
            <table>
                <tr>
                    <td>SO NO</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_ReNo" ClientInstanceName="txt_ReNo" Width="150" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                           window.location='StoringOrderEdit.aspx?no='+txt_ReNo.GetText();
                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>Port</td>
                    <td>
                        <dxe:ASPxButtonEdit ID="btn_Port" ClientInstanceName="btn_Port" runat="server" Width="150" HorizontalAlign="Left" AutoPostBack="False" Enabled="false">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupPort(btn_Port);
                                                                    }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton7" Width="100" runat="server" Text="Add New" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                           window.location='StoringOrderEdit.aspx?no=0';
                        }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="New Search" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                           window.location='StoringOrderList.aspx';
                        }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="850px" AutoGenerateColumns="False" DataSourceID="dsTransport"
                OnInitNewRow="grid_Transport_InitNewRow" OnInit="grid_Transport_Init" OnCustomCallback="grid_Transport_CustomCallback"
                OnCustomDataCallback="grid_Transport_CustomDataCallback" OnHtmlEditFormCreated="grid_Transport_HtmlEditFormCreated">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <Settings ShowColumnHeaders="false" />
                <Templates>
                    <EditForm>
                        <div style="padding: 2px 2px 2px 2px">
                            <table style="width: 100%;">
                                <tr>
                                    <td width="80%"></td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_Save" Width="100" runat="server" Text="Save" AutoPostBack="false" UseSubmitBehavior="false">
                                            <ClientSideEvents Click="function(s,e) {
                                            detailGrid.PerformCallback('Save');
                                            }" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                            <table style="padding: 2px 2px 2px 2px">
                                <tr>
                                    <td>Print Document:
                                    </td>
                                </tr>
                            </table>

                            <!---- main info --->
                            <hr />
                            <div style="display: none">
                                <dxe:ASPxTextBox ID="txt_Id" ClientInstanceName="txt_Id" runat="server" ReadOnly="true"
                                    BackColor="Control" Text='<%# Eval("Id") %>' Width="170">
                                </dxe:ASPxTextBox>
                                <dxe:ASPxTextBox ID="txt_DocType" ClientInstanceName="txt_DocType" runat="server" ReadOnly="true"
                                    BackColor="Control" Text='<%# Eval("DocType") %>' Width="170">
                                </dxe:ASPxTextBox>
                            </div>
                            <table>
                                <tr>
                                    <td>SO No
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_DocNo" ClientInstanceName="txt_DocNo" runat="server" Text='<%# Eval("DocNo") %>' Width="150" ReadOnly="true" BackColor="Control">
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>Depot/Yard
                                    </td>
                                    <td>
                                        <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_DepotCode"
                                            DataSourceID="dsDepot" TextField="Code" ValueField="Code" Width="150" Value='<%# Eval("DepotCode")%>'>
                                        </dxe:ASPxComboBox>
                                    </td>
                                    <td>ReturnType
                                    </td>
                                    <td>
                                        <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_ReturnType"
                                            DataSourceID="dsReturnType" TextField="Code" ValueField="Code" Width="150" Value='<%# Eval("ReturnType")%>'>
                                        </dxe:ASPxComboBox>
                                    </td>
                                    <td>Job Ref
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_JobNo" ClientInstanceName="txt_JobNo" runat="server" Text='<%# Eval("JobNo") %>' Width="150">
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Demur Start
                                    </td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_DemurrageStartDate" Width="150" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" Value='<%# Eval("DemurrageStartDate") %>'>
                                        </dxe:ASPxDateEdit>
                                    </td>
                                    <td>Demur Days
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="spin_DemurrageFreeDay" runat="server" Width="150" Increment="0" DisplayFormatString="0" SpinButtons-ShowIncrementButtons="false" Value='<%# Eval("DemurrageFreeDay") %>'>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>Demur Ref
                                    </td>
                                    <td colspan="3">
                                        <dxe:ASPxTextBox runat="server" Width="370" ID="txt_DemurrageRef" ClientInstanceName="txt_DemurrageRef" Text='<%# Eval("DemurrageRef")%>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Deten Start
                                    </td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_DetentionStartDate" Width="150" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" Value='<%# Eval("DetentionStartDate") %>'>
                                        </dxe:ASPxDateEdit>
                                    </td>
                                    <td>Deten Days
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="spin_DetentionFreeDay" runat="server" Width="150" Increment="0" DisplayFormatString="0" SpinButtons-ShowIncrementButtons="false" Value='<%# Eval("DetentionFreeDay") %>'>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>Deten Ref
                                    </td>
                                    <td colspan="3">
                                        <dxe:ASPxTextBox runat="server" Width="370" ID="txt_DetentionRef" ClientInstanceName="txt_DetentionRef" Text='<%# Eval("DetentionRef")%>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Haulier
                                    </td>
                                    <td>
                                        <dxe:ASPxButtonEdit ID="btn_HaulierCode" ClientInstanceName="btn_HaulierCode" runat="server" Width="150" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("HaulierCode")%>'>
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupParty(btn_HaulierCode,null,'CVA');
                                                                    }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td>Compl.Date
                                    </td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_HaulierCompleteDate" Width="150" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" Value='<%# Eval("HaulierCompleteDate") %>'>
                                        </dxe:ASPxDateEdit>
                                    </td>
                                    <td>Instruction
                                    </td>
                                    <td colspan="3">
                                        <dxe:ASPxTextBox runat="server" Width="370" ID="txt_Instruction" ClientInstanceName="txt_Instruction" Text='<%# Eval("Instruction")%>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Carrier
                                    </td>
                                    <td>
                                        <dxe:ASPxButtonEdit runat="server" ID="btn_ShipCarrierCode" ClientInstanceName="btn_ShipCarrierCode" Width="150" HorizontalAlign="Left" AutoPostBack="false" Text='<%# Eval("ShipCarrierCode")%>'>
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupParty(btn_ShipCarrierCode,txt_ShipCarrierRef,'CVA');
                                                                    }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td>Carrier Name
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox runat="server" Width="150" ID="txt_ShipCarrierRef" ClientInstanceName="txt_ShipCarrierRef" Text='<%# Eval("ShipCarrierRef")%>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>Vessel
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_ShipVessel" ClientInstanceName="txt_ShipVessel" runat="server" Text='<%# Eval("ShipVessel") %>' Width="150">
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>Voyage
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_ShipVoyage" ClientInstanceName="txt_ShipVoyage" runat="server" Text='<%# Eval("ShipVoyage") %>' Width="150">
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Pol
                                    </td>
                                    <td>
                                        <dxe:ASPxButtonEdit ID="btn_ShipPol" ClientInstanceName="btn_ShipPol" runat="server" Text='<%# Eval("ShipPol")%>' Width="150" MaxLength="5" AutoPostBack="false">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                              PopupPort(btn_ShipPol);
                                                                    }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td>Pod
                                    </td>
                                    <td>
                                        <dxe:ASPxButtonEdit ID="btn_ShipPod" ClientInstanceName="btn_ShipPod" runat="server" Width="150" HorizontalAlign="Left" Text='<%# Eval("ShipPod")%>' AutoPostBack="False" MaxLength="5">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupPort(btn_ShipPod);
                                                                    }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td>Eta
                                    </td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_ShipEta" Width="150" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" Value='<%# Eval("ShipEta") %>'>
                                        </dxe:ASPxDateEdit>
                                    </td>
                                    <td>Etd
                                    </td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_ShipEtd" Width="150" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" Value='<%# Eval("ShipEtd") %>'>
                                        </dxe:ASPxDateEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Shipper
                                    </td>
                                    <td>
                                        <dxe:ASPxButtonEdit ID="btn_ShipperCode" ClientInstanceName="btn_ShipperCode" runat="server" Width="150" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("ShipperCode")%>'>
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupParty(btn_ShipperCode,txt_ShipperName,'CVA');
                                                                    }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td>ShipperName
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox runat="server" Width="150" ID="txt_ShipperName" ClientInstanceName="txt_ShipperName" Text='<%# Eval("ShipperName")%>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>Consignee
                                    </td>
                                    <td>
                                        <dxe:ASPxButtonEdit ID="btn_ConsigneeCode" ClientInstanceName="btn_ConsigneeCode" runat="server" Width="150" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("ConsigneeCode")%>'>
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupParty(btn_ConsigneeCode,txt_ConsigneeName,'CVA');
                                                                    }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td>ConName
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox runat="server" Width="150" ID="txt_ConsigneeName" ClientInstanceName="txt_ConsigneeName" Text='<%# Eval("ConsigneeName")%>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Note
                                    </td>
                                    <td colspan="3" rowspan="2">
                                        <dxe:ASPxMemo ID="txt_ShipNote" ClientInstanceName="txt_ShipNote" Rows="4" runat="server" Text='<%# Eval("ShipNote") %>' Width="390">
                                        </dxe:ASPxMemo>
                                    </td>
                                </tr>
                            </table>
                            <%---- AssetEvent info ---%>
                            <hr />


                            <dxe:ASPxButton ID="ASPxButton15a" Width="150" runat="server" Text="Add Container" AutoPostBack="false"
                                Visible='<%# SafeValue.SafeString(Eval("Id"),"0")!="0" %>' Enable='<%# SafeValue.SafeString(Eval("Id"),"0")!="0" %>' UseSubmitBehavior="false">
                                <ClientSideEvents Click="function(s,e) {
                                                        grid_Event.AddNewRow();
                                                        }" />
                            </dxe:ASPxButton>
                            <dxwgv:ASPxGridView ID="grid_Event" ClientInstanceName="grid_Event" runat="server"
                                DataSourceID="dsEvent" KeyFieldName="Id" Width="100%" OnBeforePerformDataSelect="grid_Event_DataSelect"
                                OnInit="grid_Event_Init" OnInitNewRow="grid_Event_InitNewRow" OnRowInserting="grid_Event_RowInserting"
                                OnRowInserted="grid_Event_RowInserted" OnRowUpdating="grid_Event_RowUpdating" OnRowUpdated="grid_Event_RowUpdated"
                                OnRowDeleting="grid_Event_RowDeleting" OnRowDeleted="grid_Event_RowDeleted" OnHtmlEditFormCreated="grid_Event_HtmlEditFormCreated">
                                <SettingsBehavior ConfirmDelete="True" />
                                <SettingsEditing Mode="EditFormAndDisplayRow" />
                                <Columns>
                                    <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                        <DataItemTemplate>

                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_Event_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                            ClientSideEvents-Click='<%# "function(s) { grid_Event.StartEditRow("+Container.VisibleIndex+") }" %>'>
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_Event_del" runat="server" UseSubmitBehavior="false"
                                                            Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Event.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                        </dxe:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </DataItemTemplate>
                                    </dxwgv:GridViewDataColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="ContainerNo" FieldName="ContainerNo" VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="ContainerType" FieldName="ContainerType" VisibleIndex="5">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="VehicleNo" FieldName="VehicleNo" VisibleIndex="6">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Insturction" FieldName="Insturction" VisibleIndex="7">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="DocNo" VisibleIndex="7" Visible="false">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <Templates>
                                    <EditForm>
                                        <div style="display: none">
                                            <dxe:ASPxTextBox runat="server" Width="150" ID="txt_Event_DocNo"
                                                Text='<%# Eval("DocNo") %>'>
                                            </dxe:ASPxTextBox>
                                            <dxe:ASPxTextBox runat="server" Width="150" ID="txt_Event_JobNo"
                                                Text='<%# Eval("JobNo") %>'>
                                            </dxe:ASPxTextBox>
                                        </div>
                                        <table>
                                            <tr>
                                                <td>Cont No
                                                </td>
                                                <td>
                                                    <dxe:ASPxDropDownEdit ID="DropDownEdit" runat="server" ClientInstanceName="DropDownEdit"
                                                        Text='<%# Bind("ContainerNo") %>' Width="150" AllowUserInput="True">
                                                        <DropDownWindowTemplate>
                                                            <dxwgv:ASPxGridView ID="gridPopCont" runat="server" AutoGenerateColumns="False" ClientInstanceName="GridView"
                                                                Width="300px" OnCustomJSProperties="gridPopCont_CustomJSProperties">
                                                                <Columns>
                                                                    <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" VisibleIndex="0">
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                    <dxwgv:GridViewDataTextColumn FieldName="ContainerType" Caption="Type" VisibleIndex="1">
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                </Columns>
                                                                <ClientSideEvents RowClick="RowClickHandler" />
                                                            </dxwgv:ASPxGridView>
                                                        </DropDownWindowTemplate>
                                                    </dxe:ASPxDropDownEdit>

                                                </td>
                                                <td>ContainerType
                                                </td>
                                                <td>
                                                    <dxe:ASPxTextBox runat="server" Width="150" ID="txt_ContainerType" ClientInstanceName="txt_ContainerType" Text='<%# Bind("ContainerType")%>' ReadOnly="true">
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td>VehicleNo
                                                </td>
                                                <td>
                                                    <dxe:ASPxTextBox runat="server" Width="150" ID="txt_VehicleNo" ClientInstanceName="txt_VehicleNo" Text='<%# Bind("VehicleNo")%>'>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Insturction
                                                </td>
                                                <td colspan="5">
                                                    <dxe:ASPxMemo runat="server" Width="620" ID="txt_Insturction" ClientInstanceName="txt_Insturction" Text='<%# Bind("Insturction")%>'>
                                                    </dxe:ASPxMemo>
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="text-align: right; padding: 2px 2px 2px 2px">
                                            <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateEvent" ReplacementType="EditFormUpdateButton"
                                                runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                            <dxwgv:ASPxGridViewTemplateReplacement ID="CancelEvent" ReplacementType="EditFormCancelButton"
                                                runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                        </div>
                                    </EditForm>
                                </Templates>
                            </dxwgv:ASPxGridView>

                        </div>
                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="600" EnableViewState="False">
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr1"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="570"
                Width="900" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      if(grid!=null)
	    grid.Refresh();
	    grid=null;
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
