<%@ Page Title="CFS Import" Language="C#" AutoEventWireup="true" CodeFile="PhotoEdit.aspx.cs"
    Inherits="SelectPage_PhotoEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script type="text/javascript">
        function OnCallBack(v) {
            alert(v);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsCont" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJobDet1" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsContTrip" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJobDet2" KeyMember="Id" FilterExpression="1=0" />
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" OnCustomDataCallback="grid_CustomCallback">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <Settings ShowColumnHeaders="false" />
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="File" VisibleIndex="1" Width="100">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="RefNo" VisibleIndex="1" Width="80">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" VisibleIndex="2" Width="80">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="TripIndex" VisibleIndex="3" Width="140">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="FileName" VisibleIndex="4" Width="140">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="FileNote" VisibleIndex="5" Width="140">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <SettingsPager PageSize="1000" />
            <Templates>
                <DataRow>
                    <table border="0" style="border-bottom: solid 1px BLACK; width: 100%">
                        <tr style="font-size: large; font-family: Times New Roman">
                            <td width="60" rowspan="5">
                                <a href='<%# Eval("ImgPath")%>' target="_blank" style="padding:4px;">
                                    <dxe:ASPxImage ID="ASPxImage1" Width="200" Height="200" runat="server" ImageUrl='<%# Eval("ImgPath") %>'>
                                    </dxe:ASPxImage>
                                </a>
                            </td>
                            <td>
                               JobNo
                            </td>
                            <td style="border-right: solid 1px BLUE;">
                                <%# Eval("RefNo")%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Cont No
                            </td>
                            <td>
                                <dxe:ASPxComboBox ID="cmb_ContNo" Width="200" runat="server" Text='<%# Eval("ContainerNo") %>'
                                    DataSourceID="dsCont" TextField="ContainerNo" ValueField="ContainerNo">
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Name
                            </td>
                            <td>
                                <dxe:ASPxTextBox ID="txt_FileName" Width="200" BackColor="Control" ReadOnly="true"
                                    runat="server" Text='<%# Eval("FileName") %>'>
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                               Trip
                            </td>
                            <td>
                                <dxe:ASPxComboBox ID="cmb_Trip" Width="200" runat="server" Value='<%# Eval("TripId") %>'
                                    DataSourceID="dsContTrip" TextField="TripIndex" ValueField="Id"> 
                                    
                                </dxe:ASPxComboBox>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                Remarks
                            </td>
                            <td colspan="2">
                                <dxe:ASPxTextBox ID="txt_Rmk" runat="server" Text='<%# Eval("FileNote") %>' Width="300">
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>

                    </table>
                </DataRow>
                <EditForm>
                </EditForm>
            </Templates>
        </dxwgv:ASPxGridView>
	<br>
        <dxe:ASPxButton ID="ASPxButton2" runat="server" Text="Save" AutoPostBack="false"
            Width="100px">
            <ClientSideEvents Click="function(s, e) {
    grid.GetValuesOnCustomCallback('post',OnCallBack);
}" />
        </dxe:ASPxButton>
    </div>
    </form>
</body>
</html>
