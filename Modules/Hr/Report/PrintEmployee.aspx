<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintEmployee.aspx.cs" Inherits="ReportJob_PrintEmployee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td>Date</td>
                <td>
                    <dxe:ASPxDateEdit ID="date_Employee" Width="120" runat="server" EditFormat="DateTime" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Search" OnClick="btn_search_Click">
                    </dxe:ASPxButton>
                </td>
                <td>
                     <dxe:ASPxButton ID="btn_export" Width="120" runat="server" Text="Save Excel" OnClick="btn_export_Click">
                        </dxe:ASPxButton>
                </td>
            </tr>
            
        </table>
    <div>
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="detailGrid" runat="server" Styles-Header-HorizontalAlign="Left" 
            KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" Styles-HeaderFilterItem-HorizontalAlign="Left" >
            <Settings ShowTitlePanel="true" />
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsEditing Mode="EditForm" />
            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
            <Columns>
                <dxwgv:GridViewBandColumn Caption="Collin's Movers Employee Database"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Border-BorderStyle="None">
                    <HeaderTemplate>
                        <%=System.Configuration.ConfigurationManager.AppSettings["CompanyName"] %> Employee Database
                    </HeaderTemplate>
                    <Columns>
                        <dxwgv:GridViewBandColumn Caption="Date"  HeaderStyle-Paddings-PaddingTop="10" HeaderStyle-Paddings-PaddingBottom="10">
                            <HeaderTemplate>
                                Date:&nbsp;&nbsp;&nbsp;&nbsp;<%=PrintDate() %>
                            </HeaderTemplate>
                            <Columns>
                                <dxwgv:GridViewBandColumn HeaderStyle-Paddings-PaddingTop="0">
                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn Caption="S/N" FieldName="No" />
                                        <dxwgv:GridViewDataTextColumn Caption="Employee Name"  FieldName="Name"/>
                                        <dxwgv:GridViewDataTextColumn Caption="NRIC No./ FIN No." FieldName="IcNo" />
                                        <dxwgv:GridViewDataTextColumn Caption="Natioonality" FieldName="Country"/>
                                        <dxwgv:GridViewDataTextColumn Caption="Category"  FieldName="Department"/>
                                        <dxwgv:GridViewDataTextColumn Caption="Work Pass Expiry Date" FieldName="ExpiryDate" />
                                        <dxwgv:GridViewDataTextColumn Caption="Days to Expiry" FieldName="Days" />
                                        <dxwgv:GridViewDataTextColumn Caption="Designation" FieldName="Remark2" Width="200px"/>
                                        <dxwgv:GridViewDataTextColumn Caption="Date of Birth" FieldName="BirthDay"/>
                                        <dxwgv:GridViewDataTextColumn Caption="Age"  FieldName="Age" Width="200"/>
                                        <dxwgv:GridViewDataTextColumn Caption="Date Joined" FieldName="BeginDate"/>
                                        <dxwgv:GridViewDataTextColumn Caption="Year of Service" FieldName="ServiceYears" Width="200px"/>
                                        <dxwgv:GridViewDataTextColumn Caption="Date Left" FieldName="ResignDate"/>
                                        <dxwgv:GridViewDataTextColumn Caption="CPF/Non-CPF" FieldName="IsCPF"/>
                                        <dxwgv:GridViewDataTextColumn Caption="Address" FieldName="Address" Width="300px"/>
                                        <dxwgv:GridViewDataTextColumn Caption="Bank" FieldName="BankCode"/>
                                        <dxwgv:GridViewDataTextColumn Caption="Account Number" FieldName="AccNo"/>
                                        <dxwgv:GridViewDataTextColumn Caption="Basic Salary" FieldName="Salary"/>
                                        <dxwgv:GridViewDataTextColumn Caption="Laundry Expenses" FieldName="Account1"/>
                                        <dxwgv:GridViewDataTextColumn Caption="Accommodation" FieldName="Account2"/>
                                        <dxwgv:GridViewDataTextColumn Caption="Witholding Tax" FieldName="Account3"/>
                                    </Columns>
                                </dxwgv:GridViewBandColumn>
                                <dxwgv:GridViewBandColumn Caption="CPF" HeaderStyle-HorizontalAlign="Center">
                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn Caption="Employer" FieldName="CPF1"/>
                                        <dxwgv:GridViewDataTextColumn Caption="Employee" FieldName="CPF2"/>
                                    </Columns>
                                </dxwgv:GridViewBandColumn>
                                <dxwgv:GridViewBandColumn>
                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn Caption="Levy" FieldName="Account4" Width="120"/>
                                        <dxwgv:GridViewDataTextColumn Caption="MBMF/SINDA/CDAC" FieldName="Account5"/>
                                    </Columns>
                                </dxwgv:GridViewBandColumn>
                            </Columns>
                        </dxwgv:GridViewBandColumn>
                    </Columns>
                </dxwgv:GridViewBandColumn>
            </Columns>

        </dxwgv:ASPxGridView>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
        </dxwgv:ASPxGridViewExporter>
    </div>
    </form>
</body>
</html>
