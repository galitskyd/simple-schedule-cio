<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="Content/StyleSheet.css" />
    <link rel="stylesheet" type="html/sandboxed" href="Content/bootstrap.min.css" />
</head>
<body>
    <div class="row">
        <div class="col-lg-12">
            <form id="form1" runat="server">
                <div id="mainPageContainer">
                    <div class="header-bar">
                        <span class="header-title">Central Indiana Orthepedics</span> 
                        <span class="header-subtitle">Main Schedule View</span>
                    </div>
                    <asp:GridView ID="GridView1" runat="server" AllowSorting="true" OnSorting="GridView1_Sorting" ></asp:GridView>
                    <asp:Table ID="info" runat="server"></asp:Table>
                </div>
            </form>
        </div>
    </div>
</body>
</html>
