<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WhsSchedule.aspx.cs" Inherits="WareHouse_Job_WhsSchedule" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
            <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript">
        function ShowJob(masterId) {
            parent.navTab.openTab(masterId, "/Warehouse/Job/JobEdit.aspx?refN=" + masterId, { title: masterId, fresh: false, external: true });
        }
        function PrintSchedule() {
            window.open("/ReportJob/PrintWhsSchedule.aspx?DateFrom=" + txt_from.GetText() + "&DateTo=" + txt_end.GetText());
        }
        function PopubCrews(jobNo, date,type) {
            popubCtr.SetHeaderText('Crews List');
            popubCtr.SetContentUrl('/Warehouse/SelectPage/SelectCrewsList.aspx?no=' + jobNo + '&date=' + date+"&type="+type);
            popubCtr.Show();
        }
		function PopubMaterials(jobNo) {
		    popubCtr.SetHeaderText('Materials List');
		    popubCtr.SetContentUrl('/Warehouse/SelectPage/SelectMaterialsList.aspx?no=' + jobNo);
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
		 <style media="screen">
	.noprint {}
	.doprint {}
	.onlyprint {display:none;}

	</style>	
</head>
<body>
    <form id="form1" runat="server">
                    <wilson:DataSource ID="dsJobSchedule" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobInventory"
                KeyMember="Id" FilterExpression="1=0" />
        <div>
            <table class="noprint">
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="Label1" runat="server" Text="Schedule Date :">
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
			<tr><td align="center"><h2>Collin's Movers : Daily Job Schedule</h2></td></tr>
			</table>
            
             <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsJobSchedule" OnRowUpdating="grid_RowUpdating"
                KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" OnCustomDataCallback="grid_CustomDataCallback" OnInit="grid_Init">
                <SettingsEditing Mode="Inline" />
				<SettingsBehavior AllowSort="false" AllowGroup="false" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                     <dxwgv:GridViewCommandColumn Caption="#" VisibleIndex="0" Width="40">
                        <EditButton Visible="true" Text="Edit"></EditButton>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="20">
                        <DataItemTemplate>
                            <a onclick="ShowJob('<%# Eval("JobNo") %>');">Open</a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                   
                    <dxwgv:GridViewDataTextColumn Caption="No" FieldName="JobNo" VisibleIndex="1" Width="130" ReadOnly="true">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Schedule Date" FieldName="Date1" VisibleIndex="2" Width="80">
                       <PropertiesDateEdit Width="100" DisplayFormatString="dd/MM/yyyy" EditFormat="DateTime" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Time" FieldName="MoveDate" VisibleIndex="2" Width="120">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDate(Eval("Date1"),DateTime.Today).ToString("HH:mm")%>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxTimeEdit ID="date_Time" Width="80" runat="server" Value='<%# Bind("Date1") %>'
                                EditFormat="Time" EditFormatString="HH:mm" DisplayFormatString="HH:mm">
                            </dxe:ASPxTimeEdit>
                        </EditItemTemplate>
                        <PropertiesDateEdit Width="60" DisplayFormatString="HH:mm" EditFormat="Time" EditFormatString="HH:mm"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Client Name" FieldName="CustomerName" VisibleIndex="2">
                        <DataItemTemplate>
                            <%# Eval("JobNo") %>/<br />
                            <%# D.Text("select CustomerName from jobinfo where jobno='"+Eval("JobNo")+"'") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Origin" FieldName="OriginAdd" VisibleIndex="2" Width="120">
                        <DataItemTemplate>
                            <%# D.Text("select OriginAdd from jobinfo where jobno='"+Eval("JobNo")+"'") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Destination" FieldName="DestinationAdd" VisibleIndex="2" Width="120">
                        <DataItemTemplate>
                            <%# D.Text("select DestinationAdd from jobinfo where jobno='"+Eval("JobNo")+"'") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Volume/Loads" FieldName="VolumneRmk" VisibleIndex="4" Width="80">
                        <DataItemTemplate>
                            <%# D.Text("select VolumneRmk from jobinfo where jobno='"+Eval("JobNo")+"'") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Contact" FieldName="Contact" Width="80" VisibleIndex="4">
                        <DataItemTemplate>
                            <%# D.Text("select contact from jobinfo where jobno='"+Eval("JobNo")+"'") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Time" VisibleIndex="4" Visible="false">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDate(Eval("Date1"),DateTime.Now).ToShortTimeString() %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Schedule Status" FieldName="Status1" VisibleIndex="2" Width="100">
                        <PropertiesComboBox Width="100">
                            <Items>
                                <dxe:ListEditItem Text="Pending" Value="Pending" />
                                <dxe:ListEditItem Text="Complete" Value="Complete" />
                                <dxe:ListEditItem Text="Cancel" Value="Cancel" />
                            </Items>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Whs Type" FieldName="DoType" VisibleIndex="2" Width="100">
                        <PropertiesComboBox Width="100">
                            <Items>
                                <dxe:ListEditItem Text="Receive" Value="WI" />
                                <dxe:ListEditItem Text="Release" Value="WO" />
                            </Items>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Vessel" FieldName="Note1" VisibleIndex="4" Width="100">
                        <DataItemTemplate>
                            <%# Eval("Note1") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_Value1" runat="server" Text='<%# Bind("Note1") %>' Rows="2" Width="120"></dxe:ASPxMemo>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ContNo" FieldName="Note2" VisibleIndex="4" Width="100">
                        <DataItemTemplate>
                            <%# Eval("Note2") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_Value2" runat="server" Text='<%# Bind("Note2") %>' Rows="2" Width="120"></dxe:ASPxMemo>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="SealNo" FieldName="Note3" VisibleIndex="4" Width="100">
                        <DataItemTemplate>
                            <%# Eval("Note3") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_Value3" runat="server" Text='<%# Bind("Note3") %>' Rows="2" Width="120"></dxe:ASPxMemo>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ContType" FieldName="Note4" VisibleIndex="4" Width="100">
                        <DataItemTemplate>
                            <%# Eval("Note4") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_Value4" runat="server" Text='<%# Bind("Note4") %>' Rows="2" Width="120"></dxe:ASPxMemo>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Carrier" FieldName="Note5" VisibleIndex="4" Width="100">
                        <DataItemTemplate>
                            <%# Eval("Note5") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_Value5" runat="server" Text='<%# Bind("Note5") %>' Rows="2" Width="120"></dxe:ASPxMemo>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Port" FieldName="Note6" VisibleIndex="4" Width="100">
                        <DataItemTemplate>
                            <%# Eval("Note6") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_Value6" runat="server" Text='<%# Bind("Note6") %>' Rows="2" Width="120"></dxe:ASPxMemo>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                   
                    <dxwgv:GridViewDataTextColumn Caption="Note" FieldName="Note8" VisibleIndex="4" Width="100">
                        <DataItemTemplate>
                            <%# Eval("Note8") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_Value8" runat="server" Text='<%# Bind("Note8") %>' Rows="2" Width="120"></dxe:ASPxMemo>
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
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="400"
                AllowResize="True" Width="900px" EnableViewState="False">
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr1"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="400"
                AllowResize="True" Width="900px" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
