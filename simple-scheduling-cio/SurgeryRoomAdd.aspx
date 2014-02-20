<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SurgeryRoomAdd.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New Surgery Event</title>
    <link rel="stylesheet" type="text/css" href="Content/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="mainPageContainer">
            <h1>Central Indiana Orthepedics</h1>
            <h3>New Surgery Event</h3>
            <div style="float:left; width:50%">
                <asp:Label ID="lbLocation" Text="Location: " runat="server" ></asp:Label>
                <asp:DropDownList ID="ddlLocation" runat="server" DataTextField="Location" DataValueField="Location" AutoPostBack="true">
                    <asp:ListItem Value="Office Muncie">Muncie</asp:ListItem>
                    <asp:ListItem Value="Office Anderson">Anderson</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div style="float:left; width:50%">
                <asp:Label ID="lbORRoom" Text="OR#: " runat="server"></asp:Label>
                <asp:DropDownList ID="ddlRoom" runat="server" DataTextField="Location" DataValueField="Location" AutoPostBack="true">
                    <asp:ListItem Value="1">OR1</asp:ListItem>
                    <asp:ListItem Value="2">OR2</asp:ListItem>
                    <asp:ListItem Value="3">Minor Procedure Room</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div style="clear:both"></div><br />
            <div style="float:left; width:50%">
                <asp:Label ID="lbDate" Text="Date (yyy/MM/dd): " runat="server" ></asp:Label>
                <asp:TextBox ID="tbDate" runat="server" />
            </div>
            <div style="float:left; width:50%">
                <asp:Label ID="lbDuartion" Text="Duration (in minutes): " runat="server"></asp:Label>
                <asp:TextBox ID="tbDuration" runat="server" />
            </div>
            <div style="clear:both"></div><br />
            <div style="float:left; width:50%">
                <asp:Label ID="lbPatient" Text="MedRec#: " runat="server"></asp:Label>
                <!--<asp:DropDownList ID="ddlPatient" runat="server" DataTextField="Location" DataValueField="Location" AutoPostBack="true"/>-->
                <asp:TextBox ID="tbPatient" runat="server" />
            </div>
            <div style="float:left; width:50%">
                <asp:Label ID="lbProvider" Text="Provider: " runat="server"></asp:Label>
                <asp:DropDownList ID="ddlProvider" runat="server" DataTextField="Provider" DataValueField="Provider" AutoPostBack="true"/>
            </div>
            <div style="clear:both"></div><br />
            <div style="float:left; width:50%">
                <asp:Label ID="lbSurgery" Text="Surgery: " runat="server"></asp:Label>
                <asp:TextBox ID="tbSurgery" runat="server" />
            </div>
            <div style="float:left; width:50%">
                <asp:Label ID="lbDetails" Text="Details: " runat="server"></asp:Label>
                <asp:TextBox ID="tbDetails" runat="server" />
            </div>
            <div style="clear:both"></div><br />
            <asp:Button ID="btnAdd" runat="server" Text="Add Event" OnClick="btnAdd_Click"/>
        </div>
    </form>
</body>
</html>
