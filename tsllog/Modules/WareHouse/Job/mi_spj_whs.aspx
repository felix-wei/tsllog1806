﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mi_SPJ_WHS.aspx.cs" Inherits="WareHouse_Job_Mi_SPJ_WHS" %>

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
        function PrintDo() {
            window.open("/ReportJob/PrintDoSpj.aspx?DateFrom=" + txt_from.GetText() + "&DateTo=" + txt_end.GetText());
        }
        function PrintIv() {
            window.open("/ReportJob/PrintIvSpj.aspx?DateTo=" + txt_end.GetText());
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
                    <wilson:DataSource ID="dsStock" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.Material"
                KeyMember="Id" FilterExpression="1=0" />
                    <wilson:DataSource ID="dsMaterial" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.RefMaterial"
                KeyMember="Id" FilterExpression="Note1='SPJ'" />
                    <wilson:DataSource ID="dsParty" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXParty2"
                KeyMember="SequenceId" FilterExpression="IsVendor='true'"  />
				      
            <wilson:DataSource ID="dsTerm" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXTerm" KeyMember="SequenceId" />

        <div>
            <table class="noprint">
                <tr>
                    <td>
						
                        <dxe:ASPxLabel ID="Label1x" runat="server" Text="Do/Invoice No :">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_no" Width="100" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
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
             <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsStock" 
				KeyFieldName="Id" Width="920" AutoGenerateColumns="False" 
				OnRowUpdating="grid_RowUpdating" 
				OnRowInserting="grid_RowInserting" 
                OnRowDeleting="grid_RowDeleting" 
				OnInitNewRow="grid_InitNewRow"
				OnCustomDataCallback="grid_CustomDataCallback" 
				OnInit="grid_Init"
				>
                <SettingsEditing Mode="Inline" />
				<SettingsBehavior AllowSort="false" AllowGroup="false" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                                        <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                            <DataItemTemplate>
                                                <div style="display:none" >
                                                    <a href="#" onclick='<%# "grid.StartEditRow("+Container.VisibleIndex+"); " %>'>Edit</a>
                                                    <a href="#" onclick='if(confirm("Confirm Delete it?"))  {<%# "grid.DeleteRow("+Container.VisibleIndex+");"  %>}'>
                                                        Del</a>
                                                </div>
                                            </DataItemTemplate>
											<EditItemTemplate>
                                                <div  style="display:none">
                                                    <a href="#" onclick='<%# "grid.UpdateEdit("+Container.VisibleIndex+"); " %>'>Save</a>
                                                    <a href="#" onclick='grid.CancelEdit();'>
                                                        Cancel</a>
                                                </div>
											
											</EditItemTemplate>
											</dxwgv:GridViewDataColumn>

 
                  
                    <dxwgv:GridViewDataDateColumn Caption="Date" FieldName="DocDate" VisibleIndex="2" Width="80">
                       <PropertiesDateEdit Width="100" DisplayFormatString="dd/MM/yyyy" EditFormat="DateTime" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Do No" FieldName="DocNo" VisibleIndex="2" Width="80">
						<DataItemTemplate>
							<b><%# Eval("DocNo")%></b>
							<div style='display:<%# Eval("DoType","{0}") == "OUT3" ? "" : "none" %>'>
							<br><a href='/ReportJob/PrintDoSpj.aspx?no=<%# Eval("DocNo")%>' Target=_blank>Print</a>
							</div>
						</DataItemTemplate>
					</dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Bill No" FieldName="BillNo" VisibleIndex="2" Width="80">
						<DataItemTemplate>
							<b><%# Eval("BillNo")%></b>
							<div style='display:<%# Eval("DoType","{0}") == "OUT3" ? "" : "none" %>'>
							<br><a href='/ReportJob/PrintInvoiceSpj.aspx?no=<%# Eval("BillNo")%>' Target=_blank>Print</a>
							</div>
						</DataItemTemplate>
					</dxwgv:GridViewDataTextColumn>
                 <dxwgv:GridViewDataComboBoxColumn Caption="Term" Visible="False" Width="80" FieldName="BillTerm" VisibleIndex="2" >
                                    <PropertiesComboBox DataSourceID="dsTerm" ValueField="Code" TextField="Code" 
									DropDownStyle="DropDownList" ValueType="System.String" IncrementalFilteringMode="StartsWith">
                                    </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>					
                 <dxwgv:GridViewDataComboBoxColumn Caption="Item Code" Width="80" FieldName="Code" VisibleIndex="2" >
                                    <PropertiesComboBox DataSourceID="dsMaterial" ValueField="Code" TextField="Name" 
									DropDownStyle="DropDownList" ValueType="System.String" IncrementalFilteringMode="StartsWith">
                                    </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>					
                    <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="2" Width="80">
					<DataItemTemplate>
						<%# Eval("Qty","{0:0}") %>
					</DataItemTemplate>
					</dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Unit"  FieldName="Code" VisibleIndex="2" Width="80">
					<DataItemTemplate>
						<%# R.MatUnit(Eval("Code")) %>
					</DataItemTemplate>
					<EditItemTemplate />
					</dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Whole/Loose"  FieldName="Code" VisibleIndex="2" Width="80">
					<DataItemTemplate>
						<%# R.MatLoose(Eval("Code"),Eval("Qty")) %>
					</DataItemTemplate>
					<EditItemTemplate />
					</dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Price" Visible="False" FieldName="Price" VisibleIndex="2" Width="80">
					</dxwgv:GridViewDataTextColumn>
					<dxwgv:GridViewDataComboBoxColumn Caption="GST" Visible="False" FieldName="GstType" VisibleIndex="2" Width="100">
                        <PropertiesComboBox Width="100">
                            <Items>
                                <dxe:ListEditItem Value="S" Text="GST 7%" />
                                <dxe:ListEditItem Value="N" Text="NON-TAX" />
                            </Items>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="GstAmt" Visible="False" FieldName="GstAmt" Readonly="true" VisibleIndex="2" Width="80">
					</dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Total" Visible="False" FieldName="DocAmt" Readonly="true" VisibleIndex="2" Width="80">
					</dxwgv:GridViewDataTextColumn>
					<dxwgv:GridViewDataComboBoxColumn Caption="Supplier" Width="80" FieldName="PartyTo" VisibleIndex="2" >
                                    <PropertiesComboBox DataSourceID="dsParty" ValueField="PartyId" TextField="Name" 
									DropDownStyle="DropDownList" ValueType="System.String" IncrementalFilteringMode="StartsWith">
                                    </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>					
                </Columns>
                <Settings ShowFooter="True" />
                 <TotalSummary>
                     <dxwgv:ASPxSummaryItem  FieldName="DocNo" DisplayFormat="{0}" SummaryType="Count"/>
                     <dxwgv:ASPxSummaryItem  FieldName="Qty" DisplayFormat="{0}" SummaryType="Sum"/>
                     <dxwgv:ASPxSummaryItem  FieldName="GstAmt" DisplayFormat="{0:0.00}" SummaryType="Sum"/>
                     <dxwgv:ASPxSummaryItem  FieldName="DocAmt" DisplayFormat="{0:0.00}" SummaryType="Sum"/>
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
