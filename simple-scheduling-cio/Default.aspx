﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="Content/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="Content/StyleSheet.css" />
    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script src="Scripts/msv.js"></script>
    <!--[if lt IE 9]>
    <script type="text/javascript" src="Scripts/html5shiv.js"></script>
    <script type="text/javascript" src="Scripts/respond.min.js"></script>
    <![endif]-->

</head>
<body>
        <script type="text/javascript">

            $(document).ready(function () {
                $("#<%=startDateTxtBx.ClientID %>").keypress(function (e) {
                if (e.keyCode == 13) {
                    e.preventDefault();
                    $("#<%=apptDateBTN.ClientID%>").click();
                }
                });
                $("#<%=EndDate.ClientID %>").keypress(function (e) {
                    if (e.keyCode == 13) {
                        e.preventDefault();
                        $("#<%=apptDateBTN.ClientID%>").click();
                }
                });
                $("#<%=providerName.ClientID %>").keypress(function (e) {
                    if (e.keyCode == 13) {
                        e.preventDefault();
                        $("#<%=providerNameBTN.ClientID%>").click();
                    }
                });
                $("#<%=patientName.ClientID %>").keypress(function (e) {
                    if (e.keyCode == 13) {
                        e.preventDefault();
                        $("#<%=patientNameBTN.ClientID%>").click();
                    }
                });
                $("#<%=locationName.ClientID %>").keypress(function (e) {
                    if (e.keyCode == 13) {
                        e.preventDefault();
                        $("#<%=locationNameBTN.ClientID%>").click();
                    }
                });

                $("#<%=details.ClientID %>").keypress(function (e) {
                    if (e.keyCode == 13) {
                        e.preventDefault();
                        $("#<%=detailsBTN.ClientID%>").click();
                    }
                });
                $("#<%=status.ClientID %>").keypress(function (e) {
                    if (e.keyCode == 13) {
                        e.preventDefault();
                        $("#<%=statusBTN.ClientID%>").click();
                    }
                });
                $("#<%=birthDateName.ClientID %>").keypress(function (e) {
                    if (e.keyCode == 13) {
                        e.preventDefault();
                        $("#<%=birthDateBTN.ClientID%>").click();
                    }
                });
                $("#<%=medRecNum.ClientID %>").keypress(function (e) {
                    if (e.keyCode == 13) {
                        e.preventDefault();
                        $("#<%=medRecNumBTN.ClientID%>").click();
                    }
                });
               

        });
    </script>
    <form id="form1" runat="server">
        <header class="navbar navbar-default" role="navigation">
            <a class="navbar-brand">Central Indiana Orthopedics</a> 
            <p class="navbar-text">Main Schedule View</p>
            <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                <ul class="nav navbar-nav navbar-right">
                    <li><asp:LinkButton ID="clearAll" runat="server" OnClick="clearAll_Click" Text="Clear All Filters" TabIndex="-1"></asp:LinkButton></li>
                    <li><a href="SurgeryRoom.aspx">Genie</a></li>
                </ul>
            </div>
        </header>
        <div id="mainPageContainer">
            <asp:GridView ID="GridView1" CssClass="table table-striped" RowStyle-CssClass="table-row" runat="server" AllowSorting="true" OnSorting="GridView1_Sorting" ></asp:GridView>
        </div>
            <div id="expanded-filter" class="filter-bar filter-box">
                <i id="filter-collapse" class="glyphicon glyphicon-arrow-down pull-right"></i> 
                <div id="clearContainer">
                    
                </div>
        <div class="filter-tabs">
        <div class="tabs">
        <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server" />
            <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" style="visibility:visible">
                <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Provider">
                    <ContentTemplate>
                        <br /><br />
                        Provider Name:
                         <asp:TextBox ID="providerName" runat="server" placeholder="Provider Name" ToolTip="Type In Doctor's Names To Be Filtered By"></asp:TextBox>
                        <asp:Button Text="Filter" runat="server" ID="providerNameBTN" OnClick="providerNameBTN_Click" ToolTip="Click To Filter The View" />
                        <asp:Button Text="Clear" runat="server" ID="providerNameClearBTN" OnClick="providerNameClearBTN_Click" ToolTip="Click To Clear Provider Filters" />
                        <br />
                        <asp:Label ID="errorProvider" runat="server" Text=""></asp:Label>
                        <br />
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>

  
                <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="Patient">
                    <ContentTemplate>
                        <br /><br />
                        Patient Name:
                         <asp:TextBox ID="patientName" runat="server" placeholder="Patient Name"></asp:TextBox>
                        <asp:Button Text="Filter" runat="server" ID="patientNameBTN" OnClick="patientNameBTN_Click" />
                        <asp:Button Text="Clear" runat="server" ID="patientNameClearBTN" OnClick="patientNameClearBTN_Click" ToolTip="Click To Clear Patient Filters" />
                        <br />
                        <asp:Label ID="errorPatient" runat="server" Text=""></asp:Label>
                        <br />
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>

                <ajaxToolkit:TabPanel ID="TabPanel8" runat="server" HeaderText="Birth Date">
                    <ContentTemplate>
                        <br /><br />
                        Birth Date:
                         <asp:TextBox ID="birthDateName" runat="server" placeholder="Birth Date"></asp:TextBox>
                        <asp:Button Text="Filter" runat="server" ID="birthDateBTN" OnClick="birthDateBTN_Click" />
                        <asp:Button Text="Clear" runat="server" ID="birthDateClearBTN" OnClick="birthDateClearBTN_Click" ToolTip="Click To Clear Birth Date Filters" />
                        <br />
                        <asp:Label ID="errorBirthDate" runat="server" Text=""></asp:Label>
                        <br />
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>

                <ajaxToolkit:TabPanel ID="TabPanel9" runat="server" HeaderText="Med Rec Number">
                    <ContentTemplate>
                        <br /><br />
                        Med Rec Number:
                         <asp:TextBox ID="medRecNum" runat="server" placeholder="Med Rec Number"></asp:TextBox>
                        <asp:Button Text="Filter" runat="server" ID="medRecNumBTN" OnClick="medRecNumBTN_Click" />
                        <asp:Button Text="Clear" runat="server" ID="medRecNumClearBTN" OnClick="medRecNumClearBTN_Click" ToolTip="Click To Clear Med Rec Number Filters" />
                        <br />
                        <asp:Label ID="errorMedRecNum" runat="server" Text=""></asp:Label>
                        <br />
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
               
                <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="Location">
                    <ContentTemplate>
                        <br /><br />
                        Location:
                         <asp:TextBox ID="locationName" runat="server" placeholder="Location"></asp:TextBox>
                        <asp:Button Text="Filter" runat="server" ID="locationNameBTN" OnClick="locationNameBTN_Click"/>
                        <asp:Button Text="Clear" runat="server" ID="locationNameClearBTN" OnClick="locationNameClearBTN_Click" ToolTip="Click To Clear Location Filters" />
                        <br />
                        <asp:Label ID="errorLocation" runat="server" Text=""></asp:Label>
                        <br />
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>

                <ajaxToolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="Appointment Date">
                    <ContentTemplate>
                      
                        Appointment Date:<asp:Button Text="Filter" runat="server" ID="apptDateBTN" OnClick="apptDateBTN_Click" />
                        <asp:Button Text="Reset" runat="server" ID="apptDateClearBTN" OnClick="apptDateClearBTN_Click" />
                        <br /><br />
                        Start Date: <asp:TextBox ID="startDateTxtBx" runat="server" placeholder="Start Date" Width="100px" OnTextChanged="startDateTxtBx_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <div id="endDateContainer"> End Date: &nbsp;&nbsp;<asp:TextBox ID="EndDate" runat="server" placeholder="End Date" Width="100px" OnTextChanged="EndDate_TextChanged" AutoPostBack="true"></asp:TextBox></div>
                        <div id="dateSubmitButtonContainer">
                        
                        </div>
                            <br />
                        <asp:Label ID="errorApptDate" runat="server" Text=""></asp:Label>
                        <br />
                        
                         <ajaxToolkit:CalendarExtender 
                                ID="startDatePicker" 
                                TargetControlID="startDateTxtBx" 
                                runat="server" 
                                Format="MM/dd/yyyy"
                                PopupPosition="Right"
                                />
                        <ajaxToolkit:CalendarExtender 
                                ID="CalendarExtender2" 
                                TargetControlID="endDate" 
                                runat="server" 
                                Format="MM/dd/yyyy"
                                PopupPosition="Right"
                                />

                        <style type="text/css">
                            .ajax__calendar_container{
                                position:relative;
                                top:-100px;
                            }
                        #dateSubmitButtonContainer{
                           
                        }
                        #endDateContainer{
                        }
                        </style>
                       
                            
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>

                <ajaxToolkit:TabPanel ID="TabPanel6" runat="server" HeaderText="Details">
                    <ContentTemplate>
                        <br /><br />
                        Details:
                         <asp:TextBox ID="details" runat="server" placeholder="Details"></asp:TextBox>
                        <asp:Button Text="Filter" runat="server" ID="detailsBTN" OnClick="detailsBTN_Click" />
                        <asp:Button Text="Clear" runat="server" ID="detailsClearBTN" OnClick="detailsClearBTN_Click" ToolTip="Click To Clear Details Filters" />
                        <br />
                        <asp:Label ID="errorDetails" runat="server" Text=""></asp:Label>
                        <br />
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>

                <ajaxToolkit:TabPanel ID="TabPanel7" runat="server" HeaderText="Status">
                    <ContentTemplate>
                        <br /><br />
                        Status:
                         <asp:TextBox ID="status" runat="server" placeholder="Status"></asp:TextBox>
                        <asp:Button Text="Filter" runat="server"  ID ="statusBTN" OnClick="statusBTN_Click"/>
                        <asp:Button Text="Clear" runat="server" ID="statusClearBTN" OnClick="statusClearBTN_Click" ToolTip="Click To Clear Status Filters" />
                        <br />
                        <asp:Label ID="errorStatus" runat="server" Text=""></asp:Label>
                        <br />
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>

            </ajaxToolkit:TabContainer>
        </div> 
            </div></div>
    </form>
    <div id="collapsed-filter" class="filter-bar no-show">
        <b style="padding:8px 10px; display:inline-block">Filters</b>
        <i id="filter-expand" class="glyphicon glyphicon-arrow-up pull-right"></i>
    </div>
</body>
</html>
