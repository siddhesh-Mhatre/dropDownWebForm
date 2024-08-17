<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GridViewOpearation.aspx.cs" Inherits="CurdWithCrud.GridViewOpearation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
              <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" /><br /><br />

              <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                AllowSorting="True" AllowPaging="True" PageSize="5"
                OnSorting="GridView1_Sorting" OnPageIndexChanging="GridView1_PageIndexChanging"
                OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" 
                OnRowDeleting="GridView1_RowDeleting" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                CssClass="my-grid" DataKeyNames="EmployeeID">


                
                <Columns>
                    <asp:BoundField DataField="EmployeeID" HeaderText="Employee ID" ReadOnly="True" SortExpression="EmployeeID" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" SortExpression="PhoneNumber" />
                    <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                    <asp:CommandField ShowEditButton="True" />
                    <asp:CommandField ShowDeleteButton="True" />
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>


               
                <RowStyle CssClass="grid-row" />
                <AlternatingRowStyle CssClass="grid-row-alternate" />
                <HeaderStyle CssClass="grid-header" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
