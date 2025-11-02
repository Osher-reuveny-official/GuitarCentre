<%@ Page Title="Guitar Centre | Manufactor" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Manufactor.aspx.cs" Inherits="GuitarCentre.Manufactor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-title{
            color:#ED1C24; margin:12px 0 6px; font-weight:700; letter-spacing:.5px;
        }
        .toolbar{
            display:flex; gap:8px; align-items:center; margin:8px 0 14px;
        }
        .toolbar input[type="text"]{
            padding:8px 10px; border:1px solid #ddd; border-radius:8px; min-width:240px;
        }
        .toolbar .btn{
            padding:8px 12px; border:1px solid #ddd; border-radius:8px; background:#fff; cursor:pointer;
        }
        .toolbar .btn-primary{ background:#1a73e8; color:#fff; border-color:#1a73e8; }
        .wrap{ max-width:1100px; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrap">
        <h1 class="page-title">Manufactor</h1>

        <!-- טולבר חיפוש (ללא קוד מאחורה) -->
        <div class="toolbar">
            <asp:TextBox ID="txtSearch" runat="server" placeholder="Search by name, email, contact, branch…" AutoPostBack="true" />
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn" OnClientClick="document.getElementById('<%= txtSearch.ClientID %>').value='';" />
        </div>

        <!-- מקור נתונים: משתמש בקונקשן מה-web.config -->
        <asp:SqlDataSource ID="ManDB" runat="server"
            ConnectionString="<%$ ConnectionStrings:MyDb %>"
            ProviderName="System.Data.SqlClient"
            SelectCommand="
                SELECT manufactorID, manufactorName, email, nameOfContact, contactNumber, branchToOrder, customLineBrand, CustomLineContact
                FROM tblManufactor
                WHERE (@q IS NULL OR @q = '' OR
                       manufactorName   LIKE '%' + @q + '%' OR
                       email            LIKE '%' + @q + '%' OR
                       nameOfContact    LIKE '%' + @q + '%' OR
                       contactNumber    LIKE '%' + @q + '%' OR
                       branchToOrder    LIKE '%' + @q + '%' OR
                       customLineBrand  LIKE '%' + @q + '%' OR
                       CustomLineContact LIKE '%' + @q + '%')
                ORDER BY manufactorID">
            <SelectParameters>
                <asp:ControlParameter Name="q" ControlID="txtSearch" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>

        <asp:GridView ID="GridView1" runat="server"
            AutoGenerateColumns="False"
            DataKeyNames="manufactorID"
            DataSourceID="ManDB"
            AllowPaging="True" PageSize="10"
            AllowSorting="True"
            BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px"
            CellPadding="4" GridLines="Horizontal"
            Width="100%">
            <Columns>
                <asp:BoundField DataField="manufactorID" HeaderText="manufactorID" ReadOnly="True" SortExpression="manufactorID" />
                <asp:BoundField DataField="manufactorName" HeaderText="manufactorName" SortExpression="manufactorName" />
                <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
                <asp:BoundField DataField="nameOfContact" HeaderText="nameOfContact" SortExpression="nameOfContact" />
                <asp:BoundField DataField="contactNumber" HeaderText="contactNumber" SortExpression="contactNumber" />
                <asp:BoundField DataField="branchToOrder" HeaderText="branchToOrder" SortExpression="branchToOrder" />
                <asp:BoundField DataField="customLineBrand" HeaderText="customLineBrand" SortExpression="customLineBrand" />
                <asp:BoundField DataField="CustomLineContact" HeaderText="CustomLineContact" SortExpression="CustomLineContact" />
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#333333" />
            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#487575" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#275353" />
        </asp:GridView>
    </div>
</asp:Content>
