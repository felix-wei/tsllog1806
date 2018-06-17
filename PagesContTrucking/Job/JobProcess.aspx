<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobProcess.aspx.cs" Inherits="Modules_Tpt_WarehouseList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript">
        function goJob(jobno) {
            //window.location = "JobEdit.aspx?jobNo=" + jobno;
            parent.navTab.openTab(jobno, "/PagesContTrucking/Job/JobEdit.aspx?jobNo=" + jobno, { title: jobno, fresh: false, external: true });
        }
        function NewAdd_Visible(doShow) {
            if (doShow) {
                var t = new Date();
                txt_new_JobDate.SetText(t);
                cbb_new_jobtype.SetValue("");
                btn_new_ClientId.SetText('ELOG');
                txt_new_ClientName.SetText('');
                txt_FromAddress.SetText('');
                txt_WarehouseAddress.SetText('');
                txt_ToAddress.SetText('');
                txt_DepotAddress.SetText('');
                txt_new_remark.SetText('');

                ASPxPopupClientControl.Show();

            }
            else {
                ASPxPopupClientControl.Hide();
            }
        }
        function NewAdd_Save() {
            var jobType = cbb_new_jobtype.GetValue();
            //console.log(jobType);
            if (jobType && jobType.length > 0) {
                detailGrid.GetValuesOnCustomCallback('OK', OnSaveCallBack);
            } else {
                alert('Require JobType!');
            }
        }
        function OnSaveCallBack(v) {
            if (v != null && v.indexOf("Fail") > -1) {
                alert(v);
            }
            else {
                if (v != null) {
                    //parent.navTab.openTab(v, "/Warehouse/Job/JobEdit.aspx?no=" + v, { title: v, fresh: false, external: true });
                    goJob(v);
                    ASPxPopupClientControl.Hide();
                }
            }
        }
        function AfterAddTrip() {
            popubCtr1.Hide();
            popubCtr1.SetContentUrl('about:blank');
        }
        function cbb_checkbox_Type(name) {
            //console.log('========= checkbox', name);
            if (name == 'ALL') {
                cb_ContStatus1.SetValue(cb_ContStatus0.GetValue());
                cb_ContStatus2.SetValue(cb_ContStatus0.GetValue());
                cb_ContStatus3.SetValue(cb_ContStatus0.GetValue());
            } else {
                if (cb_ContStatus1.GetValue() && cb_ContStatus2.GetValue() && cb_ContStatus3.GetValue()) {
                    cb_ContStatus0.SetValue(true);
                } else {
                    cb_ContStatus0.SetValue(false);
                }
            }
        }
        function cbb_checkbox_Type1(name) {
            //console.log('========= checkbox', name);
            if (name == 'Uncomplete') {
                if (cb_ContStatus4.GetValue()) {
                    cb_ContStatus1.SetValue(false);
                    cb_ContStatus2.SetValue(false);
                    cb_ContStatus3.SetValue(false);
                }
            } else {
                if (cb_ContStatus1.GetValue() || cb_ContStatus2.GetValue() || cb_ContStatus3.GetValue()) {
                    cb_ContStatus4.SetValue(false);
                }
            }
        }
        function rowStatusChange(s, e, status) {
            var ar_id = s.uniqueID.split('$');
            if (ar_id.length == 3) {
                var ar_temp = ar_id[1].replace('cell', '').split('_');
                console.log(ar_temp[0] + '_' + status);
                detailGrid.GetValuesOnCustomCallback('UpdateInlineStatus_' + ar_temp[0] + '_' + status, rowStatusChange_callback);
            } else {
                alert('Save Error');
            }
        }
        function rowStatusChange_callback(res) {
            //console.log(res, btn_search);
            if (res.indexOf('successful') >= 0) {
                btn_search.OnClick();
            } else {
                alert(res);
            }
        }
        function PopupLotNo(txtId, txtName) {
            clientId = txtId;
            clientName = txtName;
            popubCtr.SetHeaderText('Lot No');
            popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/SelectLotNo.aspx');
            popubCtr.Show();
        }
    </script>
    <style type="text/css">
        a:hover {
            color: black;
        }

        .link a:link {
            color: red;
        }

        .link a:hover {
            color: red;
        }

        .none a:link {
        }


        .a_ltrip span {
            display: inline-block;
            border: 1px solid #e8e8e8;
            box-sizing: border-box;
            color: white;
            padding: 2px;
            width: 33px;
            height: 21px;
            overflow: hidden;
            white-space: nowrap;
            text-align: center;
            margin: 2px;
            /*margin-top:2px;
            margin-left:2px;
            margin-bottom:2px;
            margin-right:4px;*/
        }

        .a_ltrip .S {
            background-color: green;
        }

        .a_ltrip .X {
            background-color: gray;
        }

        .a_ltrip .C {
            background-color: blue;
        }

        .a_ltrip .P {
            background-color: orange;
        }


        .a_ltrip .div_FixWith {
            min-width: 112px;
        }

        .div_contStatus {
            width: 80px;
            height: 21px;
            text-align: center;
            border: 1px solid #e8e8e8;
            box-sizing: border-box;
            color: white;
            padding-top: 2px;
        }

        .div_hr {
            width: 30px;
            height: 21px;
            text-align: center;
            border: 1px solid #e8e8e8;
            box-sizing: border-box;
            padding-top: 2px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="lbl1" runat="server" Text="Job No"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_jobNo" runat="server" Width="100"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Plan Date From"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_search_dateFrom" runat="server" Width="100" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="To"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_search_dateTo" runat="server" Width="100" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" ClientInstanceName="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>

                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton2" ClientInstanceName="btn_AddNew" runat="server" Text="Add New" AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e){
                                detailGrid.AddNewRow();
                                }" />
                        </dxe:ASPxButton>

                    </td>
                </tr>
            </table>

            <wilson:DataSource ID="dsProcess" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.JobProcess" KeyMember="Id" />
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" OnCustomDataCallback="grid_Transport_CustomDataCallback" DataSourceID="dsProcess" OnInit="grid_Transport_Init"
                 OnRowInserting="grid_Transport_RowInserting" OnInitNewRow="grid_Transport_InitNewRow" OnBeforePerformDataSelect="grid_Transport_BeforePerformDataSelect" OnRowUpdating="grid_Transport_RowUpdating" OnRowDeleting="grid_Transport_RowDeleting">
                <SettingsBehavior ConfirmDelete="True" />
                <SettingsEditing Mode="Inline" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Settings ShowFooter="true" />
                <Columns>
                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="60px">
                        <EditButton Visible="true"></EditButton>
                        <DeleteButton Visible="true"></DeleteButton>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataColumn VisibleIndex="0" Caption="#">
                        <DataItemTemplate>
                            <div style="display: none">
                                <dxe:ASPxLabel ID="lb_Id" runat="server" Value='<%# Bind("Id") %>'></dxe:ASPxLabel>
                            </div>
                            <dxe:ASPxButton ID="btn_contStart" Width="60" Text="Start" runat="server" Visible='<%# (SafeValue.SafeString(Eval("ProcessStatus"))!="Started"&&(SafeValue.SafeString(Eval("ProcessStatus"))!="Completed")) %>' AutoPostBack="false">
                                <ClientSideEvents Click="function(s,e){rowStatusChange(s,e,'Started');}" />
                            </dxe:ASPxButton>
                            <dxe:ASPxButton ID="ASPxButton1" Width="60" Text="End" runat="server" Visible='<%# SafeValue.SafeString(Eval("ProcessStatus"))=="Started" %>' AutoPostBack="false">
                                <ClientSideEvents Click="function(s,e){rowStatusChange(s,e,'Completed');}" />
                            </dxe:ASPxButton>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Entry Date" FieldName="DateEntry" VisibleIndex="1">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("DateEntry")) %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxDateEdit ID="date_DateEntry" runat="server" Value='<%# Bind("DateEntry") %>' Width="120" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </dxe:ASPxDateEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Plan Date" FieldName="DatePlan" VisibleIndex="1">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("DatePlan")) %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxDateEdit ID="date_DatePlan" runat="server" Value='<%# Bind("DatePlan") %>' Width="120" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </dxe:ASPxDateEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Inspect Date" FieldName="DateInspect" VisibleIndex="1">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("DateInspect")) %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxDateEdit ID="date_DateInspect" runat="server" Value='<%# Bind("DateInspect") %>' Width="120" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </dxe:ASPxDateEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Process Date" FieldName="DateProcess" VisibleIndex="1">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("DateProcess")) %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxDateEdit ID="date_DateProcess" runat="server" Value='<%# Bind("DateProcess") %>' Width="120" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </dxe:ASPxDateEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn FieldName="ProcessType" Caption="Process Type" Width="50" VisibleIndex="1">
                        <PropertiesComboBox>
                            <Items>
                                <dxe:ListEditItem Text="" Value="" />
                                <dxe:ListEditItem Text="Inspection" Value="Inspection" />
                                <dxe:ListEditItem Text="Refurbish" Value="Refurbish" />
                                <dxe:ListEditItem Text="Painting" Value="Painting" />
                                <dxe:ListEditItem Text="Washing" Value="Washing" />
                                <dxe:ListEditItem Text="Others" Value="Others" />
                            </Items>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn FieldName="ProcessStatus" Caption="Process Status" Width="50" VisibleIndex="1">
                        <PropertiesComboBox>
                            <Items>
                                <dxe:ListEditItem Text="Pending" Value="Pending" />
                                <dxe:ListEditItem Text="Scheduled" Value="Scheduled" />
                                <dxe:ListEditItem Text="Started" Value="Started" />
                                <dxe:ListEditItem Text="Completed" Value="Completed" />
                                <dxe:ListEditItem Text="Cancelled" Value="Cancelled" />
                            </Items>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="2">
                        <EditItemTemplate>
                            <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="60"
                                ID="spin_Qty" ClientInstanceName="spin_Qty" Height="21px" Value='<%# Bind("Qty")%>' DecimalPlaces="3" Increment="0">
                                <SpinButtons ShowIncrementButtons="false" />
                            </dxe:ASPxSpinEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Inventory Id" FieldName="InventoryId" VisibleIndex="2" Width="100">
                         <EditItemTemplate>
                            <dxe:ASPxButtonEdit ID="btn_InventoryId" ClientInstanceName="btn_InventoryId" runat="server" Text='<%# Bind("InventoryId") %>' AutoPostBack="False" Width="100">
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupLotNo(btn_InventoryId,btn_LotNo);
                                                                        }" />
                            </dxe:ASPxButtonEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="LotNo" FieldName="LotNo" VisibleIndex="2" Width="180">
                        <EditItemTemplate>
                            <dxe:ASPxTextBox ID="btn_LotNo" ClientInstanceName="btn_LotNo" runat="server" Text='<%# Bind("LotNo") %>' AutoPostBack="False" Width="180">
                            </dxe:ASPxTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Pipe No" FieldName="PipeNo" VisibleIndex="2" Width="180">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Heat No" FieldName="HeatNo" VisibleIndex="2" Width="180">
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataTextColumn Caption="LocationCode" FieldName="LocationCode" VisibleIndex="2" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Process Qty" FieldName="ProcessQty1" VisibleIndex="2" Width="100">
                        <EditItemTemplate>
                            <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="60"
                                ID="spin_ProcessQty1" ClientInstanceName="spin_ProcessQty1" Height="21px" Value='<%# Bind("ProcessQty1")%>' DecimalPlaces="3" Increment="0">
                                <SpinButtons ShowIncrementButtons="false" />
                            </dxe:ASPxSpinEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Specs 1" FieldName="Specs1" Width="200px" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Specs 2" FieldName="Specs2" Width="200px" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Specs 3" FieldName="Specs3" Width="200px" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Specs 4" FieldName="Specs4" Width="200px" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark1" Width="200px" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Remark1" FieldName="Remark2" Width="200px" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="TotalVol" SummaryType="Sum" DisplayFormat="0.000" />
                    <dxwgv:ASPxSummaryItem FieldName="TotalWt" SummaryType="Sum" DisplayFormat="0.000" />
                    <dxwgv:ASPxSummaryItem FieldName="SkuQty" SummaryType="Sum" DisplayFormat="0.000" />
                </TotalSummary>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="400"
                Width="900" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr1"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="530"
                Width="980" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      document.getElementById('btn_search').click();
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid_Transport">
            </dxwgv:ASPxGridViewExporter>
        </div>
    </form>
</body>
</html>
