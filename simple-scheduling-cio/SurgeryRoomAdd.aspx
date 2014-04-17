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
                <asp:Label ID="lblLocation" Text="Location: " runat="server" ></asp:Label><br />
                <asp:DropDownList ID="ddlLocation" runat="server" DataTextField="Location" DataValueField="Location" AutoPostBack="true">
                    <asp:ListItem Value="Office Muncie">Muncie</asp:ListItem>
                    <asp:ListItem Value="Office Anderson">Anderson</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div style="float:left; width:50%">
                <asp:Label ID="lblORRoom" Text="OR#: " runat="server"></asp:Label><br />
                <asp:DropDownList ID="ddlRoom" runat="server" DataTextField="Location" DataValueField="Location" AutoPostBack="true">
                    <asp:ListItem Value="1">OR1</asp:ListItem>
                    <asp:ListItem Value="2">OR2</asp:ListItem>
                    <asp:ListItem Value="3">Minor Procedure Room</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div style="clear:both"></div><br />
            <div style="float:left; width:50%">
                <asp:Label ID="lblDate" Text="Date (yyy/MM/dd): " runat="server" ></asp:Label><br />
                <asp:TextBox ID="tbDate" runat="server" />
            </div>
            <div style="float:left; width:50%">
                <asp:Label ID="lblDuartion" Text="Duration (in minutes): " runat="server"></asp:Label><br />
                <asp:TextBox ID="tbDuration" runat="server" />
            </div>
            <div style="clear:both"></div><br />
            <div style="float:left; width:25%">
                <asp:Label ID="lblPatient" Text="MedRec#: " runat="server"></asp:Label><br />
                <asp:TextBox ID="tbPatient" runat="server" AutoPostBack="true" OnTextChanged="tbPatient_TextChanged"/>
            </div>
            <div style="float:left; width:25%">
                <br />
                <asp:ListBox ID="lbPatient" runat="server" DataTextField="Patient" DataValueField="Patient" AutoPostBack="true" TabIndex="-1"/>
            </div>
            <div style="float:left; width:50%">
                <asp:Label ID="lblProvider" Text="Provider: " runat="server"></asp:Label><br />
                <asp:ListBox ID="lbProvider" runat="server" DataTextField="Provider" DataValueField="Provider" AutoPostBack="true" SelectionMode="Multiple"/>
            </div>
            <div style="clear:both"></div><br />
            <div style="float:left; width:50%">
                <asp:Label ID="lblSurgery" Text="Surgery Details: " runat="server"></asp:Label><br />
                <asp:TextBox ID="tbSurgery" runat="server" />
            </div>
            <div style="clear:both"></div><br />
            <div style="float:left; width:33%">
                <asp:Label ID="lblAnesthesia" Text="Anesthesia: " runat="server"></asp:Label><br />
                <asp:ListBox ID="lbAnesthesia" runat="server" SelectionMode="Multiple"/>
            </div>
            <div style="float:left; width:33%">
                <asp:Label ID="lblEquipment" Text="Equipment: " runat="server"></asp:Label><br />
                <asp:ListBox ID="lbEquipment" runat="server" SelectionMode="Multiple"/>
            </div>
            <div style="float:left; width:33%">
                <asp:Label ID="lblPlatesImplants" Text="Plates/Implants: " runat="server"></asp:Label><br />
                <asp:ListBox ID="lbPlatesImplants" runat="server" SelectionMode="Multiple"/>
            </div>

            <div style="clear:both"></div><br />
            <asp:Button ID="btnAdd" runat="server" Text="Add Event" OnClick="btnAdd_Click"/>
        </div>
    </form>
</body>
</html>
