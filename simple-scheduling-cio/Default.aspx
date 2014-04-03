<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="html/sandboxed" href="Content/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="Content/StyleSheet.css" />
    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script src="Scripts/msv.js"></script>
</head>
<body>
    <div class="navbar navbar-default">
        <a class="navbar-brand">Central Indiana Orthopedics</a> 
        <p class="navbar-text">Main Schedule View</p>
        <a class="pull-right" href="SurgeryRoom.aspx">SurgeryGenie</a>
    </div>
    <form id="form1" runat="server">
        <div id="mainPageContainer">
            <asp:GridView ID="GridView1" CssClass="table table-striped" RowStyle-CssClass="table-row" runat="server" AllowSorting="true" OnSorting="GridView1_Sorting" ></asp:GridView>
        </div>
    <div id="expanded-filter" class="filter-bar filter-box">
        <div class="filter-tabs">
            <span class="active">Provider</span><span>Patient</span><span>Other Tab</span>
            <i id="filter-collapse" class="glyphicon glyphicon-arrow-down pull-right"></i>
        </div>
        <div class="filter-content">
            Last Name:
            &nbsp;<asp:TextBox ID="FilterSearchTermsDoctor" runat="server"></asp:TextBox>
            <asp:TextBox ID="FilterSearchTermsPatient" runat="server"></asp:TextBox>
            <asp:TextBox ID="FilterSearchTermsLocation" runat="server"></asp:TextBox>
            <asp:TextBox ID="FilterSearchTermsAppointment" runat="server"></asp:TextBox>
            <asp:TextBox ID="FilterSearchTermsDuration" runat="server"></asp:TextBox>
            <asp:TextBox ID="FilterSearchTermsDetail" runat="server"></asp:TextBox>
            <asp:TextBox ID="FilterSearchTermsStatus" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="txtSearch_KeyUp" Text="Filter" />
        </div>
    </div>
    </form>
    <div id="collapsed-filter" class="filter-bar no-show">
        <b style="padding:8px 10px; display:inline-block">Filters</b>
        <i id="filter-expand" class="glyphicon glyphicon-arrow-up pull-right"></i>
    </div>
</body>
</html>
