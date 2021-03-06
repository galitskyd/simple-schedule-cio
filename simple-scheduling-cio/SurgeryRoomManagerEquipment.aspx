﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SurgeryRoomManagerEquipment.aspx.cs" Inherits="SurgeryRoomManagerEquipment" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>SurgeryGenie - Equipment Manager</title>
        <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
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
    </head>
    <body style="background-color: #F0F1F1">
        <header class="navbar navbar-default" role="navigation">
            <div class="container-fluid">
                <a class="navbar-brand">Genie</a>
                <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                    <ul class="nav navbar-nav navbar-right">
                        <li><a href="SurgeryRoom.aspx">Back to Genie</a></li>
                    </ul>
                </div>
            </div>
        </header>
        <form id="form1" runat="server">
            <div class="container-fluid">
                <div class="row management-title">
                    <div class="col-lg-8 col-lg-offset-2 col-md-10 col-md-offset-1 col-sm-12">
                        <h1>Equipment Manager</h1>
                    </div>
                </div>
                <div class="row management-content">
                    <div class="col-lg-8 col-lg-offset-2 col-md-10 col-md-offset-1 col-sm-12">
                        <div class="row">
                            <div class="col-lg-6 col-sm-6">
                                <div class="form-group">
                                    <label id="lblEnabled">Enabled</label>
                                    <asp:ListBox ID="lbEnabled" runat="server" CssClass="form-control" ToolTip="Enabled" SelectionMode="Multiple" Height="400px"></asp:ListBox>
                                </div>
                            </div>
                            <div class="col-lg-6 col-sm-6">
                                <div class="form-group">
                                    <label id="lblDisabled">Disabled</label>
                                    <asp:ListBox ID="lbDisabled" runat="server" CssClass="form-control" ToolTip="Disabled" SelectionMode="Multiple" Height="400px"></asp:ListBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row management-content" style="padding-top: 0;">
                    <div class="col-lg-8 col-lg-offset-2 col-md-10 col-md-offset-1 col-sm-12">
                        <div class="row">
                            <div class="col-lg-4 col-sm-4">
                                <a id="btnAdd" class="btn btn-primary btn-lg btn-full" runat="server" onclick="$('#add-equipment').modal();">Add Equipment</a>
                            </div>
                            <div class="col-lg-4 col-sm-4">
                                <asp:Button ID="btnToggle" CssClass="btn btn-primary btn-lg btn-full" runat="server" Text="Toggle Equipment" OnClick="btnToggle_Click" />
                            </div>
                            <div class="col-lg-4 col-sm-4">
                                <asp:Button ID="btnRemove" CssClass="btn btn-danger btn-lg btn-full" runat="server" Text="Delete Equipment" OnClick="btnDelete_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="add-equipment" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="form-group">
                                <label>Equipment Name</label>
                                <asp:TextBox  runat="server" ID="txtEquipment" CssClass="form-control" PlaceHolder="Equipment Name"></asp:TextBox>
                                <p class="help-block">Click "Add Item" to add item to Enabled list.</p>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="pull-right clearfix">
                                <asp:Button ID="btnAddItem" CssClass="btn btn-primary" runat="server" Text="Add Item" OnClick="btnAddItem_Click"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </body>
</html>
