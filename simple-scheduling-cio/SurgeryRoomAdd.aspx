<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SurgeryRoomAdd.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New Surgery Event</title>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
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
        })
    </script>
</head>
<body style="background-color: #E2E3E3">
    <form id="form1" role="form" runat="server">
        <header class="navbar navbar-default" role="navigation">
            <div class="container-fluid">
                <a class="navbar-brand">SurgeryGenie</a>
                <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                    <ul class="nav navbar-nav navbar-right">
                        <li><a href="javascript:history.back()">Back to SurgeryGenie</a></li>
                    </ul>
                </div>
            </div>
        </header>
        <div class="container-fluid">
            <div class="row" style="background-color: #D4E6E8">
                <div  class="col-lg-8 col-lg-offset-2">
                    <h1>New Surgery Event</h1>
                </div>
            </div>
            <div class="row" style="background-color: #F0F1F1; padding-top: 20px; padding-bottom: 20px;">
                <div class="col-lg-8 col-lg-offset-2">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label id="lblLocation">Location</label>
                                <asp:DropDownList TabIndex="1" ID="ddlLocation" CssClass="form-control" runat="server" DataTextField="Location" DataValueField="Location" AutoPostBack="true">
                                    <asp:ListItem Value="Office CIO Muncie">Muncie</asp:ListItem>
                                    <asp:ListItem Value="Office CIO Anderson">Anderson</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <label id="lblORRom">OR#</label>
                            <asp:DropDownList TabIndex="2" ID="ddlRoom" CssClass="form-control" runat="server" DataTextField="Location" DataValueField="Location" AutoPostBack="true">
                                <asp:ListItem Value="1">OR1</asp:ListItem>
                                <asp:ListItem Value="2">OR2</asp:ListItem>
                                <asp:ListItem Value="3">Minor Procedure Room</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label id="lblDate">Date (yyyy/MM/dd)</label>
                                <asp:TextBox TabIndex="3" ID="tbDate" CssClass="form-control" runat="server" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label id="lblDuration">Duration (in minutes)</label>
                                <asp:TextBox TabIndex="4" ID="tbDuration" CssClass="form-control" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label id="lblPatient">MedRec#</label>
                                <asp:TextBox TabIndex="5" ID="tbPatient" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="tbPatient_TextChanged"/>
                                <asp:Label ID="showPatient" runat="server"></asp:Label>
                            </div>
                        </div>

                        <asp:ListBox ID="lbPatient" Visible="false" runat="server" DataTextField="Patient" DataValueField="Patient" AutoPostBack="true" TabIndex="-1"/>

                        <div class="col-lg-6">
                            <div class="form-group">
                                <label id="lblProvider">Provider</label>
                                <asp:ListBox ID="lbProvider" CssClass="form-control" runat="server" DataTextField="Provider" DataValueField="Provider" AutoPostBack="true" SelectionMode="Multiple" Rows="5"/>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label id="lblSurgery">Surgery Details</label>
                        <asp:TextBox ID="tbSurgery" TextMode="MultiLine" CssClass="form-control" Rows="5" runat="server" />
                    </div>
                    <div class="form-group">
                        <label id="lblAnesthesia">Anesthesia</label>
                        <asp:ListBox ID="lbAnesthesia" CssClass="form-control" runat="server" SelectionMode="Multiple" Rows="6"/>
                    </div>
                    <div class="form-group">
                        <label id="lblEquipment">Equipment</label>
                        <asp:ListBox ID="lbEquipment" CssClass="form-control" runat="server" SelectionMode="Multiple" Rows="6"/>
                    </div>
                    <div class="form-group">
                        <label id="lblPlatesImplants">Plates/Implants</label>
                        <asp:ListBox ID="lbPlatesImplants" CssClass="form-control" runat="server" SelectionMode="Multiple" Rows="6"/>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-lg-4 col-lg-offset-4">
                                <asp:Button ID="btnAdd" CssClass="btn btn-lg btn-primary submit-add-surgery" runat="server" Text="Add Event" OnClick="btnAdd_Click"/>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
