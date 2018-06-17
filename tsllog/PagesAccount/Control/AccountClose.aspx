<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="AccountClose.aspx.cs" Inherits="MastData_AccountClose" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Account Close</title>
    <script type="text/javascript">
        function OnCallback(v) {
            alert(v);
            grid.Refresh();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <table border="0">
                            <tr>
                            <td>
                                <dxe:ASPxButton ID="btn1" Width="160" runat="server" Text="Close Current Peroid" AutoPostBack="false" UseSubmitBehavior="false">
                                <ClientSideEvents Click="function(s,e){
                                grid.GetValuesOnCustomCallback('ClosePeriod',OnCallback);
                                }" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btn2" Width="160" runat="server" Text="Open Previous Peroid" AutoPostBack="false" UseSubmitBehavior="false">
                                <ClientSideEvents Click="function(s,e){
                                grid.GetValuesOnCustomCallback('OpenPeriod',OnCallback);
                                }" />
                                </dxe:ASPxButton>
                            </td><td></td>
                        </tr>
                    </table>

        <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" Width="300"
            KeyFieldName="SequenceId" AutoGenerateColumns="False" oncustomdatacallback="ASPxGridView1_CustomDataCallback" >
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="Year" FieldName="Year" VisibleIndex="1">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Period" FieldName="Period" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
        </dxwgv:ASPxGridView>
    </div>
    </form>
</body>
</html>

