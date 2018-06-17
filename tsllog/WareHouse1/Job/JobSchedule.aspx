<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobSchedule.aspx.cs" Inherits="WareHouse_Job_JobSchedule" %>

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
        function ShowSchedule(masterId) {
            parent.navTab.openTab(masterId, "/Warehouse/Job/JobScheduleEdit.aspx?no=" + masterId, { title: masterId, fresh: false, external: true });
        }
        function PrintSchedule() {
            window.open("/ReportJob/PrintJobSchedule.aspx?DateFrom=" + txt_from.GetText() + "&DateTo=" + txt_end.GetText());
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
                    <wilson:DataSource ID="dsJobSchedule" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobSchedule"
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
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="120">
                        <DataItemTemplate>
                            <a onclick="ShowSchedule('<%# Eval("JobNo") %>');"><%# Eval("JobNo") %></a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
<%--                     <dxwgv:GridViewCommandColumn Caption="#" VisibleIndex="0" Width="40" Visible="false">
                        <EditButton Visible="true" Text="Edit"></EditButton>
                    </dxwgv:GridViewCommandColumn>--%>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="100">
                        <DataItemTemplate>
                            <a onclick="ShowJob('<%# Eval("RefNo") %>');"><%# Eval("RefNo") %></a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
<%--                   
                    <dxwgv:GridViewDataTextColumn Caption="No" FieldName="RefNo" VisibleIndex="1" Width="130" ReadOnly="true">
                    </dxwgv:GridViewDataTextColumn>--%>
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
                    <dxwgv:GridViewDataTextColumn Caption="Days" FieldName="PackRmk" VisibleIndex="2" Width="120">
                        <DataItemTemplate>
                            <%# Eval("PackRmk") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_PackRmk" runat="server" Text='<%# Bind("PackRmk") %>' Rows="2" Width="120"></dxe:ASPxMemo>
                                                    <dxe:ASPxComboBox BackColor="LightBlue" EnableIncrementalFiltering="True" Width="100" ID="cmb_SchStatus" ClientInstanceName="cmb_SchStatus" Text='<%# Bind("WorkStatus") %>'  runat="server">
                                                        <Items>
                                                            <dxe:ListEditItem Text="Pending" Value="Pending" />
                                                            <dxe:ListEditItem Text="Cancel" Value="Cancel" />
                                                        </Items>
                                                    </dxe:ASPxComboBox>

                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Client Name" FieldName="CustomerName" VisibleIndex="2">
                        <DataItemTemplate>
                            <%# Eval("RefNo") %>/<%# R.SalesCode(D.Text("select Value4 from jobinfo where JobNo='"+Eval("RefNo","{0}")+"'")) %><br />
                            <%# Eval("CustomerName") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_CustomerName" runat="server" Text='<%# Bind("CustomerName") %>' Rows="2" Width="120"></dxe:ASPxMemo>
                        </EditItemTemplate>
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
                    <dxwgv:GridViewDataTextColumn Caption="Volume/Loads" FieldName="VolumneRmk" VisibleIndex="4" Width="80">
                        <DataItemTemplate>
                            <%# Eval("VolumneRmk") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_VolumneRmk" runat="server" Text='<%# Bind("VolumneRmk") %>' Rows="2" Width="120"></dxe:ASPxMemo>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Contact" FieldName="Contact" Width="80" VisibleIndex="4">
                        <DataItemTemplate>
                            <%# Eval("Contact") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_Contact" runat="server" Text='<%# Bind("Contact") %>' Rows="2" Width="120"></dxe:ASPxMemo>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataMemoColumn Caption="Materials" FieldName="Note2" VisibleIndex="4" Width="150" PropertiesMemoEdit-Rows="3" PropertiesMemoEdit-Width="150">
                    </dxwgv:GridViewDataMemoColumn>
<%--                    <dxwgv:GridViewDataTextColumn Caption="Materials" VisibleIndex="4" Width="40" Visible="false">
					 <DataItemTemplate>
                          <%# GetMaterials(SafeValue.SafeString(Eval("JobNo"))) %> 
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>--%>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="4" Width="40">
					 <DataItemTemplate>
                         <a onclick='javascript:PopubMaterials("<%# Eval("JobNo") %>");' href="#">Materials</a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Trucks/Mode" FieldName="Mode" VisibleIndex="4" Width="100">
                        <DataItemTemplate>
                            <%# Eval("TruckNo") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_TruckNo" runat="server" Text='<%# Bind("TruckNo") %>' Rows="2" Width="60"></dxe:ASPxMemo>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataMemoColumn Caption="Supervisor/Crews" ReadOnly="true" FieldName="Note1" VisibleIndex="4" Width="150" PropertiesMemoEdit-Rows="3" PropertiesMemoEdit-Width="150">
                    </dxwgv:GridViewDataMemoColumn>
<%--                    <dxwgv:GridViewDataTextColumn Caption="Supervisor/Crews" VisibleIndex="4" Width="40" CellStyle-VerticalAlign="Top" Visible="false">
					 <DataItemTemplate>
                            <%# GetCrews(SafeValue.SafeString(Eval("JobNo"))) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>--%>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="4" Width="40">
					 <DataItemTemplate>
                         <div style="display:none">
                             <dxe:ASPxLabel ID="lbl_SchNo" runat="server" Text='<%# Eval("JobNo") %>'></dxe:ASPxLabel>
                         </div>
                         <a onclick='javascript:PopubCrews("<%# Eval("JobNo") %>","<%# SafeValue.SafeDateStr(Eval("JobDate")) %>","Supervisor");' href="#">Supervisor</a>
                           &nbsp;&nbsp;<a onclick='javascript:PopubCrews("<%# Eval("JobNo") %>","<%# SafeValue.SafeDateStr(Eval("JobDate")) %>","Casual");' href="#">Crews</a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Time" VisibleIndex="4" Visible="false">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDate(Eval("JobDate"),DateTime.Now).ToShortTimeString() %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataComboBoxColumn Visible="false" Caption="Schedule Status" FieldName="WorkStatus" VisibleIndex="2" Width="100">
                        <PropertiesComboBox Width="100">
                            <Items>
                                <dxe:ListEditItem Text="Pending" Value="Pending" />
                                <dxe:ListEditItem Text="Cancel" Value="Cancel" />
                            </Items>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                   
                    <dxwgv:GridViewDataTextColumn Caption="Note" FieldName="Value1" VisibleIndex="4" Width="100">
                        <DataItemTemplate>
                            <%# Eval("Value1") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_Value1" runat="server" Text='<%# Bind("Value1") %>' Rows="2" Width="120"></dxe:ASPxMemo>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Leave Note" FieldName="Value2" VisibleIndex="4" Width="100">
                        <DataItemTemplate>
                            <%# Eval("Value2") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_Value2" runat="server" Text='<%# Bind("Value2") %>' Rows="2" Width="120"></dxe:ASPxMemo>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="WareHouse Note" FieldName="Value3" VisibleIndex="4" Width="100">
                        <DataItemTemplate>
                            <%# Eval("Value3") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_Value3" runat="server" Text='<%# Bind("Value3") %>' Rows="2" Width="120"></dxe:ASPxMemo>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="#"  VisibleIndex="4" Visible="false" Width="40">
                        <DataItemTemplate>
                            <dxe:ASPxButton ID="btn_CopySch" runat="server" Width="80" AutoPostBack="false"
                                Text="Copy"  OnInit="btn_CopySch_Init">
                            </dxe:ASPxButton>
                            <dxe:ASPxTextBox runat="server" ID="txtSId" OnInit="txt_Id_Init" ClientVisible="false" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Update"  VisibleIndex="99" Width="90">
                        <DataItemTemplate>
							<%# Eval("UpdateBy") + " " + Eval("UpdateDateTime","{0:dd/MMM#HH:mm}") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
							<%# Eval("UpdateBy") + " " + Eval("UpdateDateTime","{0:dd/MMM#HH:mm}") %>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
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
