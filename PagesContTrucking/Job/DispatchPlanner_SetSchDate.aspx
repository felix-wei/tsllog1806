<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DispatchPlanner_SetSchDate.aspx.cs" Inherits="PagesContTrucking_Job_DispatchPlanner_SetSchDate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title></title>
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.AfterPopup(); }
        }
        document.onkeydown = keydown;
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsTransport" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.CtmJobDet1" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsContType" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.Container_Type" KeyMember="id" FilterExpression="" />


            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" DataSourceID="dsTransport" KeyFieldName="Id" AutoGenerateColumns="false" Width="100%" OnInit="grid_Transport_Init" OnBeforePerformDataSelect="grid_Transport_BeforePerformDataSelect" OnRowUpdating="grid_Transport_RowUpdating" OnRowUpdated="grid_Transport_RowUpdated">
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="EditForm" />
                <SettingsBehavior ConfirmDelete="true" />
                <Columns>
                    <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="30">
                        <DataItemTemplate>
                            <a href="#" onclick='<%# "detailGrid.StartEditRow("+Container.VisibleIndex+"); " %>'>Edit</a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="JobNo" Caption="Job No"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ContainerNo" Caption="Container No"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Container Type"></dxwgv:GridViewDataColumn>
                    <%--<dxwgv:GridViewDataColumn Caption="Schedule Date">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr( Eval("ScheduleDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>--%>
                    <dxwgv:GridViewDataColumn FieldName="SealNo" Caption="Seal No"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Weight" Caption="Weight"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Volume" Caption="Volume"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Qty" Caption="Qty"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="PackageType" Caption="PackageType"></dxwgv:GridViewDataColumn>
                </Columns>
                <Templates>
                    <EditForm>
                        <div style="display: none">
                            <dxe:ASPxTextBox ID="txt_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                        </div>

                        <table>
                            <tr>
                                <td>ContainerNo</td>
                                <td>
                                    <dxe:ASPxButtonEdit ID="btn_ContNo" ClientInstanceName="btn_ContNo" runat="server" Text='<%# Bind("ContainerNo") %>' AutoPostBack="False" Width="165">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupContainer(btn_ContNo,txt_ContType);
                                                                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                                <td>SealNo</td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_SealNo" runat="server" Text='<%# Bind("SealNo") %>' Width="165"></dxe:ASPxTextBox>
                                </td>
                                <td>ContainerType</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbbContType" ClientInstanceName="txt_ContType" runat="server" Width="165" DataSourceID="dsContType" ValueField="containerType" TextField="containerType" Value='<%# Bind("ContainerType") %>'></dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Weight
                                </td>
                                <td>
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="165"
                                        ID="spin_Wt" Height="21px" Value='<%# Bind("Weight")%>' DecimalPlaces="3" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                                <td>Volume
                                </td>
                                <td>
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="165"
                                        ID="spin_M3" Height="21px" Value='<%# Bind("Volume")%>' DecimalPlaces="3" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                                <td>Qty

                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <dxe:ASPxSpinEdit runat="server" Width="40"
                                                    ID="spin_Pkgs" Height="21px" Value='<%# Bind("Qty")%>' NumberType="Integer" Increment="0" DisplayFormatString="0">
                                                    <SpinButtons ShowIncrementButtons="false" />
                                                </dxe:ASPxSpinEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxButtonEdit ID="txt_PkgsType" ClientInstanceName="txt_PkgsType" runat="server"
                                                    Width="120" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("PackageType")%>'>
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupUom(txt_PkgsType,2);
                                                                    }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>ReqeustDate</td>
                                <td>
                                    <dxe:ASPxDateEdit ID="date_Cont_Request" runat="server" Width="165" Value='<%# Bind("RequestDate")%>'
                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td>ScheduleDate</td>
                                <td>
                                    <dxe:ASPxDateEdit ID="date_Cont_Schedule" runat="server" Width="165" Value='<%# Bind("ScheduleDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td>DgClass</td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" Width="165" ID="txt_DgClass" Text='<%# Bind("DgClass")%>'></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>CfsInDate</td>
                                <td>
                                    <dxe:ASPxDateEdit ID="date_Cont_CfsIn" runat="server" Width="165" Value='<%# Bind("CfsInDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td>CfsOutDate</td>
                                <td>
                                    <dxe:ASPxDateEdit ID="date_Cont_CfsOut" runat="server" Width="165" Value='<%# Bind("CfsOutDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td>PortnetStatus</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_PortnetStatus" runat="server" Value='<%# Bind("PortnetStatus") %>' DropDownStyle="DropDown" Width="165"></dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>YardPickupDate</td>
                                <td>
                                    <dxe:ASPxDateEdit ID="date_Cont_YardPickup" runat="server" Width="165" Value='<%# Bind("YardPickupDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td>YardReturnDate</td>
                                <td>
                                    <dxe:ASPxDateEdit ID="date_Cont_YardReturn" runat="server" Width="165" Value='<%# Bind("YardReturnDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td>StatusCode</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_StatusCode" runat="server" Width="165" Value='<%# Bind("StatusCode") %>'>
                                        <Items>
                                            <dxe:ListEditItem Value="New" Text="New" />
                                            <dxe:ListEditItem Value="Scheduled" Text="Scheduled" />
                                            <dxe:ListEditItem Value="InTransit" Text="InTransit" />
                                            <dxe:ListEditItem Value="Completed" Text="Completed" />
                                            <dxe:ListEditItem Value="Canceled" Text="Canceled" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>F5Ind</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_F5Ind" runat="server" Value='<%# Bind("F5Ind") %>' Width="165">
                                        <Items>
                                            <dxe:ListEditItem Value="Y" Text="Y" />
                                            <dxe:ListEditItem Value="N" Text="N" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>UrgentInd</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_UrgentInd" runat="server" Value='<%# Bind("UrgentInd") %>' Width="165">
                                        <Items>
                                            <dxe:ListEditItem Value="Y" Text="Y" />
                                            <dxe:ListEditItem Value="N" Text="N" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Remark</td>
                                <td colspan="3">
                                    <dxe:ASPxTextBox ID="txt_ContRemark" runat="server" Text='<%# Bind("Remark") %>' Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <div style="text-align: right; padding: 2px 2px 2px 2px">
                                        <span style="float: right">&nbsp
                                           <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                        </span>
                                        <span style='float: right; display: <%# Eval("canChange")%>'>
                                            <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                        </span>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>
        </div>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="500"
            Width="700" EnableViewState="False">
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
