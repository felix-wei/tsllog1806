<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KDExport.aspx.cs" Inherits="PagesContTrucking_Daily_KDExport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="display: none">
                <dxe:ASPxLabel ID="lb_JobType" runat="server"></dxe:ASPxLabel>
            </div>
            <table>
                <tr>
                    <td>Schedule Date</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_searchDate" runat="server" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                            <td>
                                <dxe:ASPxButton ID="btn_Export" Width="100" runat="server" Text="Save Excel" OnClick="btn_Export_Click">
                                </dxe:ASPxButton>
                            </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" Width="850px" KeyFieldName="Id" AutoGenerateColumns="False" OnRowUpdating="grid_Transport_RowUpdating">
                <SettingsPager Mode="ShowPager" PageSize="100" />
                <SettingsEditing Mode="Inline" />
                <SettingsBehavior ConfirmDelete="true" />
                <Columns>
                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="10%">
                        <EditButton Visible="True" />
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataColumn FieldName="JobNo" Caption="JobNo" ReadOnly="true">
                        <EditItemTemplate>
                            <%# Eval("JobNo") %>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ContainerNo" Caption="ContainerNo">
                        <EditItemTemplate>
                            <div style="display: none">
                                <dxe:ASPxLabel ID="lb_Id" runat="server" Text='<%# Bind("Id") %>'></dxe:ASPxLabel>
                            </div>
                            <dxe:ASPxButtonEdit ID="btn_ContNo" ClientInstanceName="btn_ContNo" runat="server" Text='<%# Bind("ContainerNo") %>' AutoPostBack="False" Width="120">
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupContainer(btn_ContNo,txt_ContainerType);
                                                                        }" />
                            </dxe:ASPxButtonEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="SealNo" Caption="SealNo">
                        <EditItemTemplate>
                            <dxe:ASPxTextBox ID="txt_SealNo" runat="server" Text='<%# Bind("SealNo") %>' Width="70"></dxe:ASPxTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size">
                        <EditItemTemplate>
                            <dxe:ASPxTextBox ID="txt_ContainerType" ClientInstanceName="txt_ContainerType" runat="server" Text='<%# Bind("ContainerType") %>' ReadOnly="true" Width="50"></dxe:ASPxTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ScheduleDate" Caption="ScheduleDate">
                        <EditItemTemplate>
                            <dxe:ASPxDateEdit ID="date_ScheduleDate" runat="server" Value='<%# Bind("ScheduleDate") %>' Width="100" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                        </EditItemTemplate>
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("ScheduleDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Terminalcode" Caption="Terminal" ReadOnly="true"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="RequestDate" Caption="ReqeustDate">
                        <EditItemTemplate>
                            <dxe:ASPxDateEdit ID="date_RequestDate" runat="server" Value='<%# Bind("RequestDate") %>' Width="100" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                        </EditItemTemplate>
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("RequestDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="CfsInDate" Caption="CfsInDate">
                        <EditItemTemplate>
                            <dxe:ASPxDateEdit ID="date_CfsInDate" runat="server" Value='<%# Bind("CfsInDate") %>' Width="100" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                        </EditItemTemplate>
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("CfsInDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="CfsOutDate" Caption="CfsOutDate">
                        <EditItemTemplate>
                            <dxe:ASPxDateEdit ID="date_CfsOutDate" runat="server" Value='<%# Bind("CfsOutDate") %>' Width="100" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                        </EditItemTemplate>
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("CfsOutDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="YardPickupDate" Caption="YardPickupDate">
                        <EditItemTemplate>
                            <dxe:ASPxDateEdit ID="date_YardPickupDate" runat="server" Value='<%# Bind("YardPickupDate") %>' Width="100" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                        </EditItemTemplate>
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("YardPickupDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="YardReturnDate" Caption="YardReturnDate">
                        <EditItemTemplate>
                            <dxe:ASPxDateEdit ID="date_YardReturnDate" runat="server" Value='<%# Bind("YardReturnDate") %>' Width="100" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                        </EditItemTemplate>
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("YardReturnDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Weight" Caption="Weight">
                        <EditItemTemplate>
                            <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="165"
                                ID="spin_Wt" Height="21px" Value='<%# Bind("Weight")%>' DecimalPlaces="3" Increment="0">
                                <SpinButtons ShowIncrementButtons="false" />
                            </dxe:ASPxSpinEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Volume" Caption="Volume">
                        <EditItemTemplate>
                            <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="165"
                                ID="spin_M3" Height="21px" Value='<%# Bind("Volume")%>' DecimalPlaces="3" Increment="0">
                                <SpinButtons ShowIncrementButtons="false" />
                            </dxe:ASPxSpinEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="QTY" Caption="QTY">
                        <EditItemTemplate>
                            <dxe:ASPxSpinEdit runat="server" Width="40"
                                ID="spin_Pkgs" Height="21px" Value='<%# Bind("QTY")%>' NumberType="Integer" Increment="0" DisplayFormatString="0">
                                <SpinButtons ShowIncrementButtons="false" />
                            </dxe:ASPxSpinEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="PackageType" Caption="PackageType">
                        <EditItemTemplate>
                            <dxe:ASPxTextBox ID="txt_PackageType" runat="server" Text='<%# Bind("PackageType") %>' Width="70"></dxe:ASPxTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="DgClass" Caption="DgClass">
                        <EditItemTemplate>
                            <dxe:ASPxTextBox ID="txt_DgClass" runat="server" Text='<%# Bind("DgClass") %>' Width="70"></dxe:ASPxTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="PortnetStatus" Caption="PortnetStatus">
                        <EditItemTemplate>
                            <dxe:ASPxComboBox ID="cbb_PortnetStatus" runat="server" Value='<%# Bind("PortnetStatus") %>' DropDownStyle="DropDown" Width="100"></dxe:ASPxComboBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="F5Ind" Caption="F5Ind">
                        <EditItemTemplate>
                            <dxe:ASPxComboBox ID="cbb_F5Ind" runat="server" Value='<%# Bind("F5Ind") %>' Width="50">
                                <Items>
                                    <dxe:ListEditItem Value="Y" Text="Y" />
                                    <dxe:ListEditItem Value="N" Text="N" />
                                </Items>
                            </dxe:ASPxComboBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="UrgentInd" Caption="UrgentInd">
                        <EditItemTemplate>
                            <dxe:ASPxComboBox ID="cbb_UrgentInd" runat="server" Value='<%# Bind("UrgentInd") %>' Width="50">
                                <Items>
                                    <dxe:ListEditItem Value="Y" Text="Y" />
                                    <dxe:ListEditItem Value="N" Text="N" />
                                </Items>
                            </dxe:ASPxComboBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ClientName" Caption="ClientName" ReadOnly="true">
                        <EditItemTemplate>
                            <%# Eval("ClientName") %>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="HaulierName" Caption="HaulierName" ReadOnly="true">
                        <EditItemTemplate>
                            <%# Eval("HaulierName") %>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ClientRefNo" Caption="ClientRefNo" ReadOnly="true">
                        <EditItemTemplate>
                            <%# Eval("ClientRefNo") %>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="EtaDate" Caption="Eta" ReadOnly="true">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("EtaDate")) %>&nbsp;<%# SafeValue.SafeString(Eval("EtaTime")) %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("EtaDate")) %>&nbsp;<%# SafeValue.SafeString(Eval("EtaTime")) %>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Remark" Caption="Remark">
                        <EditItemTemplate>
                            <dxe:ASPxTextBox ID="txt_Remark" runat="server" Text='<%# Bind("Remark") %>' Width="200"></dxe:ASPxTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                                        <dxwgv:GridViewDataColumn FieldName="Id" Visible="false">
</dxwgv:GridViewDataColumn>
                </Columns>

            </dxwgv:ASPxGridView>
        </div>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid_Transport">
        </dxwgv:ASPxGridViewExporter>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="570"
            Width="900" EnableViewState="False">
            <ClientSideEvents CloseUp="function(s, e) {
      
}" />
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
