<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobList.aspx.cs" Inherits="Modules_Freight_Job_JobList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript">
        function goJob(jobno) {
            //window.location = "JobEdit.aspx?jobNo=" + jobno;
            parent.navTab.openTab(jobno, "/Modules/Freight/Job/JobEdit.aspx?no=" + jobno, { title: jobno, fresh: false, external: true });
        }
        function NewAdd_Visible(doShow, par) {
            if (doShow) {
                var t = new Date();
                txt_new_JobDate.SetText(t);
                cbb_new_jobtype.SetValue("CONSOL");
                btn_new_ClientId.SetText('');
                txt_new_ClientName.SetText('');
                txt_FromAddress.SetText('');
                txt_WarehouseAddress.SetText('');
                txt_ToAddress.SetText('');
                txt_DepotAddress.SetText('');
                txt_new_remark.SetText('');
                if (par == 'J') {
                    lbl_Header.SetText('New Job');
                    cbb_new_jobstatus.SetText("Confirmed");
                }
                ASPxPopupClientControl.Show();
            }
            else {
                ASPxPopupClientControl.Hide();
            }
        }

        function NewAdd_Save() {
            var jobType = cbb_new_jobtype.GetValue();
            //console.log(jobType);
            if (jobType && jobType.length > 0) {
                detailGrid.GetValuesOnCustomCallback('OK', OnSaveCallBack);
            } else {
                alert('Require JobType!');
            }
        }
        function OnSaveCallBack(v) {
            if (v != null && v.indexOf("Fail") > -1) {
                alert(v);
            }
            else {
                if (v != null) {

                    goJob(v);

                    ASPxPopupClientControl.Hide();
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>CFS IMPORT</h2>
            <table>
                <tr>
                    <td width="60">JobNo
                    </td>
                    <td width="80">
                        <dxe:ASPxTextBox ID="txt_JobNo" Width="70" runat="server" Text="">
                        </dxe:ASPxTextBox>
                    </td>
                    <td width="60">ContNo
                    </td>
                    <td width="80">
                        <dxe:ASPxTextBox ID="txt_ContNo" Width="90" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td width="60">HblNo
                    </td>
                    <td width="80">
                        <dxe:ASPxTextBox ID="txt_Hbl" Width="90" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td width="60">Ocean BL
                    </td>
                    <td width="80">
                        <dxe:ASPxTextBox ID="txt_Obl" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>

                </tr>
                <tr>
                    <td>ClientRef
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_RefNo" Width="70" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>Eta
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="90" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>To
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_end" Width="90" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td colspan="2">
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="btn_search" ClientInstanceName="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>

                                </td>
                                <td width="90%"></td>
                                <td>
                                    <dxe:ASPxButton ID="btn_Add" runat="server" Text="New&nbsp;Job" AutoPostBack="False">
                                        <ClientSideEvents Click="function(s, e) {
                                    NewAdd_Visible(true,'J');
                                    }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td width="80">
                                    <dxe:ASPxButton ID="btn_Export" Width="90" runat="server" Text="Save Excel" OnClick="btn_Export_Click">
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <dxpc:ASPxPopupControl ID="ASPxPopupControl1" ClientInstanceName="ASPxPopupClientControl" SkinID="None" Width="240px"
                ShowOnPageLoad="false" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="TopSides"
                AllowDragging="True" CloseAction="None" PopupElementID="popupArea"
                EnableViewState="False" runat="server" PopupHorizontalOffset="0"
                PopupVerticalOffset="33" EnableHierarchyRecreation="True">
                <HeaderTemplate>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 100%;">
                                <dxe:ASPxLabel ID="lbl_Header" ClientInstanceName="lbl_Header" runat="server" Text="New Job"></dxe:ASPxLabel>
                            </td>
                            <td>
                                <a id="a_X" onclick="NewAdd_Visible(false)" onmousedown="event.cancelBubble = true;" style="width: 15px; height: 14px; cursor: pointer;">X</a>
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <ContentStyle>
                    <Paddings Padding="0px" />
                </ContentStyle>
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                        <div style="padding: 2px 2px 2px 2px; width: 690px">
                            <table style="text-align: left; padding: 2px 2px 2px 2px; width: 680px">
                                <tr>
                                    <td>Client</td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="90">
                                                    <dxe:ASPxButtonEdit ID="btn_new_ClientId" ClientInstanceName="btn_new_ClientId" runat="server" Width="90" AutoPostBack="False">
                                                        <Buttons>
                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                        </Buttons>
                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_new_ClientId,txt_new_ClientName);
                                                                        }" />
                                                    </dxe:ASPxButtonEdit>
                                                </td>
                                                <td>
                                                    <dxe:ASPxTextBox runat="server" Width="250" ID="txt_new_ClientName" ClientInstanceName="txt_new_ClientName">
                                                    </dxe:ASPxTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>

                                    <td>Job&nbsp;Date</td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="txt_new_JobDate" ClientInstanceName="txt_new_JobDate" runat="server" Width="160" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                                    </td>

                                </tr>
                                <tr>
                                    <td>From Address
                                    </td>
                                    <td rowspan="2">
                                        <dxe:ASPxMemo ID="txt_FromAddress" Rows="3" Width="340" ClientInstanceName="txt_FromAddress"
                                            runat="server">
                                        </dxe:ASPxMemo>
                                    </td>
                                    <td>Job&nbsp;Type</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cbb_new_jobtype" ClientInstanceName="cbb_new_jobtype" runat="server" Width="160" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith">
                                            <Items>
                                                <dxe:ListEditItem Text="AIR" Value="AIR" />
                                                <dxe:ListEditItem Text="CONSOL" Value="CONSOL" />
                                                <dxe:ListEditItem Text="FCL" Value="FCL" />
                                                <dxe:ListEditItem Text="LCL" Value="LCL" />
                                                <dxe:ListEditItem Text="RAILING" Value="RAILING" />
                                                <dxe:ListEditItem Text="TSLOAD" Value="TSLOAD" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>Shipper
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_WarehouseAddress" ClientInstanceName="txt_WarehouseAddress" runat="server" Width="160"></dxe:ASPxTextBox>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td style="vertical-align: top">To Address
                                    </td>
                                    <td rowspan="2">
                                        <dxe:ASPxMemo ID="txt_ToAddress" Rows="3" Width="340" ClientInstanceName="txt_ToAddress"
                                            runat="server">
                                        </dxe:ASPxMemo>
                                    </td>
                                    <td>WareHouse</td>
                                    <td>
                                        <div style="display: none">
                                            <dxe:ASPxLabel ID="lb_new_WareHouseId" runat="server" ClientInstanceName="lb_new_WareHouseId"></dxe:ASPxLabel>
                                        </div>
                                        <dxe:ASPxButtonEdit ID="txt_new_WareHouseId" ReadOnly="true" BackColor="Control" ClientInstanceName="txt_new_WareHouseId" runat="server" Text='<%# Eval("WareHouseCode") %>' Width="160" AutoPostBack="False">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupWh(txt_new_WareHouseId,null);
                                                                        }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>Job&nbsp;Status</td>
                                    <td>
                                        <dxe:ASPxTextBox ID="cbb_new_jobstatus" ReadOnly="true" BackColor="Control" ClientInstanceName="cbb_new_jobstatus" runat="server" Width="160">
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top">
                                        <%--<a href="#" onclick="PopupCustAdr(null,txt_WarehouseAddress);"></a>--%>Depot&nbsp;Address
                                    </td>
                                    <td rowspan="2">
                                        <dxe:ASPxMemo ID="txt_DepotAddress" Rows="3" Width="340" ClientInstanceName="txt_DepotAddress"
                                            runat="server">
                                        </dxe:ASPxMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>Remark
                                    </td>
                                    <td rowspan="2">
                                        <dxe:ASPxMemo ID="txt_new_remark" Rows="3" Width="340" ClientInstanceName="txt_new_remark"
                                            runat="server">
                                        </dxe:ASPxMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                            <table style="text-align: right; padding: 2px 2px 2px 2px; width: 660px">
                                <tr>
                                    <td colspan="4" style="width: 90%"></td>
                                    <td>

                                        <dxe:ASPxButton ID="btn_Ref_Save" runat="server" Width="80" AutoPostBack="false"
                                            Text="Save">
                                            <ClientSideEvents Click="function(s,e) {
                                                NewAdd_Save();
                                                 }" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" OnHtmlDataCellPrepared="grid_Transport_HtmlDataCellPrepared"
                KeyFieldName="Id" Width="1800" AutoGenerateColumns="False" OnCustomDataCallback="grid_Transport_CustomDataCallback">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="Job No" FieldName="JobNo">
                        <DataItemTemplate>
                            <%--<a onclick="goJob('<%# Eval("JobNo") %>')"><%# Eval("JobNo") %></a>--%>
                            <div class='<%# SafeValue.SafeString(string.Format("{0}",Eval("StatusCode")))=="New"?"link":"none" %>' style="min-width: 70px;">
                                <a href='javascript: parent.navTab.openTab("<%# Eval("JobNo") %>","/Modules/Freight/Job/JobEdit.aspx?no=<%# Eval("JobNo") %>",{title:"<%# Eval("JobNo") %>", fresh:false, external:true});'><%# Eval("JobNo") %></a>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataColumn FieldName="JobType" Caption="JobType"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="client" Caption="Client"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="JobStatus" Caption="Job Status" Visible="false">
                        <DataItemTemplate>
                            <%# VilaStatus(SafeValue.SafeString(Eval("JobStatus"),""))%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="ScheduleDate" Caption="Schedule Date" Width="80">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("ScheduleDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Container No" FieldName="ContainerNo">
                        <DataItemTemplate>
                            <%# Eval("ContainerNo") %><br />
                            <%--<a href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("ContId") %>")' style="display: <%# SafeValue.SafeString(Eval("ContId")).Length>0?"":"none" %>">Link Trips</a>--%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="DischargeCell" FieldName="DischargeCell" Width="40"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="OP" FieldName="OperatorCode" Width="40"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Email" FieldName="EmailInd">
                        <DataItemTemplate>
                            <div class="div_hr" style='<%# SafeValue.SafeString(Eval("EmailInd"))=="Y"?"background-color:green;color:white": "" %>'>
                                <%# SafeValue.SafeString(Eval("EmailInd"))=="Y"?"@":"" %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Urgent" FieldName="UrgentInd">
                        <DataItemTemplate>
                            <div class="div_hr" style='<%# SafeValue.SafeString(Eval("UrgentInd"))=="Y"?"background-color:red;color:white": "" %>'>
                                <%# SafeValue.SafeString(Eval("UrgentInd"))=="Y"?"Y":"" %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="SealNo" FieldName="SealNo" Width="80"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="J5" FieldName="F5Ind" Width="80"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="OOG" FieldName="oogInd">
                        <DataItemTemplate>
                            <div class="div_hr" style='<%# SafeValue.SafeString(Eval("oogInd"))=="Y"?"background-color:red;color:white": "" %>'>
                                <%# SafeValue.SafeString(Eval("oogInd"))=="Y"?"Y":"" %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <%--<dxwgv:GridViewDataColumn Caption="MT" FieldName="WarehouseStatus" Width="80"></dxwgv:GridViewDataColumn>--%>
                    <dxwgv:GridViewDataColumn Caption="Next" FieldName="NextTrip" Width="80"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Link Trips" FieldName="trips">
                        <DataItemTemplate>
                            <a class="a_ltrip" href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("ContId") %>","<%# SafeValue.SafeString(Eval("ContId")).Length>0?"GO":"" %>","<%# Eval("IsWarehouse") %>")'>
                                <div class="div_FixWith"><%# xmlChangeToHtml(Eval("trips"),Eval("ContId")) %></div>
                            </a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="Cont Type" FieldName="ContainerType"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="StatusCode" Caption="Cont Status">
                        <DataItemTemplate>
                            <div style="background-color: <%# ShowColor(SafeValue.SafeString(Eval("StatusCode"))) %>" class="div_contStatus">
                                <%# Eval("StatusCode") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="hr" Caption="Hours">
                        <DataItemTemplate>
                            <div class="div_hr" style='<%# SafeValue.SafeInt(Eval("hr"),0)>72?"background-color:red;color:white": (SafeValue.SafeInt(Eval("hr"),0)>48?"background-color:orange;color:white":"")%>'>
                                <%# Eval("hr") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="CfsStatus" Caption="WH Status"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="PortnetStatus" Caption="Portnet"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Vessel" Caption="Vessel"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Voyage" Caption="Voyage"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="REFNo" FieldName="CarrierBkgNo"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ETA" Width="80">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("EtaDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="TT" Width="60">
                        <DataItemTemplate>
                            <%# SafeValue.SafeString(Eval("EtaTime"),"0000") %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="PickupFrom" Caption="From">
                        <DataItemTemplate>
                            <div style="min-width: 100px"><%# Eval("PickupFrom") %></div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="DeliveryTo" Caption="To">
                        <DataItemTemplate>
                            <div style="min-width: 100px"><%# Eval("DeliveryTo") %></div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Depot" Caption="Depot">
                        <DataItemTemplate>
                            <div style="min-width: 100px"><%# Eval("Depot") %></div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="PermitNo" Caption="PermitNo"></dxwgv:GridViewDataTextColumn>
                    <%--<dxwgv:GridViewDataColumn FieldName="Haulier" Caption="Contractor"></dxwgv:GridViewDataColumn>--%>
                    <dxwgv:GridViewDataColumn FieldName="Terminalcode" Caption="Terminal"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ClientRefNo" Caption="Client Ref No"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Job Date">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("JobDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="Remark" Caption="Remark" Visible="false"></dxwgv:GridViewDataColumn>
                </Columns>
            </dxwgv:ASPxGridView>
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid_Transport">
            </dxwgv:ASPxGridViewExporter>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="400"
                Width="600" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr1"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="530"
                Width="980" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      document.getElementById('btn_search').click();
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
