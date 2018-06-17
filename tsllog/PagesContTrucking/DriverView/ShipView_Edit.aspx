<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ShipView_Edit.aspx.cs" Inherits="PagesContTrucking_DriverView_ShipView_Edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Edi.js"></script>
    <script type="text/javascript" src="/Script/Sea/Acc.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <script type="text/javascript">
        function RowClickHandler(s, e) {
            dde_Trip_ContNo.SetText(gridPopCont.cpContN[e.visibleIndex]);
            dde_Trip_ContId.SetText(gridPopCont.cpContId[e.visibleIndex]);
            dde_Trip_ContNo.HideDropDown();
        }
        function onCallBack(v) {
            if (v != null && v.length > 0) {
                alert(v);
            }
            else {
                grid_job.Refresh();
            }
        }

        var isUpload = false;
        function PopupUploadPhoto() {
            popubCtrPic.SetHeaderText('Upload Attachment');
            popubCtrPic.SetContentUrl('../Upload.aspx?Type=CTM&Sn=' + txt_JobNo.GetText());
            popubCtrPic.Show();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsJob" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJob" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsCont" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJobDet1" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsTrip" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJobDet2" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsTripLog" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmTripLog" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsJobPhoto" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmAttachment" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsInvoice" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAArInvoice" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsVoucher" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAApPayable" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsCosting" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmCosting" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsSalesman" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXSalesman" KeyMember="Code" FilterExpression="" />
        <wilson:DataSource ID="dsContType" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.Container_Type" KeyMember="id" FilterExpression="" />
        <wilson:DataSource ID="dsTripCode" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmMastData" KeyMember="Id" FilterExpression="type='tripcode'" />
        <wilson:DataSource ID="dsTerminal" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmMastData" KeyMember="Id" FilterExpression="type='location' and type1='TERMINAL'" />


        <div>
            <table>
                <tr>
                    <td>Job No</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_JobNo" ClientInstanceName="txt_search_JobNo" runat="server"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Retrieve" runat="server" Text="Retrieve" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                        window.location='ShipView_Edit.aspx?jobNo='+txt_search_JobNo.GetText();
                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_AddNew" runat="server" Text="Add New" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                        window.location='ShipView_Edit.aspx?jobNo=0';
                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_GoSearch" runat="server" Text="Go Search" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){window.location='ShipView_List.aspx';}" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_job" runat="server" ClientInstanceName="grid_job" KeyFieldName="Id" DataSourceID="dsJob" Width="900px" AutoGenerateColumns="False" OnInit="grid_job_Init" OnInitNewRow="grid_job_InitNewRow" OnCustomDataCallback="grid_job_CustomDataCallback" OnCustomCallback="grid_job_CustomCallback" OnHtmlEditFormCreated="grid_job_HtmlEditFormCreated">
                <SettingsCustomizationWindow Enabled="True" />
                <Settings ShowColumnHeaders="false" />
                <SettingsEditing Mode="EditForm" />
                <Templates>
                    <EditForm>
                        <div style="display: none">
                            <dxe:ASPxLabel ID="lb_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>
                        </div>
                        <%--<div style="float: right; padding-right: 50px">

                        </div>--%>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 70%">
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <dxe:ASPxButton ID="btn_JobSave" ClientInstanceName="btn_JobSave" runat="server" Text="Save" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' Width="100">
                                                    <ClientSideEvents Click="function(s,e) {
                                                    grid_job.PerformCallback('save');
                                                 }" />
                                                </dxe:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <dxtc:ASPxPageControl runat="server" ID="pageControl" Height="450px" Width="900px">
                            <TabPages>
                                <dxtc:TabPage Text="Job">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <table>
                                                <tr>
                                                    <td></td>
                                                    <td width="170"></td>
                                                    <td></td>
                                                    <td width="170"></td>
                                                    <td></td>
                                                    <td width="170"></td>
                                                </tr>
                                                <tr>
                                                    <td>Job No</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_JobNo" ClientInstanceName="txt_JobNo" runat="server" ReadOnly="true" BackColor="Control" Text='<%# Eval("JobNo") %>' Width="100%"></dxe:ASPxTextBox>
                                                    </td>
                                                    <td>Job Date</td>
                                                    <td>
                                                        <dxe:ASPxDateEdit ID="txt_JobDate" runat="server" Value='<%# Eval("JobDate") %>' Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                                                    </td>
                                                    <td>Job Type</td>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="cbb_JobType" runat="server" Value='<%# Eval("JobType") %>' Width="100%">
                                                            <Items>
                                                                <dxe:ListEditItem Text="KD-IMP" Value="KD-IMP" />
                                                                <dxe:ListEditItem Text="KD-EXP" Value="KD-EXP" />
                                                                <dxe:ListEditItem Text="FCL-IMP" Value="FCL-IMP" />
                                                                <dxe:ListEditItem Text="FCL-EXP" Value="FCL-EXP" />
                                                                <dxe:ListEditItem Text="LOCAL" Value="LOCAL" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Vessel
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_Ves" Text='<%# Eval("Vessel")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>Voyage
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_Voy" Text='<%# Eval("Voyage")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>Pol
                                                    </td>
                                                    <td>
                                                        
                                                        <dxe:ASPxTextBox ID="txt_Pol" ClientInstanceName="txt_Pol" runat="server" Width="100%" HorizontalAlign="Left"    MaxLength="5" Text='<%# Eval("Pol")%>'></dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Eta
                                                    </td>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxDateEdit ID="date_Eta" Width="100" runat="server" Value='<%# Eval("EtaDate")%>'
                                                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                    </dxe:ASPxDateEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="txt_EtaTime" runat="server" Text='<%# Eval("EtaTime") %>' Width="60">
                                                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" IncludeLiterals="None" />
                                                                        <ValidationSettings ErrorDisplayMode="None" />
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>Etd
                                                    </td>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxDateEdit ID="date_Etd" Width="100" runat="server" Value='<%# Eval("EtdDate")%>'
                                                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                    </dxe:ASPxDateEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="txt_EtdTime" runat="server" Text='<%# Eval("EtdTime") %>' Width="60">
                                                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" IncludeLiterals="None" />
                                                                        <ValidationSettings ErrorDisplayMode="None" />
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>Pod
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_Pod" ClientInstanceName="txt_Pod" runat="server" Width="100%" HorizontalAlign="Left" MaxLength="5" Text='<%# Eval("Pod")%>'></dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Client Ref No
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_ClientRefNo" runat="server" Text='<%# Eval("ClientRefNo") %>' Width="100%"></dxe:ASPxTextBox>
                                                    </td>
                                                    <td>Terminal</td>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="ASPxComboBox2" runat="server" Value='<%# Eval("Terminalcode") %>' Width="100%" DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith" DataSourceID="dsTerminal" ValueField="Code" TextField="Name" ></dxe:ASPxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <table>
                                                            <tr>
                                                                <td style="vertical-align: top">
                                                                    <%--<dxe:ASPxButton ID="btn_PickupFrom" runat="server" Width="100" HorizontalAlign="Left" Text="Pickup From" AutoPostBack="False">
                                                                        <ClientSideEvents Click="function(s, e) {
                                                                        PopupCustAdr(null,txt_PickupFrom);
                                                                            }" />
                                                                    </dxe:ASPxButton>--%>
                                                                    <a href="#" onclick="PopupCustAdr(null,txt_PickupFrom);">Pick From</a>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxMemo ID="txt_PickupFrom" Rows="4" ClientInstanceName="txt_PickupFrom" runat="server"
                                                                        Width="250" Text='<%# Eval("PickupFrom") %>'>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                                <td style="vertical-align: top">
                                                                    <%--<dxe:ASPxButton ID="btn_DeliveryTo" runat="server" Width="85" HorizontalAlign="Left" Text="Delivery To" AutoPostBack="False">
                                                                        <ClientSideEvents Click="function(s, e) {
                                                                        PopupCustAdr(null,txt_DeliveryTo);
                                                                            }" />
                                                                    </dxe:ASPxButton>--%>
                                                                    <a href="#" onclick="PopupCustAdr(null,txt_DeliveryTo);">Delivery To</a>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxMemo ID="txt_DeliveryTo" Rows="4" ClientInstanceName="txt_DeliveryTo" runat="server"
                                                                        Width="250" Text='<%# Eval("DeliveryTo") %>'>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: top">Remark</td>
                                                                <td>
                                                                    <dxe:ASPxMemo ID="txt_Remark" Rows="4" runat="server" Text='<%# Eval("Remark") %>' Width="250">
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                                <td style="vertical-align: top">SpecialInstruction</td>
                                                                <td>
                                                                    <dxe:ASPxMemo ID="txt_SpecialInstruction" Rows="4" runat="server" Text='<%# Eval("SpecialInstruction") %>' Width="250">
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Container">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <div>
                                                <dxe:ASPxButton ID="btn_ContAdd" runat="server" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' Text="Add Container" AutoPostBack="false">
                                                    <ClientSideEvents Click="function(s,e){
                                grid_Cont.AddNewRow();
                                }" />
                                                </dxe:ASPxButton>
                                                <dxwgv:ASPxGridView ID="grid_Cont" ClientInstanceName="grid_Cont" runat="server" KeyFieldName="Id" DataSourceID="dsCont" Width="800px" AutoGenerateColumns="False" OnInit="grid_Cont_Init" OnBeforePerformDataSelect="grid_Cont_BeforePerformDataSelect" OnRowDeleting="grid_Cont_RowDeleting" OnInitNewRow="grid_Cont_InitNewRow" OnRowInserting="grid_Cont_RowInserting" OnRowUpdating="grid_Cont_RowUpdating" OnRowDeleted="grid_Cont_RowDeleted" OnRowUpdated="grid_Cont_RowUpdated" OnRowInserted="grid_Cont_RowInserted">
                                                    <SettingsPager PageSize="100" />
                                                    <SettingsEditing Mode="EditForm" />
                                                    <SettingsBehavior ConfirmDelete="true" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="12%">
                                                            <DataItemTemplate>
                                                                <a href="#" onclick='<%# "grid_Cont.StartEditRow("+Container.VisibleIndex+"); " %>'>Edit</a>
                                                                <a href="#" onclick='if(confirm("Confirm Delete"))  {<%# "grid_Cont.DeleteRow("+Container.VisibleIndex+");"  %>}' style='display: <%# Eval("canChange")%>'>Delete</a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="1" FieldName="ContainerNo" Caption="Container No"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="ContainerType" Caption="Container Type"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="3" FieldName="SealNo" Caption="Seal No"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="4" FieldName="StatusCode" Caption="Status"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="5" FieldName="Weight" Caption="Weight"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="6" FieldName="Volume" Caption="Volume"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="7" FieldName="Qty" Caption="Qty"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="8" FieldName="PackageType" Caption="PackageType"></dxwgv:GridViewDataColumn>
                                                    </Columns>
                                                    <Templates>
                                                        <EditForm>
                                                            <div style="display: none">
                                                                <dxe:ASPxTextBox ID="txt_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                                                            </div>
                                                            <table>
                                                                <tr>
                                                                    <td>ContainerNo</td>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="btn_ContNo" ClientInstanceName="btn_ContNo" runat="server" Text='<%# Bind("ContainerNo") %>' AutoPostBack="False" Width="165">
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupContainer(btn_ContNo,txt_ContType);
                                                                        }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td>SealNo</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_SealNo" runat="server" Text='<%# Bind("SealNo") %>' Width="165"></dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>ContainerType</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="cbbContType" ClientInstanceName="txt_ContType" runat="server" Width="165" DataSourceID="dsContType" ValueField="containerType" TextField="containerType" Value='<%# Bind("ContainerType") %>'></dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Weight
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="165"
                                                                            ID="spin_Wt" Height="21px" Value='<%# Bind("Weight")%>' DecimalPlaces="3" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Volume
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="165"
                                                                            ID="spin_M3" Height="21px" Value='<%# Bind("Volume")%>' DecimalPlaces="3" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Qty</td>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit runat="server" Width="70"
                                                                                        ID="spin_Pkgs" Height="21px" Value='<%# Bind("Qty")%>' NumberType="Integer" Increment="0" DisplayFormatString="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxButtonEdit ID="txt_PkgsType" ClientInstanceName="txt_PkgsType" runat="server"
                                                                                        Width="90" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("PackageType")%>'>
                                                                                        <Buttons>
                                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                        </Buttons>
                                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupUom(txt_PkgsType,2);
                                                                    }" />
                                                                                    </dxe:ASPxButtonEdit>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>ReqeustDate</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_Cont_Request" runat="server" Width="165" Value='<%# Bind("RequestDate")%>'
                                                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td>ScheduleDate</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_Cont_Schedule" runat="server" Width="165" Value='<%# Bind("ScheduleDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy" ReadOnly="true">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td>DgClass</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="165" ID="txt_DgClass" Text='<%# Bind("DgClass")%>'></dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>CfsInDate</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_Cont_CfsIn" runat="server" Width="165" Value='<%# Bind("CfsInDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td>CfsOutDate</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_Cont_CfsOut" runat="server" Width="165" Value='<%# Bind("CfsOutDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td>PortnetStatus</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="ASPxComboBox1" runat="server" Value='<%# Bind("PortnetStatus") %>' DropDownStyle="DropDown" Width="165"></dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>YardPickupDate</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_Cont_YardPickup" runat="server" Width="165" Value='<%# Bind("YardPickupDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td>YardReturnDate</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_Cont_YardReturn" runat="server" Width="165" Value='<%# Bind("YardReturnDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td>StatusCode</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="cbb_StatusCode" runat="server" Width="165" Value='<%# Bind("StatusCode") %>' ReadOnly="true">
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="New" Text="New" />
                                                                                <dxe:ListEditItem Value="Scheduled" Text="Scheduled" />
                                                                                <dxe:ListEditItem Value="InTransit" Text="InTransit" />
                                                                                <dxe:ListEditItem Value="Completed" Text="Completed" />
                                                                                <dxe:ListEditItem Value="Canceled" Text="Canceled" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>F5Ind</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="cbb_F5Ind" runat="server" Width="165" Value='<%# Bind("F5Ind") %>'>
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="Y" Text="Y" />
                                                                                <dxe:ListEditItem Value="N" Text="N" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td>UrgentInd</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="cbb_UrgentInd" runat="server" Width="165" Value='<%# Bind("UrgentInd") %>'>
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="Y" Text="Y" />
                                                                                <dxe:ListEditItem Value="N" Text="N" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Remark</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxTextBox ID="txt_ContRemark" runat="server" Text='<%# Bind("Remark") %>' Width="100%"></dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6">
                                                                        <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                            <span style="float: right">&nbsp
                                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                            </span>
                                                                            <span style='float: right; display: <%# Eval("canChange")%>'>
                                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                            </span>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EditForm>
                                                    </Templates>
                                                </dxwgv:ASPxGridView>
                                            </div>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Trip">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <div>
                                                <dxwgv:ASPxGridView ID="grid_Trip" ClientInstanceName="grid_Trip" runat="server" Width="850px" DataSourceID="dsTrip" KeyFieldName="Id" OnInit="grid_Trip_Init" AutoGenerateColumns="False" OnBeforePerformDataSelect="grid_Trip_BeforePerformDataSelect" OnHtmlEditFormCreated="grid_Trip_HtmlEditFormCreated" OnRowInserting="grid_Trip_RowInserting" OnInitNewRow="grid_Trip_InitNewRow" OnRowDeleting="grid_Trip_RowDeleting" OnRowUpdating="grid_Trip_RowUpdating" OnRowDeleted="grid_Trip_RowDeleted" OnRowInserted="grid_Trip_RowInserted" OnRowUpdated="grid_Trip_RowUpdated">
                                                    <SettingsPager PageSize="100" />
                                                    <SettingsEditing Mode="EditForm" />
                                                    <SettingsBehavior ConfirmDelete="true" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="7%">
                                                            <DataItemTemplate>
                                                                <a href="#" onclick='<%# "grid_Trip.StartEditRow("+Container.VisibleIndex+"); " %>'>View</a>
                                                                
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="ContainerNo" Caption="Container No"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="3" FieldName="DriverCode" Caption="Driver"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="4" FieldName="TowheadCode" Caption="PrimeMover"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="5" FieldName="ChessisCode" Caption="Trailer"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="6" FieldName="FromTime" Caption="Time">
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="7" FieldName="ToTime" Caption="Time">
                                                        </dxwgv:GridViewDataColumn>
                                                    </Columns>
                                                    <Templates>
                                                        <EditForm>
                                                            <div style="display: none">
                                                                <dxe:ASPxLabel ID="lb_tripId" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>
                                                                <dxe:ASPxTextBox ID="dde_Trip_ContId" ClientInstanceName="dde_Trip_ContId" runat="server" Text='<%# Bind("Det1Id") %>'></dxe:ASPxTextBox>
                                                            </div>
                                                            <table>
                                                                <tr>
                                                                    <td>Container No
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxDropDownEdit ID="dde_Trip_ContNo" runat="server" ClientInstanceName="dde_Trip_ContNo"
                                                                            Text='<%# Bind("ContainerNo") %>' Width="165" AllowUserInput="false">
                                                                            <DropDownWindowTemplate>
                                                                                <dxwgv:ASPxGridView ID="gridPopCont" runat="server" AutoGenerateColumns="False" ClientInstanceName="gridPopCont"
                                                                                    Width="300px" KeyFieldName="Id" OnCustomJSProperties="gridPopCont_CustomJSProperties">
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
                                                                    <td>Cfs</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="btn_CfsCode" ClientInstanceName="btn_CfsCode" runat="server" Text='<%# Bind("CfsCode") %>' AutoPostBack="False" Width="165"></dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>Bay</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="cbb_Trip_BayCode" runat="server" Value='<%# Bind("BayCode") %>' Width="165" EnableIncrementalFiltering="True">
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="B1" Text="B1" />
                                                                                <dxe:ListEditItem Value="B2" Text="B2" />
                                                                                <dxe:ListEditItem Value="B3" Text="B3" />
                                                                                <dxe:ListEditItem Value="B4" Text="B4" />
                                                                                <dxe:ListEditItem Value="B5" Text="B5" />
                                                                                <dxe:ListEditItem Value="B6" Text="B6" />
                                                                                <dxe:ListEditItem Value="B7" Text="B7" />
                                                                                <dxe:ListEditItem Value="B8" Text="B8" />
                                                                                <dxe:ListEditItem Value="B9" Text="B9" />
                                                                                <dxe:ListEditItem Value="B10" Text="B10" />
                                                                                <dxe:ListEditItem Value="B11" Text="B11" />
                                                                                <dxe:ListEditItem Value="B12" Text="B12" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Driver</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="btn_DriverCode" ClientInstanceName="btn_DriverCode" runat="server" Text='<%# Bind("DriverCode") %>' AutoPostBack="False" Width="165"></dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>PrimeMover</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="btn_TowheadCode" ClientInstanceName="btn_TowheadCode" runat="server" Text='<%# Bind("TowheadCode") %>' AutoPostBack="False" Width="165"></dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>Trail</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="btn_ChessisCode" ClientInstanceName="btn_ChessisCode" runat="server" Text='<%# Bind("ChessisCode") %>' AutoPostBack="False" Width="165"></dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Sublet</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="cbb_Trip_SubletFlag" runat="server" Value='<%# Bind("SubletFlag") %>' Width="165">
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="Y" Text="Y" />
                                                                                <dxe:ListEditItem Value="N" Text="N" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td>Sublet Haulier</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_SubletHauliername" runat="server" Width="165" Text='<%# Bind("SubletHauliername") %>'></dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>Trip Code</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="cbb_Trip_TripCode" runat="server" Value='<%# Bind("TripCode") %>' Width="165" DropDownStyle="DropDown" DataSourceID="dsTripCode" ValueField="Code" TextField="Code">
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <%--<td>ToDate</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_Trip_toDate" runat="server" Value='<%# Bind("ToDate") %>' Width="165"></dxe:ASPxDateEdit>
                                                                    </td><td>FromDate</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_Trip_fromDate" runat="server" Value='<%# Bind("FromDate") %>' Width="165"></dxe:ASPxDateEdit>
                                                                    </td>--%>
                                                                </tr>
                                                                <tr>
                                                                    <td>Remark</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="txt_Trip_Remark" ClientInstanceName="txt_Trip_Remark" runat="server" Text='<%# Bind("Remark") %>' Width="426">
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                    <td>Status</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="cbb_Trip_StatusCode" runat="server" Value='<%# Bind("Statuscode") %>' Width="165">
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="U" Text="Use" />
                                                                                <dxe:ListEditItem Value="S" Text="Start" />
                                                                                <dxe:ListEditItem Value="D" Text="Doing" />
                                                                                <dxe:ListEditItem Value="W" Text="Waiting" />
                                                                                <dxe:ListEditItem Value="P" Text="Pending" />
                                                                                <dxe:ListEditItem Value="C" Text="Completed" />
                                                                                <dxe:ListEditItem Value="X" Text="Cancel" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>From</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="txt_Trip_FromCode" runat="server" Text='<%# Bind("FromCode") %>' Width="426"></dxe:ASPxMemo>
                                                                    </td>
                                                                    <td>Time</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_Trip_fromTime" runat="server" Text='<%# Bind("FromTime") %>' Width="165">
                                                                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                            <ValidationSettings ErrorDisplayMode="None" />
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>To</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="txt_Trip_ToCode" runat="server" Text='<%# Bind("ToCode") %>' Width="426"></dxe:ASPxMemo>
                                                                    </td>
                                                                    <td>Time</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_Trip_toTime" runat="server" Text='<%# Bind("ToTime") %>' Width="165">
                                                                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                            <ValidationSettings ErrorDisplayMode="None" />
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6">
                                                                        <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                            <span style="float: right">&nbsp
                                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                            </span>
                                                                            
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EditForm>
                                                    </Templates>
                                                </dxwgv:ASPxGridView>
                                            </div>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="TripLog">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <div>
                                                <dxwgv:ASPxGridView ID="grid_TripLog" ClientInstanceName="grid_TripLog" runat="server" Width="850" DataSourceID="dsTripLog" KeyFieldName="Id" AutoGenerateColumns="false" OnInit="grid_TripLog_Init" OnBeforePerformDataSelect="grid_TripLog_BeforePerformDataSelect" OnInitNewRow="grid_TripLog_InitNewRow" OnRowInserting="grid_TripLog_RowInserting" OnRowUpdating="grid_TripLog_RowUpdating" OnRowDeleting="grid_TripLog_RowDeleting">
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                    <SettingsEditing Mode="Inline" />
                                                    <SettingsBehavior ConfirmDelete="true" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn FieldName="Driver" Caption="Driver" Width="130">
                                                            <EditItemTemplate>
                                                                <dxe:ASPxButtonEdit ID="btn_TripLog_DriverCode" ClientInstanceName="btn_TripLog_DriverCode" runat="server" Text='<%# Bind("Driver") %>' AutoPostBack="False" Width="100%">
                                                                    <Buttons>
                                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                    </Buttons>
                                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_Driver(btn_TripLog_DriverCode,null,null);
                                                                        }" />
                                                                </dxe:ASPxButtonEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="Status" Caption="Status" Width="100">
                                                            <PropertiesComboBox>
                                                                <Items>
                                                                    <dxe:ListEditItem Value="U" Text="Use" />
                                                                    <dxe:ListEditItem Value="S" Text="Start" />
                                                                    <dxe:ListEditItem Value="D" Text="Doing" />
                                                                    <dxe:ListEditItem Value="W" Text="Waiting" />
                                                                    <dxe:ListEditItem Value="P" Text="Pending" />
                                                                    <dxe:ListEditItem Value="C" Text="Completed" />
                                                                    <dxe:ListEditItem Value="X" Text="Cancel" />
                                                                </Items>
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataDateColumn FieldName="LogDate" Caption="Date" PropertiesDateEdit-EditFormatString="dd/MM/yyyy" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" Width="120"></dxwgv:GridViewDataDateColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="LogTime" Caption="Time" Width="80">
                                                            <PropertiesTextEdit>
                                                                <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                <ValidationSettings ErrorDisplayMode="None" />
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="Remark" Caption="Remark" MinWidth="150"></dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                </dxwgv:ASPxGridView>

                                            </div>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                
                                <dxtc:TabPage Text="Attachments" Name="Attachments" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl8" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton6" Width="150" runat="server" Text="Upload Attachments"
                                                            Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' AutoPostBack="false"
                                                            UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s,e) {
                                                                isUpload=true;
                                                        PopupUploadPhoto();
                                                        }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_Refresh" runat="server" Text="Refresh" AutoPostBack="false"
                                                            UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s,e) {
                                                        grd_Photo.Refresh();
                                                        }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                            <dxwgv:ASPxGridView ID="grd_Photo" ClientInstanceName="grd_Photo" runat="server" DataSourceID="dsJobPhoto"
                                                KeyFieldName="Id" Width="100%" EnableRowsCache="False" OnBeforePerformDataSelect="grd_Photo_BeforePerformDataSelect"
                                                AutoGenerateColumns="false" OnRowDeleting="grd_Photo_RowDeleting" OnInit="grd_Photo_Init" OnInitNewRow="grd_Photo_InitNewRow" OnRowUpdating="grd_Photo_RowUpdating">
                                                <Settings />
                                                <Columns>
                                                    <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40px">
                                                        <DataItemTemplate>
                                                            <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                Enabled='<%# SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE" %>' ClientSideEvents-Click='<%# "function(s) { grd_Photo.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                            </dxe:ASPxButton>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn Caption="#" Width="40px">
                                                        <DataItemTemplate>
                                                            <dxe:ASPxButton ID="btn_mkg_del" runat="server"
                                                                Text="Delete" Width="40" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE" %>' ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grd_Photo.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                            </dxe:ASPxButton>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn Caption="Photo" Width="100px">
                                                        <DataItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <a href='<%# Eval("Path")%>' target="_blank">
                                                                            <dxe:ASPxImage ID="ASPxImage1" Width="80" Height="80" runat="server" ImageUrl='<%# Eval("ImgPath") %>'>
                                                                            </dxe:ASPxImage>
                                                                        </a>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="FileName" FieldName="FileName" Width="200px"></dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="FileNote"></dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                                <Templates>
                                                    <EditForm>
                                                        <div style="display: none">
                                                            <dxe:ASPxTextBox ID="txt_PhotoId" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                                                        </div>
                                                        <table width="100%">
                                                            <tr>
                                                                <td>Remark
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxMemo ID="txt_Rmk" runat="server" Rows="4" Width="600" Text='<%# Bind("FileNote") %>'>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                        <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateMkgs" ReplacementType="EditFormUpdateButton"
                                                                            runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                        <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                                            runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                    </div>
                                                                </td>
                                                            </tr>

                                                        </table>
                                                    </EditForm>
                                                </Templates>
                                            </dxwgv:ASPxGridView>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                            </TabPages>
                        </dxtc:ASPxPageControl>
                    </EditForm>
                </Templates>

            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="570"
                Width="900" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="popubCtrPic" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtrPic"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="570"
                Width="900" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
                    if(grd_Photo!=null)
                        grd_Photo.Refresh();
      
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
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
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
