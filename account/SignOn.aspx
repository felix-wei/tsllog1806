<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignOn.aspx.cs" Inherits="Frames_SignOn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="content-script-type" content="text/javascript">
    <style type="text/css">
        input
        {
            font-size: 13px;
        }

        .loginBtn
        {
            width: 180px;
            height:40px;
            font-size: 16px;
            padding: 4px;
            color: white;
            font-weight: bold;
            background: #0066bb;
            border: 0px;
        }

        .loginTxt
        {
            height: 32px;
            font-size: 20px;
            width: 320px;
            margin-bottom: 8px;
            margin-top: 4px;
            padding:4px;
            background: white;
            border: solid 2px #dddddd;
        }

        .Failure
        {
            font-size: 12px;
            color: Red;
        }

        td
        {
            font-size: 14px;
        }
    </style>
    <script type="text/javascript" src="/script/jquery.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {

            var h = $(window).height();
            var w = $(window).width();
           $("#divzoom").css('width', w / 2 + 200);
           $("#divzoom").css('height', h);

            $(window).resize(function () {
                var h = $(window).height();
                var w = $(window).width();
                $("#divzoom").css('width', w / 2 + 200);
                $("#divzoom").css('height', h);
                ////   $("#divLogin").css('margin-top', h / 2 - 180);
            });

            // if ($("#Login1_UserName").val() == "")
            //     $("#Login1_UserName").focus();
            //  else {
            //     if ($("#Login1_Password").val() == "")
            //         $("#Login1_Password").focus();
            // }
        });
    </script>

</head>
<body style="margin: 0px; font-family: Tahoma; background: #FFFFFF; overflow: hidden;">

    <form id="form1" runat="server" autocomplete="off">
        <div id="divLogin" style="background: white;margin:0px;">
            <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="100%" height="100%" align="left" valign="middle">

                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="left" width="65%" >
                                    <div style="text-align:left;overflow:hidden" id="divzoom">
                                    <img src="/custom/container.jpg" height="100%" width="100%" />

                                    </div>
                                </td>

                                <td align="right" width="35%" valign="top">
                                    <div style="border: solid 0px blue; text-align: left; padding-left: 8px;">
                                    </div>
                                    <div style="border: solid 0px red; text-align: left; padding-left: 10px;">

                                        <table width="90%" style="margin-left:20px;">

                                                                                                <tr>
                                                        <td style="font-size: 18px;font-weight:bold;color:#888888">
                                                            
                                                            <br /><br />
                                                            <br /><br /><br /><br />
                                                            <%--<img src="/custom/logo-brand.jpg" />--%>
                                                            <br /><br />
                                                            <%= System.Configuration.ConfigurationManager.AppSettings["CompanyName"].Replace("PTE LTD","") %>
                                                            <br /><br />

                                                        </td>
                                                        </tr>

                                        </table>
                                        <asp:Login ID="Login1" runat="server" OnAuthenticate="Login1_Authenticate" ForeColor="black" DisplayRememberMe="True"  TitleText="">

                                            <LayoutTemplate>

                                                <table style="margin-left: 20px; margin-top: 20px;" border="0" width="90%">
                                                    
                                              
                                                    <tr>

                                                        <td align="left" style="font-size: 12px;">
                                                            <asp:TextBox ID="UserName" runat="server" Width="300" CssClass="loginTxt" Text=""></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="font-size: 12px;">
                                                            <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="300" Text="ADMINVK" CssClass="loginTxt"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr style="display: none;">
                                                        <td align="left" class="Failure">
                                                            <asp:Literal ID="FailureText" runat="server"></asp:Literal></td>
                                                    </tr>
                                                    <tr style="display: none;">
                                                        <td align="left" valign="top">
                                                            <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me"></asp:CheckBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="padding-right: 22px;">
                                                            <br />
                                                            <br />
                                                            <asp:Button ID="Login" CommandName="Login" runat="server" CssClass="loginBtn" Text="Login"></asp:Button>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </LayoutTemplate>
                                        </asp:Login>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
