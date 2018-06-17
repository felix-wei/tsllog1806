<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintMcst.aspx.cs" Inherits="WareHouse_Job_JobMcst" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
            <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript">
        function ShowJob(masterId) {
            parent.navTab.openTab(masterId, "/Warehouse/Job/JobEdit.aspx?no=" + masterId, { title: masterId, fresh: false, external: true });
        }

	function ShowHouse(masterId) {
            parent.navTab.openTab(masterId, "/Warehouse/Job/JobEdit.aspx?no=" + masterId, { title: masterId, fresh: false, external: true });
        }
        function PrintSchedule() {
            window.open("/ReportJob/PrintJobSchedule.aspx?DateFrom=" + txt_from.GetText() + "&DateTo=" + txt_end.GetText());
        }
    </script>
		 <style media="print">
	.noprint {display:none;}
	.doprint {font-size:10pt;}
	.onlyprint {font-size:10pt;}

	</style>	
		 <style media="screen">
	.noprint {}
	.doprint {}
	.onlyprint {display:none;}

	</style>	
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsJobMCST" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.JobMCST" KeyMember="Id" />
        <div>
            <table class="noprint">
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="Label1" runat="server" Text="MCST Date :">
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
                        <dxe:ASPxButton ID="btn_search" Width="120" runat="server" Text="Search" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                     <td>
                        <dxe:ASPxButton ID="btn_Add" Width="120px" runat="server" Text="Print"
                            AutoPostBack="False" UseSubmitBehavior="False">
                            <ClientSideEvents Click="function(s, e) {
                                 window.print(); // PrintSchedule();
                                    }" />
                        </dxe:ASPxButton>
                    </td>
                      <td>
                        <dxe:ASPxButton ID="btn_export" Width="120" runat="server" Text="To Excel" OnClick="btn_export_Click">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
			<table class="noprint" style="display:none;" width="100%">
			<tr><td align="center"><h2>Collin's Movers : MCST Report</h2></td></tr>
			</table>
            <dxwgv:ASPxGridView ID="grid_Mcst" ClientInstanceName="grid_Mcst"  DataSourceID="dsJobMCST" runat="server" OnRowInserting="grid_Mcst_RowInserting" Width="100%"
                OnRowDeleting="grid_Mcst_RowDeleting" OnRowUpdating="grid_Mcst_RowUpdating"
                KeyFieldName="Id" OnInit="grid_Mcst_Init" OnInitNewRow="grid_Mcst_InitNewRow">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="Inline"/>
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="40">
                        <EditButton Visible="true" Text="Edit"></EditButton>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Quotation" FieldName="RefNo" VisibleIndex="1" Width="100px">
						<DataItemTemplate>
						<a onclick="ShowJob('<%# Eval("RefNo") %>');">View</a>
						</DataItemTemplate>
                        <EditItemTemplate />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn
                        Caption="MCST No" FieldName="McstNo" VisibleIndex="1" Width="100px">
                        <EditItemTemplate>
                            <dxe:ASPxLabel ID="lbl_CondoName"  Width="100px" runat="server" Text='<%# Eval("McstNo") %>'></dxe:ASPxLabel>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="C/S" FieldName="CreateBy" VisibleIndex="1" Width="100px">
                        <EditItemTemplate><%# Eval("CreateBy") %></EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Deposit" FieldName="Amount1" VisibleIndex="3" Width="60px" ReadOnly="true">
                        <EditItemTemplate>
                             <dxe:ASPxLabel ID="lbl_CondoName" Width="60px" runat="server" Text='<%# Eval("Amount1") %>'></dxe:ASPxLabel>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Other Fee" FieldName="Amount2" VisibleIndex="6" Width="60px" ReadOnly="true">
                        <EditItemTemplate>
                            <dxe:ASPxLabel ID="lbl_CondoName" Width="60px" runat="server" Text='<%# Eval("Amount2") %>'></dxe:ASPxLabel>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Remarks" FieldName="McstRemark3" VisibleIndex="6" Width="60px" ReadOnly="true">
                        <EditItemTemplate><%# Eval("McstRemark3") %></EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Condo Name" FieldName="McstRemark1" VisibleIndex="10" Width="180px" ReadOnly="true">
                        <EditItemTemplate>
                            <dxe:ASPxLabel ID="lbl_CondoName" Width="280px" runat="server" Text=' <%# Eval("McstRemark1") %>'></dxe:ASPxLabel>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataDateColumn Caption="Date Out" FieldName="McstDate1" VisibleIndex="12" Width="100px">
                         <EditItemTemplate>
                             <dxe:ASPxDateEdit ID="date_DateOut" Width="120" runat="server" Value='<%# Bind("McstDate1") %>'
                                 EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy" SpinButtons-ShowIncrementButtons="false">
                             </dxe:ASPxDateEdit>
                         </EditItemTemplate>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Date In" FieldName="McstDate2" VisibleIndex="12" Width="100px">
                        <EditItemTemplate>
                             <dxe:ASPxDateEdit ID="date_DateIn" Width="120" runat="server" Value='<%# Bind("McstDate2") %>'
                                 EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyym">
                             </dxe:ASPxDateEdit>
                         </EditItemTemplate>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ChqNo" FieldName="States" VisibleIndex="12" Width="100">
                        <EditItemTemplate>
                           <dxe:ASPxTextBox runat="server" Width="100" ID="txt_States"
                                                                            Text='<%# Bind("States") %>' ClientInstanceName="txt_States">
                                                                        </dxe:ASPxTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>

                </Columns>
                <Settings ShowFooter="true" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="Name" SummaryType="Count" DisplayFormat="{0:0}" />
                </TotalSummary>
            </dxwgv:ASPxGridView>
			            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
            </dxwgv:ASPxGridViewExporter>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="600" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
