<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Crews2.aspx.cs" Inherits="Modules_Hr_Job_Crews2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript">
        var clientId = null;
        var clientName = null;
        function RowClickHandler(s, e) {
            SetLookupKeyValue(e.visibleIndex);
            DropDownEdit.HideDropDown();
		}
		
        function SetLookupKeyValue(rowIndex) {
            DropDownEdit.SetText(GridView.cpName[rowIndex]);
            txt_Code.SetText(GridView.cpCode[rowIndex]);
        }
        function PopupJobNo(no) {
            popubCtr.SetContentUrl('/warehouse/SelectPage/SelectJobNo.aspx?DateFrom=' + lbl_JobDate.GetText() + "&Sn=" + no);
            popubCtr.SetHeaderText('Job No');
            popubCtr.Show();       
        }
        function PopupCrew(name,code, date1, date2) {
            clientId = name;
            clientName = code;
            popubCtr.SetContentUrl('/PagesHr/SelectPage/CrewList.aspx?DateFrom=01/01/2014&DateTo=' + date2.GetText());
            popubCtr.SetHeaderText('Job No');
            popubCtr.Show();
        }
		
		function PutValueCrew(s,sn, name) {
			btn_Name.SetText(sn);
			//txt_Remark.SetText(sn);
			//txt_Code.SetText(name);
			popubCtr.Hide();
			popubCtr.SetContentUrl('about:blank');
		}
		
        function AfterPopubMultiInv() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            grid.Refresh();
        }
        function PrintPayment() {
            window.open("/ReportJob/PrintCrewPayment.aspx?Crew=" + btn_Name.GetText());
        }
        function PrintSummary() {
            window.open("/ReportJob/PrintLabourSummary.aspx?DateFrom=" + txt_PayDate.GetText() + "&DateTo=" + txt_PayDate.GetText());
        }
        function OnCallback(v) {
            if (v == "Success") {
                alert(v);
                grid.Refresh();
            }
            else if (v == "Fail") {
                alert("Fail!Pls try again");
            }
        }
    </script>
</head>
<body>
        <form id="form1" runat="server">
    <wilson:DataSource ID="dsCrews" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.JobCrews" KeyMember="Id"/>
    <wilson:DataSource ID="dsPerson" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.HrPerson" KeyMember="Id" FilterExpression="HrRole='Casual' and Id>0" />
    <table >
        <tr>
			<td>
			Crew Short Name
			</td>
             <td width="100px">
                <dxe:ASPxButtonEdit ID="btn_Name" ClientInstanceName="btn_Name" runat="server"
                    Width="120" HorizontalAlign="Left" AutoPostBack="False">
                    <Buttons>
                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                    </Buttons>
                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupCrew(btn_Name,null,txt_date,txt_date2);
                                                                    }" />
                </dxe:ASPxButtonEdit>
                 <dxe:ASPxComboBox ID="cmb_Person" ClientVisible="false" ClientInstanceName="cmb_Person"   Width="120" DataSourceID="dsPerson" TextField="Name" runat="server" ValueField="Name" ValueType="System.String" DropDownStyle="DropDown">
                 </dxe:ASPxComboBox>
            </td>
            <td width="50px" style="display:none;">
                <dxe:ASPxLabel ID="Label1" runat="server" Text="JobDate">
                </dxe:ASPxLabel>
            </td>
             <td width="50px" style="display:none;">
               <dxe:ASPxDateEdit ID="txt_from" ClientInstanceName="txt_from" Width="90" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                </dxe:ASPxDateEdit>
                <div style="display: none">
                    <dxe:ASPxDateEdit ID="txt_date" ClientInstanceName="txt_date" Width="90" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </div>
            </td>
            <td width="50px" style="display:none;">
                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="To">
                </dxe:ASPxLabel>
            </td>
            <td width="100px"  style="display:none;">
                <dxe:ASPxDateEdit ID="txt_to" ClientInstanceName="txt_to" Width="90" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                </dxe:ASPxDateEdit>
                <div style="display: none">
                    <dxe:ASPxDateEdit ID="txt_date2" ClientInstanceName="txt_date2" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </div>
            </td>
            <td width="50px"  style="display:none;">
                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="Name">
                </dxe:ASPxLabel>
            </td>

            <td style="display:none;">
                <dxe:ASPxCheckBox ID="chk_All" runat="server" Text="All"></dxe:ASPxCheckBox>
            </td>
            <td width="50px" style="display:none;">
				<dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="PayDate">
                </dxe:ASPxLabel></td>
            <td width="50px" style="display:none;">

                <dxe:ASPxDateEdit ID="txt_PayDate" ClientInstanceName="txt_PayDate" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                </dxe:ASPxDateEdit>
            </td>
            <td >
                <dxe:ASPxButton ID="btn_search" Width="80" runat="server" Text="Search" OnClick="btn_search_Click">
                </dxe:ASPxButton>
            </td>
            <td width="50px" style="display:none;">

                <dxe:ASPxButton ID="btn_AddNew" Width="100" runat="server" Text="Auto Create" AutoPostBack="false">
                    <ClientSideEvents Click="function(s, e) {
                                                        grid.GetValuesOnCustomCallback('Add',OnCallback);
                                                        }" />
                </dxe:ASPxButton>
            </td>
            <td>
                 <dxe:ASPxButton ID="btn_Add" Width="80px" runat="server" Text="Print"
                            AutoPostBack="False" UseSubmitBehavior="False">
                            <ClientSideEvents Click="function(s, e) {
                                  PrintPayment();
                                    }" />
                        </dxe:ASPxButton>
            </td>
            <td style="display:none;">
                 <dxe:ASPxButton ID="ASPxButton2" Width="120px" runat="server" Text="Print Summary"
                            AutoPostBack="False" UseSubmitBehavior="False">
                            <ClientSideEvents Click="function(s, e) {
                                  PrintSummary();
                                    }" />
                        </dxe:ASPxButton>
            </td>
            <td>
                <dxe:ASPxButton ID="btn_export" Width="120" runat="server" Text="Save To Excel" OnClick="btn_export_Click">
                </dxe:ASPxButton>
            </td>
            <td style="display:none;">

                <dxe:ASPxButton ID="ASPxButton1" Width="110" runat="server" Text="Add New" AutoPostBack="false">
                    <ClientSideEvents Click="function(s, e) {
                                                        grid.AddNewRow();
                                                        }" />
                </dxe:ASPxButton>
            </td>
           

            <td></td>
        </tr>
    </table>

    <div>
        <dxwgv:ASPxGridView ID="grid" runat="server" ClientInstanceName="grid" DataSourceID="dsCrews"
            KeyFieldName="Id" AutoGenerateColumns="False" OnCustomDataCallback="grid_CustomDataCallback"
            Width="100%" OnInit="grid_Init" OnInitNewRow="grid_InitNewRow" OnRowDeleting="grid_RowDeleting"
            OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating">
            <SettingsEditing Mode="Inline" />
			<SettingsBehavior ConfirmDelete="True" AllowSort="false" AllowGroup="false" />
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsCustomizationWindow Enabled="false" />
    
            <Columns>
                <dxwgv:GridViewDataColumn Caption="" VisibleIndex="0" Width="40">
					<DataItemTemplate>
					<%# Container.ItemIndex + 1 %>
					
					</DataItemTemplate>
				</dxwgv:GridViewDataColumn>

                <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="2" Width="120">
                    <EditItemTemplate>
                       <dxe:ASPxButtonEdit ID="btn_Name" ClientInstanceName="btn_Name" runat="server"
                            Width="120" HorizontalAlign="Left" Text='<%# Bind("Name") %>' AutoPostBack="False">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                           
                                                                    }" />
                        </dxe:ASPxButtonEdit>
                        <dxe:ASPxDropDownEdit ID="DropDownEdit2" ClientVisible="false" runat="server" ClientInstanceName="DropDownEdit"
                            Text='<%# Eval("Name") %>' Width="120px" AllowUserInput="True">
                            <DropDownWindowTemplate>
                                <dxwgv:ASPxGridView ID="gridPopCont" runat="server" AutoGenerateColumns="False" ClientInstanceName="GridView"
                                    Width="150" DataSourceID="dsPerson" KeyFieldName="Id" OnCustomJSProperties="gridPopCont_CustomJSProperties">
									<Settings ShowFilterRow="True" />
                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn FieldName="Name" VisibleIndex="1">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="IcNo" VisibleIndex="2">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <ClientSideEvents RowClick="RowClickHandler" />
                                </dxwgv:ASPxGridView>
                            </DropDownWindowTemplate>
                        </dxe:ASPxDropDownEdit>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Short" FieldName="Remark" VisibleIndex="2" Width="100">
                    <EditItemTemplate>
                        <dxe:ASPxTextBox runat="server" Width="100" ID="txt_Remark"
                            Text='<%# Bind("Remark") %>' ClientInstanceName="txt_Remark">
                        </dxe:ASPxTextBox>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="ICNO" FieldName="Code" VisibleIndex="2" Width="100">
                    <EditItemTemplate>
                        <dxe:ASPxTextBox runat="server" Width="100" ID="txt_Code"
                            Text='<%# Bind("Code") %>' ClientInstanceName="txt_Code">
                        </dxe:ASPxTextBox>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Job No" FieldName="RefNo" VisibleIndex="2" Width="160">
                    <DataItemTemplate>
                        <%# Eval("RefNo") %>
                    </DataItemTemplate>
                    <EditItemTemplate>
                        <dxe:ASPxMemo ID="memo_RefNo" runat="server" Text='<%# Bind("RefNo") %>' Rows="2" Width="120"></dxe:ASPxMemo>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <%-- <dxwgv:GridViewDataTextColumn Caption="#" Width="80" VisibleIndex="3">
                <DataItemTemplate>
                    <a onclick='javascript:PopupJobNo("<%# Eval("Id") %>");' href="#">Add Job No</a>
                </DataItemTemplate>
            </dxwgv:GridViewDataTextColumn>--%>
                <dxwgv:GridViewDataDateColumn Caption="JobDate" FieldName="JobTime" VisibleIndex="3" Width="100" SortIndex="0"  SortOrder="Ascending">
                    <DataItemTemplate>
                        <dxe:ASPxLabel ID="lbl_JobDate" ClientInstanceName="lbl_JobDate" runat="server" Value='<%# SafeValue.SafeDate(Eval("JobTime"),DateTime.Today).ToString("dd/MM/yyyy") %>'>
                        </dxe:ASPxLabel>
                    </DataItemTemplate>
                    <EditItemTemplate>
                        <dxe:ASPxDateEdit ID="date_JobDate" ClientInstanceName="date_JobDate"  ReadOnly="true" runat="server" Value='<%# Bind("JobTime") %>'
                            EditFormat="DateTime" DisplayFormatString="dd/MM/yyyy" Width="100" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </EditItemTemplate>
   
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataDateColumn Caption="PayDate" FieldName="PayDate" VisibleIndex="3" Width="100">
                    <PropertiesDateEdit EditFormat="DateTime" DisplayFormatString="dd/MM/yyyy" Width="100" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="STD" FieldName="Amount1" VisibleIndex="4" Width="100">
                    <PropertiesSpinEdit Increment="0" NumberType="Float" DecimalPlaces="2" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="REF" FieldName="Amount2" VisibleIndex="4" Width="100">
                    <PropertiesSpinEdit Increment="0" NumberType="Float" DecimalPlaces="2" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="OTHERS" FieldName="Amount3" VisibleIndex="4" Width="100">
                    <PropertiesSpinEdit Increment="0" NumberType="Float" DecimalPlaces="2" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="OtHour" FieldName="OtHour" VisibleIndex="4" Width="60">
                    <PropertiesSpinEdit Increment="0" NumberType="Float" DecimalPlaces="2" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="OT" FieldName="Amount4" VisibleIndex="4" Width="60">
                    <PropertiesSpinEdit Increment="0" NumberType="Float" DecimalPlaces="2" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="OT Per Hour" FieldName="Amount6" VisibleIndex="4" Width="60">
                    <PropertiesSpinEdit Increment="0" NumberType="Float" DecimalPlaces="2" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="DEC" FieldName="Amount5" VisibleIndex="4" Width="60">
                    <PropertiesSpinEdit Increment="0" NumberType="Float" DecimalPlaces="2" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Total Amt" FieldName="Amount7" VisibleIndex="4" Width="60">
                    <PropertiesSpinEdit Increment="0" NumberType="Float" DecimalPlaces="2" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
            </Columns>
            <Styles Header-HorizontalAlign="Center">
                <Header HorizontalAlign="Center"></Header>
                <Cell HorizontalAlign="Center"></Cell>
            </Styles>
			<Settings ShowFooter="True" />
            <TotalSummary>
                <dxwgv:ASPxSummaryItem FieldName="Name" SummaryType="Count" DisplayFormat="{0}" />
                <dxwgv:ASPxSummaryItem FieldName="Amount1" SummaryType="Sum" DisplayFormat="{0:#,##0.00}"  />
                <dxwgv:ASPxSummaryItem FieldName="Amount2" SummaryType="Sum" DisplayFormat="{0:#,##0.00}"  />
                <dxwgv:ASPxSummaryItem FieldName="Amount3" SummaryType="Sum" DisplayFormat="{0:#,##0.00}"  />
                <dxwgv:ASPxSummaryItem FieldName="Amount4" SummaryType="Sum" DisplayFormat="{0:#,##0.00}"  />
                <dxwgv:ASPxSummaryItem FieldName="Amount5" SummaryType="Sum" DisplayFormat="{0:#,##0.00}"  />
                <dxwgv:ASPxSummaryItem FieldName="Amount7" SummaryType="Sum" DisplayFormat="{0:#,##0.00}"  />
            </TotalSummary>
        </dxwgv:ASPxGridView>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
        </dxwgv:ASPxGridViewExporter>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="400"
            AllowResize="True" Width="500" EnableViewState="False">
        </dxpc:ASPxPopupControl>
    </div>
    </form>
</body>
</html>
