<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SurgeryRoom.aspx.cs" Inherits="_Default" EnableEventValidation="false" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Surgery Room Schedule</title>
        <link rel="stylesheet" type="html/sandboxed" href="Content/bootstrap.min.css" />
        <link rel="stylesheet" type="text/css" href="Content/StyleSheet.css" />
        <script type="text/javascript" src="Scripts/jquery-1.10.2.js"></script>
        <script type="text/javascript" src="Scripts/jquery-ui-1.10.4.custom.min.js"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                $('a.login-window').click(function () {
                    // Getting the variable's value from a link 
                    var loginBox = $(this).attr('href');
                    $(".login-popup").fadeIn(300, function () {
                        $('.login-popup').css("display", "block");
                    });
                    //Set the center alignment padding + border
                    var popMargTop = ($(loginBox).height() + 24) / 2;
                    var popMargLeft = ($(loginBox).width() + 24) / 2;
                    $(loginBox).css({
                        'margin-top': -popMargTop,
                        'margin-left': -popMargLeft
                    });
                    // Add the mask to body
                    $('body').append('<div id="mask"></div>');
                    $('#mask').fadeIn(300);
                    $('#user').focus();
                });
                $('a.close').click(function () {
                    $(".login-popup").fadeOut(300, function () {
                        $('.login-popup').css("display", "none");
                    });
                    $('#mask').fadeOut(300, function () {
                        $('#mask').remove();
                    });
                });
                var start; var finish;
                $('.selectable').sortable({
                    axis: "y",
                    cursor: "move",
                    containment: "parent",
                    tolerance: "pointer",
                    opacity: 0.8,
                    placeholder: "sortable-placeholder",

                    start: function(e, ui) {
                        start = ui.item.index() + 1;
                    },

                    update: function(e, ui) {
                        finish = ui.item.index() + 1;
                    },

                    stop: function (e, ui) {
                        $("#beginVal").val(start);
                        $("#finalVal").val(finish);
                        $("#loginform").submit();
                    }
                });
                $('.selectable').disableSelection();
            });
        </script>
    </head>
    <body>
        <div class="navbar navbar-default">
            <a class="navbar-brand">SurgeryGenie</a>
            <a class="pull-right" href="Default.aspx">Main Schedule View</a>
        </div>
        <form id="loginform" runat="server">
            <asp:HiddenField ID="beginVal" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="finalVal" runat="server" OnValueChanged="finalVal_TextChanged"></asp:HiddenField>
            <asp:GridView ID="test" runat="server"></asp:GridView>
            <a href="#login-box" class="login-window pull-right btn primary" runat="server" id="signIN">Sign In</a>
            <asp:LinkButton ID="signOUT" runat="server" OnClick="signOUT_Click" Text="Sign Out"></asp:LinkButton>
            <div id="mainPageContainer">
                <h3>Surgery Viewer</h3>
                <asp:TextBox ID="tbTime" runat="server" AutoPostBack="true" />
                <asp:Button ID="btnAdd" runat="server" Text="Add Event" OnClick="btnAdd_Click" /><br />
                <div id="login-box" class="login-popup">
                    <a href="#" class="close"><img src="close_pop.png" class="btn_close" title="Close Window" alt="Close" /></a>
            	    Username:<label class="username" style="position:relative;left:30px;"> <asp:TextBox  runat="server" ID="user" CssClass="inputs"></asp:TextBox></label><br />
                    Password:<label class="password" style="position:relative;left:30px;"> <asp:TextBox TextMode="Password" runat="server" ID="pass" CssClass="inputs"></asp:TextBox></label><br />
                    <asp:Button ID="loginUser" class="submit button" runat="server" Text="Sign in" OnClick="loginUser_Click"></asp:Button>
		        </div>
            </div>
            <asp:TextBox ID="tbDate" runat="server" AutoPostBack="true" />
            <asp:DropDownList ID="ddlLocation" runat="server" DataTextField="Location" DataValueField="Location" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                <asp:ListItem Value="Office Muncie">Muncie</asp:ListItem>
                <asp:ListItem Value="Office Anderson">Anderson</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="ddlRoom" runat="server" DataTextField="room_name" DataValueField="room_number" AutoPostBack="true"></asp:DropDownList>
            <asp:Button ID="Print" runat="server" OnClick="Print_Click" style="height: 26px" Text="Print Schedule" />
            <asp:GridView ID="GridView1" runat="server" AllowSorting="false"></asp:GridView>
            <asp:ListView ID="ListView1" runat="server" OnItemCommand="ListView1_ItemCommand">
                <LayoutTemplate>
                    <ul class="selectable surgery-holdings col-lg-8 col-lg-offset-2">
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                    </ul>
                </LayoutTemplate>
                <ItemTemplate>
                    <li>
                        <table style="table-layout: fixed">
                            <tr>
                                <td rowspan="2" style="width:200px;vertical-align:top"><%#Eval("Position") %></td>
                                <td rowspan="2" style="width:200px;vertical-align:top"><%#Eval("Duration") %></td>
                                <td style="width:200px;text-align:left"><%#Eval("Start Time") %></td>
                                <td style="width:1100px;text-align:left"><%#Eval("Patient") %> &nbsp&nbsp&nbsp <%#Eval("Birthdate") %></td>
                                <td style="width:50px"><%#Eval("Weight") %></td>
                                <td style="width:50px"><%#Eval("Room") %></td>
                                <td style="width:400px;text-align:left"><b>Anasthesia</b><br /><%#Eval("Anesthesia") %> </td>
                                <td style="width:50px"><asp:Button ID="btnModifyItem" runat="server" Text="Modify" CommandName="ModifyEvent" CommandArgument='<%#Eval("ID") %>' OnCommand="ListView1_ItemCommand" /></td>
                            </tr>
                            <tr>
                                <td style="width:200px;text-align:left"><%#Eval("End Time") %></td>
                                <td style="width:1200px;text-align:left"><%#Eval("Provider") %></td>
                                <td style="width:50px"><%#Eval("Age") %></td>
                                <td style="width:50px"><%#Eval("Gender") %></td>
                                <td style="width:400px"><b>Surgery Equipment</b><br /><%#Eval("Equipment") %></td>
                                <td style="width:50px"><asp:Button ID="btnDeleteItem" runat="server" Text="Delete" CommandName="DeleteEvent" CommandArgument='<%#Eval("ID") %>' OnCommand="ListView1_ItemCommand" /></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width:400px"><%#Eval("MedRec#") %></td>
                                <td colspan="4" style="width:1800px;text-align:left"><%#Eval("Surgery Details") %></td>
                                <td style="width:400px"><b>Plates/Implants</b><br /><%#Eval("Plates") %></td>
                            </tr>
                        </table>
                    </li>
                </ItemTemplate>
            </asp:ListView>
            <asp:Table ID="info" runat="server"></asp:Table>
        </form>
    </body>
</html>