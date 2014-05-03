<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SurgeryRoom.aspx.cs" Inherits="_Default" EnableEventValidation="false" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <title>Genie</title>
        <link rel="stylesheet" type="text/css" href="Content/bootstrap.css" />
        <link rel="stylesheet" type="text/css" href="Content/jquery-ui-1.10.4.custom.min.css" />
        <link rel="stylesheet" type="text/css" href="Content/StyleSheet.css" />
        <script type="text/javascript" src="Scripts/jquery-1.10.2.js"></script>
        <script type="text/javascript" src="Scripts/jquery-ui-1.10.4.custom.min.js"></script>
        <script type="text/javascript" src="Scripts/bootstrap.js"></script>
        <!--[if lt IE 9]>
        <script type="text/javascript" src="Scripts/html5shiv.js"></script>
        <script type="text/javascript" src="Scripts/respond.min.js"></script>
        <![endif]-->
        <script type="text/javascript">
            $(document).ready(function () {
                $('#tbDate').datepicker({
                    dateFormat: "mm/dd/yy",
                    showOtherMonths: true,
                    selectOtherMonths: true,
                    changeMonth: true,
                    changeYear: true
                });
 
            });
        </script>
    </head>
    <body style="background-color: #E2E3E3">
        <form id="loginform" runat="server">
            <header class="navbar navbar-default" role="navigation">
                <div class="container-fluid">
                    <a class="navbar-brand">Genie</a>
                    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                        <ul class="nav navbar-nav navbar-right">
                            <li><a href="Default.aspx">Main Schedule View</a></li>
                            <li><a data-toggle="login-box" data-target="#login-modal" class="btn primary" runat="server" id="signIN" onclick="$('#login-modal').modal();">Sign In</a></li>
                            <li><asp:LinkButton ID="signOUT" runat="server" OnClick="signOUT_Click" Text="Sign Out"></asp:LinkButton></li>
                        </ul>
                    </div>
                </div>
            </header>
            <section style="background-color: #D4E6E8">
                <asp:HiddenField ID="beginVal" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="finalVal" runat="server" OnValueChanged="finalVal_TextChanged"></asp:HiddenField>
                <asp:GridView ID="test" runat="server"></asp:GridView>
                
                <div id="login-modal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-body">
                                <div class="form-group">
                                    <label>Username</label>
                                    <asp:TextBox  runat="server" ID="user" CssClass="form-control" PlaceHolder="Username"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Password</label>
                                    <asp:TextBox TextMode="Password" runat="server" ID="pass" CssClass="form-control" PlaceHolder="Password"></asp:TextBox>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="pull-right clearfix">
                                    <asp:Button ID="loginUser" class="btn btn-primary" runat="server" Text="Sign in" OnClick="loginUser_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="mainPageContainer" class="col-lg-10 col-lg-offset-1 col-md-10 col-md-offset-1 col-xs-12" style="float: none; padding: 15px;">
                    <div class="row">
                        <div class="col-lg-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="tbTime" runat="server" AutoPostBack="true" Visible="false" />
                            <asp:TextBox ID="tbDate" runat="server" AutoPostBack="true" CssClass="surgery-menu" />
                            <asp:DropDownList ID="ddlLocation" CssClass="surgery-menu smaller" runat="server" DataTextField="Location" DataValueField="Location" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                                <asp:ListItem Value="Office CIO Muncie">Muncie</asp:ListItem>
                                <asp:ListItem Value="Office CIO Anderson">Anderson</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlRoom" CssClass="surgery-menu smaller" runat="server" DataTextField="room_name" DataValueField="room_number" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-lg-6 col-sm-6 col-xs-12">
                            <div class="row" style="margin-top: 10px;">
                                <div class="col-lg-12 col-xs-12">
                                    <div class="pull-right">
                                        <asp:Button ID="aManager" runat="server" CssClass="btn" Text="Anesthesia Manager" OnClick="btnAnesthesia_Click" />
                                        <asp:Button ID="eManager" runat="server" CssClass="btn" Text="Equipment Manager" OnClick="btnEquipment_Click" />
                                        <asp:Button ID="pManager" runat="server" CssClass="btn" Text="Plates/Implants Manager" OnClick="btnPlatesAndImplants_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 10px;">
                                <div class="col-lg-12 col-xs-12">
                                    <div class="pull-right">
                                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="Add Event" OnClick="btnAdd_Click" />
                                        <asp:Button ID="Print" runat="server" OnClick="Print_Click" CssClass="btn btn-primary" Text="Print Schedule" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
                <asp:GridView ID="GridView1" runat="server" AllowSorting="false"></asp:GridView>
                <asp:ListView ID="ListView1" runat="server" OnItemCommand="ListView1_ItemCommand">
                    <LayoutTemplate>
                        <section style="background-color: #F0F1F1; padding-bottom: 20px;">
                            <ul class="selectable surgery-holdings col-lg-10 col-md-10 col-xs-12 col-lg-offset-1 col-md-offset-1" style="float: none; margin-bottom: 0;">
                                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                            </ul>
                        </section>
                    </LayoutTemplate>
                <ItemTemplate>
                    <li class="appointment-listitem">
                        <div class="row">
                            <div class="col-lg-7 col-xs-7 clearfix">
                                <div class="row">
                                    <div id="appointment-time" class="col-lg-4 col-xs-4"><b><%#Eval("Start Time") %> - <%#Eval("End Time") %></b></div>
                                    <div id="appointment-patient" class="col-lg-4 col-xs-4"><%#Eval("Patient") %></div>
                                    <div id="appointment-dob" class="col-lg-3 col-xs-3"><%#Eval("Birthdate") %></div>
                                    <div id="appointment-weight" class="col-lg-1 col-xs-1"><%#Eval("Weight") %></div>
                                </div>
                                <div class="row" style="margin-top: 5px;">
                                    <div id="appointment-duration" class="col-lg-4 col-xs-4"><%#Eval("Duration") %></div>
                                    <div id="appointment-provider" class="col-lg-6 col-xs-6"><%#Eval("Provider") %></div>
                                    <div id="appointment-age" class="col-lg-1 col-xs-1"><%#Eval("Age") %></div>
                                    <div id="appointment-gender" class="col-lg-1 col-xs-1"><%#Eval("Gender") %></div>
                                </div>
                                <div class="row" style="margin-top: 5px;">
                                    <div id="appointment-mrn" class="col-lg-4 col-xs-4"><%#Eval("MedRec#") %></div>
                                </div>
                                <div class="row" style="margin-top: 10px;">
                                    <div id="appointment-surgery" class="col-lg-12 col-xs-12"><b>Details:</b> <%#Eval("Surgery Details") %></div>
                                </div>
                            </div>
                            <div class="col-lg-4 col-xs-4">
                                <div class="row">
                                    <div class="col-lg-12 col-xs-12">
                                        <b>Anasthesia</b>
                                        <br />
                                        <%#Eval("Anesthesia") %>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12 col-xs-12">
                                        <b>Surgery Equipment</b>
                                        <br />
                                        <%#Eval("Equipment") %>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12 col-xs-12">
                                        <b>Plates/Implants</b>
                                        <br />
                                        <%#Eval("Plates") %>
                                    </div>
                                </div>
                            </div>
                            <%# checkLatex(Eval("Latex").ToString()) %>
                            <%# checkDiabetes(Eval("Diabetic").ToString()) %>
                            <%# checkVancomycin(Eval("Vanco").ToString()) %>
                            <%# checkCoaguCheck(Eval("Coaguchek").ToString()) %>
                            <div class="col-lg-1 col-xs-1">
                                <div class="row">
                                    <div class="col-lg-12 col-xs-12">
                                        <asp:Button ID="btnModifyItem" CssClass="btn-link" runat="server" Text="Modify" CommandName="ModifyEvent" CommandArgument='<%#Eval("ID") %>' OnCommand="ListView1_ItemCommand" Enabled="false" Visible="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </li>
                </ItemTemplate>
            </asp:ListView>
        </form>
        <footer id="genie-footer">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-8 col-lg-offset-2 col-xs-10 col-xs-offset-1">
                        <div class="row">
                            <div class="col-lg-3 col-xs-3">
                                <label>Latex Allergy</label>
                                <img src="Content/images/latex-icon-small.png" alt='Latex Allergy' width='25' />
                            </div>
                            <div class="col-lg-3 col-xs-3">
                                <label>Diabetic</label>
                                <img src="Content/images/diabetes-icon-small.png" alt='Diabetic' width='25' />
                            </div>
                            <div class="col-lg-3 col-xs-3">
                                <label>Vanco Preop</label>
                                <img src="Content/images/vancomycin-icon-small.png" alt='Vanco preop' width='25' />
                            </div>
                            <div class="col-lg-3 col-xs-3">
                                <label>CoaguCheck</label>
                                <img src="Content/images/coagucheck-small.png" alt='CoaguCheck' width='25' />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </footer>
    </body>
</html>