<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="job_edit.aspx.cs" Inherits="Z_job_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript">
        var clientId = null;
        var clientName = null;
        var add1 = null;
        var add2 = null;
        var add3 = null;
        var add4 = null;
        clientAcCode = null;
        function gcancel(g, i) {
            window.location = jQuery('#ctl_page').val() + ".aspx";
        }
        function g_save(g, i) {
            g.PerformCallback("UPDATEEDIT");
        }
        function RowClickHandler(s, e) {
            SetLookupKeyValue(e.visibleIndex);
            DropDownEdit.HideDropDown();
        }
        function SetLookupKeyValue(rowIndex) {
            DropDownEdit.SetText(GridView.cpContN[rowIndex]);
            txt_sealN.SetText(GridView.cpSealN[rowIndex]);
            txt_contType.SetText(GridView.cpContType[rowIndex]);
        }
        function PopupUploadPhoto() {
            popubCtr1.SetHeaderText('Upload Attachment');
            popubCtr1.SetContentUrl('Upload.aspx?Type=E&jobNo=0&Sn=' + txtHouseNo.GetText());
            popubCtr1.Show();
        }
        function PopupShipper(custId, name, a1, a2, a3, a4, nameAdd, partyType) {
            clientId = custId;
            clientName = name;
            add1 = a1;
            add2 = a2;
            add3 = a3;
            add4 = a4;
            clientAcCode = nameAdd;
            popubCtr.SetHeaderText('Party');
            popubCtr.SetContentUrl('/SelectPage/Party_Shipper.aspx?partyType=' + partyType);
            popubCtr.Show();
        }
        function PopupParty(txtId, txtName, partyType) {
            clientId = txtId;
            clientName = txtName;
            popubCtr.SetContentUrl('/SelectPage/PartyList.aspx?partyType=' + partyType);
            if (partyType == "A")
                popubCtr.SetHeaderText('Agent');
            else if (partyType == "C")
                popubCtr.SetHeaderText('Customer');
            else if (partyType == "V")
                popubCtr.SetHeaderText('Vendor');
            else
                popubCtr.SetHeaderText('Party');
            popubCtr.Show();
        }
        function PopupPort(txtId, txtName) {
            clientId = txtId;
            clientName = txtName;
            popubCtr.SetHeaderText('Port');
            popubCtr.SetContentUrl('/SelectPage/PortList.aspx?type=S');
            popubCtr.Show();
        }
        function PopupChgCode(codeId, desId, mastType) {
            clientId = codeId;
            clientName = desId;
            popubCtr.SetHeaderText('Charge Code');
            popubCtr.SetContentUrl('/SelectPage/ChgCodeList.aspx?jobType=' + mastType);
            popubCtr.Show();
        }
        function PutValue(s, name) {
            if (clientId != null) {
                clientId.SetText(s);
            }
            if (clientName != null) {
                clientName.SetText(name);
            }
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
        }
        function Calc(qtyV, priceV, exRateV, num, totControl) {
            var qty = parseFloat(qtyV);
            var price = parseFloat(priceV);
            var exRate = parseFloat(exRateV);
            if (exRate == 1)
                totControl.SetNumber(FormatNumber(qty * price, num));
            else
                totControl.SetNumber(FormatNumber(FormatNumber(qty * price, num) * exRate, num));
        }
        function FormatNumber(srcStr, nAfterDot) {
            var srcStr, nAfterDot;
            var resultStr, nTen;
            srcStr = "" + srcStr + "";
            strLen = srcStr.length;
            dotPos = srcStr.indexOf(".", 0);
            if (dotPos == -1) {
                resultStr = srcStr + ".";
                for (i = 0; i < nAfterDot; i++) {
                    resultStr = resultStr + "0";
                }
            }
            else {
                if ((strLen - dotPos - 1) >= nAfterDot) {
                    nAfter = dotPos + nAfterDot + 1;
                    nTen = 1;
                    for (j = 0; j < nAfterDot; j++) {
                        nTen = nTen * 10;
                    }
                    resultStr = Math.round(parseFloat(srcStr) * nTen) / nTen;
                }
                else {
                    resultStr = srcStr;
                    for (i = 0; i < (nAfterDot - strLen + dotPos + 1) ; i++) {
                        resultStr = resultStr + "0";
                    }

                }
            }
            return resultStr;

        }
        function PutShipper(shipperId, name, contact, tel, fax, email, nameAdd) {
            clientId.SetText(shipperId);
            clientName.SetText(name);
            add1.SetText(contact);
            add2.SetText(tel);
            add3.SetText(fax);
            add4.SetText(email);
            if (clientAcCode != null)
                clientAcCode.SetText(nameAdd);
            clientId = null;
            clientName = null;
            add1 = null;
            add2 = null;
            add3 = null;
            add4 = null;
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
        }
        function PopupUom(codeId, typ) {
            clientId = codeId;
            popubCtr.SetHeaderText('UOM');
            popubCtr.SetContentUrl('/SelectPage/UomList.aspx?type=' + typ);
            popubCtr.Show();
        }
        function PopupGeneralCode(txtId) {
            clientId = txtId;
            clientName = null;
            popubCtr.SetHeaderText('General Code');
            popubCtr.SetContentUrl('/SelectPage/GenCodeList.aspx');
            popubCtr.Show();
        }
        function PopupCurrency(txtId, txtName) {
            clientId = txtId;
            clientName = txtName;
            popubCtr.SetHeaderText('Currency');
            popubCtr.SetContentUrl('/SelectPage/CurrencyList.aspx');
            popubCtr.Show();
        }
        function ChangeBackColor(s) {
            s.GetMainElement().style.backgroundColor = "PapayaWhip";
            s.GetInputElement().style.background = "content-box";
            document.getElementById("a_save").style.display = "";
        }
        function checkLeave() {
            alert(document.getElementById("a_save").style.display);
            event.returnValue = "确定离开当前页面吗？";
            document.getElementById('a_save').click();
            alert(document.getElementById("a_save").style.display);
            //if (document.getElementById("a_save").style.display != "none") {
            //    if (confirm("Data modified, do you want to save changes ?"))
                    
            //}
        }
        //window.onbeforeunload = function (event) {
        //    event.returnValue = "Data maybe modified, do you want to leave this page ?";
        //}
        //window.onbeforeunload = function (event) {
        //    alert("===onbeforeunload===");
        //    if(event.clientX>document.body.clientWidth && event.clientY < 0 || event.altKey){
        //        alert("你关闭了浏览器");
        //    }else{
        //        alert("你正在刷新页面");
        //    }
        //}
        $(function () {
            $(":input").keyup(function () {
                $(this).css("background", "PapayaWhip");
                document.getElementById("a_save").style.display = "";
            })
            $(":input").change(function () {
                $(this).css("background", "PapayaWhip");
                document.getElementById("a_save").style.display = "";
            })
            $(":text").keyup(function () {
                $(this).css("background", "PapayaWhip");
                document.getElementById("a_save").style.display = "";
            })
            $(":text").change(function () {
                $(this).css("background", "PapayaWhip");
                document.getElementById("a_save").style.display = "";
            })
            $("textarea").keyup(function () {
                $(this).css("background", "PapayaWhip");
                document.getElementById("a_save").style.display = "";
            })
            $("textarea").change(function () {
                $(this).css("background", "PapayaWhip");
                document.getElementById("a_save").style.display = "";
            })
        })
    </script>
    

</head>
<body>
    <form id="form1" runat="server">

        <wilson:DataSource ID="ds1" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.JobOrder" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsCosting" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.JobCost" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="ds12" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.JobCargo" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="ds13" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.JobWorkOrder" KeyMember="SequenceId" FilterExpression="1=0" />
        <asp:SqlDataSource ID="ds14" runat="server" ConnectionString="<%$ ConnectionStrings:local %>"  SelectCommand="SELECT Id, RefNo, JobNo,RefType,Action,CreateBy,CreateDateTime from [LogEvent] Where 1=1 Order By CreateDateTime" />
        
        <wilson:DataSource ID="dsMarking" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.JobCargo" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsRefCont" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.JobCargo" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsJobPhoto" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.SeaAttachment" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsCustomerMast" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="IsCustomer='true' or IsAgent='true'" />
        <wilson:DataSource ID="dsVendorMast" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="IsVendor='true'" />
        <wilson:DataSource ID="dsJobCate" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXMastData" KeyMember="Id" FilterExpression="CodeType='JobCate'" />
        <wilson:DataSource ID="dsUom" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXUom" KeyMember="SequenceId" FilterExpression="CodeType='2'" />
        <wilson:DataSource ID="dsSalesman" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXSalesman" KeyMember="Code" FilterExpression="" />

        <div>
            <table width="100%">
                <tr>
                    <td style="display:none">
                        <dxe:ASPxTextBox runat="server" ID="txtSid"></dxe:ASPxTextBox>
                    </td>
                    <td colspan="6">
                        <dxwgv:ASPxGridView ID="grid1" ClientInstanceName="grid1" runat="server"
                            DataSourceID="ds1" KeyFieldName="SequenceId" OnHtmlEditFormCreated="grid1_HtmlEditFormCreated"
                            OnBeforePerformDataSelect="grid1_BeforePerformDataSelect"
                            OnAfterPerformCallback="base_AfterPerformCallback"
                            OnRowUpdating="grid1_RowUpdating"
                            OnInit="grid1_Init" Width="100%"
                            AutoGenerateColumns="False">
                             <Settings ShowColumnHeaders="false" />
                            <SettingsEditing Mode="EditForm" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <Templates>
                                <EditForm>
                                    <table width="100%" cellpadding="2" cellspacing="2" style="border-bottom: solid 2px #888888;">
                                        <tr>
                                            <td>
                                                <h1>Job Order</h1>
                                                <td align="right">
                                                    <div style="display: none;">
                                                        <dxe:ASPxTextBox runat="server" Text='<%# Eval("SequenceId")%>' ClientInstanceName="ctl_id" Width="100%" ID="ctl_id"></dxe:ASPxTextBox>
                                                    </div>
                                                    <table cellspacing="0" cellpadding="2" border="0">
                                                        <tr>
                                                            <td  style="border: solid 0px #888888;">
                                                                <a id="a_save" href="javascript:g_save(grid1)" style="display:none">Save</a>
                                                            </td>
                                                            <td style="border: solid 0px #888888;display: none">
                                                                <a href="javascript:gcancel(grid1,<%# Container.VisibleIndex %>);">Cancel</a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </td>
                                        </tr>
                                    </table>
                                    <table cellspacing="2" cellpadding="2">

                                        <tr>
                                            <td>Job No/Type
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="TXT0" ClientInstanceName="txtHouseNo" runat="server" ReadOnly="true"
                                                                BackColor="Control" Text='<%# Bind("JobNo") %>' Width="150">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox EnableIncrementalFiltering="True" runat="server" Width="100" ID="txt_JobType" Text='<%#Eval("Note1") %>' ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>Order No
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox ID="txtBkgNo" ClientInstanceName="txtBkgNo" runat="server" Text='<%# Bind("BkgRefNo") %>' Width="150">
                                                
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td>Hbl No
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox ID="txtHouseBl" runat="server" Text='<%# Bind("HblNo") %>' Width="150">
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Client/Bill To
                                            </td>
                                            <td colspan="3">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txtCustomerId" ClientInstanceName="txtCustomerId" runat="server"
                                                                Width="100" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("CustomerId") %>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                                            PopupParty(txtCustomerId,txtCustomer,'C');
                                                                                                }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtCustomer" ClientInstanceName="txtCustomer" ReadOnly="true"
                                                                BackColor="Control" runat="server" Text='' Width="300">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>Category</td>
                                            <td>
                                                <dxe:ASPxComboBox runat="server" ID="cbx_jobCate2" ClientInstanceName="cbx_jobCate2" DataSourceID="dsJobCate" TextField="Code" ValueField="Code" Text='<%# Bind("Note2") %>'
                                                    Width="150" EnableCallbackMode="true" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith">
                                                </dxe:ASPxComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <hr />
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Carrier
                                            </td>
                                            <td colspan="3">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txtShipperId" ClientInstanceName="txtShipperId" runat="server" Width="100" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("ShipperId") %>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                                            PopupShipper(txtShipperId,txtShipperName,txtContact,txtTel,txtFax,txtEmail,null,'C');
                                                                                                }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtShipperName" ClientInstanceName="txtShipperName" runat="server" Text='<%# Bind("ShipperName") %>'
                                                                Width="300">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>Contact
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox ID="txtContact" ClientInstanceName="txtContact" runat="server" Text='<%# Bind("ShipperContact") %>'
                                                    Width="100%">
                                                                
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Fax No
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox ID="txtFax" runat="server" ClientInstanceName="txtFax" Width="100%" Text='<%# Bind("ShipperFax") %>'>
                                                
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td>Tel No </td>
                                            <td>
                                                <dxe:ASPxTextBox ID="txtTel" runat="server" ClientInstanceName="txtTel" Width="100%" Text='<%# Bind("ShipperTel") %>'>
                                                
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td>Email </td>
                                            <td>
                                                <dxe:ASPxTextBox ID="txtEmail" runat="server" ClientInstanceName="txtEmail" Width="100%" Text='<%# Bind("ShipperEmail") %>'>
                                                
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Pol
                                            </td>
                                            <td>
                                                <dxe:ASPxButtonEdit ID="txt_Hbl_Pol" ClientInstanceName="txt_Hbl_Pol" runat="server"
                                                    MaxLength="5" Width="100%" Text='<%# Bind("Pol")%>' HorizontalAlign="Left" AutoPostBack="False">
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                                        PopupPort(txt_Hbl_Pol,txt_pol_name);
                                                                                            }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                            <td>Name
                                            </td>
                                            <td>

                                                <dxe:ASPxTextBox ID="txt_pol_name" ClientInstanceName="txt_pol_name" Width="100%" runat="server"
                                                    Text='<%# Bind("PlaceLoadingName")%>'>
                                                
                                                </dxe:ASPxTextBox>

                                            </td>
                                            <td>FRT Term
                                            </td>
                                            <td>
                                                <dxe:ASPxComboBox EnableIncrementalFiltering="True"
                                                    ID="cmb_FrtTerm" Width="160" runat="server" Text='<%# Bind("FrtTerm") %>'>
                                                    <Items>
                                                        <dxe:ListEditItem Text="FP" Value="FP" />
                                                        <dxe:ListEditItem Text="FC" Value="FC" />
                                                    </Items>
                                                
                                                </dxe:ASPxComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Pod
                                            </td>
                                            <td>
                                                <dxe:ASPxButtonEdit ID="txt_Hbl_Pod" ClientInstanceName="txt_Hbl_Pod" runat="server" MaxLength="5" Text='<%# Bind("Pod")%>'
                                                    Width="100%" HorizontalAlign="Left" AutoPostBack="False">
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                                        PopupPort(txt_Hbl_Pod,txt_pod_name);
                                                                                            }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                            <td>Name
                                            </td>
                                            <td>

                                                <dxe:ASPxTextBox ID="txt_pod_name" ClientInstanceName="txt_pod_name" Width="100%" runat="server"
                                                    Text='<%# Bind("PlaceDischargeName")%>'>
                                                
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td>Pre-Carriage
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox ID="txt_Hbl_PreCarriage" Width="160" runat="server" Text='<%# Bind("PreCarriage")%>'>
                                                
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Place Del
                                            </td>
                                            <td>
                                                <dxe:ASPxButtonEdit ID="txt_Hbl_DelId" ClientInstanceName="txt_Hbl_DelId" runat="server" MaxLength="5"
                                                    Width="100%" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("PlaceDeliveryId")%>'>
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                                        PopupPort(txt_Hbl_DelId,txt_Hbl_DelName);
                                                                                            }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                            <td>Del Name
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox ID="txt_Hbl_DelName" ClientInstanceName="txt_Hbl_DelName" Width="100%"
                                                    runat="server" Text='<%# Bind("PlaceDeliveryname")%>'>
                                                
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td>Del Terms
                                            </td>
                                            <td>
                                                <dxe:ASPxButtonEdit ID="txt_Hbl_DelTerm" ClientInstanceName="txt_Hbl_DelTerm" runat="server"
                                                    Width="158" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("PlaceDeliveryTerm")%>'>
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupGeneralCode(txt_Hbl_DelTerm);
                                                                    }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Place Rec
                                            </td>
                                            <td>
                                                <dxe:ASPxButtonEdit ID="txt_Hbl_RecId" ClientInstanceName="txt_Hbl_RecId" runat="server" MaxLength="5"
                                                    Width="100%" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("PlaceReceiveId")%>'>
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                                        PopupPort(txt_Hbl_RecId,txt_Hbl_RecName);
                                                                                            }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                            <td>Rec Name
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox ID="txt_Hbl_RecName" ClientInstanceName="txt_Hbl_RecName"
                                                    Width="100%" runat="server" Text='<%# Bind("PlaceReceiveName")%>'>
                                                    
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td>Rec Term
                                            </td>
                                            <td>
                                                <dxe:ASPxButtonEdit ID="txt_Hbl_RecTerm" ClientInstanceName="txt_Hbl_RecTerm" runat="server"
                                                    Width="158" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("PlaceReceiveTerm")%>'>
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupGeneralCode(txt_Hbl_RecTerm);
                                                                    }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Ship On Board
                                            </td>
                                            <td>
                                                <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100%" ID="cmb_Hbl_ShipOnBrd"
                                                    runat="server" Text='<%# Bind("ShipOnBoardInd")%>'>
                                                    <Items>
                                                        <dxe:ListEditItem Text="Y" Value="Y" />
                                                        <dxe:ListEditItem Text="N" Value="N" />
                                                    </Items>
                                                    
                                                </dxe:ASPxComboBox>
                                            </td>
                                            <td>ShipOnBrd Date
                                            </td>
                                            <td>
                                                <dxe:ASPxDateEdit ID="date_Hbl_ShipOnBrdDate" Width="100%" runat="server" Value='<%# Bind("ShipOnBoardDate") %>'
                                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                    <ClientSideEvents DateChanged="function(s, e) {ChangeBackColor(s)}" />
                                                </dxe:ASPxDateEdit>
                                            </td>
                                            <td>Express Bl
                                            </td>
                                            <td>
                                                <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100%" ID="cmb_Hbl_ExpressBl"
                                                    runat="server" Text='<%# Bind("ExpressBl") %>'>
                                                    <Items>
                                                        <dxe:ListEditItem Text="Y" Value="Y" />
                                                        <dxe:ListEditItem Text="N" Value="N" />
                                                    </Items>
                                                    
                                                </dxe:ASPxComboBox>
                                                <table>
                                                    <tr>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Cust Ref No</td>
                                            <td>
                                                <dxe:ASPxTextBox ID="txt_custRefNo" runat="server" Width="100%" Text='<%# Bind("CustRefNo") %>'>
                                                    
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td>Service Type</td>
                                            <td>
                                                <dxe:ASPxComboBox ID="cbx_serviceType" runat="server" Width="100%" Value='<%# Bind("ServiceType") %>'>
                                                    <Items>
                                                        <dxe:ListEditItem Text="DTD" Value="DTD" />
                                                        <dxe:ListEditItem Text="DTP" Value="DTP" />
                                                        <dxe:ListEditItem Text="PTD" Value="PTD" />
                                                        <dxe:ListEditItem Text="PTP" Value="PTP" />
                                                    </Items>
                                                    
                                                </dxe:ASPxComboBox>
                                            </td>
                                            <td>Surrender BL
                                            </td>
                                            <td>
                                                <dxe:ASPxComboBox EnableIncrementalFiltering="True"
                                                    ID="cmb_SurrenderBl" Width="100%" runat="server" Text='<%# Bind("SurrenderBl") %>'>
                                                    <Items>
                                                        <dxe:ListEditItem Text="Y" Value="Y" />
                                                        <dxe:ListEditItem Text="N" Value="N" />
                                                    </Items>
                                                    
                                                </dxe:ASPxComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <hr />
                                            </td>
                                        </tr>



                                        <tr>
                                            <td class="ctl" valign="top">Costing</td>
                                            <td colspan="7" valign="top">
                                                
                                                <dxwgv:ASPxGridView ID="Grid_Costing" ClientInstanceName="Grid_Costing" Styles-InlineEditCell-Paddings-Padding="0"
                                                    runat="server" KeyFieldName="SequenceId" DataSourceID="dsCosting" Width="100%" OnRowDeleting="Grid_Costing_RowDeleting" OnRowUpdating="Grid_Costing_RowUpdating" OnRowInserting="Grid_Costing_RowInserting"
                                                    OnBeforePerformDataSelect="Grid_Costing_BeforePerformDataSelect" OnInit="Grid_Costing_Init" OnInitNewRow="Grid_Costing_InitNewRow">
                                                    <SettingsEditing Mode="Inline" />
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                                            <HeaderTemplate>
                                                                <a href="javascript:Grid_Costing.AddNewRow()">Add</a>
                                                            </HeaderTemplate>
                                                            <DataItemTemplate>
                                                                <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_cost_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                            ClientSideEvents-Click='<%# "function(s) { Grid_Costing.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_cost_del" runat="server"
                                                                            Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){Grid_Costing.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </DataItemTemplate>
                                                        <EditItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_cost_update" runat="server" Text="Update" Width="40" AutoPostBack="false"
                                                                            ClientSideEvents-Click='<%# "function(s) { Grid_Costing.UpdateEdit()() }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_cost_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                            ClientSideEvents-Click='<%# "function(s) { Grid_Costing.CancelEdit() }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EditItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="SplitType" FieldName="SplitType" VisibleIndex="1"
                                                            Width="8%">
                                                            <EditItemTemplate>
                                                                <dxe:ASPxComboBox ID="cbo_SplitType" runat="server" Text='<%# Bind("SplitType")%>' Width="60">
                                                                    <Items>
                                                                        <dxe:ListEditItem Text="Set" Value="Set" />
                                                                        <dxe:ListEditItem Text="WtM3" Value="WtM3" />
                                                                    </Items>
                                                                </dxe:ASPxComboBox>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="ChgCode" FieldName="ChgCode" VisibleIndex="3"
                                                            Width="5%">
                                                            <EditItemTemplate>
                                                                <dxe:ASPxButtonEdit ID="txt_CostChgCode" ClientInstanceName="txt_CostChgCode" Width="100" runat="server" Text='<%# Bind("ChgCode")%>' ReadOnly='True'>
                                                                    <Buttons>
                                                                        <dxe:EditButton Text=".."></dxe:EditButton>
                                                                    </Buttons>
                                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                                        PopupChgCode(txt_CostChgCode,txt_CostDes);
                                                                    }" />
                                                                </dxe:ASPxButtonEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="ChgCodeDes" VisibleIndex="3">
                                                            <EditItemTemplate>
                                                                <dxe:ASPxTextBox Width="100%" ID="txt_CostDes" ClientInstanceName="txt_CostDes" BackColor="Control"
                                                                    ReadOnly="true" runat="server" Text='<%# Bind("ChgCodeDes") %>'>
                                                                </dxe:ASPxTextBox>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="SaleQty" VisibleIndex="4"
                                                            Width="5%">
                                                            <EditItemTemplate>
                                                                <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="70" ClientInstanceName="spin_CostSaleQty"
                                                                    ID="spin_CostSaleQty" Text='<%# Bind("SaleQty")%>' Increment="0" DisplayFormatString="0.000" DecimalPlaces="3">
                                                                    <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostSaleQty.GetText(),spin_CostSalePrice.GetText(),1,2,spin_CostSaleAmt);
	                                                   }" />
                                                                </dxe:ASPxSpinEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Price" FieldName="SalePrice" VisibleIndex="5"
                                                            Width="8%">
                                                            <EditItemTemplate>
                                                                                <dxe:ASPxSpinEdit DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_CostSalePrice"
                                                                                    runat="server" Width="70" ID="spin_CostRevPrice" Value='<%# Bind("SalePrice")%>' Increment="0" DecimalPlaces="2">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostSaleQty.GetText(),spin_CostSalePrice.GetText(),1,2,spin_CostSaleAmt);
	                                                   }" />
                                                                                </dxe:ASPxSpinEdit></EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Total" FieldName="SaleDocAmt" VisibleIndex="6"
                                                            Width="8%">
                                                            <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                                            </PropertiesTextEdit>
                                                            <EditItemTemplate>
                                                                                <dxe:ASPxSpinEdit DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_CostSaleAmt"
                                                                                    BackColor="Control" ReadOnly="true" runat="server" Width="90" ID="spin_CostSaleAmt"
                                                                                    Value='<%# Eval("SaleDocAmt")%>'>
                                                                                </dxe:ASPxSpinEdit></EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="SaleCurrency" VisibleIndex="7"
                                                            Width="8%">
                                                            <EditItemTemplate>
                                                                                <dxe:ASPxButtonEdit ID="cmb_CostSaleCurrency" ClientInstanceName="cmb_CostSaleCurrency" runat="server" Width="60" Text='<%# Bind("SaleCurrency") %>' MaxLength="3">
                                                                                    <Buttons>
                                                                                        <dxe:EditButton Text=".."></dxe:EditButton>
                                                                                    </Buttons>
                                                                                    <ClientSideEvents ButtonClick="function(s, e) {
                            PopupCurrency(cmb_CostSaleCurrency,null);
                        }" />
                                                                                </dxe:ASPxButtonEdit></EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="8"
                                                            Width="8%">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Salesman" FieldName="Salesman" VisibleIndex="8"
                                                            Width="8%">
                                                            <EditItemTemplate>
                                                                <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_Salesman"
                                                                    DataSourceID="dsSalesman" TextField="Code" ValueField="Code" Width="100%" Value='<%# Bind("Salesman")%>'>
                                                                </dxe:ASPxComboBox>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="DocNo" FieldName="DocNo" VisibleIndex="8" Visible="false"
                                                            Width="8%">
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                </dxwgv:ASPxGridView>

                                            </td>
                                        </tr>

                                        <tr>
                                            <td class="ctl" valign="top">Cargo</td>
                                            <td colspan="7" valign="top">
                                                <dxwgv:ASPxGridView ID="grid_Mkgs" ClientInstanceName="grid_Mkgs" runat="server"
                                                    DataSourceID="dsMarking" KeyFieldName="SequenceId" Width="100%" OnBeforePerformDataSelect="grid_Mkgs_DataSelect"
                                                    OnRowUpdating="grid_Mkgs_RowUpdating" OnInit="grid_Mkgs_Init" OnInitNewRow="grid_Mkgs_InitNewRow"
                                                    OnRowInserting="grid_Mkgs_RowInserting" OnRowDeleting="grid_Mkgs_RowDeleting"
                                                    OnRowInserted="grid_Mkgs_RowInserted" OnRowUpdated="grid_Mkgs_RowUpdated" OnRowDeleted="grid_Mkgs_RowDeleted">
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <SettingsEditing Mode="EditForm" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_Mkgs.StartEditRow("+Container.VisibleIndex+") }" %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_mkg_del" runat="server"
                                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Mkgs.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <HeaderTemplate>
                                                                <a href="javascript:grid_Mkgs.AddNewRow()">Add</a>
                                                            </HeaderTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Container No" FieldName="ContainerNo" VisibleIndex="4">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Type" FieldName="ContainerType" VisibleIndex="4">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="BK.Weight" FieldName="Weight" VisibleIndex="4">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="BK.Volume" FieldName="Volume" VisibleIndex="5">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="BK.Qty" FieldName="Qty" VisibleIndex="6">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="BK.UOM" FieldName="PackageType" VisibleIndex="7">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="RP.Weight" FieldName="Weight1" VisibleIndex="8">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="RP.Volume" FieldName="Volume1" VisibleIndex="8">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="RP.Qty" FieldName="Qty1" VisibleIndex="8">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="RP.UOM" FieldName="PackageType1" VisibleIndex="8">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="JobNo" VisibleIndex="7" Visible="false">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="MkgType" VisibleIndex="8" Visible="false">
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Settings ShowFooter="True" />
                                                    <TotalSummary>
                                                        <dxwgv:ASPxSummaryItem FieldName="Weight" SummaryType="Sum" DisplayFormat="{0:#,##0.000}" />
                                                        <dxwgv:ASPxSummaryItem FieldName="Volume" SummaryType="Sum" DisplayFormat="{0:#,##0.000}" />
                                                        <dxwgv:ASPxSummaryItem FieldName="Qty" SummaryType="Sum" DisplayFormat="{0:0}" />
                                                        <dxwgv:ASPxSummaryItem FieldName="Weight1" SummaryType="Sum" DisplayFormat="{0:#,##0.000}" />
                                                        <dxwgv:ASPxSummaryItem FieldName="Volume1" SummaryType="Sum" DisplayFormat="{0:#,##0.000}" />
                                                        <dxwgv:ASPxSummaryItem FieldName="Qty1" SummaryType="Sum" DisplayFormat="{0:0}" />
                                                    </TotalSummary>
                                                    <Templates>
                                                        <EditForm>
                                                            <div style="display: none">
                                                                <dxe:ASPxTextBox runat="server" Width="120" ID="txt_mkg_jobNo"
                                                                    Text='<%# Eval("JobNo") %>'>
                                                                </dxe:ASPxTextBox>
                                                            </div>
                                                            <table>
                                                                <tr style="vertical-align: top;">
                                                                    <td>
                                                                        <fieldset>
                                                                            <legend>CM</legend>
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td></td>
                                                                                                <td>Booked</td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>Qty</td>
                                                                                                <td>
                                                                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="120"
                                                                                                        ID="spin_pkg2" Text='<%# Bind("Qty")%>' Increment="0">
                                                                                                    </dxe:ASPxSpinEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>UOM</td>
                                                                                                <td>
                                                                                                    <dxe:ASPxButtonEdit ID="txt_pkgType2" ClientInstanceName="txt_pkgType2" runat="server"
                                                                                                        Width="120" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("PackageType")%>'>
                                                                                                        <Buttons>
                                                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                                        </Buttons>
                                                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupUom(txt_pkgType2,2);
                                                                    }" />
                                                                                                    </dxe:ASPxButtonEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>Weight</td>
                                                                                                <td>
                                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" SpinButtons-ShowIncrementButtons="false"
                                                                                                        runat="server" Width="120" ID="spin_wt2" Value='<%# Bind("Weight")%>' Increment="0" DecimalPlaces="3">
                                                                                                    </dxe:ASPxSpinEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>Volume</td>
                                                                                                <td>
                                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" SpinButtons-ShowIncrementButtons="false"
                                                                                                        runat="server" Width="120" ID="spin_m4" Value='<%# Bind("Volume")%>' Increment="0" DecimalPlaces="3">
                                                                                                    </dxe:ASPxSpinEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>Length</td>
                                                                                                <td>
                                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" SpinButtons-ShowIncrementButtons="false"
                                                                                                        runat="server" Width="120" ID="ASPxSpinEdit10" Value='<%# Bind("L")%>' Increment="0" DecimalPlaces="3">
                                                                                                    </dxe:ASPxSpinEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>Width</td>
                                                                                                <td>
                                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" SpinButtons-ShowIncrementButtons="false"
                                                                                                        runat="server" Width="120" ID="ASPxSpinEdit11" Value='<%# Bind("W")%>' Increment="0" DecimalPlaces="3">
                                                                                                    </dxe:ASPxSpinEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>Heigth</td>
                                                                                                <td>
                                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" SpinButtons-ShowIncrementButtons="false"
                                                                                                        runat="server" Width="120" ID="ASPxSpinEdit12" Value='<%# Bind("H")%>' Increment="0" DecimalPlaces="3">
                                                                                                    </dxe:ASPxSpinEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td>Repack</td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="120"
                                                                                                        ID="ASPxSpinEdit3" Text='<%# Bind("Qty1")%>' Increment="0">
                                                                                                    </dxe:ASPxSpinEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxe:ASPxButtonEdit ID="txt_pkgType3" ClientInstanceName="txt_pkgType3" runat="server"
                                                                                                        Width="120" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("PackageType1")%>'>
                                                                                                        <Buttons>
                                                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                                        </Buttons>
                                                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupUom(txt_pkgType3,2);
                                                                    }" />
                                                                                                    </dxe:ASPxButtonEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" SpinButtons-ShowIncrementButtons="false"
                                                                                                        runat="server" Width="120" ID="ASPxSpinEdit1" Value='<%# Bind("Weight1")%>' Increment="0" DecimalPlaces="3">
                                                                                                    </dxe:ASPxSpinEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" SpinButtons-ShowIncrementButtons="false"
                                                                                                        runat="server" Width="120" ID="ASPxSpinEdit2" Value='<%# Bind("Volume1")%>' Increment="0" DecimalPlaces="3">
                                                                                                    </dxe:ASPxSpinEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" SpinButtons-ShowIncrementButtons="false"
                                                                                                        runat="server" Width="120" ID="ASPxSpinEdit4" Value='<%# Bind("L1")%>' Increment="0" DecimalPlaces="3">
                                                                                                    </dxe:ASPxSpinEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" SpinButtons-ShowIncrementButtons="false"
                                                                                                        runat="server" Width="120" ID="ASPxSpinEdit5" Value='<%# Bind("W1")%>' Increment="0" DecimalPlaces="3">
                                                                                                    </dxe:ASPxSpinEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" SpinButtons-ShowIncrementButtons="false"
                                                                                                        runat="server" Width="120" ID="ASPxSpinEdit6" Value='<%# Bind("H1")%>' Increment="0" DecimalPlaces="3">
                                                                                                    </dxe:ASPxSpinEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </fieldset>
                                                                    </td>
                                                                    <td>
                                                                        <fieldset>
                                                                            <legend>Container Info</legend>
                                                                            <table>
                                                                                <tr>
                                                                                    <td>Cont No
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxDropDownEdit ID="DropDownEdit2" runat="server" ClientInstanceName="DropDownEdit"
                                                                                            Text='<%# Bind("ContainerNo") %>' Width="120px" AllowUserInput="True">
                                                                                            <DropDownWindowTemplate>
                                                                                                <dxwgv:ASPxGridView ID="gridPopCont" runat="server" AutoGenerateColumns="False" ClientInstanceName="GridView"
                                                                                                    Width="300px" DataSourceID="dsRefCont" KeyFieldName="ContainerNo" OnCustomJSProperties="gridPopCont_CustomJSProperties">
                                                                                                    <Columns>
                                                                                                        <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" VisibleIndex="1">
                                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                                        <dxwgv:GridViewDataTextColumn FieldName="SealNo" VisibleIndex="2">
                                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                                        <dxwgv:GridViewDataTextColumn FieldName="ContainerType" Caption="Type" VisibleIndex="2">
                                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                                    </Columns>
                                                                                                    <ClientSideEvents RowClick="RowClickHandler" />
                                                                                                </dxwgv:ASPxGridView>
                                                                                            </DropDownWindowTemplate>
                                                                                        </dxe:ASPxDropDownEdit>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Seal No
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox runat="server" Width="120" ID="txt_sealN2"
                                                                                            Text='<%# Bind("SealNo") %>' ClientInstanceName="txt_sealN">
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Cont Type
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxButtonEdit ID="txt_contType" ClientInstanceName="txt_contType" runat="server"
                                                                                            Width="120" HorizontalAlign="Left" AutoPostBack="False" Value='<%# Bind("ContainerType") %>'>
                                                                                            <Buttons>
                                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                            </Buttons>
                                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupUom(txt_contType,1);
                                                                    }" />
                                                                                        </dxe:ASPxButtonEdit>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </fieldset>
                                                                    </td>
                                                                    <td>
                                                                        <fieldset>
                                                                            <legend>MKG&DES</legend>
                                                                            <table>
                                                                                <tr>
                                                                                    <td>Description
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxMemo runat="server" Rows="6" Width="300" ID="txt_des2" Text='<%# Bind("Description")%>'>
                                                                                        </dxe:ASPxMemo>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Marking
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxMemo runat="server" Rows="5" Width="300" ID="txt_mkg2" Text='<%# Bind("Marking")%>'>
                                                                                        </dxe:ASPxMemo>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </fieldset>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateMkgs" ReplacementType="EditFormUpdateButton"
                                                                    runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                                    runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                            </div>
                                                        </EditForm>
                                                    </Templates>
                                                </dxwgv:ASPxGridView>
                                                <dxwgv:ASPxGridView ID="grid12" ClientInstanceName="grid12" runat="server" Visible="false"
                                                    DataSourceID="ds12" KeyFieldName="SequenceId" OnBeforePerformDataSelect="grid12_BeforePerformDataSelect"
                                                    OnRowUpdating="grid12_RowUpdating" OnRowInserting="grid12_RowInserting" OnRowDeleting="grid12_RowDeleting"
                                                    OnInit="grid12_Init" Width="100%" Styles-InlineEditCell-Paddings-Padding="1"
                                                    AutoGenerateColumns="False">
                                                    <SettingsEditing Mode="Inline" NewItemRowPosition="Bottom" />
                                                    <SettingsPager Mode="ShowAllRecords" />
                                                    <Settings />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_cont_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid12.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_cont_del" runat="server"
                                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid12.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_cont_update" runat="server" Text="Update" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid12.UpdateEdit() }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_cont_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid12.CancelEdit() }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="RefNo" FieldName="RefNo" ReadOnly="true" VisibleIndex="98" Width="60">
                                                            <EditItemTemplate />
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                </dxwgv:ASPxGridView>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td class="ctl" valign="top">Remark</td>
                                            <td colspan="7" valign="top">

                                                <dxwgv:ASPxGridView ID="grid13" ClientInstanceName="grid13" runat="server"
                                                    DataSourceID="ds13" KeyFieldName="SequenceId" OnBeforePerformDataSelect="grid13_BeforePerformDataSelect"
                                                    OnRowUpdating="grid13_RowUpdating" OnRowInserting="grid13_RowInserting" OnRowDeleting="grid13_RowDeleting"
                                                    OnInit="grid13_Init" Width="100%" Styles-Customization-Paddings-Padding="0" Styles-InlineEditCell-Paddings-Padding="1"
                                                    AutoGenerateColumns="False">
                                                    <SettingsEditing Mode="Inline" NewItemRowPosition="Bottom" />
                                                    <SettingsPager Mode="ShowAllRecords" />
                                                    <Settings />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                                            <HeaderTemplate>
                                                                <a href="javascript:grid13.AddNewRow()">Add</a>
                                                            </HeaderTemplate>
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_cont_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid13.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_cont_del" runat="server"
                                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid13.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_cont_update" runat="server" Text="Update" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid13.UpdateEdit() }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_cont_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid13.CancelEdit() }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn Caption="Type" FieldName="Note1" VisibleIndex="98" Width="60">
                                                            <PropertiesComboBox>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Tracking" Value="Tracking" />
                                                                    <dxe:ListEditItem Text="Sales" Value="Sales" />
                                                                    <dxe:ListEditItem Text="Billing" Value="Billing" />
                                                                    <dxe:ListEditItem Text="Internal" Value="Internal" />
                                                                </Items>
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Note2" VisibleIndex="98">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="User" FieldName="UpdateBy" ReadOnly="true" VisibleIndex="98" Width="110">
                                                            <EditItemTemplate />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="DateTime" FieldName="CreateDateTime" ReadOnly="true" VisibleIndex="98" Width="150">
                                                            <EditItemTemplate />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="RefNo" FieldName="RefNo" ReadOnly="true" VisibleIndex="98" Width="60" Visible="false">
                                                            <EditItemTemplate />
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>

                                                </dxwgv:ASPxGridView>
                                            </td>


                                        </tr>

                                        <tr>
                                            <td class="ctl" valign="top">Audit Log</td>
                                            <td colspan="7" valign="top">

                                                <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid14" runat="server"
                                                    DataSourceID="ds14" KeyFieldName="Id" OnBeforePerformDataSelect="grid14_BeforePerformDataSelect"
                                                    OnInit="grid14_Init" Width="100%" Styles-InlineEditCell-Paddings-Padding="1"
                                                    AutoGenerateColumns="False">
                                                    <SettingsEditing Mode="Inline" NewItemRowPosition="Bottom" />
                                                    <SettingsPager Mode="ShowAllRecords" />
                                                    <Settings />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0" Visible="false">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_cont_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid14.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_cont_del" runat="server"
                                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid14.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_cont_update" runat="server" Text="Update" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid14.UpdateEdit() }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_cont_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid14.CancelEdit() }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Actions" FieldName="Action" VisibleIndex="3" Width="400" />
                                                        <dxwgv:GridViewDataTextColumn Caption="User" FieldName="CreateBy" ReadOnly="true" VisibleIndex="98" Width="60">
                                                            <EditItemTemplate />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataDateColumn Caption="DateTime" FieldName="CreateDateTime" ReadOnly="true" PropertiesDateEdit-DisplayFormatString="{0:dd/MM/yy HH:mm:ss}" SortOrder="Descending" VisibleIndex="98" Width="110">
                                                            <EditItemTemplate />
                                                        </dxwgv:GridViewDataDateColumn>
                                                    </Columns>

                                                </dxwgv:ASPxGridView>
                                            </td>
                                        </tr>







                                        <%--<tr>
                                            <td class="ctl" valign="top">Files</td>
                                            <td colspan="7" valign="top">



                                                <dxwgv:ASPxGridView ID="grd_Photo" ClientInstanceName="grd_Photo" runat="server" DataSourceID="dsJobPhoto" OnInit="grd_Photo_Init"
                                                    KeyFieldName="SequenceId" Width="100%" EnableRowsCache="False" OnBeforePerformDataSelect="grd_Photo_BeforePerformDataSelect"
                                                    AutoGenerateColumns="false" OnRowDeleting="grd_Photo_RowDeleting" OnInitNewRow="grd_Photo_InitNewRow" OnRowUpdating="grd_Photo_RowUpdating">
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="80px">
                                                            <DataItemTemplate>
                                                                <table>
                                                                                                                                        <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("JobStatusCode"))=="USE" %>'
                                                                                ClientSideEvents-Click='<%# "function(s) { grd_Photo.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_mkg_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("JobStatusCode"))=="USE" %>'
                                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grd_Photo.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <HeaderTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="ASPxButton4" runat="server" Text="Upload" AutoPostBack="false"
                                                                                UseSubmitBehavior="false">
                                                                                <ClientSideEvents Click="function(s,e) {
                                                                isUpload=true;
                                                        PopupUploadPhoto();
                                                        }" />
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_Refresh" runat="server" Text="Refresh" ClientVisible="false" AutoPostBack="false"
                                                                                UseSubmitBehavior="false">
                                                                                <ClientSideEvents Click="function(s,e) {
                                                        grd_Photo.Refresh();
                                                        }" />
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="Photo" Width="100px">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <a href='<%# Eval("Path")%>' target="_blank">
                                                                                <dxe:ASPxImage ID="ASPxImage1" Width="60" Height="60" runat="server" ImageUrl='<%# Eval("ImgPath") %>'>
                                                                                </dxe:ASPxImage>
                                                                            </a>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="FileName" FieldName="FileName" Width="200px"></dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Des" FieldName="FileNote"></dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Templates>
                                                        <EditForm>
                                                            <div style="display: none">
                                                                <dxe:ASPxTextBox ID="txt_PhotoId" runat="server" Text='<%# Eval("SequenceId") %>'></dxe:ASPxTextBox>
                                                            </div>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>Remark
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxMemo ID="txt_Rmk" runat="server" Rows="4" Width="600" Text='<%# Eval("FileNote") %>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                            <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateMkgs" ReplacementType="EditFormUpdateButton"
                                                                                runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                            <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                                                runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                        </div>
                                                                    </td>
                                                                </tr>

                                                            </table>
                                                        </EditForm>
                                                    </Templates>
                                                </dxwgv:ASPxGridView>
                                            </td>
                                        </tr>--%>
                                    </table>
                                </EditForm>
                            </Templates>
                        </dxwgv:ASPxGridView>
                    </td>
                </tr>
            </table>


        </div>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
            AllowResize="True" Width="600" EnableViewState="False">
        </dxpc:ASPxPopupControl>
        <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr1"
            HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
            AllowResize="True" Width="600" EnableViewState="False">
            <ClientSideEvents CloseUp="function(s, e) {
      if(isUpload)
	    grd_Photo.Refresh();
}" />
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
