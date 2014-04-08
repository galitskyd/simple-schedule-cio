<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SurgeryRoomManagerAnesthesia.aspx.cs" Inherits="SurgeryRoomManagerAnesthesia" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Anesthesia Manager</title>
        <link rel="stylesheet" type="text/css" href="Content/StyleSheet.css" />
        <script type="text/javascript" src="Scripts/jquery-1.10.2.js"></script>
        <script type="text/javascript">
            function addPopup() {
                // Getting the variable's value from a link 
                var addBox = $(this).attr('href');
                $(".login-popup").fadeIn(300, function () {
                    $('.login-popup').css("display", "block");
                });
                //Set the center alignment padding + border
                var popMargTop = ($(addBox).height() + 24) / 2;
                var popMargLeft = ($(addBox).width() + 24) / 2;
                $(addBox).css({
                    'margin-top': -popMargTop,
                    'margin-left': -popMargLeft
                });
                // Add the mask to body
                $('body').append('<div id="mask"></div>');
                $('#mask').fadeIn(300);
                $('#txtAnesthesia').focus();
            };
            $(document).ready(function () {
                $('a.close').click(function () {
                    $(".login-popup").fadeOut(300, function () {
                        $('.login-popup').css("display", "none");
                    });
                    $('#mask').fadeOut(300, function () {
                        $('#mask').remove();
                    });
                });
            });
        </script>
    </head>
    <body>
        <form id="form1" runat="server">
        <div>
            <div style="display: table-cell;">
                <asp:Label ID="lblEnabled" runat="server" Text="Enabled"/>
                <br />
                <asp:ListBox ID="lbEnabled" runat="server" ToolTip="Enabled" Height="400px"></asp:ListBox>
            </div>
            <div style="display: table-cell;">
                <asp:Label ID="lblDisabled" runat="server" Text="Disabled"/>
                <br />
                <asp:ListBox ID="lbDisabled" runat="server" ToolTip="Disabled" Height="400px"></asp:ListBox>
            </div>
            <br />
            <div id="login-box" class="login-popup">
                    <a href="#" class="close"><img src="close_pop.png" class="btn_close" title="Close Window" alt="Close" /></a>
            	    Anesthesia:<label class="lblAnesthesia" style="position:relative;left:30px;"> <asp:TextBox  runat="server" ID="txtAnesthesia" CssClass="inputs"></asp:TextBox></label><br />
                    <asp:Button ID="btnAddItem" class="submit button" runat="server" Text="Add Item" OnClick="btnAddItem_Click"></asp:Button>
		    </div>
            <asp:Button ID="btnAdd" CssClass="add-window" runat="server" Text="Add Anesthesia" OnClientClick="addPopup();return false;" />
            <asp:Button ID="btnToggle" runat="server" Text="Toggle Anesthesia" OnClick="btnToggle_Click" />
            <asp:Button ID="btnRemove" runat="server" Text="Delete Anesthesia" OnClick="btnDelete_Click" />
        </div>
        </form>
    </body>
</html>
