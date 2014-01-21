<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SurgeryRoom.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="Content/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
        <!--<asp:LoginView ID="LoginView1" runat="server">
            <LoggedInTemplate>
                <asp:LoginName ID="LoginName1" runat="server" FormatString="Welcome, {0}" />
                <br />
            </LoggedInTemplate>
            <AnonymousTemplate>
                <asp:Login BackColor="#9999FF" ID="login1" runat="server" UserNameLabelText="Username:" OnAuthenticate="login1_Authenticate"/>
            </AnonymousTemplate>
        </asp:LoginView>-->
        <div id="mainPageContainer">
            <h1>Central Indiana Orthepedics</h1>
            <h3>Surgery Viewer</h3>
            <asp:TextBox ID="tbTime" runat="server" AutoPostBack="true" />
            <!--<asp:Button ID="btnAdd" runat="server" Text="Add Event" OnClientClick="$('#divAddEvent').showModal(); return false;"/>-->
            <br />
            <asp:TextBox ID="tbDate" runat="server" AutoPostBack="true" />
            <asp:DropDownList ID="ddlLocation" runat="server" DataTextField="Location" DataValueField="Location" AutoPostBack="true">
                <asp:ListItem Value="Office Muncie">Muncie</asp:ListItem>
                <asp:ListItem Value="Office Anderson">Anderson</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="ddlRoom" runat="server" DataTextField="Room" DataValueField="Room" AutoPostBack="true">
                <asp:ListItem Value="1">OR1</asp:ListItem>
                <asp:ListItem Value="2">OR2</asp:ListItem>
                <asp:ListItem Value="3">OR3</asp:ListItem>
            </asp:DropDownList>
            <asp:GridView ID="GridView1" runat="server" AllowSorting="false"></asp:GridView>
            <asp:Table ID="info" runat="server"></asp:Table>
        </div>
    </form>
</body>
</html>
