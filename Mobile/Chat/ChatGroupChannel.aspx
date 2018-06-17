<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChatGroupChannel.aspx.cs" Inherits="Mobile_Chat_ChatGroupChannel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="script/ionic.min.css" rel="stylesheet" />
    <link href="script/StyleSheet.css" rel="stylesheet" />
    <script src="script/angular.js"></script>
    <script type="text/javascript" src="/PagesContTrucking/script/jquery.js"></script>
    <script type="text/javascript" src="/PagesContTrucking/script/firebase.js"></script>
    <script type="text/javascript" src="/PagesContTrucking/script/js_company.js"></script>
    <script src="script/js_firebase.js"></script>
    <script src="script/js_ChatGroupChannel.js"></script>
    <script type="text/javascript">
        $(function () {
            //$('#btn_send').bind('keypress',function(event){
            //    if (event.keyCode == 13) {
            //        alert('huiche');
            //    }
            //});
            $('#input_msg').bind('keypress', function (event) {
                if (event.keyCode == 13) {
                    document.getElementById("btn_send").click();
                    return false;
                }
            });
        })

    </script>
</head>
<body ng-app>
    <form id="form1" runat="server" ng-controller="Ctrl_ChatChannel">
        <div>
            <dxe:ASPxLabel ID="lb_user" ClientInstanceName="lb_user" runat="server"></dxe:ASPxLabel>
            <dxe:ASPxLabel ID="lb_chat" ClientInstanceName="lb_chat" runat="server"></dxe:ASPxLabel>
            <dxe:ASPxLabel ID="lb_chatId" ClientInstanceName="lb_chatId" runat="server"></dxe:ASPxLabel>
        </div>
        <div class="bar bar-header bar-positive">
            <input type="button" class="button button-clear" value="< Back" ng-click="action.GoBack();" />
            <h1 class="title">{{vm.chatTitle}}</h1>
        </div>
        <div class="pane">
            <div id="scroll" class="content f-scorll f-scorll-has-header f-scorll-has-footer">
                <div class="list">
                    <div class="item item-divider f-messagechat_to_view_more" ng-show="vm.haveHistory" ng-click="action.viewMore();">View More</div>
                    <div class="item f-item-message" ng-repeat="row in vm.list" ng-class="{true:'f-item-message-right',false:'f-item-message-left'}[action.isOwn(row)]">
                        
                        <img class="f-message-usericon f-message-usericon-side" src="script/jay.jpg" />
                        <div class="f-message-textbox f-message-textbox-side" >
                            <h2>{{row.sender}}&nbsp;&nbsp;
                    <b style="font-size: 14px; font-weight: normal; color: #666;">({{row.date}})</b>
                            </h2>
                            <p>&nbsp;&nbsp;{{row.msg}}</p>
                            <div class="f-message-textbox-from-side"></div>
                        </div>
                    </div>
                </div>

            </div>

        </div>
        <div class="bar bar-footer bar-positive">
            <div class="item-input-inset f-full-row f-padding-0">
                <div class="item-input-wrapper">
                    <input type="text" id="input_msg" ng-model="vm.msg_text" />
                </div>
                <input type="button" id="btn_send" class="button button-small" ng-click="action.sendMsg()" value="Send" />
            </div>
        </div>
    </form>
</body>
</html>
