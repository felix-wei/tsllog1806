<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function LockJob(partyId) {
            grid.GetValuesOnCustomCallback(partyId, onCallback)
        }
        function onCallback(v) {
            grid.Refresh();
        }
        function ShowNews(id)
        {
            popubCtr.SetContentUrl('/PagesHr/MasterData/NewsEdit.aspx?id=' + id);
            popubCtr.SetHeaderText('News');
            popubCtr.Show();
        }
        function ClosePopupCtr() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
        }
    </script>
    <script type="text/javascript">
        setInterval("txt_Time.SetText((new Date()).getDate() + '/' + ((new Date()).getMonth() + 1) + '/' + (new Date()).getFullYear() + ' ' + (new Date()).getHours() + ':' + (new Date()).getMinutes() + ':' + (new Date()).getSeconds())", 1000)
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsTask" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrTask" KeyMember="Id" FilterExpression="1=0" />
            <div>
                <table>
                    <tr>
                        <td width="48%"></td>
                        <td>
                            <dxe:ASPxLabel ID="txt_Time" ClientInstanceName="txt_Time" runat="server" Width="120"></dxe:ASPxLabel>
                        </td>
                        <td width="30%"></td>
                        <td>
                            <dxe:ASPxButton ID="btn_LogOn" runat="server" Text="LogOn" Width="100" OnClick="btn_LogOn_Click">
                            </dxe:ASPxButton>
                        </td>
                        <td>
                            <dxe:ASPxButton ID="btn_LogOff" runat="server" Text="LogOff" Width="100" OnClick="btn_LogOff_Click">
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                </table>
                <hr />
                <table>
                    <tr style="vertical-align:top">
                        <td>
                            <table>
                                <tr style="font-weight: bold; font-size: 11px">
                                    <td width="8"></td>
                                    <td width="100">Name
                                    </td>
                                    <td width="90">Department
                                    </td>
                                    <td width="110">Email
                                    </td>
                                    <td width="110">Telephone
                                    </td>
                                    <td width="90">Birthday
                                    </td>
                                </tr>
                            </table>
                            <dxwgv:ASPxGridView ID="Grid_BOD" ClientInstanceName="Grid_Task" runat="server"
                                KeyFieldName="Id" Width="500"
                                OnCustomDataCallback="grid_CustomDataCallback">
                                <SettingsPager Mode="ShowAllRecords" />
                                <Settings ShowColumnHeaders="false" />
                                <Templates>
                                    <DataRow>
                                        <div style="padding: 5px">
                                            <table width="500" style="border-bottom: solid 1px black;">
                                                <tr style="font-weight: bold; font-size: 11px">
                                                    <td style="width: 100px">
                                                        <dxe:ASPxTextBox Width="100" ID="txt_Name" runat="server" ReadOnly="true" Border-BorderWidth="0"
                                                            Value='<%# Eval("Name")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 90px">
                                                        <dxe:ASPxTextBox Width="90" ID="txt_Department" runat="server" ReadOnly="true" Border-BorderWidth="0"
                                                            Value='<%# Eval("Department")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 110px">
                                                        <dxe:ASPxTextBox Width="110" ID="txt_Email" runat="server" ReadOnly="true" Border-BorderWidth="0"
                                                            Value='<%# Eval("Email")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 110px">
                                                        <dxe:ASPxTextBox Width="110" ID="txt_Telephone" runat="server" ReadOnly="true" Border-BorderWidth="0"
                                                            Value='<%# Eval("Telephone")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 90px">
                                                        <dxe:ASPxTextBox runat="server" ID="txt_Birthday" EditFormat="Custom" DisplayFormatString="dd/MM/yyyy" Width="90" ReadOnly="true"
                                                            Value='<%# Eval("Birthday").ToString() =="1/1/0001 12:00:00 AM" ? "" : DataBinder.Eval(Container.DataItem,"Birthday","{0:dd/MM/yyyy}") %>' Border-BorderWidth="0">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="50" valign="top">
                                                        <div style="display: none">
                                                            <dxe:ASPxTextBox ID="txt_detId" runat="server"
                                                                Text='<%# Eval("Id") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </DataRow>
                                </Templates>
                            </dxwgv:ASPxGridView>
                        </td>
                        <td>
                            <table>
                                <tr style="font-weight: bold; font-size: 11px">
                                    <td width="8"></td>
                                    <td width="100">#
                                    </td>
                                    <td width="90">Date
                                    </td>
                                    <td width="110">Department
                                    </td>
                                    <td width="110">PIC
                                    </td>
                                </tr>
                            </table>
                            <dxwgv:ASPxGridView ID="Grid_Interview" ClientInstanceName="Grid_Task" runat="server"
                                KeyFieldName="Id" Width="500"
                                OnCustomDataCallback="grid_CustomDataCallback">
                                <SettingsPager Mode="ShowAllRecords" />
                                <Settings ShowColumnHeaders="false" />
                                <Templates>
                                    <DataRow>
                                        <div style="padding: 5px">
                                            <table width="500" style="border-bottom: solid 1px black;">
                                                <tr style="font-weight: bold; font-size: 11px">
                                                    <td style="width: 100px">
                                                        <dxe:ASPxTextBox Width="100" ID="txt_Name" runat="server" ReadOnly="true" Border-BorderWidth="0"
                                                            Value='<%# Eval("Name")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 90px">
                                                        <dxe:ASPxTextBox runat="server" ID="txt_Birthday" EditFormat="Custom" DisplayFormatString="dd/MM/yyyy" Width="90" ReadOnly="true"
                                                            Value='<%# Eval("Date").ToString() =="1/1/0001 12:00:00 AM" ? "" : DataBinder.Eval(Container.DataItem,"Date","{0:dd/MM/yyyy}") %>' Border-BorderWidth="0">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 90px">
                                                        <dxe:ASPxTextBox Width="90" ID="txt_Department" runat="server" ReadOnly="true" Border-BorderWidth="0"
                                                            Value='<%# Eval("Department")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 110px">
                                                        <dxe:ASPxTextBox Width="110" ID="txt_PIC" runat="server" ReadOnly="true" Border-BorderWidth="0"
                                                            Value='<%# Eval("PIC")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="50" valign="top">
                                                        <div style="display: none">
                                                            <dxe:ASPxTextBox ID="txt_detId" runat="server"
                                                                Text='<%# Eval("Id") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </DataRow>
                                </Templates>
                            </dxwgv:ASPxGridView>
                        </td>
                    </tr>
                </table>
                <hr />
                <table>
                    <tr>
                        <td style="width: 500px; text-align: center">
                            <b>Task</b>
                        </td>
                        <td width="280"></td>
                        <td style="width: 50px; text-align: center">
                            <b>News</b>
                        </td>
                        <td>
                            <a href="#" onclick='ShowNews(0)' style="text-decoration:none">+</a>
                        </td>
                    </tr>
                </table>
                <hr />
                <table>
                    <tr style="vertical-align:top">
                        <td>
                            <table>
                                <tr style="font-weight: bold; font-size: 11px">
                                    <td width="8"></td>
                                    <td width="125">Name
                                    </td>
                                    <td width="80">Date
                                    </td>
                                    <td width="150">Content
                                    </td>
                                    <td width="90">Status
                                    </td>
                                </tr>
                            </table>
                            <dxwgv:ASPxGridView ID="Grid_Task" ClientInstanceName="Grid_Task" runat="server"
                                KeyFieldName="Id" Width="500"
                                OnCustomDataCallback="grid_CustomDataCallback">
                                <SettingsPager Mode="ShowAllRecords" />
                                <Settings ShowColumnHeaders="false" />
                                <Templates>
                                    <DataRow>
                                        <div style="padding: 5px">
                                            <table width="500" style="border-bottom: solid 1px black;">
                                                <tr style="font-weight: bold; font-size: 11px">
                                                    <td style="width: 100px">
                                                        <dxe:ASPxTextBox Width="110" ID="ASPxTextBox2" runat="server" ReadOnly="true" Border-BorderWidth="0"
                                                            Value='<%# Eval("Person")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 90px">

                                                        <dxe:ASPxTextBox runat="server" ID="txt_Date" EditFormat="Custom" DisplayFormatString="dd/MM/yyyy" Width="90" ReadOnly="true"
                                                            Value='<%# Eval("Date").ToString() =="1/1/0001 12:00:00 AM" ? "" : DataBinder.Eval(Container.DataItem,"Date","{0:dd/MM/yyyy}") %>' Border-BorderWidth="0">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 150px">
                                                        <dxe:ASPxTextBox Width="150" ID="txt_RefNo" runat="server" ReadOnly="true" Border-BorderWidth="0"
                                                            Value='<%# Eval("Remark")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 90px">
                                                        <dxe:ASPxTextBox Width="90" ID="txt_Status" runat="server" ReadOnly="true" Border-BorderWidth="0"
                                                            Value='<%# Eval("Status")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="50" valign="top">
                                                        <div style="display: none">
                                                            <dxe:ASPxTextBox ID="txt_detId" runat="server"
                                                                Text='<%# Eval("Id") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </DataRow>
                                </Templates>
                            </dxwgv:ASPxGridView>
                        </td>
                        <td>
                            <table cellspacing="0" cellpadding="0" width="100%">
                                <asp:Repeater runat="server" ID="rpt_News">
                                    <ItemTemplate>
                                        <tr style="font-weight: bold; font-size: 11px">
                                            <td id="Title" style="text-align: center; width: 550px">
                                                <a href="#" onclick='ShowNews("<%# Eval("Id")%>")'><%#SafeValue.SafeString(Eval("Title")).Length<1?"NOTICE":Eval("Title")%></a>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxe:ASPxMemo runat="server" ID="memo_Note" ClientInstanceName="memo_Note" Width="550" ReadOnly="true" Height="150" Border-BorderWidth="0" Text='<%#Eval("Note") %>'>
                                                </dxe:ASPxMemo>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <hr />
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="400"
                Width="650" EnableViewState="False">
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
