<%@ Page Title="Guitar Centre | Employees" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Employees.aspx.cs" Inherits="GuitarCentre.Employees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style4 { height: 26px; width: 228px; }
        .auto-style5 { height: 28px; width: 228px; }
        .auto-style6 { height: 29px; width: 228px; }
        .auto-style7 { height: 22px; width: 228px; }
        .auto-style8 { height: 27px; width: 228px; }
        .auto-style10 { height: 61px; width: 228px; }
        .auto-style11 { height: 858px; width: 288px; }
        .auto-style12 { width: 228px; }
        .auto-style13 { width: 228px; height: 74px; }
        .auto-style14 { width: 228px; height: 78px; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <!-- מקור נתונים יחיד: משתמש ב-MyDb מה-web.config (לא AttachDbFilename) -->
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
        ConnectionString="<%$ ConnectionStrings:MyDb %>"
        ProviderName="System.Data.SqlClient"
        SelectCommand="
            SELECT employeeID, firstName, lastName, Email, adress, position, branch, Phone, birthdate, salary
            FROM tblEmployees
            ORDER BY employeeID">
    </asp:SqlDataSource>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- רענון כל 10 שניות (אם צריך). שים לב: 10000ms = 10s -->
            <asp:Timer ID="Timer1" runat="server" Interval="10000" OnTick="Timer1_Tick" />

            <asp:GridView ID="GridView1" runat="server"
                AutoGenerateColumns="False"
                DataKeyNames="employeeID"
                DataSourceID="SqlDataSource1"
                BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                CellPadding="4" Width="715px" Height="247px">
                <Columns>
                    <asp:BoundField DataField="employeeID" HeaderText="Employee ID" ReadOnly="True" />
                    <asp:BoundField DataField="firstName" HeaderText="First Name" />
                    <asp:BoundField DataField="lastName" HeaderText="Last Name" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="adress" HeaderText="Address" />
                    <asp:BoundField DataField="position" HeaderText="Position" />
                    <asp:BoundField DataField="branch" HeaderText="Branch" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone" />
                    <asp:BoundField DataField="birthdate" HeaderText="Birthdate" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                    <asp:BoundField DataField="salary" HeaderText="Salary" DataFormatString="{0:C}" HtmlEncode="false" />
                </Columns>
                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#330099" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- טופס רישום עובד חדש -->
    <table cellpadding="8" cellspacing="20" style="margin-right: 0px;" class="auto-style11">
        <tr>
            <td class="auto-style12">
                First Name&nbsp;
                <asp:TextBox ID="EmployeeRegisterFirstName" runat="server" BorderColor="Black" BorderStyle="Dotted" OnTextChanged="EmployeeRegisterFirstName_TextChanged" />
            </td>
        </tr>
        <tr>
            <td class="auto-style6">
                Last Name&nbsp;
                <asp:TextBox ID="EmployeeRegisterLastName" runat="server" BorderColor="Black" BorderStyle="Dotted" OnTextChanged="EmployeeRegisterLastName_TextChanged" />
            </td>
        </tr>
        <tr>
            <td class="auto-style14">
                ID&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="EmployeeRegisterID" runat="server" BorderColor="Black" BorderStyle="Dotted" OnTextChanged="EmployeeRegisterID_TextChanged" />
                <!-- RegexValidator תקין לת״ז: 9 ספרות -->
                <asp:RegularExpressionValidator ID="IDvalidator" runat="server"
                    ControlToValidate="EmployeeRegisterID"
                    ErrorMessage="ID Entered Is Not Correct. Please Try Inserting Again."
                    ValidationExpression="^\d{9}$"
                    ForeColor="Red" ValidationGroup="ValGroup" />
            </td>
        </tr>
        <tr>
            <td class="auto-style7">
                Birthdate<br />
                <asp:TextBox ID="EmployeeRegisterBirhtDate" runat="server" TextMode="Date" OnTextChanged="EmployeeRegisterBirhtDate_TextChanged" />
            </td>
        </tr>
        <tr>
            <td class="auto-style8">
                Position&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="EmployeeRegisterPosition" runat="server" BorderColor="Black" BorderStyle="Dotted" OnTextChanged="EmployeeRegisterPosition_TextChanged" />
            </td>
        </tr>
        <tr>
            <td class="auto-style4">
                Salary&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="EmployeeRegisterSalary" runat="server" BorderColor="Black" BorderStyle="Dotted" OnTextChanged="EmployeeRegisterSalary_TextChanged" />
                <asp:Label ID="Label1" runat="server" Text="Label" Visible="False" />
            </td>
        </tr>
        <tr>
            <td class="auto-style5">
                Branch&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="EmployeeRegisterBranch" runat="server" BorderColor="Black" BorderStyle="Dotted" OnTextChanged="EmployeeRegisterBranch_TextChanged" />
            </td>
        </tr>
        <tr>
            <td class="auto-style10">
                Email&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="EmployeeRegisterEmail" runat="server" OnTextChanged="EmployeeRegisterEmail_TextChanged" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                    ErrorMessage="Email address is not valid."
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ForeColor="Red" ValidationGroup="ValGroup"
                    ControlToValidate="EmployeeRegisterEmail" />
            </td>
        </tr>
        <tr>
            <td class="auto-style13">
                Phone Number&nbsp;
                <asp:TextBox ID="EmployeeRegisterPhoneNumber" runat="server" BorderColor="Black" BorderStyle="Dotted" OnTextChanged="EmployeeRegisterPhoneNumber_TextChanged" />
                <!-- פורמט ישראלי בסיסי: 0/ +972 ואז ספרות -->
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                    ErrorMessage="Invalid phone number format."
                    ValidationExpression="^(?:\+972|0)[2-9](?:[-\s]?\d){7,8}$"
                    ForeColor="Red" ValidationGroup="ValGroup"
                    ControlToValidate="EmployeeRegisterPhoneNumber" />
            </td>
        </tr>
        <tr>
            <td class="auto-style13">
                Adress&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <!-- משאיר את ה-ID עם השגיאה הקטנה (Adress) כדי לא לשבור CodeBehind קיים -->
                <asp:TextBox ID="EmployeeRegisterAdress" runat="server" BorderColor="Black" BorderStyle="Dotted" OnTextChanged="EmployeeRegisterAdress_TextChanged" />
            </td>
        </tr>
        <tr>
            <td>
                <center>
                    <asp:Button ID="EmployeeRegisterBtn" runat="server" Text="Register"
                        OnClick="EmployeeRegisterBtn_Click" ValidationGroup="ValGroup" />
                </center>
            </td>
        </tr>
    </table>
</asp:Content>
