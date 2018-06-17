<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuoteTitle.aspx.cs" Inherits="PagesFreight_SeaQuoteTitle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Quotation Edit</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />


</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsQuotationTitle" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.SeaQuoteTitle" KeyMember="SequenceId" />
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
                        DataSourceID="dsQuotationTitle" KeyFieldName="SequenceId" OnRowUpdating="grid_InvDet_RowUpdating"
                        OnRowInserting="grid_InvDet_RowInserting" OnInitNewRow="grid_InvDet_InitNewRow" OnRowDeleting="grid_InvDet_RowDeleting"
                        OnInit="grid_InvDet_Init" Width="100%" AutoGenerateColumns="False">
                        <SettingsEditing Mode="Inline" />
                        <SettingsBehavior ConfirmDelete="true" />
                        <SettingsPager Mode="ShowAllRecords" />
                        <Columns>
                <dxwgv:GridViewCommandColumn  Width="5%" VisibleIndex="0">
                    <EditButton Visible="True" />
                    <DeleteButton Visible="True" />
                </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Title" FieldName="Title" VisibleIndex="3">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
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
