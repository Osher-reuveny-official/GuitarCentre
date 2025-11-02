<%@ Page Title="Guitar Centre | Clients" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Clients.aspx.cs" Inherits="GuitarCentre.Clients" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- מקור נתונים: משתמשים בקונקשן מתוך web.config -->
    <asp:SqlDataSource 
        ID="ClientsDataView" 
        runat="server" 
        ConnectionString="<%$ ConnectionStrings:MyDb %>" 
        ProviderName="System.Data.SqlClient"
        SelectCommand="
            SELECT customerID, FirstName, LastName, Phone, Email
            FROM tblClients
            WHERE (@q IS NULL OR @q = '' OR
                   FirstName LIKE '%' + @q + '%' OR
                   LastName  LIKE '%' + @q + '%' OR
                   Email     LIKE '%' + @q + '%' OR
                   Phone     LIKE '%' + @q + '%')
            ORDER BY customerID">
        <SelectParameters>
            <asp:ControlParameter Name="q" ControlID="searchKey" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>

    <h1 style="color:black;margin:16px 0;">Clients</h1>

    <div style="margin-bottom:8px;">
        <asp:Label ID="Label1" runat="server" Text="Find"></asp:Label>
        &nbsp;
        <asp:TextBox ID="searchKey" runat="server" AutoPostBack="true" OnTextChanged="searchKey_TextChanged"></asp:TextBox>
        &nbsp;
        <asp:Button ID="searchBtn" runat="server" Text="Search" OnClick="searchBtn_Click" />
    </div>

    <asp:GridView ID="GridView1" runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="customerID"
        DataSourceID="ClientsDataView"
        BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px"
        CellPadding="3" CellSpacing="2" Width="100%">
        <Columns>
            <asp:BoundField DataField="customerID" HeaderText="ID" ReadOnly="True" SortExpression="customerID" />
            <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
            <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
        </Columns>
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FFF1D4" />
        <SortedAscendingHeaderStyle BackColor="#B95C30" />
        <SortedDescendingCellStyle BackColor="#F1E5CE" />
        <SortedDescendingHeaderStyle BackColor="#93451F" />
    </asp:GridView>

    <!-- ביטול ה-GridView השני שמכוון ל-HR. אם אתה צריך אותו – נעביר לדף עובדים. -->
    <%-- 
    <asp:GridView ... Visible="False" />
    <asp:SqlDataSource ID="searchdata" ... SelectCommand="SELECT * FROM [tbl_HR]" />
    --%>

    <hr />

    <h2>Register New Client</h2>
    <table cellspacing="12">
        <tr>
            <td>First Name</td>
            <td><asp:TextBox ID="ClientRegisterFirstName" runat="server" BorderColor="Black" BorderStyle="Dotted"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Last Name</td>
            <td><asp:TextBox ID="ClientRegisterLastName" runat="server" BorderColor="Black" BorderStyle="Dotted"></asp:TextBox></td>
        </tr>
        <tr>
            <td>ID</td>
            <td>
                <asp:TextBox ID="ClientRegisterID" runat="server" BorderColor="Black" BorderStyle="Dotted" ValidationGroup="ValGroup"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="ClientRegisterID" runat="server" ErrorMessage="ID Entered is not valid" ValidationGroup="ValGroup" ValidationExpression="^\d{9}$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>Phone Number</td>
            <td>
                <asp:TextBox ID="ClientRegisterPhoneNumber" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="ClientRegisterPhoneNumber" ErrorMessage="Phone Number entered is not valid" ValidationGroup="ValGroup" ValidationExpression="^[0-9]{10}$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>Email</td>
            <td>
                <asp:TextBox ID="ClientRegisterEmail" runat="server" BorderColor="Black" BorderStyle="Dotted"></asp:TextBox>
                <asp:RegularExpressionValidator ControlToValidate="ClientRegisterEmail" ID="RegularExpressionValidator3" runat="server" ErrorMessage="Email Entered not correctly" ValidationGroup="ValGroup" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="ClientRegisterBtn" runat="server" Text="Register" OnClick="ClientRegisterBtn_Click" BorderStyle="Solid" ValidationGroup="ValGroup" BorderWidth="1px" Height="40px" Width="120px" />
            </td>
        </tr>
    </table>

</asp:Content>
