<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CurdWebFrom.aspx.cs" Inherits="CurdWithCrud.CurdWebFrom" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label for="txtUserName">User Name:</asp:Label>
            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>  <br/>

            <asp:Label for="txtPhoneNumber">Phone Number:</asp:Label>
            <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>  <br/>

            <asp:Label for="ddlCountry">Country:</asp:Label>
            <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList> <br/>


            <asp:Label for="ddlCity">City:</asp:Label>
            <asp:DropDownList ID="ddlCity" runat="server"></asp:DropDownList> <br/>

           <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />

        </div>
    </form>
</body>
</html>
