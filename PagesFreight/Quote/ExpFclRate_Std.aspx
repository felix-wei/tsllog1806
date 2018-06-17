﻿<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ExpFclRate_Std.aspx.cs" Inherits="PagesFreight_Quote_ExpFclRate_Std" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Quotation Edit</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/BasePages.js" ></script>
    <script type="text/javascript" src="/Script/Pages.js" ></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsQuotationDet" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.SeaQuoteDet1" KeyMember="SequenceId" FilterExpression="FclLclInd='Fcl' and QuoteId = '-1'" />
        <table>
            <tr>
                <td>
                    <dxe:ASPxButton ID="ASPxButton1" Width="110" runat="server" Text="Add New" OnClick="btn_add_Click">
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <table width="700">
            <tr>
                <td colspan="6">
                    <dxwgv:ASPxGridView ID="grid_InvDet" ClientInstanceName="grid_det" runat="server"
                        DataSourceID="dsQuotationDet" KeyFieldName="SequenceId" OnRowUpdating="grid_InvDet_RowUpdating"
                        OnRowInserting="grid_InvDet_RowInserting" OnInitNewRow="grid_InvDet_InitNewRow"
                        OnInit="grid_InvDet_Init" Width="100%" AutoGenerateColumns="False">
                        <SettingsEditing Mode="EditForm" />
                        <SettingsPager Mode="ShowAllRecords" />
                        <Columns>
                            <dxwgv:GridViewDataColumn Caption="#" Width="10%">
                                <DataItemTemplate>
                                    <div style='display: block'>
                                        <a href="#" onclick='<%# "grid_det.StartEditRow("+Container.VisibleIndex+"); " %>'>Edit</a>
                                        <a href="#" onclick='if(confirm("Confirm Delete"))  {<%# "grid_det.DeleteRow("+Container.VisibleIndex+");"  %>}'>
                                            Del</a>
                                    </div>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="ChgDes" VisibleIndex="3"
                                Width="150">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="Currency" VisibleIndex="5"
                                Width="50">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Price" FieldName="Price" VisibleIndex="5"
                                Width="50">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="GstType" FieldName="GstType" VisibleIndex="5" Width="50">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Unit" FieldName="Unit" VisibleIndex="5" Width="50">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Amt" FieldName="Amt" VisibleIndex="5"
                                Width="50">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <Settings ShowFooter="true" />
                        <TotalSummary>
                            <dxwgv:ASPxSummaryItem FieldName="ChgCode" SummaryType="Count" DisplayFormat="{0:0}" />
                        </TotalSummary>
                        <Templates>
                            <EditForm>
                                <table style="border: solid 1px black;">
                                    <tr>
                                        <td>Charge Code
                                        </td>
                                        <td>
                                            <dxe:ASPxButtonEdit ID="txt_det_ChgCode" ClientInstanceName="txt_det_ChgCode" runat="server" Width="80"
                                                HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("ChgCode") %>' BackColor="Control" ReadOnly="true">
                                                <Buttons>
                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                </Buttons>
                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                        PopupChgCode(txt_det_ChgCode,txt_det_ChgCodeDes,txt_det_Unit,txt_det_GstType,null,null);
                                                            }" />
                                            </dxe:ASPxButtonEdit>
                                        </td>
                                        <td>
                                            Description
                                        </td>
                                        <td colspan="7">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_ChgCodeDes" ClientInstanceName="txt_det_ChgCodeDes"
                                                            runat="server" Text='<%# Bind("ChgDes") %>'>
                                                        </dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Currency
                                        </td>
                                        <td>
                                            <dxe:ASPxComboBox Width="80" ID="txt_det_Currency" ClientInstanceName="txt_det_Currency"
                                                runat="server" Text='<%# Bind("Currency") %>'>
                                                <Items>
                                                    <dxe:ListEditItem Text="SGD" Value="SGD" />
                                                    <dxe:ListEditItem Text="USD" Value="USD" />
                                                </Items>
                                            </dxe:ASPxComboBox>
                                        </td>
                                        <td>
                                            ExRate
                                        </td>
                                        <td>
                                            <dxe:ASPxSpinEdit Increment="0" Width="80" ID="spin_det_ExRate" ClientInstanceName="spin_det_ExRate"
                                                DisplayFormatString="0.000000" DecimalPlaces="6" ReadOnly="false" runat="server" Value='<%# Bind("ExRate") %>'>
                                                <SpinButtons ShowIncrementButtons="false" />
                                            </dxe:ASPxSpinEdit>
                                        </td>
                                        <td>
                                            Price
                                        </td>
                                        <td>
                                            <dxe:ASPxSpinEdit Increment="0" Width="80" ID="spin_det_Price" ClientInstanceName="spin_det_Price"
                                                DisplayFormatString="0.00" DecimalPlaces="2" ReadOnly="false" runat="server" Value='<%# Bind("Price") %>'>
                                                <SpinButtons ShowIncrementButtons="false" />
                                            </dxe:ASPxSpinEdit>
                                        </td>
                                        <td>
                                            Unit
                                        </td>
                                        <td>
                                            <dxe:ASPxComboBox Width="80" ID="txt_det_Unit" ClientInstanceName="txt_det_Unit"
                                                DropDownStyle="DropDown" runat="server" Value='<%# Bind("Unit") %>'>
                                                <Items>
                                                    <dxe:ListEditItem Text="20GP" Value="20GP" />
                                                    <dxe:ListEditItem Text="40GP" Value="40GP" />
                                                    <dxe:ListEditItem Text="40HC" Value="40HC" />
                                                    <dxe:ListEditItem Text="45GP" Value="45GP" />
                                                    <dxe:ListEditItem Text="45HC" Value="45HC" />
                                                </Items>
                                            </dxe:ASPxComboBox>
                                        </td>
                                        <td>
                                            GstType
                                        </td>
                                        <td>
                                            <dxe:ASPxComboBox Width="80" ID="txt_det_GstType" ClientInstanceName="txt_det_GstType"
                                                DropDownStyle="DropDown" runat="server" Value='<%# Bind("GstType") %>'>
                                                <Items>
                                                    <dxe:ListEditItem Text="S" Value="S" />
                                                    <dxe:ListEditItem Text="Z" Value="Z" />
                                                    <dxe:ListEditItem Text="E" Value="E" />
                                                </Items>
                                            </dxe:ASPxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Remark
                                        </td>
                                        <td colspan="9">
                                            <dxe:ASPxMemo Width="100%" ID="txt_det_Remakrs3" Rows="3" runat="server" Text='<%# Bind("Rmk") %>'>
                                            </dxe:ASPxMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8" style="text-align: right; padding: 2px 2px 2px 2px">
                                            <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton1" ReplacementType="EditFormUpdateButton"
                                                runat="server">
                                            </dxwgv:ASPxGridViewTemplateReplacement>
                                            <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton1" ReplacementType="EditFormCancelButton"
                                                runat="server">
                                            </dxwgv:ASPxGridViewTemplateReplacement>
                                        </td>
                                    </tr>
                                </table>
                            </EditForm>
                        </Templates>
                    </dxwgv:ASPxGridView>
                </td>
            </tr>
        </table>
    </div>
    <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
        HeaderText="Ar Invoice Edit" AllowDragging="True" EnableAnimation="False" Height="400"
        Width="800" EnableViewState="False">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
