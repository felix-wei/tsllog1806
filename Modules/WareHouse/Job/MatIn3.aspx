<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MatIn3.aspx.cs" Inherits="WareHouse_Job_MatIn3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript">
        function ShowJob(masterId) {
            parent.navTab.openTab(masterId, "/Modules/WareHouse/Job/JobEdit.aspx?refN=" + masterId, { title: masterId, fresh: false, external: true });
        }
        function PrintSchedule() {
            window.open("/ReportJob/PrintJobSchedule.aspx?DateFrom=" + txt_from.GetText() + "&DateTo=" + txt_end.GetText());
        }
        function PopubCrews(jobNo, date, type) {
            popubCtr.SetHeaderText('Crews List');
            popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/SelectCrewsList.aspx?no=' + jobNo + '&date=' + date + "&type=" + type);
            popubCtr.Show();
        }
        function PopubMaterials(jobNo,company) {
            popubCtr.SetHeaderText('Materials List');
            popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/AddMaterials.aspx?no=' + jobNo + '&type=IN3&company=' + company);
            popubCtr.Show();
        }
        function AfterPopubMultiInv() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            if (grid != null) {
                grid.Refresh();
            }
            //document.location.reload();
        }
        function AfterPopubMultiInv1() {
            popubCtr1.Hide();
            popubCtr1.SetContentUrl('about:blank');
            grid.Refresh();

            //document.location.reload();
        }
        function OnScheduleCallback() {
            document.location.reload();
        }
        function OnCopyClick(id) {
            grid.GetValuesOnCustomCallback('Copy' + id, OnScheduleCallback);
        }
    </script>
		 <style media="print">
	.noprint {display:none;}
	.doprint {font-size:10pt;}
	.onlyprint {font-size:10pt;}

	</style>	
</head>
<body>
    <form id="form1" runat="server">
                    <wilson:DataSource ID="dsJobSchedule" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobSchedule"
                KeyMember="Id" FilterExpression="1=0" />
        <div>
            <table class="noprint">
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="Label1" runat="server" Text=" Date :">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                        <div style="display: none">
                            <dxe:ASPxDateEdit ID="txt_date" ClientInstanceName="txt_date" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </dxe:ASPxDateEdit>
                        </div>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="Label2" runat="server" Text="To">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                        <div style="display: none">
                            <dxe:ASPxDateEdit ID="txt_date2" ClientInstanceName="txt_date2" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </dxe:ASPxDateEdit>
                        </div>
                    </td>
                      <td>
                        <dxe:ASPxButton ID="btn_search" Width="120" ClientInstanceName="btn_search" runat="server" Text="Search" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                     <td>
                        <dxe:ASPxButton ID="btn_Add" Width="120px" runat="server" Text="Add New"
                            AutoPostBack="False" UseSubmitBehavior="False">
                            <ClientSideEvents Click="function(s, e) {
                                 grid.AddNewRow();
                                    }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
             <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsJobSchedule" OnRowUpdating="grid_RowUpdating" OnRowInserting="grid_RowInserting" OnInitNewRow="grid_InitNewRow"
                KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" OnRowDeleting="grid_RowDeleting" OnCustomDataCallback="grid_CustomDataCallback" OnInit="grid_Init">
                <SettingsEditing Mode="Inline" />
				<SettingsBehavior AllowSort="false" AllowGroup="false" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                     <dxwgv:GridViewCommandColumn Caption="#" VisibleIndex="0" Width="40">
                        <EditButton Visible="true" Text="Edit"></EditButton>
                         <DeleteButton Visible="false" Text="Delete"></DeleteButton>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="No" FieldName="RefNo" VisibleIndex="1" Width="100" ReadOnly="true">
                        <DataItemTemplate>
                            <dxe:ASPxLabel runat="server" ID="txt_ReNo" Text='<%# Eval("RefNo") %>' Width="100"></dxe:ASPxLabel>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxTextBox runat="server" ID="txt_ReNo" Text='<%# Bind("RefNo") %>' Width="100" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Schedule Date" FieldName="JobDate" VisibleIndex="2" Width="80">
                       <PropertiesDateEdit Width="100" DisplayFormatString="dd/MM/yyyy" EditFormat="DateTime" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Time" FieldName="MoveDate" VisibleIndex="2" Width="120">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDate(Eval("MoveDate"),DateTime.Today).ToString("HH:mm")%>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxTimeEdit ID="date_Time" Width="80" runat="server" Value='<%# Bind("MoveDate") %>'
                                EditFormat="Time" EditFormatString="HH:mm" DisplayFormatString="HH:mm">
                            </dxe:ASPxTimeEdit>
                        </EditItemTemplate>
                        <PropertiesDateEdit Width="60" DisplayFormatString="HH:mm" EditFormat="Time" EditFormatString="HH:mm"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                     <dxwgv:GridViewDataComboBoxColumn Caption="Company Name" FieldName="CustomerName" VisibleIndex="2" Width="100">
                        <PropertiesComboBox Width="100">
                            <Items>
                                <dxe:ListEditItem Text="SPJ" Value="SPJ" />
                                <dxe:ListEditItem Text="MST" Value="MST" />
                            </Items>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Suppiler" FieldName="OriginAdd" VisibleIndex="2" Width="120">
                          <DataItemTemplate>
                            <%# Eval("OriginAdd") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_Origin" runat="server" Text='<%# Bind("OriginAdd") %>' Rows="2" Width="120"></dxe:ASPxMemo>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Status" FieldName="Value1" VisibleIndex="2" Width="100">
                        <PropertiesComboBox Width="100">
                            <Items>
                                <dxe:ListEditItem Text="Pending" Value="Pending" />
                                <dxe:ListEditItem Text="Billing" Value="Billing" />
                                <dxe:ListEditItem Text="Paid" Value="Paid" />
                                <dxe:ListEditItem Text="Cancel" Value="Cancel" />
                            </Items>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Inv No" FieldName="Value2" VisibleIndex="2" Width="120">
                          <DataItemTemplate>
                            <%# Eval("Value2") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_Value2" runat="server" Text='<%# Bind("Value2") %>' Rows="2" Width="120"></dxe:ASPxMemo>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataDateColumn Caption="Inv Date" FieldName="DateTime2" VisibleIndex="2" Width="80">
                       <PropertiesDateEdit Width="100" DisplayFormatString="dd/MM/yyyy" EditFormat="DateTime" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Do No" FieldName="Value3" Width="80" VisibleIndex="2">
                        <DataItemTemplate>
                            <%# Eval("Value3") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_Value3" runat="server" Text='<%# Bind("Value3") %>' Rows="2" Width="120"></dxe:ASPxMemo>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Do Date" FieldName="DateTime3" VisibleIndex="2" Width="80">
                        <PropertiesDateEdit Width="100" DisplayFormatString="dd/MM/yyyy" EditFormat="DateTime" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Materials" VisibleIndex="2" Width="40">
					 <DataItemTemplate>
                         <a onclick='javascript:PopubMaterials("<%# Eval("RefNo") %>","<%# Eval("CustomerName") %>");' href="#">Pick</a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Whs Date" FieldName="DateTime4" VisibleIndex="2" Width="80">
                        <PropertiesDateEdit Width="100" DisplayFormatString="dd/MM/yyyy" EditFormat="DateTime" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Whs Status" FieldName="Value4" VisibleIndex="2" Width="100">
                        <PropertiesComboBox Width="100">
                            <Items>
                                <dxe:ListEditItem Text="Pending" Value="Pending" />
                                <dxe:ListEditItem Text="Complete" Value="Complete" />
                            </Items>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Whs Remark" FieldName="Value5" VisibleIndex="4">
                        <DataItemTemplate>
                          <%# Eval("Value5") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_Value1" runat="server" Text='<%# Bind("Value5") %>' Rows="2" Width="120"></dxe:ASPxMemo>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataDateColumn>
                </Columns>
                <Settings ShowFooter="True" />
                 <TotalSummary>
                     <dxwgv:ASPxSummaryItem  FieldName="RefNo" DisplayFormat="{0}" SummaryType="Count"/>
                 </TotalSummary>
            </dxwgv:ASPxGridView>
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
            </dxwgv:ASPxGridViewExporter>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500px"
                AllowResize="True" Width="900px" EnableViewState="False">
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr1"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500px"
                AllowResize="True" Width="800px" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
