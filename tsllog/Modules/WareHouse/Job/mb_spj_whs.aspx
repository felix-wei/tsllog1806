<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mb_SPJ_WHS.aspx.cs" Inherits="WareHouse_Job_Mb_SPJ_WHS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
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
                                <dxe:ListEditItem Text="SPJ" Value="SPJ" />                               
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="120" ClientInstanceName="btn_search" runat="server" Text="Search" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
    <div>
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" Width="600px"
            KeyFieldName="Id" AutoGenerateColumns="False">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsEditing Mode="Inline" />
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsBehavior ConfirmDelete="True" />
            <Columns>
                <dxwgv:GridViewDataTextColumn
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
						<%# R.MatUnit(Eval("Code"))%>
					</DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Onhand Qty" FieldName="Bal" VisibleIndex="3" Width="100px">
					<DataItemTemplate>
						<%# Eval("Bal","{0:0}") %>
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
