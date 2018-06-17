<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SurveySchedule.aspx.cs" Inherits="WareHouse_Job_SurveySchedule" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
            <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript">
        function ShowJob(masterId) {
            parent.navTab.openTab(masterId, "/Modules/WareHouse/Job/JobEdit.aspx?no=" + masterId, { title: masterId, fresh: false, external: true });
        }
        function PrintSchedule() {
            window.open("/ReportJob/PrintSurveySchedule.aspx?DateFrom=" + txt_from.GetText() + "&DateTo=" + txt_end.GetText());
        }
		function AfterPopubMultiInv() {
		    popubCtr.Hide();
		    popubCtr.SetContentUrl('about:blank');
		    if (grid != null) {
		        grid.Refresh();
		    }
		    document.location.reload();
		}
		function AfterPopubMultiInv1() {
		    popubCtr1.Hide();
		    popubCtr1.SetContentUrl('about:blank');
		    //grid.Refresh();

		    document.location.reload();
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
		 <style media="screen">
	.noprint {}
	.doprint {}
	.onlyprint {display:none;}

	</style>	
</head>
<body>
    <form id="form1" runat="server">
            <wilson:DataSource ID="dsSalesman" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXSalesman" KeyMember="Code" />

	<wilson:DataSource ID="dsJobSchedule" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobInfo"
                KeyMember="Id" FilterExpression="Value2<>'NA'" />
        <div>
            <table class="noprint">
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="Label1" runat="server" Text="Survey Date :">
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
					Surveyor
					</td>
					<td>
						                <dxe:ASPxComboBox ID="cmb_SurveyId" ClientInstanceName="cmb_SurveyId" runat="server" DataSourceID="dsSalesman" TextFormatString="{0}" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith"   TextField="Code" ValueField="Code" Width="70">
                                            <Columns>
                                                <dxe:ListBoxColumn FieldName="Code" Width="50px" />
                                                <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                                            </Columns>
                                        </dxe:ASPxComboBox>

					</td>
                      <td>
                        <dxe:ASPxButton ID="btn_search" Width="120" ClientInstanceName="btn_search" runat="server" Text="Search" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                     <td>
                        <dxe:ASPxButton ID="btn_Add" Width="120px" runat="server" Text="Print"
                            AutoPostBack="False" UseSubmitBehavior="False">
                            <ClientSideEvents Click="function(s, e) {
                                  PrintSchedule();
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
			<tr><td align="center"><h2>Collin's Movers : Daily Survey Schedule</h2></td></tr>
			</table>
            
             <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server"  OnRowUpdating="grid_RowUpdating"
                KeyFieldName="Id" Width="1400px" AutoGenerateColumns="False" OnCustomDataCallback="grid_CustomDataCallback" OnInit="grid_Init">

                <SettingsEditing Mode="Inline" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="20">
                        <DataItemTemplate>
                            <a onclick="ShowJob('<%# Eval("QuotationNo") %>');">Open</a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                   
                    <dxwgv:GridViewDataTextColumn Caption="No" FieldName="QuotationNo" VisibleIndex="1"
                         Width="130" ReadOnly="true">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Survey Date" FieldName="DateTime2" VisibleIndex="2" Width="80" ReadOnly="true" SortIndex="1" SortOrder="Descending">
                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Time" FieldName="DateTime2" VisibleIndex="2" Width="80" ReadOnly="true" SortIndex="1" SortOrder="Descending">
                        <DataItemTemplate>
                          <%# SafeValue.SafeDate(Eval("DateTime2"),DateTime.Now).ToShortTimeString() %>
						</DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Surveyor" FieldName="Value2" VisibleIndex="1"
                         Width="100" ReadOnly="true">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Client Name" FieldName="CustomerName" Width="160" VisibleIndex="2">
                        <DataItemTemplate>
                            <%# Eval("CustomerName") %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Contact" FieldName="Contact" Width="80" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Tel" FieldName="Tel" Width="80" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Origin" FieldName="OriginAdd" VisibleIndex="2" Width="120">
                          <DataItemTemplate>
                            <%# Eval("OriginAdd") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_Origin" runat="server" Text='<%# Bind("OriginAdd") %>' Rows="2" Width="120"></dxe:ASPxMemo>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Destination" FieldName="DestinationAdd" VisibleIndex="2" Width="120">
                          <DataItemTemplate>
                            <%# Eval("DestinationAdd") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_Destination" runat="server" Text='<%# Bind("DestinationAdd") %>' Rows="2" Width="120"></dxe:ASPxMemo>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                 

                   
                   
                </Columns>
                <Settings ShowFooter="True" />
                 <TotalSummary>
                     <dxwgv:ASPxSummaryItem  FieldName="JobNo" DisplayFormat="{0}" SummaryType="Count"/>
                 </TotalSummary>
            </dxwgv:ASPxGridView>
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
            </dxwgv:ASPxGridViewExporter>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="600"
                AllowResize="True" Width="100%" EnableViewState="False">
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr1"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="600"
                AllowResize="True" Width="100%" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
