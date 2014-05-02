<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPage.aspx.cs" Inherits="PrintPage" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <title>Printed: <%= timestamp %></title>
        <link rel="stylesheet" type="text/css" href="Content/bootstrap.css" />
        <link rel="stylesheet" type="text/css" href="Content/jquery-ui-1.10.4.custom.min.css" />
        <link rel="stylesheet" type="text/css" href="Content/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
            <asp:ListView ID="ListViewPrime" runat="server" OnItemDataBound="ListViewPrime_ItemDataBound">
                <LayoutTemplate>
                    <ul class="holder-print">
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                    </ul>
                </LayoutTemplate>
                <ItemTemplate>
                    <li class="pagebreak">
                        <div class="container-fluid list-header">
                            <div class="row clearfix">
                                <div class="col-lg-12 col-sx-12 surgery-heading">
                                    <span class="surgery-room">Room: <%#Eval("Key") %></span>
                                    <span class="surgery-date pull-right"><%=date %></span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 col-sx-12">
                                    <span class="small-text pull-left">Start time of 6:30:00 AM</span>
                                </div>
                            </div>
                        </div>
                        <asp:ListView ID="ListView1" runat="server">
                            <LayoutTemplate>
                                <section style="background-color: #F0F1F1; padding-bottom: 20px;">
                                    <ul class="appointment-holdings col-xs-12" style="float: none; margin-bottom: 0; padding-left: 0; padding-right:0;">
                                        <li class="appointment-listitem" style="border-top: 0; border-bottom: 0;">
                                            <div class="row">
                                                <div class="col-lg-7 col-xs-7 clearfix">
                                                    <div class="row">
                                                        <div id="appointment-time" class="col-lg-4 col-xs-4">Start Time - End Time</div>
                                                        <div id="appointment-patient" class="col-lg-4 col-xs-4">Patient</div>
                                                        <div id="appointment-dob" class="col-lg-3 col-xs-3">Birthdate</div>
                                                        <div id="appointment-weight" class="col-lg-1 col-xs-1">Weight</div>
                                                    </div>
                                                    <div class="row" style="margin-top: 5px;">
                                                        <div id="appointment-duration" class="col-lg-4 col-xs-4">Duration</div>
                                                        <div id="appointment-provider" class="col-lg-4 col-xs-4">Provider</div>
                                                        
                                                        <div class="col-lg-2 col-xs-2"></div>
                                                        <div id="appointment-age" class="col-lg-1 col-xs-1">Age</div>
                                                        <div id="appointment-gender" class="col-lg-1 col-xs-1">Gender</div>
                                                    </div>
                                                    <div class="row">
                                                        <div id="appointment-mrn" class="col-lg-4 col-xs-4">MedRec#</div>
                                                    </div>
                                                    <div class="row" style="margin-top: 10px;">
                                                        <div id="appointment-surgery" class="col-lg-12 col-xs-12"><b>Details:</b> Surgery Details</div>
                                                    </div>
                                                </div>
                                            </div>
                                        </li>
                                    </ul>
                                    <ul class="appointment-holdings col-xs-12" style="float: none; margin-bottom: 0; padding-left: 0; padding-right:0;">
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
                                        <%# checkDiabetes(Eval("Diabetic").ToString()) %><br />
                                        <%# checkVancomycin(Eval("Vanco").ToString()) %>
                                        <%# checkCoaguCheck(Eval("Coaguchek").ToString()) %>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    
                    </li>
                </ItemTemplate>
            </asp:ListView>
    </form>
</body>
</html>
