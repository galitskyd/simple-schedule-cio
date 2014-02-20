<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addInfo.aspx.cs" Inherits="addInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Number of People to add:
        <asp:TextBox ID="TextBox1" runat="server" Height="30px" Width="35px"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Height="36px" OnClick="Button1_Click" Text="Add To Database" Width="115px" />
        <br />
        <asp:Label ID="Label1" runat="server" ForeColor="#CC0000"></asp:Label>
    
    </div>
    </form>
</body>
</html>
