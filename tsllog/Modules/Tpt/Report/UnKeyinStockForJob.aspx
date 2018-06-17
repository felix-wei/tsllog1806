<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnKeyinStockForJob.aspx.cs" Inherits="Modules_Tpt_Report_UnKeyinStockForJob" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/ConTrucking_planner.css" rel="stylesheet" />
    <link href="../script/f_dev.css" rel="stylesheet" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <script src="/script/firebase.js"></script>
    <script src="/script/js_company.js"></script>
    <script src="/script/js_firebase.js"></script>
    <script src="/script/jquery.js"></script>
    <style type="text/css">
        #grid_Transport_DXMainTable > tbody > tr:hover {
            background-color: #e9e9e9;
        }

        .show {
            display: block;
        }

        .hide {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                      <td>
                        <dxe:ASPxLabel ID="lbl1" runat="server" Text="Job No"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_jobNo" runat="server" Width="100"></dxe:ASPxTextBox>
                    </td>
                    
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="From"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_search_dateFrom" runat="server" Width="100" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="To"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_search_dateTo" runat="server" Width="100" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Text="Client"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="btn_ClientId" ClientInstanceName="btn_ClientId" runat="server" Width="100" AutoPostBack="False">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".." ></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_ClientId,txt_ClientName);
                                                                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td colspan="4">
                        <dxe:ASPxTextBox ID="txt_ClientName" ClientInstanceName="txt_ClientName" runat="server" Width="100%" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" ClientInstanceName="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_SaveExcel" ClientInstanceName="btn_SaveExcel" runat="server" Text="Save To Excel" OnClick="btn_SaveExcel_Click"></dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" 
                KeyFieldName="Id" Width="100%" AutoGenerateColumns="False"  OnPageSizeChanged="grid_Transport_PageSizeChanged" OnPageIndexChanged="grid_Transport_PageIndexChanged">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsPager Mode="ShowPager" PageSize="20"></SettingsPager>
                <Columns>
                     <dxwgv:GridViewDataTextColumn Caption="Job No" FieldName="JobNo" Width="260" FixedStyle="Left">
                         <DataItemTemplate>
                             <%--<a onclick="goJob('<%# Eval("JobNo") %>')"><%# Eval("JobNo") %></a>--%>
                             <a href='javascript: parent.navTab.openTab("<%# Eval("JobNo") %>","/PagesContTrucking/Job/JobEdit.aspx?no=<%# Eval("JobNo") %>",{title:"<%# Eval("JobNo") %>", fresh:false, external:true});'><%# Eval("JobNo") %></a>
                         </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="JobStatus" Caption="Status" Width="60"  FixedStyle="Left"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="JobType" Caption="JobType" Width="60"  FixedStyle="Left"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="IsTrucking" Caption="CT" Width="30">
                        <DataItemTemplate>
                            <%# Eval("IsTrucking","{0}")=="Yes"?"X":""%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="IsWarehouse" Caption="WH" Width="30">
                        <DataItemTemplate>
                            <%# Eval("IsWarehouse","{0}")=="Yes"?"X":""%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="IsLocal" Caption="TP" Width="30">
                        <DataItemTemplate>
                            <%# Eval("IsLocal","{0}")=="Yes"?"X":""%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="IsAdhoc" Caption="CR" Width="30">
                        <DataItemTemplate>
                            <%# Eval("IsAdhoc","{0}")=="Yes"?"X":""%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="contsCnt" Caption="Conts" Width="45" Visible="false"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="tripsCnt" Caption="Trips" Width="40" Visible="false"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="jobsCnt" Caption="Sub Jobs" Width="60" Visible="false"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="subContsCnt" Caption="SubJob Conts" Visible="false"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="subTripsCnt" Caption="SubJob Trips" Visible="false"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="client" Caption="Client"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="JobStatus" Caption="Status" Visible="false">
                        <DataItemTemplate>
                            <%# SafeValue.SafeString(Eval("JobStatus")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="PickupFrom" Caption="From" Visible="false">
                        <DataItemTemplate>
                            <div style="min-width: 200px"><%# Eval("PickupFrom") %></div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="DeliveryTo" Caption="To" Visible="false">
                        <DataItemTemplate>
                            <div style="min-width: 200px"><%# Eval("DeliveryTo") %></div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="billed" Caption="Billing" Visible="false"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Vessel" Caption="Vessel" Visible="false"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Voyage" Caption="Voyage" Visible="false"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ETA" Width="80" Visible="false">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("EtaDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Depot" Caption="Depot" Visible="false"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="PermitNo" Caption="PermitNo" Visible="false"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="Haulier" Caption="Contractor" Visible="false"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Terminalcode" Caption="Terminal" Visible="false"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ClientRefNo" Caption="Client Ref No"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Job Date">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("JobDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="Remark" Caption="Remark" Visible="false"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewBandColumn Caption="Stock In" HeaderStyle-HorizontalAlign="Center">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Qty" FieldName="InQty"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Weight" FieldName="InWeight"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Volume" FieldName="InVolume"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="ItemCode" FieldName="InBookingItem"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Location" FieldName="InLocation"></dx:GridViewDataTextColumn>
                        </Columns>
                    </dxwgv:GridViewBandColumn>
                     <dxwgv:GridViewBandColumn Caption="Stock Out" HeaderStyle-HorizontalAlign="Center">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Qty" FieldName="OutQty"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Weight" FieldName="OutWeight"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Volume" FieldName="OutVolume"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="ItemCode" FieldName="OutBookingItem"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Location" FieldName="OutLocation"></dx:GridViewDataTextColumn>
                        </Columns>
                    </dxwgv:GridViewBandColumn>
                </Columns>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="400"
                Width="600" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr1"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="530"
                Width="1100" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid_Transport">
            </dxwgv:ASPxGridViewExporter>
        </div>
    </form>
</body>
</html>
