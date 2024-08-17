<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListViewOperation.aspx.cs" Inherits="CurdWithCrud.ListViewOperation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ListView Operations</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
 
            <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" /><br /><br />

    
            <asp:ListView ID="ListView1" runat="server" 
                DataKeyNames="EmployeeID" 
                InsertItemPosition="None" 
                OnItemEditing="ListView1_ItemEditing"
                OnItemUpdating="ListView1_ItemUpdating"
                OnItemDeleting="ListView1_ItemDeleting"
                OnItemCommand="ListView1_ItemCommand"
                ItemPlaceholderID="itemPlaceholder">
 
                <LayoutTemplate>
                    <table border="1" style="width: 100%">
                        <tr>
                            <th>Employee ID</th>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Email</th>
                            <th>Phone Number</th>
                            <th>Department</th>
                            <th>Actions</th>
                        </tr>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                    </table>
                </LayoutTemplate>

     
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("EmployeeID") %></td>
                        <td><%# Eval("FirstName") %></td>
                        <td><%# Eval("LastName") %></td>
                        <td><%# Eval("Email") %></td>
                        <td><%# Eval("PhoneNumber") %></td>
                        <td><%# Eval("Department") %></td>
                        <td>
                            <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                            <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                            <asp:LinkButton ID="SelectButton" runat="server" CommandName="Select" Text="Select" />
                        </td>
                    </tr>
                </ItemTemplate>
 
                <EditItemTemplate>
                    <tr>
                        <td><%# Eval("EmployeeID") %></td>
                        <td><asp:TextBox ID="txtFirstName" runat="server" Text='<%# Bind("FirstName") %>' /></td>
                        <td><asp:TextBox ID="txtLastName" runat="server" Text='<%# Bind("LastName") %>' /></td>
                        <td><asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("Email") %>' /></td>
                        <td><asp:TextBox ID="txtPhoneNumber" runat="server" Text='<%# Bind("PhoneNumber") %>' /></td>
                        <td><asp:TextBox ID="txtDepartment" runat="server" Text='<%# Bind("Department") %>' /></td>
                        <td>
                            <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                            <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                        </td>
                    </tr>
                </EditItemTemplate>

 
                <EmptyDataTemplate>
                    <tr>
                        <td colspan="7">No records found.</td>
                    </tr>
                </EmptyDataTemplate>
            </asp:ListView>
 
            <asp:DataPager ID="DataPager1" runat="server" PageSize="5" PagedControlID="ListView1">
                <Fields>
                    <asp:NumericPagerField ButtonCount="5" />
                </Fields>
            </asp:DataPager>
        </div>
    </form>
</body>
</html>
