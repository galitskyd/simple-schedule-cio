<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPage.aspx.cs" Inherits="PrintPage" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <title>Surgery Print Page</title>
        <link rel="stylesheet" type="text/css" href="Content/bootstrap.css" />
        <link rel="stylesheet" type="text/css" href="Content/jquery-ui-1.10.4.custom.min.css" />
        <link rel="stylesheet" type="text/css" href="Content/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
            <asp:ListView ID="ListViewPrime" runat="server" OnItemDataBound="ListViewPrime_ItemDataBound">
                <LayoutTemplate>
                    <ul>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                    </ul>
                </LayoutTemplate>
                <ItemTemplate>
                    <li>
                        <div><%#Eval("Key") %></div>
                        <asp:ListView ID="ListView1" runat="server">
                            <LayoutTemplate>
                                <section style="background-color: #F0F1F1; padding-bottom: 20px;">
                                    <ul class="col-lg-10 col-lg-offset-1" style="float: none; margin-bottom: 0;">
                                        <li class="appointment-listitem">
                                            <div class="row">
                                                <div class="col-lg-7 clearfix">
                                                    <div class="row">
                                                        <div id="appointment-time" class="col-lg-4"><b>Start Time - End Time</b></div>
                                                        <div id="appointment-patient" class="col-lg-4">Patient</div>
                                                        <div id="appointment-dob" class="col-lg-2">Birthdate</div>
                                                        <div id="appointment-weight" class="col-lg-1">Weight</div>
                                                        <div id="appointment-room" class="col-lg-1">Room</div>
                                                    </div>
                                                    <div class="row" style="margin-top: 5px;">
                                                        <div id="appointment-provider" class="col-lg-4">Provider</div>
                                                        <div id="appointment-mrn" class="col-lg-4">MedRec#</div>
                                                        <div class="col-lg-2"></div>
                                                        <div id="appointment-age" class="col-lg-1">Age</div>
                                                        <div id="appointment-gender" class="col-lg-1">Gender</div>
                                                    </div>
                                                    <div class="row" style="margin-top: 10px;">
                                                        <div id="appointment-surgery" class="col-lg-12"><b>Details:</b> Surgery Details</div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4"">
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <b>Anasthesia</b>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <b>Surgery Equipment</b>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <b>Plates/Implants</b>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </li>
                                    </ul>
                                    <ul class="print-holdings col-lg-10 col-lg-offset-1" style="float: none; margin-bottom: 0;">
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
                                    </div>
                                    <div>
                                        <hr />
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    <div><%= timestamp %></div>
                    </li>
                </ItemTemplate>
            </asp:ListView>
    </form>
</body>
</html>
