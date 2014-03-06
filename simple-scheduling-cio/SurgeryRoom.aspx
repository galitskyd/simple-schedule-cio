<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SurgeryRoom.aspx.cs" Inherits="_Default" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Surgery Room Schedule</title>
        <link rel="stylesheet" type="text/css" href="Content/StyleSheet.css" />
        <script type="text/javascript" src="Scripts/jquery-1.10.2.js"></script>
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
            });
        </script>
    </head>
    <body>
        <form id="loginform" runat="server">
            <asp:GridView ID="test" runat="server"></asp:GridView>
            <a href="#login-box" class="login-window" runat="server" id="signIN">Sign In</a>
            <asp:LinkButton ID="signOUT" runat="server" OnClick="signOUT_Click" Text="Sign Out"></asp:LinkButton>
            <div id="mainPageContainer">
                <h1>Central Indiana Orthepedics</h1>
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
            <asp:DropDownList ID="ddlLocation" runat="server" DataTextField="Location" DataValueField="Location" AutoPostBack="true">
                <asp:ListItem Value="Office Muncie">Muncie</asp:ListItem>
                <asp:ListItem Value="Office Anderson">Anderson</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="ddlRoom" runat="server" DataTextField="Room" DataValueField="Room" AutoPostBack="true">
                <asp:ListItem Value="1">OR1</asp:ListItem>
                <asp:ListItem Value="2">OR2</asp:ListItem>
                <asp:ListItem Value="3">Minor Procedure Room</asp:ListItem>
            </asp:DropDownList>
            <asp:GridView ID="GridView1" runat="server" AllowSorting="false"></asp:GridView>
            <asp:ListView ID="ListView1" runat="server">
                <LayoutTemplate>
                    <ul>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                    </ul>
                </LayoutTemplate>
                <ItemTemplate>
                    <li>
                        <hr />
                        <table style="table-layout: fixed">
                            <tr>
                                <td rowspan="2" valign="top" style="width:200px"><%#Eval("Position") %></td>
                                <td rowspan="2" valign="top" style="width:200px"><%#Eval("Duration") %></td>
                                <td align="left" style="width:200px"><%#Eval("Start Time") %></td>
                                <td align="left" style="width:1100px"><%#Eval("Patient") %></td>
                                <td style="width:50px">Wgt</td>
                                <td style="width:50px"><%#Eval("Room") %></td>
                                <td align="left" style="width:400px"><b>Anasthesia</b> </td>
                            </tr>
                            <tr>
                                <td align="left" style="width:200px"><%#Eval("End Time") %></td>
                                <td align="left" style="width:1200px"><%#Eval("Provider") %></td>
                                <td style="width:50px"><%#Eval("Age") %></td>
                                <td style="width:50px"><%#Eval("Gender") %></td>
                                <td style="width:400px"><b>Surgery Equipment</b></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width:400px"><%#Eval("MedRec#") %></td>
                                <td colspan="5" align="left" style="width:1800px"><%#Eval("Surgery") %>; <%#Eval("Details") %></td>
                            </tr>
                        </table>
                    </li>
                </ItemTemplate>
            </asp:ListView>
            <asp:Table ID="info" runat="server"></asp:Table>
        </form>
    </body>
</html>