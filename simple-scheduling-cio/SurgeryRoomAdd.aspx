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
    <link rel="stylesheet" type="text/css" href="Content/bootstrapValidator.min.css"/>
    <script type="text/javascript" src="Scripts/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <script type="text/javascript" src="Scripts/bootstrapValidator.min.js"></script>

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
        })
    </script>
    <script type="text/javascript">
        function bootstrapValidation() {
            var timevar = 585 - "<%=this.intSumDuration %>";
            $('#form1').bootstrapValidator({
                feedbackIcons: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    tbDuration: {
                        validators: {
                            notEmpty: {
                                message: 'The duration is required'
                            },
                            greaterThan: {
                                value: 0,
                                inclusive: true,
                                message: 'The input must be greater than 0 minutes'
                            },
                            lessThan: {
                                value: timevar,
                                inclusive: false,
                                message: 'The input must be less than or equal to ' + timevar + ' minutes'
                            }
                        }
                    },
                    tbDate: {
                        validators: {
                            notEmpty: {
                                message: 'Date is required.'
                            },
                            regexp: {
                                regexp: /^[0-9]{4}\/(0[1-9]|1[0-2])\/([0-2][0-9]|3[0-1])/,
                                message: 'The date must be of form MM/DD/YYYY'
                            }
                        }
                    }
                }
            });
        }
        $(document).ready(function () {
            bootstrapValidation()
        });
    </script>
</head>
<body style="background-color: #E2E3E3">
    <form id="form1" role="form" runat="server"
        data-bv-message="This value is not valid">
        <header class="navbar navbar-default" role="navigation">
            <div class="container-fluid">
                <a class="navbar-brand" href="SurgeryRoom.aspx">SurgeryGenie</a>
                <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                    <ul class="nav navbar-nav navbar-right">
                        <li><a href="SurgeryRoom.aspx">Back to SurgeryGenie</a></li>
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
                                <asp:DropDownList TabIndex="1" ID="ddlLocation" CssClass="form-control" runat="server" DataTextField="Location" DataValueField="Location" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                                    <asp:ListItem Value="Office CIO Muncie">Muncie</asp:ListItem>
                                    <asp:ListItem Value="Office CIO Anderson">Anderson</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <label id="lblORRom">OR#</label>
                            <asp:DropDownList TabIndex="2" ID="ddlRoom" runat="server" CssClass="form-control" DataTextField="room_name" DataValueField="room_number" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label id="lblDate">Date (MM/DD/YYYY)</label>
                                <asp:TextBox TabIndex="3" ID="tbDate" CssClass="form-control" runat="server" PlaceHolder="Date (MM/DD/YYYY)" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label id="lblDuration">Duration (in minutes)</label>
                                <asp:TextBox TabIndex="4" ID="tbDuration" CssClass="form-control" runat="server" PlaceHolder="Duration"/>
                                <asp:CompareValidator Visible="false" ID="valCompareDurationMax" runat="server" ControlToValidate="tbDuration" Type="Integer" Operator="LessThanEqual" ErrorMessage="This room cannot be booked past 4:15." ValueToCompare="1"></asp:CompareValidator>
                                <asp:CompareValidator Visible="false" ID="valCompareDurationMin" runat="server" ControlToValidate="tbDuration" Type="Integer" Operator="GreaterThan" ErrorMessage="Please select a duration greater than zero (0) minutes." ValueToCompare="0"></asp:CompareValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label id="lblPatient">Medical Record Number</label>
                                <asp:TextBox TabIndex="5" ID="tbPatient" CssClass="form-control" runat="server" AutoPostBack="true" PlaceHolder="Medical Record Number" OnTextChanged="tbPatient_TextChanged"
                                    data-bv-notempty="true"
                                    data-bv-notempty-message="The medical record number is required"
                                    data-bv-stringlength="true"
                                    data-bv-stringlength-min="12"
                                    data-bv-stringlength-max="12"
                                    data-bv-stringlength-message="The medical record number must be 12 characters long"/>
                                <asp:Label ID="showPatient" runat="server"></asp:Label>
                            </div>
                        </div>

                        <asp:ListBox ID="lbPatient" Visible="false" runat="server" DataTextField="full_name" DataValueField="med_rec_nbr" AutoPostBack="true" TabIndex="-1"/>

                        <div class="col-lg-6">
                            <div class="form-group">
                                <label id="lblProvider">Provider</label>
                                <asp:ListBox TabIndex="6" ID="lbProvider" CssClass="form-control" runat="server" DataTextField="description" DataValueField="provider_id" AutoPostBack="true" SelectionMode="Multiple" Rows="5"/>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="checkbox">
                                <label id="lblLatex">Latex Allergy</label>
                                <asp:CheckBox TabIndex="7" ID="chkLatex" runat="server" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="checkbox">
                                <label id="lblDiabetic">Diabetic</label>
                                <asp:CheckBox TabIndex="8" ID="chkDiabetic" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="checkbox">
                                <label id="lblVanco">Vanco Preop</label>
                                <asp:CheckBox TabIndex="9" ID="chkVanco" runat="server" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="checkbox">
                                <label id="lblCoaguchek">Coaguchek</label>
                                <asp:CheckBox TabIndex="10" ID="chkCoaguchek" runat="server" />
                            </div>
                        </div>
                    <div class="form-group">
                        <label id="lblSurgery">Surgery Details</label>
                        <asp:TextBox TabIndex="11" ID="tbSurgery" TextMode="MultiLine" CssClass="form-control" Rows="5" runat="server" />
                    </div>
                    <div class="form-group">
                        <label id="lblAnesthesia">Anesthesia</label>
                        <asp:ListBox TabIndex="12" ID="lbAnesthesia" CssClass="form-control" DataTextField="name" DataValueField="id" runat="server" SelectionMode="Multiple" Rows="6"/>
                    </div>
                    <div class="form-group">
                        <label id="lblEquipment">Equipment</label>
                        <asp:ListBox TabIndex="13" ID="lbEquipment" CssClass="form-control" DataTextField="name" DataValueField="id" runat="server" SelectionMode="Multiple" Rows="6"/>
                    </div>
                    <div class="form-group">
                        <label id="lblPlatesImplants">Plates/Implants</label>
                        <asp:ListBox TabIndex="14" ID="lbPlatesImplants" CssClass="form-control" DataTextField="name" DataValueField="id" runat="server" SelectionMode="Multiple" Rows="6"/>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-lg-4 col-lg-offset-4">
                                <asp:Button TabIndex="15" ID="btnAdd" CssClass="btn btn-lg btn-primary submit-add-surgery" runat="server" Text="Add Event" OnClick="btnAdd_Click" UseSubmitBehavior="false"/>

                                <asp:Button TabIndex="16" ID="btnDeleteItem" CssClass="btn btn-lg btn-primary submit-add-surgery" runat="server" Text="Delete Event" CommandName="DeleteEvent" OnCommand="btnDeleteItem_Click" UseSubmitBehavior="false" Enabled="false" Visible="false"/>

                                <asp:Button TabIndex="17" ID="btnBlock" CssClass="btn btn-lg btn-primary submit-add-surgery" runat="server" Text="Block Time" OnClick="btnBlock_Click" UseSubmitBehavior="false"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
