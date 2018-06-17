<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssignContainerList.aspx.cs" Inherits="Modules_Freight_Job_AssignContainerList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script src="../script/jquery.js"></script>
    <script src="../script/firebase.js"></script>
    <script src="../script/js_company.js"></script>
    <script src="../script/js_firebase.js"></script>
    <script type="text/javascript">
        var loading = {
            show: function () {
                $("#div_tc").css("display", "block");
            },
            hide: function () {
                $("#div_tc").css("display", "none");
            }
        }
        var config = {
            timeout: 0,
            gridview: 'grid',
        }
        function assign_one(rowIndex) {
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid.GetValuesOnCustomCallback('Assignline_' + rowIndex, assign_one_inline_callback);
            }, config.timeout);
        }
        function assign_one_inline_callback(res) {
            popubCtr.SetHeaderText('Import Cargo');
            popubCtr.SetContentUrl('/Modules/Freight/SelectPages/SelectAssignedCargo.aspx?id=' + res);
            popubCtr.Show();
        }
        function OnCallback(v) {
            if (v != null && v.indexOf("success") >= 0) {
                parent.notice(v, '', 'success');
                //alert(v);
                btn_search.OnClick(null, null);
            } else {
                //parent.notice('Successfully Assign Driver.', '', 'success');
                //alert("Successfully Assign Driver.")
                //

                btn_search.OnClick(null, null);

            }
        }
        function SelectAll() {
            if (btnSelect.GetText() == "Select All")
                btnSelect.SetText("UnSelect All");
            else
                btnSelect.SetText("Select All");
            jQuery("input[id*='ack_IsPay']").each(function () {
                this.click();
            });
        }
        function AfterPopub() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            btn_search.OnClick(null, null);
        }
        function save_inline(rowIndex) {
            console.log(rowIndex);
            loading.show();
            setTimeout(function () {
                grid.GetValuesOnCustomCallback('Saveline_' + rowIndex, save_list_inline_callback);
            }, config.timeout);
        }
        function edi_inline(rowIndex) {
            console.log(rowIndex);
            loading.show();
            setTimeout(function () {
                grid.GetValuesOnCustomCallback('Ediline_' + rowIndex, edi_list_inline_callback);
            }, config.timeout);
        }
        function save_list_inline_callback(res) {
            if (res.indexOf('Save Error') >= 0) {
                console.log('===========', res);
                //alert('Save Error');
                parent.notice(res, '', 'warn');
            } else {
                parent.notice('Save Successful', '', 'success');
            }
        }
        function edi_inline(rowIndex) {
            console.log(rowIndex);
            loading.show();
            setTimeout(function () {
                grid.GetValuesOnCustomCallback('Ediline_' + rowIndex, edi_list_inline_callback);
            }, config.timeout);
        }
        function edi_list_inline_callback(res) {
            if (res.indexOf('Edi Error') >= 0) {
                console.log('===========', res);
                //alert(res);
                parent.notice(res, '', 'warn');
            } else {
                parent.notice('Edi Successful', '', 'success');
                btn_search.OnClick(null, null);
            }
        }
        function OnCallbackBatch(v) {
            parent.notice(v, '', 'success');
            //alert(v);
            btn_search.OnClick(null, null);
        }

    </script>
    <script type="text/javascript" src="/Script/jquery.js" />
    <script type="text/javascript">
        $.noConflict();
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsContainer" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.Container" KeyMember="id" FilterExpression="" />
        <wilson:DataSource ID="dsContType" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.Container_Type" KeyMember="id" FilterExpression="" />
        <div style="display:none">
            <dxe:ASPxLabel ID="lbl_JobType" runat="server"></dxe:ASPxLabel>
            <dxe:ASPxLabel ID="lbl_CargoType" runat="server"></dxe:ASPxLabel>
        </div>
        <div>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Cont No" Width="50px"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_ContNo" runat="server" Width="140"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="Job No" Width="50px"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_jobNo" runat="server" Width="140"></dxe:ASPxTextBox>
                    </td>

                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="From"></dxe:ASPxLabel>
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

                </tr>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel8" runat="server" Text="Vessel"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Vessel" Width="140" runat="server"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Text="Clear Date"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_ClearDate" runat="server" Width="140" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                         <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="Clear Status"></dxe:ASPxLabel>
                    </td>
                    <td>
                       <dxe:ASPxComboBox ID="cmb_CustomsClearStatus" runat="server" Width="100">
                                <Items>
                                    <dxe:ListEditItem Value="" Text=""  />
                                    <dxe:ListEditItem Value="Y" Text="Y"  />
                                    <dxe:ListEditItem Value="N" Text="N" />
                                </Items>
                            </dxe:ASPxComboBox>
                    </td>
                    <td colspan="2">
                        <dxe:ASPxButton ID="btn_search" ClientInstanceName="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                     <td style="display:none">
                        <dxe:ASPxButton ID="ASPxButton3" ClientInstanceName="btnSelect" runat="server" Text="Select All" Width="100" AutoPostBack="False"
                            UseSubmitBehavior="False">
                            <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td  style="display:none">
                        <dxe:ASPxButton ID="btn_assign1" runat="server" Text="Save" AutoPostBack="false" Width="100"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                        grid.GetValuesOnCustomCallback('WHS-MT',OnCallback);
                        }" />
                        </dxe:ASPxButton>
                    </td>
                   
                     <td  style="display:none">
                        <dxe:ASPxButton ID="btn_Export" runat="server" Text="Export" AutoPostBack="false" Width="100"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                        grid.GetValuesOnCustomCallback('Export',OnCallback);
                        }" />
                        </dxe:ASPxButton>
                    </td>
                    <td  style="display:none">
                        <dxe:ASPxButton ID="btn_Import" runat="server" Text="OK" AutoPostBack="false" Width="100"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                        grid.GetValuesOnCustomCallback('OK',OnCallback);
                        }" />
                        </dxe:ASPxButton>
                    </td>
                     <td style="display:none">
                        <dxe:ASPxButton ID="btn_assign2" runat="server" Text="WHS-LD" AutoPostBack="false" Width="100"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                        grid.GetValuesOnCustomCallback('WHS-LD',OnCallback);
                        }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server"
                KeyFieldName="Id" Width="1000px" AutoGenerateColumns="False" OnCustomDataCallback="grid_CustomDataCallback">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsBehavior ConfirmDelete="true" />
                <SettingsEditing Mode="Inline" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataColumn Caption="#" FieldName="Id" Width="10" VisibleIndex="0"  SortOrder="Ascending" SortIndex="0">
                        <DataItemTemplate>
                            <%--<dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                            </dxe:ASPxCheckBox>--%>
                            <dxe:ASPxButton ID="ASPxButton24" runat="server" Text="Save" Width="40" AutoPostBack="false"
                                ClientSideEvents-Click='<%# "function(s) { save_inline("+Container.VisibleIndex+") }"  %>'>
                            </dxe:ASPxButton>
                            <div style="display: none">
                                <dxe:ASPxLabel ID="lbl_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>
                                <dxe:ASPxLabel ID="lbl_JobId" runat="server" Text='<%# Eval("JobId") %>'></dxe:ASPxLabel>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="#" FieldName="Id" Width="10" VisibleIndex="0" SortOrder="Ascending" SortIndex="0">
                        <DataItemTemplate>
                            <div style="display: <%# SafeValue.SafeString(Eval("JobType"))=="EXP"&& IsEdi(Eval("JobNo"))=="N"?"block":"none" %>">
                                <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="EDI" Width="40" AutoPostBack="false"
                                    ClientSideEvents-Click='<%# "function(s) { edi_inline("+Container.VisibleIndex+") }"  %>'>
                                </dxe:ASPxButton>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="#" FieldName="JobNo" Width="140px" VisibleIndex="0">
                        <DataItemTemplate>
                            <div style="min-width: 140px;">
                                <a href='javascript: parent.navTab.openTab("<%# Eval("JobNo") %>","/PagesContTrucking/Job/JobEdit.aspx?no=<%# Eval("JobNo") %>",{title:"<%# Eval("JobNo") %>", fresh:false, external:true});'><%# Eval("JobNo") %></a>
                            </div>
                            <div style="display:none">
                                    <dxe:ASPxLabel ID="lbl_JobNo" runat="server" Text='<%# Eval("JobNo") %>'></dxe:ASPxLabel>
                                </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Cont No" FieldName="ContainerNo" VisibleIndex="3" Width="120">
                        <DataItemTemplate>
                            <div style="display: <%# SafeValue.SafeString(Eval("JobType"))=="EXP"?"block":"none" %>">
                                <dxe:ASPxComboBox ID="cbbContainer" ClientInstanceName="cbbContainer" EnableIncrementalFiltering="True" DropDownStyle="DropDown" runat="server" Width="120px" DataSourceID="dsContainer" ValueField="ContainerNo" ViewStateMode="Disabled" TextField="ContainerNo" Value='<%# Eval("ContainerNo") %>'>
                                    
                                </dxe:ASPxComboBox>
                                <div style="display:none">
                                    <dxe:ASPxLabel ID="lbl_ContIndex" runat="server" Text='<%# Eval("ContainerNo") %>'></dxe:ASPxLabel>
                                </div>
                            </div>
                            <div style="display: <%# SafeValue.SafeString(Eval("JobType"))=="IMP"?"block":"none" %>">
                                <%# Eval("ContainerNo") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="Seal No" FieldName="SealNo" VisibleIndex="3" Width="120">
                         <DataItemTemplate>
                             <div style="display: <%# SafeValue.SafeString(Eval("JobType"))=="EXP"?"block":"none" %>">
                                 <dxe:ASPxTextBox ID="txt_SealNo" Width="120" runat="server" Text='<%# Eval("SealNo") %>'>
                                 </dxe:ASPxTextBox>
                             </div>
                             <div style="display: <%# SafeValue.SafeString(Eval("JobType"))=="IMP"?"block":"none" %>">
                                 <%# Eval("SealNo") %>
                             </div>
                         </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Cont Type" FieldName="ContainerType" VisibleIndex="3" Width="80">
                        <DataItemTemplate>
                            <div style="display: <%# SafeValue.SafeString(Eval("JobType"))=="EXP"?"block":"none" %>">
                                <dxe:ASPxComboBox ID="cbbContType" ClientInstanceName="txt_ContType" runat="server" Width="80" DataSourceID="dsContType" ValueField="containerType" TextField="containerType" Value='<%# Bind("ContainerType") %>'></dxe:ASPxComboBox>
                            </div>
                            <div style="display: <%# SafeValue.SafeString(Eval("JobType"))=="IMP"?"block":"none" %>">
                                <%# Eval("ContainerType") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="Schedule Date" FieldName="ScheduleDate" VisibleIndex="3" Width="120">
                        <DataItemTemplate>
                            <%--<%# SafeValue.SafeDateStr(Eval("TptDate")) %>--%>
                            <dxe:ASPxDateEdit ID="date_ScheduleDate" runat="server" Width="120px" Value='<%# Eval("ScheduleDate") %>'
                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </dxe:ASPxDateEdit>
                        </DataItemTemplate>
                     </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Customs Clear Date" FieldName="CustomsClearDate" VisibleIndex="3" Width="120">
                        <DataItemTemplate>
                            <%--<%# SafeValue.SafeDateStr(Eval("TptDate")) %>--%>
                            <dxe:ASPxDateEdit ID="date_CustomsClearDate" runat="server" Width="120px" Value='<%# Eval("CustomsClearDate") %>'
                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </dxe:ASPxDateEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Customs Clear Status" FieldName="CustomsClearStatus" HeaderStyle-Wrap="True" VisibleIndex="3" Width="60">
                        <DataItemTemplate>
                            <dxe:ASPxComboBox ID="cmb_CustomsClearStatus" runat="server" Text='<%# Eval("CustomsClearStatus") %>' Width="50">
                                <Items>
                                    <dxe:ListEditItem Value="Y" Text="Y" Selected="true" />
                                    <dxe:ListEditItem Value="N" Text="N" />
                                </Items>
                            </dxe:ASPxComboBox>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Verify Ind" FieldName="CustomsVerifyInd" VisibleIndex="3" Width="120">
                        <DataItemTemplate>
                            <dxe:ASPxComboBox ID="cmb_CustomsVerifyInd" runat="server" Width="50" Value='<%# Bind("CustomsVerifyInd") %>'>
                                <Items>
                                    <dxe:ListEditItem Value="Y" Text="Y" />
                                    <dxe:ListEditItem Value="N" Text="N" />
                                </Items>
                            </dxe:ASPxComboBox>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Customs Verify Date" FieldName="CustomsVerifyDate" VisibleIndex="3" Width="60">
                        <DataItemTemplate>
                            <dxe:ASPxDateEdit ID="date_CustomsVerifyDate" runat="server" Width="120px" Value='<%# Eval("CustomsVerifyDate") %>'
                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </dxe:ASPxDateEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Customs Remark" FieldName="CustomsRemark" VisibleIndex="3" Width="120">
                        <DataItemTemplate>
                            <dxe:ASPxMemo ID="memo_CustomsRemark" Rows="3" runat="server" Text='<%# Bind("CustomsRemark") %>' Width="100%">
                            </dxe:ASPxMemo>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <Settings ShowFooter="True" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="ContNo" SummaryType="Count" DisplayFormat="{0}" />
                </TotalSummary>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="500"
                Width="1050" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      
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
