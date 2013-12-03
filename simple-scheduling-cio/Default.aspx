<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="Content/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="mainPageContainer">
        <h1>Central Indiana Orthepedics</h1>
        <h3>Doctor Viewer</h3>
        <asp:GridView ID="GridView1" runat="server" AllowSorting="true" OnSorting="GridView1_Sorting" ></asp:GridView>
        <asp:Table ID="info" runat="server"></asp:Table>
    </div>
    </form>
</body>
</html>
