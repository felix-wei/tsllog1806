<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="AccountStatus.aspx.cs" Inherits="MastData_AccountStatus" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> Account Status</title>
    <link href="/Style/StyleSheet.css"  rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsAcStatus" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXAccStatus" KeyMember="SequenceId" FilterExpression="1=0" />
            <table border="0">
                            <tr>
                            <td>
                            <asp:Label ID="Label2" runat="server" Text="Year"></asp:Label></td>
                            <td>
                                <dxe:ASPxSpinEdit ID="spin_Year" Width="100" runat="server" NumberType="Integer">
                                <SpinButtons ShowIncrementButtons="false" />
                                </dxe:ASPxSpinEdit>
                            </td>
                            <td>
                            <asp:Label ID="Label1" runat="server" Text="Period"></asp:Label>
                            </td>
                            <td>
                                <dxe:ASPxSpinEdit ID="spin_Period" Width="100" runat="server" NumberType="Integer">
                                <SpinButtons ShowIncrementButtons="false" />
                                </dxe:ASPxSpinEdit>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btn_filter" Width="120" runat="server" Text="Retrieve" OnClick="btn_filter_Click" />
                            </td>
                        </tr>
                    </table>
        <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" DataSourceID="dsAcStatus" Width="100%"
            KeyFieldName="SequenceId" AutoGenerateColumns="False">
             <SettingsBehavior ConfirmDelete="True" />
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsCustomizationWindow Enabled="True" />
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="Account Code" FieldName="AcCode" VisibleIndex="2" SortIndex="0" SortOrder="Ascending" Width="10%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="CR/DB" FieldName="AcDorc" VisibleIndex="3">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Open Amount" FieldName="OpenBal" VisibleIndex="4">
                <PropertiesTextEdit DisplayFormatString="C"></PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Transaction Amount" FieldName="CurrBal" VisibleIndex="5">
                <PropertiesTextEdit DisplayFormatString="C"></PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Close Amount" FieldName="CloseBal" VisibleIndex="5">
                <PropertiesTextEdit DisplayFormatString="C"></PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Code Description" FieldName="AcDesc" VisibleIndex="10">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
        </dxwgv:ASPxGridView>
    </div>
    </form>
</body>
</html>

