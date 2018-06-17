<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignOn.aspx.cs" Inherits="Frames_SignOn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Cargo Connect Sign On</title>
    <meta http-equiv="content-script-type" content="text/javascript">
    <style type="text/css">
        input
        {
            font-size: 13px;
        }

        .loginBtn
        {
            width: 120px;
            font-size: 14px;
            padding: 4px;
            color: darkgreen;
            font-weight: bold;
            background: #dddddd;
        }

        .loginTxt
        {
            margin-top: 10px;
            height: 24px;
            font-size: 16px;
            width: 240px;
            margin-bottom: 10px;
            margin-top: 4px;
            background: white;
            border: solid 8px #dddddd;
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
            $("#divLogin").css('margin-top', h / 2 - 180);

            $(window).resize(function () {
                var h = $(window).height();
                var w = $(window).width();
                $("#divLogin").css('margin-top', h / 2 - 180);
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
<body style="margin: 0px; font-family: Tahoma; background: #EEEEEE; overflow: hidden;">

    <form id="form1" runat="server" autocomplete="off">
        <div id="divLogin" style="background: white;">
            <table width="96%" height="228" border="0">
                <tr>
                    <td width="100%" height="100%" align="center" valign="middle">

                        <table border="0">
                            <tr>
                                <td align="right" width="50%">
                                    <img src="/custom/ez-login-pic.jpg" />
                                </td>

                                <td align="right" width="50%" valign="top">
                                    <div style="border: solid 0px blue; text-align: left; padding-left: 8px;">
                                    </div>
                                    <div style="border: solid 0px red; text-align: left; padding-left: 10px;">


                                        <asp:Login ID="Login1" runat="server" OnAuthenticate="Login1_Authenticate" ForeColor="black" DisplayRememberMe="True" TitleText="">

                                            <LayoutTemplate>

                                                <table style="margin-left: 6px; margin-top: 38px;" border="0">
                                                    <tr>

                                                        <td align="left" style="font-size: 12px;">
                                                            <asp:TextBox ID="UserName" runat="server" Width="220" CssClass="loginTxt" Text=""></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="font-size: 12px;">
                                                            <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="220" Text="ADMINVK" CssClass="loginTxt"></asp:TextBox>
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
