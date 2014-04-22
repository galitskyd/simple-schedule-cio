<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SurgeryRoom.aspx.cs" Inherits="_Default" EnableEventValidation="false" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <title>SurgeryGenie</title>
        <link rel="stylesheet" type="text/css" href="Content/bootstrap.css" />
        <link rel="stylesheet" type="text/css" href="Content/jquery-ui-1.10.4.custom.min.css" />
        <link rel="stylesheet" type="text/css" href="Content/StyleSheet.css" />
        <script type="text/javascript" src="Scripts/jquery-1.10.2.js"></script>
        <script type="text/javascript" src="Scripts/jquery-ui-1.10.4.custom.min.js"></script>
        <!--[if lt IE 9]>
        <script type="text/javascript" src="Scripts/html5shiv.js"></script>
        <script type="text/javascript" src="Scripts/respond.min.js"></script>
        <![endif]-->
        <script type="text/javascript">
            $(document).ready(function () {
                $('#tbDate').datepicker({
                    dateFormat: "yy/mm/dd",
                    showOtherMonths: true,
                    selectOtherMonths: true,
                    changeMonth: true,
                    changeYear: true
                });
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
    <body style="background-color: #E2E3E3">
        <form id="loginform" runat="server">
        <header class="navbar navbar-default" role="navigation">
            <div class="container-fluid">
                <a class="navbar-brand">SurgeryGenie</a>
                <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                    <ul class="nav navbar-nav navbar-right">
                        <li><a href="Default.aspx">Main Schedule View</a></li>
                        <li><a href="#login-box" class="login-window pull-right btn primary" runat="server" id="signIN">Sign In</a></li>
                    </ul>
                </div>
            </div>
        </header>
            <section style="background-color: #D4E6E8">
                <asp:HiddenField ID="beginVal" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="finalVal" runat="server" OnValueChanged="finalVal_TextChanged"></asp:HiddenField>
                <asp:GridView ID="test" runat="server"></asp:GridView>
                <asp:LinkButton ID="signOUT" runat="server" OnClick="signOUT_Click" Text="Sign Out"></asp:LinkButton>

                <div id="mainPageContainer" class="col-lg-10 col-lg-offset-1" style="float: none; padding: 15px;">
                    <asp:TextBox ID="tbTime" runat="server" AutoPostBack="true" Visible="false" />
                    <div id="login-box" class="login-popup">
                        <a href="#" class="close"><img src="close_pop.png" class="btn_close" title="Close Window" alt="Close" /></a>
            	        <span style="color: white;">Username:</span><label class="username" style="position:relative;left:30px;"> <asp:TextBox  runat="server" ID="user" CssClass="inputs"></asp:TextBox></label><br />
                        <span style="color: white;">Password:</span><label class="password" style="position:relative;left:30px;"> <asp:TextBox TextMode="Password" runat="server" ID="pass" CssClass="inputs"></asp:TextBox></label><br />
                        <asp:Button ID="loginUser" class="submit button" runat="server" Text="Sign in" OnClick="loginUser_Click"></asp:Button>
		            </div>
                    <asp:TextBox ID="tbDate" runat="server" AutoPostBack="true" CssClass="surgery-menu" />
                    <asp:DropDownList ID="ddlLocation" CssClass="surgery-menu smaller" runat="server" DataTextField="Location" DataValueField="Location" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                        <asp:ListItem Value="Office CIO Muncie">Muncie</asp:ListItem>
                        <asp:ListItem Value="Office CIO Anderson">Anderson</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlRoom" CssClass="surgery-menu smaller" runat="server" DataTextField="room_name" DataValueField="room_number" AutoPostBack="true"></asp:DropDownList>
                    <div class="pull-right">
                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="Add Event" OnClick="btnAdd_Click" />
                        <asp:Button ID="Print" runat="server" OnClick="Print_Click" CssClass="btn btn-primary" Text="Print Schedule" Enabled="false" />
                    </div>
                </div>
            </section>
                <asp:GridView ID="GridView1" runat="server" AllowSorting="false"></asp:GridView>
                <asp:ListView ID="ListView1" runat="server" OnItemCommand="ListView1_ItemCommand">
                <LayoutTemplate>
                    <section style="background-color: #F0F1F1; padding-bottom: 20px;">
                    <ul class="selectable surgery-holdings col-lg-10 col-lg-offset-1" style="float: none; margin-bottom: 0;">
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                    </ul>
                    </section>
                </LayoutTemplate>
                <ItemTemplate>
                    <li class="appointment-listitem">
                        <div class="row">
                        <div class="col-lg-7 clearfix">
                            <div class="row">
                                <div id="appointment-time" class="col-lg-4"><b><%#Eval("Start Time") %> - <%#Eval("End Time") %></b></div>
                                <div id="appointment-patient" class="col-lg-4"><%#Eval("Patient") %></div>
                                <div id="appointment-dob" class="col-lg-2"><%#Eval("Birthdate") %></div>
                                <div id="appointment-weight" class="col-lg-1"><%#Eval("Weight") %></div>
                                <div id="appointment-room" class="col-lg-1"><%#Eval("Room") %></div>
                            </div>
                            <div class="row" style="margin-top: 5px;">
                                <div id="appointment-provider" class="col-lg-4"><%#Eval("Provider") %></div>
                                <div id="appointment-mrn" class="col-lg-4"><%#Eval("MedRec#") %></div>
                                <div class="col-lg-2"></div>
                                <div id="appointment-age" class="col-lg-1"><%#Eval("Age") %></div>
                                <div id="appointment-gender" class="col-lg-1"><%#Eval("Gender") %></div>
                            </div>
                            <div class="row" style="margin-top: 10px;">
                                <div id="appointment-surgery" class="col-lg-12"><b>Details:</b> <%#Eval("Surgery Details") %></div>
                            </div>
                        </div>
                        <div class="col-lg-4"">
                            <div class="row">
                                <div class="col-lg-12">
                                    <b>Anasthesia</b>
                                    <br />
                                    <%#Eval("Anesthesia") %>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <b>Surgery Equipment</b>
                                    <br />
                                    <%#Eval("Equipment") %>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <b>Plates/Implants</b>
                                    <br />
                                    <%#Eval("Plates") %>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <div class="row">
                                <div class="col-lg-12">
                                    <a class="btn-link" href="#">Edit</a>
                                    <asp:Button ID="btnModifyItem" runat="server" Text="Modify" CommandName="ModifyEvent" CommandArgument='<%#Eval("ID") %>' OnCommand="ListView1_ItemCommand" />
                                    <asp:Button ID="btnDeleteItem" runat="server" Text="Delete" CommandName="DeleteEvent" CommandArgument='<%#Eval("ID") %>' OnCommand="ListView1_ItemCommand" />
                                </div>
                            </div>
                        </div>
                        </div>
                    </li>
                </ItemTemplate>
            </asp:ListView>
        </form>
    </body>
</html>