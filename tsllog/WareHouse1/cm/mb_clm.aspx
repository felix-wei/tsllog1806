<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mb_clm.aspx.cs" Inherits="WareHouse_Job_Mb_Spj" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
	<style>

	@media print {
 .noprint {display:none;}
 }
	@media screen {
 .onlyprint {display:none;}
 }

	</style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="noprint">
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="Label1" runat="server" Text="Cut Off Date :">
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
                    <td style="display:none;">
                        <dxe:ASPxLabel ID="lbl_Company" runat="server" Text="Company Name">
                        </dxe:ASPxLabel>
                    </td>
                    <td style="display:none;">
                        <dxe:ASPxComboBox ID="cmb_CompanyName" runat="server" Width="60px">
                            <Items>
                                <dxe:ListEditItem Text="MST" Value="MST" />
                                <dxe:ListEditItem Text="CLM" Value="CLM" />                               
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" ClientInstanceName="btn_search" runat="server" Text="Search" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
               <td>
                    <dxe:ASPxButton ID="btn_print" Width="100" runat="server" Text="Print" AutoPostBack="False">
                        <ClientSideEvents Click="function(s, e) {
                     window.print();
                        }" />
                    </dxe:ASPxButton>
                </td>
                </tr>
            </table>
    
	<div>
			<div class=onlyprint style="text-align:center">
			     <span style="font-size:19px;font-weight:bold;">
			Collin's Movers Pte Ltd
		</span>
		<div style="font-size:11px;">
		22 Jurng Port Road, Singapore 619114<br> Tel: (65) 6643 1689
		<br> CO Reg No: 201215263H<br>GST Reg No: 201215263H <br><br>
			</div>
		</div>	
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" Width="600px"
            KeyFieldName="Id" AutoGenerateColumns="False">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsEditing Mode="Inline" />
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsBehavior ConfirmDelete="True" />
            <Columns>
                <dxwgv:GridViewDataTextColumn Visible="False"
                    Caption="Code" FieldName="Code" VisibleIndex="1" Width="80">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn
                    Caption="Description" FieldName="Code" VisibleIndex="1" Width="300">
					<DataItemTemplate>
						<%# D.Text("select top 1 name from ref_material where code='"+Eval("Code","{0}")+"'") %> 
					</DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Unit" FieldName="Code" VisibleIndex="3" Width="40px">
					<DataItemTemplate>
						<%# R.MatUnitLoose(Eval("Code"))%>
					</DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Onhand New" FieldName="BalNew" VisibleIndex="3" Width="100px">
					<DataItemTemplate>
						<%# Eval("BalNew","{0:0}") %>
					</DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
				<dxwgv:GridViewDataTextColumn Caption="Onhand Used" FieldName="BalUsed" VisibleIndex="3" Width="100px">
					<DataItemTemplate>
						<%# Eval("BalUsed","{0:0}") %>
					</DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
				</Columns>
            <Settings ShowFooter="true" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="Description" SummaryType="Count" DisplayFormat="{0}" />
                </TotalSummary>
        </dxwgv:ASPxGridView>
    </div>
    </form>
</body>
</html>
