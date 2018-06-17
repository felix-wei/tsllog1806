<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StatusInquiryEdit.aspx.cs" Inherits="PagesContainer_Job_StatusInquiryEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsTransport" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.ContAssetEvent" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsXXUom" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXUom" KeyMember="Id"  FilterExpression="CodeType='TankState'"/>
            <table>
                <tr>
                    <td><dxe:ASPxLabel ID="lblContainerNo" Text="Container No" runat="server"></dxe:ASPxLabel>
                        </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_ContainerNo" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                        </td>
                    <td><dxe:ASPxLabel ID="ASPxLabel2" Text="Status" runat="server"/>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cb_Status" runat="server" DataSourceID="dsXXUom" TextFormatString="{0}" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" TextField="Code" ValueField="Code" Width="100%">
                              <Columns>
                                  <dxe:ListBoxColumn FieldName="Code" Width="50px" />
                              </Columns>
                        </dxe:ASPxComboBox>
                        
                    </td>
                    <td><dxe:ASPxLabel ID="ASPxLabel1" Text="Date" runat="server"/></td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_EventFrom" runat="server" EditFormat="DateTime" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td><dxe:ASPxLabel ID="ASPxLabel3" Text="-" runat="server"/></td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_EventTo" runat="server" EditFormat="DateTime" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td colspan="2">
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>

                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="900px" AutoGenerateColumns="False" DataSourceID="dsTransport">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsPager Mode="ShowAllRecords" PageSize="100"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="ContainerNo" FieldName="ContainerNo" VisibleIndex="1"
                        SortIndex="1" SortOrder="Ascending" Width="300">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ContainerType" FieldName="ContainerType" VisibleIndex="2" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="EventCode" FieldName="EventCode" VisibleIndex="3" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Date/Time" FieldName="EventDateTime" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy HH:mm" ReadOnly="true" VisibleIndex="4" Width="115"> 
                    </dxwgv:GridViewDataDateColumn>
                                         <dxwgv:GridViewDataTextColumn Caption="State" FieldName="State" VisibleIndex="5" Width="100"> 
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="EventPort" FieldName="EventPort" VisibleIndex="6" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Depot/Yard" FieldName="EventDepot" VisibleIndex="7"
                        Width="100">
                    </dxwgv:GridViewDataTextColumn>


                </Columns>
            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
