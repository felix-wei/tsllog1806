<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PhotoList.aspx.cs" Inherits="SelectPage_PhotoList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="/PagesContTrucking/script/StyleSheet.css" rel="stylesheet" />
    <script src="/script/jquery.js"></script>

    <script>
        jQuery.noConflict();
    </script>
    <script type="text/javascript">
        function PopupUploadPhoto() {
            popubCtrPic.SetHeaderText('Upload Attachment');
            //popubCtrPic.SetContentUrl('/PagesContTrucking/Upload.aspx?Type=CTM&Sn=0&jobNo=' + txt_JobNo.GetText());
            popubCtrPic.SetContentUrl('/Modules/Upload/MultipleUpload.aspx?Type=CTM&Sn=0&jobNo=' + txt_JobNo.GetText());
            popubCtrPic.Show();
        }
        function PhotoEdit(id) {
            popubCtrPic.SetHeaderText('Photo Edit');
            popubCtrPic.SetContentUrl('/SelectPage/PhotoEdit.aspx?sn=' + txt_JobNo.GetText() + '&id=' + id);
            popubCtrPic.Show();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsJobPhoto" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmAttachment" KeyMember="Id" />
        <div>

            <center>
                                            <table width="750">
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton1" Width="150" runat="server" Text="Refresh"
                                                            Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' AutoPostBack="false"
                                                            UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s,e) {
                                                               location.reload();
                                                        }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton6" Width="150" runat="server" Text="Upload Attachments"
                                                            Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' AutoPostBack="false"
                                                            UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s,e) {
                                                                isUpload=true;
                                                        PopupUploadPhoto();
                                                        }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td align="left">
                                                        <div style="display:none">
                                                              
                                                            <dxe:ASPxTextBox ID="txt_JobNo" runat="server" ClientInstanceName="txt_JobNo"></dxe:ASPxTextBox>
                                                        </div>
                                                        <dxe:ASPxButton ID="btn_Del" runat="server" Text="Delete All" AutoPostBack="false"
                                                            Enabled='<%# SafeValue.SafeString(Eval("No"),"0")!="0"&&!SafeValue.SafeBool(Eval("JobClose"),false) %>'
                                                            UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s, e) {
                                grd_Photo.PerformCallback('DeleteAll');
                            }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td align="right">
                                                        <dxe:ASPxButton ID="btn_Select" Visible="true" runat="server" Text="Select All"
                                                            AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("No"),"0")!="0"&&!SafeValue.SafeBool(Eval("JobClose"),false) %>'
                                                            UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s, e) {
				                jQuery('input.fcb').each(function(el){ this.checked = true;});
                                    
                            }" />
                                                        </dxe:ASPxButton>

                                                    </td>
                                                    <td align="right">
                                                        <dxe:ASPxButton ID="btn_UnSelect" Visible="true" runat="server" Text="Unselect All"
                                                            AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("No"),"0")!="0"&&!SafeValue.SafeBool(Eval("JobClose"),false) %>'
                                                            UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s, e) {
				                jQuery('input.fcb').each(function(el){ this.checked = false;});
                                    
                            }" />
                                                        </dxe:ASPxButton>

                                                    </td>
                                                    <td align="right">
                                                        <dxe:ASPxButton ID="btn_Download" Visible="true" runat="server" Text="Download Photo"
                                                            AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("No"),"0")!="0"&&!SafeValue.SafeBool(Eval("JobClose"),false) %>'
                                                            UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s, e) {
				                
                                if(jQuery('input.fcb:checked').length == 0){
					alert('Please select photo before sending.');
					return;
					}
                                    jQuery('#email_photo').val('');
				 jQuery('input.fcb:checked').each(function(idx,el){
						
					var ar = jQuery(this).attr('id').split('_');
					var b4 = jQuery('#email_photo').val();
					jQuery('#email_photo').val(b4 +','+ ar[1]);
				});             
                                
                                
                                 if(confirm('Download Selected Photos ?'))  {                               
                                    jQuery.post('/frames/download.ashx', 
                                    { 
                                        pic:jQuery('#email_photo').val(),
                                        job:jQuery('#email_job').val() 
                                     },
                                     function(data) {
                                        alert('Click to Download : ' + data);
                                        document.location.href = data; //windows.open(data);
                                        //document.getElementById('emailbox').style.display='none';
                                     }, 
                                     'html'
                                    );
                                }

                    

                            }" />
                                                        </dxe:ASPxButton>

                                                    </td>

                                                    <td align="right">
                                                        <dxe:ASPxButton ID="btn_Email" Visible="true" runat="server" Text="Email Photo"
                                                            AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("No"),"0")!="0"&&!SafeValue.SafeBool(Eval("JobClose"),false) %>'
                                                            UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s, e) {
				                
                                
                                document.getElementById('emailbox').style.display='';
                            }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="8">
                                                        <hr />
                                                        <div style="display: none" id="emailbox">
                                                            <table cellspacing="2" cellpadding="2" border="2">
                                                                <tr>
                                                                    <td valign="top" width="150">
                                                                        Email To
                                                                    </td>
                                                                    <td valign="top" width="600">
                                                                        <textarea id='email_to' rows="2" cols="60"></textarea>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                                        Email CC
                                                                    </td>
                                                                    <td valign="top">
                                                                        <textarea id='email_cc' rows="2" cols="60"></textarea>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                                        Email BCC
                                                                    </td>
                                                                    <td valign="top">
                                                                        <textarea id='email_bcc' rows="2" cols="60"></textarea>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                                        Email Subject
                                                                    </td>
                                                                    <td valign="top">
                                                                        <textarea id='email_subject' rows="1" cols="60"></textarea>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                                        Email Message
                                                                    </td>
                                                                    <td valign="top">
                                                                        <textarea id='email_message' rows="8" cols="60"></textarea>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" align="center">
                                                                        <input type='hidden' id='email_photo' value='' />
                                                                        <dxe:ASPxButton ID="ASPxButton21" Visible="true" runat="server" Text="Send Email"
                                                                            AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("No"),"0")!="0"&&!SafeValue.SafeBool(Eval("JobClose"),false) %>'
                                                                            UseSubmitBehavior="false">
                                                                            <ClientSideEvents Click="function(s, e) {
					if(jQuery('input.fcb:checked').length == 0){
					alert('Please select photo before sending.');
					return;
					}
                                    jQuery('#email_photo').val('');
				 jQuery('input.fcb:checked').each(function(idx,el){
						
					var ar = jQuery(this).attr('id').split('_');
					var b4 = jQuery('#email_photo').val();
					jQuery('#email_photo').val(b4 +','+ ar[1]);
				});               
				//alert(jQuery('#email_photo').val());
                                if(confirm('Send This Photo Email ?'))  {                               
                                    jQuery.post('/frames/email.ashx', 
                                    { 
                                        to: jQuery('#email_to').val(), 
                                        cc: jQuery('#email_cc').val(), 
                                        bcc:jQuery('#email_bcc').val(), 
                                        sub:jQuery('#email_subject').val(), 
                                        msg:jQuery('#email_message').val(), 
                                        pic:jQuery('#email_photo').val(),
                                        job:jQuery('#email_job').val(),
                                        type:'E' 
                                     },
                                     function(data) {
                                        if (data == '')
                                            alert('Email Sent Successfully !');
                                        else
                                            alert(data);

                                        document.getElementById('emailbox').style.display='none';
                                     }, 
                                     'html'
                                    );
                                }
                            }" />
                                                                        </dxe:ASPxButton>
                                                                        <b>In order to have a copy, please remember to include your email address.</b>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
             </center>
            <dxdv:ASPxDataView ID="grd_Photo" ClientInstanceName="grd_Photo" runat="server" DataSourceID="dsJobPhoto"
                KeyFieldName="Id" Width="900" EnableRowsCache="False" 
                RowPerPage="10000" ColumnCount="4" OnCustomCallback="grd_Photo_CustomCallback"
                AlwaysShowPager="false">
                <ItemTemplate>
                    <div style="height: 210px;">
                        <a href='<%# Eval("ImgPath")%>' target="_blank">
                            <img src='<%# Eval("ImgPath") %>' width="150" height="150" id='foto_<%# Eval("Id")%>'
                                class='foto' />
                        </a>
                    </div>
                    <div>
                        <table border="1">
                            <tr>
                                <td width="100">File Name
                                </td>
                                <td width="100">
                                    <%# Eval("FileName")%>
                                </td>
                            </tr>
                            <tr>
                                <td width="100">Send Email ?
                                </td>
                                <td width="100">
                                    <input type='checkbox' style='font-size: 20px' id='fcb_<%# Eval("Id")%>' class='fcb'
                                        name='fcb' />
                                </td>
                            </tr>
                            <tr>
                                <td width="100">Job No
                                </td>
                                <td width="100">
                                    <%# Eval("RefNo")%>
                                    <input type='hidden' id='email_job' value='<%# Eval("RefNo") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td width="100">Container
                                </td>
                                <td width="100">
                                    <%# Eval("ContainerNo")%>
                                </td>
                            </tr>
                            <tr>
                                <td width="100">Trip
                                </td>
                                <td width="100" style="font-weight: bold; font-size: 13px; color: Black;">
                                    <%# Eval("TripIndex")%>
                                </td>
                            </tr>
                            <tr>
                                <td width="100">Remarks
                                </td>
                                <td width="100">
                                    <%# Eval("FileNote")%>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxButton ID="ASPxButton16" Enabled='<%# !SafeValue.SafeBool(Eval("JobStatusCode"),false) %>'
                                    runat="server" Text="Edit" AutoPostBack="false" UseSubmitBehavior="false" ClientSideEvents-Click='<%# "function(s) { PhotoEdit("+Eval("Id")+") }"  %>'>
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btn_Del1" Enabled='<%# !SafeValue.SafeBool(Eval("JobStatusCode"),false) %>'
                                    runat="server" Text="Delete" AutoPostBack="false" UseSubmitBehavior="false" ClientSideEvents-Click='<%# "function(s) { grd_Photo.PerformCallback("+Eval("Id")+") }"  %>'>
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <PagerSettings>
                    <AllButton Visible="True">
                    </AllButton>
                </PagerSettings>
            </dxdv:ASPxDataView>
        </div>
        <dxpc:ASPxPopupControl ID="popubCtrPic" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtrPic"
            HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="500"
            Width="800" EnableViewState="False">
            <ClientSideEvents CloseUp="function(s, e) {                    
                     if(grid!=null){
	                     grid.Refresh();
                    }     
}" />
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Photho" AllowDragging="True" EnableAnimation="False" Height="400"
            Width="600" EnableViewState="False">
            <ClientSideEvents CloseUp="function(s, e) {                    
                    }     
}" />
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
