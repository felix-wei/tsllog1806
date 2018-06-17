<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="AccountPeriod.aspx.cs" Inherits="MastData_AccountPeriod" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> Account Period</title>
    <link href="/Style/StyleSheet.css"  rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsGlAccPeriod" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXAccPeriod" KeyMember="SequenceId"/>
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
                                <dxe:ASPxButton ID="btn_filter" Width="120" runat="server" Text="Retrieve" OnClick="btn_filter_Click" />
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btn_Add" Width="130" runat="server" Text="Add Finance Year" 
                                    onclick="btn_Add_Click" />
                            </td>
                        </tr>
                    </table>

        <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" DataSourceID="dsGlAccPeriod" Width="880"
            KeyFieldName="SequenceId" AutoGenerateColumns="False"
            oninit="ASPxGridView1_Init" >
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsBehavior ConfirmDelete="True" />
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="Year" ReadOnly="true" FieldName="Year" VisibleIndex="1" Width="10%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Period" ReadOnly="true" FieldName="Period" VisibleIndex="2" Width="10%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataDateColumn Caption="Start" FieldName="StartDate" VisibleIndex="3">
                <PropertiesDateEdit EditFormat="Custom" EditFormatString="dd/MM/yyyy"  DisplayFormatString="dd/MM/yyyy"/>
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataDateColumn Caption="End" FieldName="EndDate" VisibleIndex="4">
                <PropertiesDateEdit EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy" />
                </dxwgv:GridViewDataDateColumn>
            </Columns>
        </dxwgv:ASPxGridView>
    </div>
    </form>
</body>
</html>

