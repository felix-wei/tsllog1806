<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContainerMonitor.aspx.cs" Inherits="Modules_WareHouse_Job_ContainerMonitor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="/Script/pages.js"></script>
        <script type="text/javascript">
            function ShowHouse(masterId,type) {
                if(type=="IN"){
                    parent.navTab.openTab(masterId, "/Modules/WareHouse/Job/DoInEdit.aspx?no=" + masterId, { title: masterId, fresh: false, external: true });
                }
                if(type=="OUT"){
                    parent.navTab.openTab(masterId, "/Modules/WareHouse/Job/DoOutEdit.aspx?no=" + masterId, { title: masterId, fresh: false, external: true });
                }
            }
    </script>
    <style type="text/css">
        a:hover {
            color: black;
        }

        .link a:link {
            color: red;
        }

        .link a:hover {
            color: red;
        }

        .none a:link {
        }


        .a_ltrip span {
            display: inline-block;
            border: 1px solid #e8e8e8;
            box-sizing: border-box;
            color: white;
            padding: 2px;
            width: 33px;
            height: 21px;
            overflow: hidden;
            white-space: nowrap;
            text-align: center;
            margin: 2px;
            /*margin-top:2px;
            margin-left:2px;
            margin-bottom:2px;
            margin-right:4px;*/
        }

        .a_ltrip .S {
            background-color: green;
        }

        .a_ltrip .X {
            background-color: gray;
        }

        .a_ltrip .C {
            background-color: blue;
        }

        .a_ltrip .P {
            background-color: orange;
        }

        .a_ltrip .div_FixWith {
            min-width: 150px;
        }

        .div_contStatus {
            width: 80px;
            height: 21px;
            text-align: center;
            border: 1px solid #e8e8e8;
            box-sizing: border-box;
            color: white;
            padding-top: 2px;
        }
    </style>
</head>
<body>
<form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="lab_PoNo" runat="server" Text="Cnt No">
                        </dxe:ASPxLabel>

                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_PoNo" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="Label1" runat="server" Text="From">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="Label2" runat="server" Text="To">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                     <td></td>
                    <td></td>
                </tr>
                <tr>
<%--                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Permit No">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_PermitNo" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="Lot No">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_LotNo" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="SKU Code">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_SKUCode" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>--%>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="Customer Ref">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_CustomerRef" ClientInstanceName="txt_CustomerRef" Width="120" runat="server" Style="margin-bottom: 0px">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="Ref Date">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_RefDate" Width="120" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                                        <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                        </dxe:ASPxButton></td>
                </tr>
                <tr>
<%--                    <td><dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Customer">
                    </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="txt_CustId" ClientInstanceName="txt_CustId" runat="server" Width="120px" HorizontalAlign="Left" AutoPostBack="False">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                            PopupCust(txt_CustId,txt_CustName);
                                }" />
                        </dxe:ASPxButtonEdit>
                    </td>--%>
<%--                    <td colspan="4">
                        <dxe:ASPxTextBox ID="txt_CustName" ClientInstanceName="txt_CustName" ReadOnly="true" BackColor="Control" Width="100%" runat="server" Style="margin-bottom: 0px">
                        </dxe:ASPxTextBox>
                    </td>--%>

                </tr>
            </table>
                        <wilson:DataSource ID="dsIssueDet3" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhDoDet3"
                KeyMember="Id" FilterExpression="1=0" />
            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="detailGrid" DataSourceID="dsIssueDet3" runat="server"  OnRowUpdating="grid_RowUpdating"
                KeyFieldName="Id" Width="100%" AutoGenerateColumns="False">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="Inline" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                        <DataItemTemplate>
                            <a onclick="ShowHouse('<%# Eval("DoNo") %>','<%# Eval("DoType") %>');"><%# Eval("DoNo") %></a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn
                        Caption="Cont No" FieldName="ContainerNo" VisibleIndex="3" Width="120" ReadOnly="true">
                    </dxwgv:GridViewDataTextColumn>
<%--                    <dxwgv:GridViewDataTextColumn
                        Caption="Client" FieldName="PartyName" VisibleIndex="3" Width="120">
                    </dxwgv:GridViewDataTextColumn>--%>
                    <dxwgv:GridViewDataTextColumn
                        Caption="Status" FieldName="ContainerStatus" VisibleIndex="3" Width="120">
                         <DataItemTemplate>
                            <div style="background-color: <%# ShowColor(SafeValue.SafeString(Eval("ContainerStatus"))) %>" class="div_contStatus">
                                <%# Eval("ContainerStatus") %>
                            </div>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxComboBox ID="cbb_StatusCode" ClientInstanceName="cbb_StatusCode" runat="server" Width="120" Value='<%# Bind("ContainerStatus") %>'>
                                <Items>
                                    <dxe:ListEditItem Value="New" Text="New" />
                                    <dxe:ListEditItem Value="Scheduled" Text="Scheduled" />
                                    <dxe:ListEditItem Value="Started" Text="Started" />
                                    <dxe:ListEditItem Value="Completed" Text="Completed" />
                                    <dxe:ListEditItem Value="Canceled" Text="Canceled" />
                                </Items>
                            </dxe:ASPxComboBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn
                        Caption="SealNo" FieldName="SealNo" VisibleIndex="3" Width="120" >
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn
                        Caption="Cont Type" FieldName="ContainerType" VisibleIndex="4" Width="120">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataSpinEditColumn
                        Caption="Weight" FieldName="Weight" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-SpinButtons-ShowIncrementButtons="false" PropertiesSpinEdit-DisplayFormatString="0.000" VisibleIndex="5" Width="100">
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataSpinEditColumn
                        Caption="Volume" FieldName="M3" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-SpinButtons-ShowIncrementButtons="false" PropertiesSpinEdit-DisplayFormatString="0.000" VisibleIndex="5" Width="100">
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataSpinEditColumn
                        Caption="Qty" FieldName="Qty" PropertiesSpinEdit-NumberType="Integer" PropertiesSpinEdit-SpinButtons-ShowIncrementButtons="false" VisibleIndex="5" Width="60">
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="PackageType" FieldName="PkgType" VisibleIndex="7" Width="120">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Job Start" FieldName="JobStart" VisibleIndex="7" Width="140">
                        <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd HH:mm" EditFormatString="yyyy/MM/dd HH:mm"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Job End" FieldName="JobEnd" VisibleIndex="7" Width="140">
                        <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd HH:mm" EditFormatString="yyyy/MM/dd HH:mm"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="8">
                        <DataItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxButton ID="btn_cont_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                            ClientSideEvents-Click='<%# "function(s) { detailGrid.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxButton ID="btn_cont_update" runat="server" Text="Update" Width="40" AutoPostBack="false" 
                                            ClientSideEvents-Click='<%# "function(s) { detailGrid.UpdateEdit() }"  %>'>
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_cont_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                            ClientSideEvents-Click='<%# "function(s) { detailGrid.CancelEdit() }"  %>'>
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                </Columns>
                <Settings ShowFooter="True" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="DoNo" SummaryType="Count" DisplayFormat="{0}" />
                </TotalSummary>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="600" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
