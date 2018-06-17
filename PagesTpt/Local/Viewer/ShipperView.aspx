<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShipperView.aspx.cs" Inherits="PagesTpt_Job_ShipperView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Transport Job</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/BasePages.js"></script>
    <script type="text/javascript">
        function onCallback(v) {

        }
        function PopupPhotoView(txt_JobNo) {
            popubCtr1.SetHeaderText('Attachment');
            popubCtr1.SetContentUrl('Attachments.aspx?Type=Tpt&JobNo=' + txt_JobNo);
            popubCtr1.Show();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsTransport" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.TptJob" KeyMember="Id" FilterExpression="1=0" />
            <table>
                <tr>
                    <td style="display: none">
                        <dxe:ASPxTextBox ID="txt_type" ClientInstanceName="txt_type" runat="server"></dxe:ASPxTextBox>
                    </td>
                    <td>Job No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_TptNo" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>From
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>To
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td colspan="2">
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton7" Width="75" runat="server" Text="Add" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                                       document.getElementById('div_add').style.display='block';
                                                        }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <div id="div_add" style="display: none">
                <table style="width: 2000px">
                    <tr>
                        <td></td>
                        <td width="100"></td>
                        <td width="110"></td>
                        <td width="100"></td>
                        <td width="110"></td>
                        <td></td>
                        <td width="110"></td>
                        <td></td>
                        <td width="110"></td>
                        <td></td>
                        <td width="110"></td>
                        <td></td>
                        <td width="110"></td>
                        <td></td>
                        <td width="150"></td>
                        <td></td>
                        <td width="150"></td>
                        <td></td>
                        <td width="150"></td>
                        <td></td>
                        <td width="150"></td>
                    </tr>
                    <tr>
                        <td rowspan="2" style="color: red;">Job1</td>
                        <td>Client Job No
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_BkgRef" Width="100%" runat="server" Text='<%# Bind("BkgRef") %>'>
                            </dxe:ASPxTextBox>
                        </td>

                        <td>Job Type</td>
                        <td>
                            <dxe:ASPxComboBox ID="cmb_JobType" Width="100%" runat="server" Text='<%# Bind("JobType") %>'>
                                <Items>
                                    <dxe:ListEditItem Text="IMP" Value="IMP" />
                                    <dxe:ListEditItem Text="EXP" Value="EXP" />
                                    <dxe:ListEditItem Text="TSP" Value="TSP" />
                                    <dxe:ListEditItem Text="LOC" Value="LOC" />
                                </Items>
                            </dxe:ASPxComboBox>
                        </td>
                        <td>PIC
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_CustPic" ClientInstanceName="txt_CustPic" Width="100%" runat="server" Text='<%# Bind("CustPic") %>'>
                            </dxe:ASPxTextBox>
                        </td>
                        <td>Vessel
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_Ves" Width="100%" runat="server" Text='<%# Bind("Vessel") %>'>
                            </dxe:ASPxTextBox>
                        </td>
                        <td>Voyage
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_Voy" Width="100%" runat="server" Text='<%# Bind("Voyage") %>'>
                            </dxe:ASPxTextBox>
                        </td>
                        <td>Pol
                        </td>
                        <td>
                            <dxe:ASPxButtonEdit ID="txt_Pol" ClientInstanceName="txt_Pol" runat="server" Text='<%# Bind("Pol") %>' Width="100%" MaxLength="5">
                                <Buttons>
                                    <dxe:EditButton Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                            PopupPort(txt_Pol,null);
                        }" />
                            </dxe:ASPxButtonEdit>
                        </td>
                        <td>Pod
                        </td>
                        <td>
                            <dxe:ASPxButtonEdit ID="txt_Pod" ClientInstanceName="txt_Pod" runat="server" Text='<%# Bind("Pod") %>' Width="100%" MaxLength="5">
                                <Buttons>
                                    <dxe:EditButton Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                            PopupPort(txt_Pod,null);
                        }" />
                            </dxe:ASPxButtonEdit>
                        </td>
                        <td>Eta
                        </td>
                        <td>
                            <dxe:ASPxDateEdit ID="date_Eta" runat="server" Width="100%" Value='<%# Bind("Eta") %>'
                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </dxe:ASPxDateEdit>
                        </td>
                        <td>Etd
                        </td>
                        <td>
                            <dxe:ASPxDateEdit ID="date_Etd" runat="server" Width="100%" Value='<%# Bind("Etd") %>'
                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </dxe:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td>Date
                        </td>
                        <td>
                            <dxe:ASPxDateEdit ID="date_BkgDate" runat="server" Width="100%" Value='<%# Bind("BkgDate") %>'
                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </dxe:ASPxDateEdit>
                        </td>
                        <td>Time
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_BkgTime" Width="100%" runat="server" Text='<%# Bind("BkgTime") %>'>
                                <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                <ValidationSettings ErrorDisplayMode="None" />
                            </dxe:ASPxTextBox>
                        </td>
                        <td>Weight
                        </td>
                        <td>
                            <dxe:ASPxSpinEdit ID="spin_BkgWt" runat="server" Increment="0" Width="100%" DisplayFormatString="0.000" DecimalPlaces="3"
                                Value='<%# Bind("BkgWt") %>'>
                                <SpinButtons ShowIncrementButtons="False">
                                </SpinButtons>
                            </dxe:ASPxSpinEdit>
                        </td>
                        <td>Volume
                        </td>
                        <td>
                            <dxe:ASPxSpinEdit ID="spin_BkgM3" runat="server" Increment="0" Width="100%" DisplayFormatString="0.000" DecimalPlaces="3"
                                Value='<%# Bind("BkgM3") %>'>
                                <SpinButtons ShowIncrementButtons="False">
                                </SpinButtons>
                            </dxe:ASPxSpinEdit>
                        </td>
                        <td>Qty
                        </td>
                        <td>
                            <dxe:ASPxSpinEdit ID="spin_BkgQty" runat="server" Width="100%" Increment="0" Number="0"
                                NumberType="Integer" Value='<%# Bind("BkgQty") %>'>
                                <SpinButtons ShowIncrementButtons="False">
                                </SpinButtons>
                            </dxe:ASPxSpinEdit>
                        </td>
                        <td>PackType
                        </td>
                        <td>
                            <dxe:ASPxButtonEdit ID="txt_BkgPackType" ClientInstanceName="txt_BkgPackType" runat="server" Text='<%# Bind("BkgPkgType") %>' Width="100%">
                                <Buttons>
                                    <dxe:EditButton Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                            PopupUom(txt_BkgPackType,2);
                        }" />
                            </dxe:ASPxButtonEdit>
                        </td>
                        <td>Remark
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_JobRmk" Width="100%" runat="server" Text='<%# Bind("JobRmk") %>'>
                            </dxe:ASPxTextBox>
                        </td>
                        <td>Description
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_cargoDes" Rows="4" Width="99%" runat="server" Text='<%# Bind("CargoDesc") %>'>
                            </dxe:ASPxTextBox>
                        </td>
                        <td>From
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_PickupFrm1" ClientInstanceName="txt_PickupFrm1" Rows="4" Width="100%" runat="server" Text='<%# Bind("PickFrm1") %>'>
                            </dxe:ASPxTextBox>
                        </td>
                        <td>To
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_DeliveryTo1" ClientInstanceName="txt_DeliveryTo1" Rows="4" Width="100%" runat="server" Text='<%# Bind("DeliveryTo1") %>'>
                            </dxe:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="22">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2" style="color: red;">Job2</td>
                        <td>Client Job No
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_BkgRef2" Width="100%" runat="server" Text='<%# Bind("BkgRef") %>'>
                            </dxe:ASPxTextBox>
                        </td>

                        <td>Job Type</td>
                        <td>
                            <dxe:ASPxComboBox ID="cmb_JobType2" Width="100%" runat="server" Text='<%# Bind("JobType") %>'>
                                <Items>
                                    <dxe:ListEditItem Text="IMP" Value="IMP" />
                                    <dxe:ListEditItem Text="EXP" Value="EXP" />
                                    <dxe:ListEditItem Text="TSP" Value="TSP" />
                                    <dxe:ListEditItem Text="LOC" Value="LOC" />
                                </Items>
                            </dxe:ASPxComboBox>
                        </td>
                        <td>PIC
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_CustPic2" ClientInstanceName="txt_CustPic2" Width="100%" runat="server" Text='<%# Bind("CustPic") %>'>
                            </dxe:ASPxTextBox>
                        </td>
                        <td>Vessel
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_Ves2" Width="100%" runat="server" Text='<%# Bind("Vessel") %>'>
                            </dxe:ASPxTextBox>
                        </td>
                        <td>Voyage
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_Voy2" Width="100%" runat="server" Text='<%# Bind("Voyage") %>'>
                            </dxe:ASPxTextBox>
                        </td>
                        <td>Pol
                        </td>
                        <td>
                            <dxe:ASPxButtonEdit ID="txt_Pol2" ClientInstanceName="txt_Pol2" runat="server" Text='<%# Bind("Pol") %>' Width="100%" MaxLength="5">
                                <Buttons>
                                    <dxe:EditButton Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                            PopupPort(txt_Pol2,null);
                        }" />
                            </dxe:ASPxButtonEdit>
                        </td>
                        <td>Pod
                        </td>
                        <td>
                            <dxe:ASPxButtonEdit ID="txt_Pod2" ClientInstanceName="txt_Pod2" runat="server" Text='<%# Bind("Pod") %>' Width="100%" MaxLength="5">
                                <Buttons>
                                    <dxe:EditButton Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                            PopupPort(txt_Pod2,null);
                        }" />
                            </dxe:ASPxButtonEdit>
                        </td>
                        <td>Eta
                        </td>
                        <td>
                            <dxe:ASPxDateEdit ID="date_Eta2" runat="server" Width="100%" Value='<%# Bind("Eta") %>'
                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </dxe:ASPxDateEdit>
                        </td>
                        <td>Etd
                        </td>
                        <td>
                            <dxe:ASPxDateEdit ID="date_Etd2" runat="server" Width="100%" Value='<%# Bind("Etd") %>'
                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </dxe:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td>Date
                        </td>
                        <td>
                            <dxe:ASPxDateEdit ID="date_BkgDate2" runat="server" Width="100%" Value='<%# Bind("BkgDate") %>'
                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </dxe:ASPxDateEdit>
                        </td>
                        <td>Time
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_BkgTime2" Width="100%" runat="server" Text='<%# Bind("BkgTime") %>'>
                                <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                <ValidationSettings ErrorDisplayMode="None" />
                            </dxe:ASPxTextBox>
                        </td>
                        <td>Weight
                        </td>
                        <td>
                            <dxe:ASPxSpinEdit ID="spin_BkgWt2" runat="server" Increment="0" Width="100%" DisplayFormatString="0.000" DecimalPlaces="3"
                                Value='<%# Bind("BkgWt") %>'>
                                <SpinButtons ShowIncrementButtons="False">
                                </SpinButtons>
                            </dxe:ASPxSpinEdit>
                        </td>
                        <td>Volume
                        </td>
                        <td>
                            <dxe:ASPxSpinEdit ID="spin_BkgM32" runat="server" Increment="0" Width="100%" DisplayFormatString="0.000" DecimalPlaces="3"
                                Value='<%# Bind("BkgM3") %>'>
                                <SpinButtons ShowIncrementButtons="False">
                                </SpinButtons>
                            </dxe:ASPxSpinEdit>
                        </td>
                        <td>Qty
                        </td>
                        <td>
                            <dxe:ASPxSpinEdit ID="spin_BkgQty2" runat="server" Width="100%" Increment="0" Number="0"
                                NumberType="Integer" Value='<%# Bind("BkgQty") %>'>
                                <SpinButtons ShowIncrementButtons="False">
                                </SpinButtons>
                            </dxe:ASPxSpinEdit>
                        </td>
                        <td>PackType
                        </td>
                        <td>
                            <dxe:ASPxButtonEdit ID="txt_BkgPackType2" ClientInstanceName="txt_BkgPackType2" runat="server" Text='<%# Bind("BkgPkgType") %>' Width="100%">
                                <Buttons>
                                    <dxe:EditButton Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                            PopupUom(txt_BkgPackType2,2);
                        }" />
                            </dxe:ASPxButtonEdit>
                        </td>
                        <td>Remark
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_JobRmk2" Width="100%" runat="server" Text='<%# Bind("JobRmk") %>'>
                            </dxe:ASPxTextBox>
                        </td>
                        <td>Description
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_cargoDes2" Rows="4" Width="99%" runat="server" Text='<%# Bind("CargoDesc") %>'>
                            </dxe:ASPxTextBox>
                        </td>
                        <td>From
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_PickupFrm12" ClientInstanceName="txt_PickupFrm1" Rows="4" Width="100%" runat="server" Text='<%# Bind("PickFrm1") %>'>
                            </dxe:ASPxTextBox>
                        </td>
                        <td>To
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_DeliveryTo12" ClientInstanceName="txt_DeliveryTo1" Rows="4" Width="100%" runat="server" Text='<%# Bind("DeliveryTo1") %>'>
                            </dxe:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="22">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2" style="color: red;">Job3</td>
                        <td>Client Job No
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_BkgRef3" Width="100%" runat="server" Text='<%# Bind("BkgRef") %>'>
                            </dxe:ASPxTextBox>
                        </td>

                        <td>Job Type</td>
                        <td>
                            <dxe:ASPxComboBox ID="cmb_JobType3" Width="100%" runat="server" Text='<%# Bind("JobType") %>'>
                                <Items>
                                    <dxe:ListEditItem Text="IMP" Value="IMP" />
                                    <dxe:ListEditItem Text="EXP" Value="EXP" />
                                    <dxe:ListEditItem Text="TSP" Value="TSP" />
                                    <dxe:ListEditItem Text="LOC" Value="LOC" />
                                </Items>
                            </dxe:ASPxComboBox>
                        </td>
                        <td>PIC
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_CustPic3" ClientInstanceName="txt_CustPic3" Width="100%" runat="server" Text='<%# Bind("CustPic") %>'>
                            </dxe:ASPxTextBox>
                        </td>
                        <td>Vessel
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_Ves3" Width="100%" runat="server" Text='<%# Bind("Vessel") %>'>
                            </dxe:ASPxTextBox>
                        </td>
                        <td>Voyage
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_Voy3" Width="100%" runat="server" Text='<%# Bind("Voyage") %>'>
                            </dxe:ASPxTextBox>
                        </td>
                        <td>Pol
                        </td>
                        <td>
                            <dxe:ASPxButtonEdit ID="txt_Pol3" ClientInstanceName="txt_Pol3" runat="server" Text='<%# Bind("Pol") %>' Width="100%" MaxLength="5">
                                <Buttons>
                                    <dxe:EditButton Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                            PopupPort(txt_Pol3,null);
                        }" />
                            </dxe:ASPxButtonEdit>
                        </td>
                        <td>Pod
                        </td>
                        <td>
                            <dxe:ASPxButtonEdit ID="txt_Pod3" ClientInstanceName="txt_Pod3" runat="server" Text='<%# Bind("Pod") %>' Width="100%" MaxLength="5">
                                <Buttons>
                                    <dxe:EditButton Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                            PopupPort(txt_Pod3,null);
                        }" />
                            </dxe:ASPxButtonEdit>
                        </td>
                        <td>Eta
                        </td>
                        <td>
                            <dxe:ASPxDateEdit ID="date_Eta3" runat="server" Width="100%" Value='<%# Bind("Eta") %>'
                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </dxe:ASPxDateEdit>
                        </td>
                        <td>Etd
                        </td>
                        <td>
                            <dxe:ASPxDateEdit ID="date_Etd3" runat="server" Width="100%" Value='<%# Bind("Etd") %>'
                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </dxe:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td>Date
                        </td>
                        <td>
                            <dxe:ASPxDateEdit ID="date_BkgDate3" runat="server" Width="100%" Value='<%# Bind("BkgDate") %>'
                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </dxe:ASPxDateEdit>
                        </td>
                        <td>Time
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_BkgTime3" Width="100%" runat="server" Text='<%# Bind("BkgTime") %>'>
                                <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                <ValidationSettings ErrorDisplayMode="None" />
                            </dxe:ASPxTextBox>
                        </td>
                        <td>Weight
                        </td>
                        <td>
                            <dxe:ASPxSpinEdit ID="spin_BkgWt3" runat="server" Increment="0" Width="100%" DisplayFormatString="0.000" DecimalPlaces="3"
                                Value='<%# Bind("BkgWt") %>'>
                                <SpinButtons ShowIncrementButtons="False">
                                </SpinButtons>
                            </dxe:ASPxSpinEdit>
                        </td>
                        <td>Volume
                        </td>
                        <td>
                            <dxe:ASPxSpinEdit ID="spin_BkgM33" runat="server" Increment="0" Width="100%" DisplayFormatString="0.000" DecimalPlaces="3"
                                Value='<%# Bind("BkgM3") %>'>
                                <SpinButtons ShowIncrementButtons="False">
                                </SpinButtons>
                            </dxe:ASPxSpinEdit>
                        </td>
                        <td>Qty
                        </td>
                        <td>
                            <dxe:ASPxSpinEdit ID="spin_BkgQty3" runat="server" Width="100%" Increment="0" Number="0"
                                NumberType="Integer" Value='<%# Bind("BkgQty") %>'>
                                <SpinButtons ShowIncrementButtons="False">
                                </SpinButtons>
                            </dxe:ASPxSpinEdit>
                        </td>
                        <td>PackType
                        </td>
                        <td>
                            <dxe:ASPxButtonEdit ID="txt_BkgPackType3" ClientInstanceName="txt_BkgPackType3" runat="server" Text='<%# Bind("BkgPkgType") %>' Width="100%">
                                <Buttons>
                                    <dxe:EditButton Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                            PopupUom(txt_BkgPackType3,2);
                        }" />
                            </dxe:ASPxButtonEdit>
                        </td>
                        <td>Remark
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_JobRmk3" Width="100%" runat="server" Text='<%# Bind("JobRmk") %>'>
                            </dxe:ASPxTextBox>
                        </td>
                        <td>Description
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_cargoDes3" Rows="4" Width="99%" runat="server" Text='<%# Bind("CargoDesc") %>'>
                            </dxe:ASPxTextBox>
                        </td>
                        <td>From
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_PickupFrm13" ClientInstanceName="txt_PickupFrm1" Rows="4" Width="100%" runat="server" Text='<%# Bind("PickFrm1") %>'>
                            </dxe:ASPxTextBox>
                        </td>
                        <td>To
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_DeliveryTo13" ClientInstanceName="txt_DeliveryTo1" Rows="4" Width="100%" runat="server" Text='<%# Bind("DeliveryTo1") %>'>
                            </dxe:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="22">
                            <hr />
                        </td>
                    </tr>

                    <tr>
                        <td colspan="18" style="text-align: right"></td>
                        <td>
                            <dxe:ASPxButton ID="btn_Update" runat="server" Text="Update" OnClick="btn_Update_Click"></dxe:ASPxButton>

                        </td>
                        <td></td>
                        <td>
                            <dxe:ASPxButton ID="ASPxButton1" Width="75" runat="server" Text="Cancel" AutoPostBack="false"
                                UseSubmitBehavior="false">
                                <ClientSideEvents Click="function(s,e) {
                                                       document.getElementById('div_add').style.display='none';
                                                        }" />
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="2050" AutoGenerateColumns="False" DataSourceID="dsTransport" OnRowUpdating="grid_Transport_RowUpdating" OnCustomDataCallback="grid_Transport_CustomDataCallback">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="Inline" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40">
                        <DataItemTemplate>
                            <div style='display: <%# Eval("JobProgress")!=""&&IsClient()=="Client"?"none":"" %>'><a href="#" onclick='<%# "detailGrid.StartEditRow("+Container.VisibleIndex+")" %>'>Edit</a></div>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <table>
                                <tr>
                                    <td><a href="#" onclick='<%# "detailGrid.UpdateEdit();" %>'>Update</a></td>
                                    <td><a href="#" onclick='<%# "detailGrid.CancelEdit();" %>'>Cancel</a></td>
                                </tr>
                            </table>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="No" FieldName="JobNo" VisibleIndex="1" SortIndex="0" SortOrder="Descending" Width="70">
                        <EditItemTemplate><%# Eval("JobNo") %></EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Job Date" FieldName="JobDate" VisibleIndex="2" Width="70">
                        <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}" />
                        <EditItemTemplate><%# SafeValue.SafeDateStr(Eval("JobDate")) %></EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Type" FieldName="JobType" VisibleIndex="3" Width="70">
                        <PropertiesComboBox>
                            <Items>
                                <dxe:ListEditItem Text="IMP" Value="IMP" />
                                <dxe:ListEditItem Text="EXP" Value="EXP" />
                                <dxe:ListEditItem Text="TSP" Value="TSP" />
                                <dxe:ListEditItem Text="LOC" Value="LOC" />
                            </Items>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="CustJobNo" FieldName="BkgRef" VisibleIndex="4" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="PIC" FieldName="CustPic" VisibleIndex="5" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Vessel" FieldName="Vessel" VisibleIndex="10" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Voyage" FieldName="Voyage" VisibleIndex="11" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Pol" FieldName="Pol" VisibleIndex="12" Width="100">
                        <EditItemTemplate>
                            <dxe:ASPxButtonEdit ID="txt_Pol" ClientInstanceName="txt_Pol" runat="server" Text='<%# Bind("Pol") %>' Width="100%" MaxLength="5">
                                <Buttons>
                                    <dxe:EditButton Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                            PopupPort(txt_Pol,null);
                        }" />
                            </dxe:ASPxButtonEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Pod" FieldName="Pod" VisibleIndex="13" Width="100">
                        <EditItemTemplate>
                            <dxe:ASPxButtonEdit ID="txt_Pod" ClientInstanceName="txt_Pod" runat="server" Text='<%# Bind("Pod") %>' Width="100%" MaxLength="5">
                                <Buttons>
                                    <dxe:EditButton Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                            PopupPort(txt_Pod,null);
                        }" />
                            </dxe:ASPxButtonEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataDateColumn Caption="ETA" FieldName="Eta" VisibleIndex="14" Width="90">
                        <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}" EditFormatString="dd/MM/yyyy" />
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataDateColumn Caption="ETD" FieldName="Etd" VisibleIndex="15" Width="90">
                        <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}" EditFormatString="dd/MM/yyyy" />
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Book Date" FieldName="BkgDate" VisibleIndex="20" Width="90">
                        <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}" EditFormatString="dd/MM/yyyy" />
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Time" FieldName="BkgTime" VisibleIndex="21" Width="80">
                        <PropertiesTextEdit MaskSettings-Mask="<00..23>:<00..59>" MaskSettings-ErrorText=""></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="From" FieldName="PickFrm1" VisibleIndex="22" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="To" FieldName="DeliveryTo1" VisibleIndex="23" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Weight" FieldName="BkgWt" VisibleIndex="24" Width="55" PropertiesTextEdit-DisplayFormatString="0.000">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Volume" FieldName="BkgM3" VisibleIndex="25" Width="55" PropertiesTextEdit-DisplayFormatString="0.000">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="BkgQty" VisibleIndex="26" Width="55">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="UOM" FieldName="BkgPkgType" VisibleIndex="27" Width="55">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="JobRmk" VisibleIndex="28" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="CargoDesc" VisibleIndex="29" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="Attachments" VisibleIndex="30" Width="100">
                        <DataItemTemplate>
                            <a href='#' onclick='PopupPhotoView("<%# Eval("JobNo") %>");'>Upload</a>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <a href='#' onclick='PopupPhotoView("<%# Eval("JobNo") %>");'>Upload</a>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                </Columns>
                <Settings ShowFooter="True" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="JobNo" SummaryType="Count" DisplayFormat="{0}" />
                    <dxwgv:ASPxSummaryItem FieldName="Wt" SummaryType="Sum" DisplayFormat="{0:#,##0.000}" />
                    <dxwgv:ASPxSummaryItem FieldName="M3" SummaryType="Sum" DisplayFormat="{0:#,##0.000}" />
                    <dxwgv:ASPxSummaryItem FieldName="Qty" SummaryType="Sum" DisplayFormat="{0}" />
                </TotalSummary>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="600" EnableViewState="False">
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr1"
                HeaderText="Attachments" AllowDragging="True" EnableAnimation="False" Height="550"
                AllowResize="True" Width="800" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
