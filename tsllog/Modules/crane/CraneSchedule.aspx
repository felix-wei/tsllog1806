<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CraneSchedule.aspx.cs" Inherits="Job_CraneSchedule" %>

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
            parent.navTab.openTab(jobno, "/PagesContTrucking/Job/JobEdit.aspx?jobNo=" + jobno, { title: jobno, fresh: false, external: true });
        }
        function PopupTripsList(jobno, contId, canGO) {
            if (canGO != "GO") {
                return;
            }
            popubCtr1.SetHeaderText('Trips List');
            popubCtr1.SetContentUrl('../SelectPage/TripListForJobList.aspx?JobNo=' + jobno + "&contId=" + contId);
            popubCtr1.Show();
        }

        function NewAdd_Visible(doShow) {
            if (doShow) {
                var t = new Date();
                txt_new_JobDate.SetText(t);
                cbb_new_jobtype.SetValue("");
                btn_new_ClientId.SetText('');
                txt_new_ClientName.SetText('');
                txt_FromAddress.SetText('');
                txt_WarehouseAddress.SetText('');
                txt_ToAddress.SetText('');
                txt_DepotAddress.SetText('');
                txt_new_remark.SetText('');

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
                    //parent.navTab.openTab(v, "/Warehouse/Job/JobEdit.aspx?no=" + v, { title: v, fresh: false, external: true });
                    goJob(v);
                    ASPxPopupClientControl.Hide();
                }
            }
        }
        function AfterAddTrip() {
            popubCtr1.Hide();
            popubCtr1.SetContentUrl('about:blank');
        }

        function cbb_checkbox_Type(name) {
            //console.log('========= checkbox', name);
            if (name == 'ALL') {
                cb_ContStatus1.SetValue(cb_ContStatus0.GetValue());
                cb_ContStatus2.SetValue(cb_ContStatus0.GetValue());
                cb_ContStatus3.SetValue(cb_ContStatus0.GetValue());
            } else {
                if (cb_ContStatus1.GetValue() && cb_ContStatus2.GetValue() && cb_ContStatus3.GetValue()) {
                    cb_ContStatus0.SetValue(true);
                } else {
                    cb_ContStatus0.SetValue(false);
                }
            }
        }

        function cbb_checkbox_Type1(name) {
            //console.log('========= checkbox', name);
            if (name == 'Uncomplete') {
                if (cb_ContStatus4.GetValue()) {
                    cb_ContStatus1.SetValue(false);
                    cb_ContStatus2.SetValue(false);
                    cb_ContStatus3.SetValue(false);
                }
            } else {
                if (cb_ContStatus1.GetValue() || cb_ContStatus2.GetValue() || cb_ContStatus3.GetValue()) {
                    cb_ContStatus4.SetValue(false);
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="lbl1" runat="server" Text="Job No"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_jobNo" runat="server" Width="100"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Booking Date From"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_search_dateFrom" runat="server" Width="100" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="To"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_search_dateTo" runat="server" Width="100" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Text="Client"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="btn_ClientId" ClientInstanceName="btn_ClientId" runat="server" Width="100" AutoPostBack="False" ReadOnly="true">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_ClientId,txt_ClientName);
                                                                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td colspan="2">
                        <dxe:ASPxTextBox ID="txt_ClientName" ClientInstanceName="txt_ClientName" runat="server" Width="100%" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" ClientInstanceName="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>

                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_save2excel" runat="server" Text="Save to excel" OnClick="btn_save2excel_Click"></dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" KeyFieldName="Id" Width="1500" AutoGenerateColumns="False">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataColumn FieldName="JobNo" Caption="JobNo">
                        <DataItemTemplate>
                            <div style="min-width: 80px;">
                                <a href='javascript: parent.navTab.openTab("<%# Eval("JobNo") %>","/PagesContTrucking/Job/JobEdit.aspx?no=<%# Eval("JobNo") %>",{title:"<%# Eval("JobNo") %>", fresh:false, external:true});'><%# Eval("JobNo") %></a>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="JobType" Caption="JobType"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="IsTrucking" Caption="CT">
                        <DataItemTemplate>
                            <%# Eval("IsTrucking","{0}")=="Yes"?"X":""%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="IsWarehouse" Caption="WH">
                        <DataItemTemplate>
                            <%# Eval("IsWarehouse","{0}")=="Yes"?"X":""%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="IsLocal" Caption="TP">
                        <DataItemTemplate>
                            <%# Eval("IsLocal","{0}")=="Yes"?"X":""%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="IsAdhoc" Caption="CR">
                        <DataItemTemplate>
                            <%# Eval("IsAdhoc","{0}")=="Yes"?"X":""%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="BookingDate" Caption="Schedule">
                        <DataItemTemplate>
                            <div style="min-width: 70px;"><%# SafeValue.SafeDate( Eval("BookingDate"),new DateTime(1900,1,1)).ToString("dd/MM/yyyy") %></div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataColumn FieldName="BKDAY" Caption="Day">
                        <DataItemTemplate>
                            <%# Eval("BKDAY","{0}").Substring(0,3)%>
                        </DataItemTemplate>

                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="BookingTime" Caption="Time"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ClientName" Caption="Client" Width="300"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Visible="false">
                        <DataItemTemplate>
                            <%# SafeValue.SafeString(Eval("Statuscode"),"")%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                     <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status">
                        <DataItemTemplate>
                            <div style="min-width: 80px;">
                                <%# GetStatus(Eval("StatusCode","{0}")) %>
                            </div>
                        </DataItemTemplate>
		</dxwgv:GridViewDataColumn>
                  
                    <dxwgv:GridViewDataTextColumn FieldName="DeliveryTo" Caption="Location">
                        <DataItemTemplate>
                            <div style="min-width: 200px"><%# Eval("DeliveryTo") %></div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <%--<dxwgv:GridViewDataColumn FieldName="ByUser" Caption="ByUser"></dxwgv:GridViewDataColumn>--%>
                    
                    <dxwgv:GridViewDataColumn FieldName="TowheadCode" Caption="Vehicle"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Ton" Caption="Ton"></dxwgv:GridViewDataColumn>
                    <%--<dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Satus" Width="30"></dxwgv:GridViewDataColumn>--%>
                    <%--<dxwgv:GridViewDataColumn FieldName="ToCode" Caption="Location"></dxwgv:GridViewDataColumn>--%>
                    <%--<dxwgv:GridViewDataColumn FieldName="BookingRemark" Caption="Booking Remark" Width="300"></dxwgv:GridViewDataColumn>--%>
                    <dxwgv:GridViewDataColumn FieldName="TotalHour" Caption="Billable Hrs"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="OtHour" Caption="OT Hrs"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Remark" Caption="Remark" Width="300"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="DeliveryRemark" Caption="Driver Remark" Width="300"></dxwgv:GridViewDataColumn>
                      <dxwgv:GridViewDataTextColumn FieldName="PickupFrom" Caption="From">
                        <DataItemTemplate>
                            <div style="min-width: 200px"><%# Eval("PickupFrom") %></div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <%--<dxwgv:GridViewDataColumn FieldName="DriverCode" Caption="Driver"></dxwgv:GridViewDataColumn>--%>
                    <%--<dxwgv:GridViewDataColumn FieldName="FromDate" Caption="ActualDate" Width="160">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDate(Eval("FromDate"),new DateTime(1900,1,1)).ToString("dd/MM")+"&nbsp;"+Eval("FromTime")+"&nbsp;-&nbsp;"+ SafeValue.SafeDate(Eval("ToDate"),new DateTime(1900,1,1)).ToString("dd/MM")+"&nbsp;"+Eval("ToTime")%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>--%>
                    <%--<dxwgv:GridViewDataColumn FieldName="Remark" Caption="Billing Remark" Width="300"></dxwgv:GridViewDataColumn>--%>
                </Columns>
            </dxwgv:ASPxGridView>
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
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid_Transport">
            </dxwgv:ASPxGridViewExporter>
        </div>
    </form>
</body>
</html>
