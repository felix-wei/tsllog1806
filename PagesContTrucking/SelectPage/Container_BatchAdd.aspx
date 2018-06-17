<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Container_BatchAdd.aspx.cs" Inherits="PagesContTrucking_SelectPage_Container_BatchAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="../../Script/ContTrucking/JobEdit.js"></script>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <title></title>
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.Popup_ContainerBatchAdd_callback('false'); }
        }
        document.onkeydown = keydown;
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsContType" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.Container_Type" KeyMember="id" FilterExpression="" />
            <table style="width: 100%;">
                <tr>
                    <td>JobNo:</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_JobNo" runat="server" ReadOnly="true" Width="165"></dxe:ASPxTextBox>
                    </td>
                    <td colspan="2">
            <dxe:ASPxButton ID="btn_Add" runat="server" Text="Save All" OnClick="btn_Add_Click"></dxe:ASPxButton></td>
                </tr>
                <tr>
                    <td style="border-top: 1px solid #808080" colspan="7">&nbsp;</td>
                </tr>
                <tr>
                    <td>ContainerNo</td>
                    <td>
                        <dxe:ASPxButtonEdit ID="btn_ContNo" ClientInstanceName="btn_ContNo" runat="server" AutoPostBack="False" Width="165">
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
                        <dxe:ASPxTextBox ID="txt_SealNo" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                    <td>ContainerType</td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_ContType" ClientInstanceName="txt_ContType" runat="server" Width="165" DataSourceID="dsContType" ValueField="containerType" TextField="containerType"></dxe:ASPxComboBox>
                    </td>
                    <td style="width: 100%"></td>
                </tr>
                <tr>
                    <td style="vertical-align: central" rowspan="2">
                        <a href="#" onclick="PopupCustAdr(null,txt_YardAddress);">YardAddress</a>
                    </td>
                    <td colspan="3" rowspan="2">
                        <dxe:ASPxMemo ID="txt_YardAddress" ClientInstanceName="txt_YardAddress" Rows="3" runat="server" Width="100%">
                        </dxe:ASPxMemo>
                    </td>
                    <td>date</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_YardExpiry" Width="165" runat="server" 
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <td>Remark</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Remark" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="border-top: 1px solid #808080" colspan="7">&nbsp;</td>
                </tr>

                <tr>
                    <td>ContainerNo</td>
                    <td>
                        <dxe:ASPxButtonEdit ID="btn_ContNo1" ClientInstanceName="btn_ContNo1" runat="server" AutoPostBack="False" Width="165">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupContainer(btn_ContNo1,txt_ContType1);
                                                                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td>SealNo</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_SealNo1" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                    <td>ContainerType</td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_ContType1" ClientInstanceName="txt_ContType1" runat="server" Width="165" DataSourceID="dsContType" ValueField="containerType" TextField="containerType"></dxe:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: central" rowspan="2">
                        <a href="#" onclick="PopupCustAdr(null,txt_YardAddress1);">YardAddress</a>
                    </td>
                    <td colspan="3" rowspan="2">
                        <dxe:ASPxMemo ID="txt_YardAddress1" ClientInstanceName="txt_YardAddress1" Rows="3" runat="server" Width="100%">
                        </dxe:ASPxMemo>
                    </td>
                    <td>date</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_YardExpiry1" ClientInstanceName="date_YardExpiry1" Width="165" runat="server" 
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <td>Remark</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Remark1" ClientInstanceName="txt_Remark1" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="border-top: 1px solid #808080" colspan="7">&nbsp;</td>
                </tr>

                <tr>
                    <td>ContainerNo</td>
                    <td>
                        <dxe:ASPxButtonEdit ID="btn_ContNo2" ClientInstanceName="btn_ContNo2" runat="server" AutoPostBack="False" Width="165">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupContainer(btn_ContNo2,txt_ContType2);
                                                                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td>SealNo</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_SealNo2" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                    <td>ContainerType</td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_ContType2" ClientInstanceName="txt_ContType2" runat="server" Width="165" DataSourceID="dsContType" ValueField="containerType" TextField="containerType"></dxe:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: central" rowspan="2">
                        <a href="#" onclick="PopupCustAdr(null,txt_YardAddress2);">YardAddress</a>
                    </td>
                    <td colspan="3" rowspan="2">
                        <dxe:ASPxMemo ID="txt_YardAddress2" ClientInstanceName="txt_YardAddress2" Rows="3" runat="server" Width="100%">
                        </dxe:ASPxMemo>
                    </td>
                    <td>date</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_YardExpiry2" ClientInstanceName="date_YardExpiry2" Width="165" runat="server" 
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <td>Remark</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Remark2" ClientInstanceName="txt_Remark2" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="border-top: 1px solid #808080" colspan="7">&nbsp;</td>
                </tr>

                <tr>
                    <td>ContainerNo</td>
                    <td>
                        <dxe:ASPxButtonEdit ID="btn_ContNo3" ClientInstanceName="btn_ContNo3" runat="server" AutoPostBack="False" Width="165">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupContainer(btn_ContNo3,txt_ContType3);
                                                                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td>SealNo</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_SealNo3" ClientInstanceName="txt_SealNo3" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                    <td>ContainerType</td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_ContType3" ClientInstanceName="txt_ContType3" runat="server" Width="165" DataSourceID="dsContType" ValueField="containerType" TextField="containerType"></dxe:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: central" rowspan="2">
                        <a href="#" onclick="PopupCustAdr(null,txt_YardAddress3);">YardAddress</a>
                    </td>
                    <td colspan="3" rowspan="2">
                        <dxe:ASPxMemo ID="txt_YardAddress3" ClientInstanceName="txt_YardAddress3" Rows="3" runat="server" Width="100%">
                        </dxe:ASPxMemo>
                    </td>
                    <td>date</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_YardExpiry3" ClientInstanceName="date_YardExpiry3" Width="165" runat="server" 
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <td>Remark</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Remark3" ClientInstanceName="txt_Remark3" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="border-top: 1px solid #808080" colspan="7">&nbsp;</td>
                </tr>

                <tr>
                    <td>ContainerNo</td>
                    <td>
                        <dxe:ASPxButtonEdit ID="btn_ContNo4" ClientInstanceName="btn_ContNo4" runat="server" AutoPostBack="False" Width="165">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupContainer(btn_ContNo4,txt_ContType4);
                                                                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td>SealNo</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_SealNo4" ClientInstanceName="txt_SealNo4" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                    <td>ContainerType</td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_ContType4" ClientInstanceName="txt_ContType4" runat="server" Width="165" DataSourceID="dsContType" ValueField="containerType" TextField="containerType"></dxe:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: central" rowspan="2">
                        <a href="#" onclick="PopupCustAdr(null,txt_YardAddress4);">YardAddress</a>
                    </td>
                    <td colspan="3" rowspan="2">
                        <dxe:ASPxMemo ID="txt_YardAddress4" ClientInstanceName="txt_YardAddress4" Rows="3" runat="server" Width="100%">
                        </dxe:ASPxMemo>
                    </td>
                    <td>date</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_YardExpiry4" ClientInstanceName="date_YardExpiry4" Width="165" runat="server" 
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <td>Remark</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Remark4" ClientInstanceName="txt_Remark4" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="border-top: 1px solid #808080" colspan="7">&nbsp;</td>
                </tr>

                <tr>
                    <td>ContainerNo</td>
                    <td>
                        <dxe:ASPxButtonEdit ID="btn_ContNo5" ClientInstanceName="btn_ContNo5" runat="server" AutoPostBack="False" Width="165">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupContainer(btn_ContNo5,txt_ContType5);
                                                                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td>SealNo</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_SealNo5" ClientInstanceName="txt_SealNo5" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                    <td>ContainerType</td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_ContType5" ClientInstanceName="txt_ContType5" runat="server" Width="165" DataSourceID="dsContType" ValueField="containerType" TextField="containerType"></dxe:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: central" rowspan="2">
                        <a href="#" onclick="PopupCustAdr(null,txt_YardAddress5);">YardAddress</a>
                    </td>
                    <td colspan="3" rowspan="2">
                        <dxe:ASPxMemo ID="txt_YardAddress5" ClientInstanceName="txt_YardAddress5" Rows="3" runat="server" Width="100%">
                        </dxe:ASPxMemo>
                    </td>
                    <td>date</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_YardExpiry5" ClientInstanceName="date_YardExpiry5" Width="165" runat="server" 
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <td>Remark</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Remark5" ClientInstanceName="txt_Remark5" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="border-top: 1px solid #808080" colspan="7">&nbsp;</td>
                </tr>


                <tr>
                    <td>ContainerNo</td>
                    <td>
                        <dxe:ASPxButtonEdit ID="btn_ContNo6" ClientInstanceName="btn_ContNo6" runat="server" AutoPostBack="False" Width="165">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupContainer(btn_ContNo6,txt_ContType6);
                                                                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td>SealNo</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_SealNo6" ClientInstanceName="txt_SealNo6" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                    <td>ContainerType</td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_ContType6" ClientInstanceName="txt_ContType6" runat="server" Width="165" DataSourceID="dsContType" ValueField="containerType" TextField="containerType"></dxe:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: central" rowspan="2">
                        <a href="#" onclick="PopupCustAdr(null,txt_YardAddress6);">YardAddress</a>
                    </td>
                    <td colspan="3" rowspan="2">
                        <dxe:ASPxMemo ID="txt_YardAddress6" ClientInstanceName="txt_YardAddress6" Rows="3" runat="server" Width="100%">
                        </dxe:ASPxMemo>
                    </td>
                    <td>date</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_YardExpiry6" ClientInstanceName="date_YardExpiry6" Width="165" runat="server" 
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <td>Remark</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Remark6" ClientInstanceName="txt_Remark6" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="border-top: 1px solid #808080" colspan="7">&nbsp;</td>
                </tr>

                <tr>
                    <td>ContainerNo</td>
                    <td>
                        <dxe:ASPxButtonEdit ID="btn_ContNo7" ClientInstanceName="btn_ContNo7" runat="server" AutoPostBack="False" Width="165">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupContainer(btn_ContNo7,txt_ContType7);
                                                                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td>SealNo</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_SealNo7" ClientInstanceName="txt_SealNo7" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                    <td>ContainerType</td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_ContType7" ClientInstanceName="txt_ContType7" runat="server" Width="165" DataSourceID="dsContType" ValueField="containerType" TextField="containerType"></dxe:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: central" rowspan="2">
                        <a href="#" onclick="PopupCustAdr(null,txt_YardAddress7);">YardAddress</a>
                    </td>
                    <td colspan="3" rowspan="2">
                        <dxe:ASPxMemo ID="txt_YardAddress7" ClientInstanceName="txt_YardAddress7" Rows="3" runat="server" Width="100%">
                        </dxe:ASPxMemo>
                    </td>
                    <td>date</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_YardExpiry7" ClientInstanceName="date_YardExpiry7" Width="165" runat="server" 
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <td>Remark</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Remark7" ClientInstanceName="txt_Remark7" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="border-top: 1px solid #808080" colspan="7">&nbsp;</td>
                </tr>

            </table>

        </div>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="450"
            Width="600" EnableViewState="False">
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
