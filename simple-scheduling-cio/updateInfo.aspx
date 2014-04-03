<LayoutTemplate>
    <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
</LayoutTemplate>

<ItemTemplate>
    <li>
        <table style="table-layout: fixed">
            <tr>
                <td rowspan="2" valign="top" style="width:200px"><%#Eval("Position") %></td>
                <td rowspan="2" valign="top" style="width:200px"><%#Eval("Duration") %></td>
                <td align="left" style="width:200px"><%#Eval("Start Time") %></td>
                <td align="left" style="width:1100px"><%#Eval("Patient") %></td>
                <td style="width:50px">Wgt</td>
                <td style="width:50px"><%#Eval("Room") %></td>
                <td align="left" style="width:400px"><b>Anasthesia</b> </td>
            </tr>
            <tr>
                <td align="left" style="width:200px"><%#Eval("End Time") %></td>
                <td align="left" style="width:1200px"><%#Eval("Provider") %></td>
                <td style="width:50px"><%#Eval("Age") %></td>
                <td style="width:50px"><%#Eval("Gender") %></td>
                <td style="width:400px"><b>Surgery Equipment</b></td>
            </tr>
            <tr>
                <td colspan="2" style="width:400px"><%#Eval("MedRec#") %></td>
                <td colspan="5" align="left" style="width:1800px"><%#Eval("Surgery") %>; <%#Eval("Details") %></td>
            </tr>
        </table>
    </li>
</ItemTemplate>