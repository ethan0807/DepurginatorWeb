<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="depurgeit.aspx.cs" Inherits="DepurginatorWeb.depurgeit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 311px">
    <form id="form1" runat="server">
        <asp:Label ID="InFile" runat="server" Text="Numbers File - "></asp:Label>
        Select a .txt file that has the list of numbers (each on its own line) you want depurginated.<br />
        <p>
            <asp:FileUpload ID="NumbersIn" runat="server" style="margin-right: 612px" Width="594px" />
        </p>
        <p>
            <asp:Label ID="ErrorLabel" runat="server"></asp:Label>
        </p>
        <asp:Button ID="DepurginateIt" runat="server" OnClick="DepurginateIt_Click" Text="Depurginate" Width="278px" />
        <br />
        <br />
        <asp:Panel ID="LinkPanel" runat="server" Height="106px" Visible="False">
            <asp:HyperLink ID="NumbersFile" runat="server" NavigateUrl="<%# outFileLink %>">DePurge Numbers</asp:HyperLink>
            <br />
            <br />
            <asp:HyperLink ID="NumbersFileRev" runat="server" NavigateUrl="<%# outFileLinkReverse %>">DePurge Numbers Reversed</asp:HyperLink>
            <br />
            <br />
            Second column of results file shows order of operations: 1 and 2 = subtract 49, 3 = multiply by .2, 4 = subtract 35, 5 = subtract 38, 6 = divide by 6, 7 = divide by 13
        </asp:Panel>
    </form>
</body>
</html>
