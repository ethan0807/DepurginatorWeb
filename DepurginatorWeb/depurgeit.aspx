<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="depurgeit.aspx.cs" Inherits="DepurginatorWeb.depurgeit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 311px">
    <form id="form1" runat="server">
        <asp:Label ID="InFile" runat="server" Text="Numbers File"></asp:Label>
        :<p>
            <asp:FileUpload ID="NumbersIn" runat="server" style="margin-right: 369px" />
        </p>
        <p>
            &nbsp;</p>
        <asp:Button ID="DepurginateIt" runat="server" OnClick="DepurginateIt_Click" Text="Depurginate" Width="278px" />
        <br />
        <br />
        <asp:Panel ID="LinkPanel" runat="server" Height="106px" Visible="False">
            <asp:HyperLink ID="NumbersFile" runat="server" NavigateUrl="~/outfile/depurginated_numbers.txt">DePurge Numbers</asp:HyperLink>
            <br />
            <br />
            <asp:HyperLink ID="NumbersFileRev" runat="server" NavigateUrl="~/outfile/depurginated_numbers_reverse.txt">DePurge Numbers Reversed</asp:HyperLink>
        </asp:Panel>
    </form>
</body>
</html>
