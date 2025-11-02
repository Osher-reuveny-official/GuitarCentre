<%@ Page Title="Guitar Centre | Sales" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="GuitarCentre.Sales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .wrap{max-width:1100px;margin:0 auto}
        .toolbar{display:flex;gap:8px;align-items:center;margin:12px 0}
        .toolbar input[type=text]{padding:8px 10px;border:1px solid #ddd;border-radius:8px;min-width:260px}
        .btn{padding:8px 12px;border:1px solid #ddd;border-radius:8px;background:#fff;cursor:pointer}
        .btn-primary{background:#1a73e8;color:#fff;border-color:#1a73e8}
        .form-grid{display:grid;grid-template-columns:repeat(auto-fit,minmax(220px,1fr));gap:12px;margin-top:16px}
        .field{display:flex;flex-direction:column;gap:6px}
        .msg{margin-top:10px;font-size:14px}
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrap">

        <h1 style="margin:10px 0">Sales</h1>

        <!-- חיפוש -->
        <div class="toolbar">
            <asp:TextBox ID="txtSearch" runat="server" placeholder="Search by SaleID / CustomerID / EmployeeID…" />
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="Search_Refresh" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn" OnClick="Clear_Search" />
        </div>

        <!-- טבלה (דינמית) -->
        <asp:GridView ID="GridView1" runat="server"
            AutoGenerateColumns="True"
            AllowPaging="True" PageSize="10"
            AllowSorting="False"
            BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px"
            CellPadding="4" GridLines="Horizontal"
            Width="100%">
            <FooterStyle BackColor="White" ForeColor="#333333" />
            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
        </asp:GridView>

        <hr style="margin:18px 0" />

        <!-- הוספת מכירה -->
        <h3 style="margin:8px 0">Insert new sale</h3>
        <div class="form-grid">
            <div class="field">
                <label for="saleIDInput"><strong>Sale ID (optional if identity)</strong></label>
                <asp:TextBox ID="saleIDInput" runat="server" />
                <asp:RegularExpressionValidator ID="valSaleId" runat="server"
                    ControlToValidate="saleIDInput" ValidationExpression="^\d*$"
                    ErrorMessage="Sale ID must be numeric." ForeColor="Red" />
            </div>

            <div class="field">
                <label for="saleItemsCodeInput"><strong>Sale Items Code *</strong></label>
                <asp:TextBox ID="saleItemsCodeInput" runat="server" />
                <asp:RequiredFieldValidator ID="reqItemsCode" runat="server"
                    ControlToValidate="saleItemsCodeInput" ErrorMessage="Items code is required." ForeColor="Red" />
                <asp:RegularExpressionValidator ID="valItemsCode" runat="server"
                    ControlToValidate="saleItemsCodeInput" ValidationExpression="^\d+$"
                    ErrorMessage="Items code must be numeric." ForeColor="Red" />
            </div>

            <div class="field">
                <label for="customerIDInput"><strong>Customer ID *</strong></label>
                <asp:TextBox ID="customerIDInput" runat="server" />
                <asp:RequiredFieldValidator ID="reqCustomer" runat="server"
                    ControlToValidate="customerIDInput" ErrorMessage="Customer ID is required." ForeColor="Red" />
                <asp:RegularExpressionValidator ID="valCustomer" runat="server"
                    ControlToValidate="customerIDInput" ValidationExpression="^\d+$"
                    ErrorMessage="Customer ID must be numeric." ForeColor="Red" />
            </div>

            <div class="field">
                <label for="employeeIDInput"><strong>Employee ID *</strong></label>
                <asp:TextBox ID="employeeIDInput" runat="server" />
                <asp:RequiredFieldValidator ID="reqEmployee" runat="server"
                    ControlToValidate="employeeIDInput" ErrorMessage="Employee ID is required." ForeColor="Red" />
                <asp:RegularExpressionValidator ID="valEmployee" runat="server"
                    ControlToValidate="employeeIDInput" ValidationExpression="^\d+$"
                    ErrorMessage="Employee ID must be numeric." ForeColor="Red" />
            </div>

            <div class="field">
                <label for="totalPriceInput"><strong>Total Price *</strong></label>
                <asp:TextBox ID="totalPriceInput" runat="server" />
                <asp:RequiredFieldValidator ID="reqPrice" runat="server"
                    ControlToValidate="totalPriceInput" ErrorMessage="Total price is required." ForeColor="Red" />
                <asp:RegularExpressionValidator ID="valPrice" runat="server"
                    ControlToValidate="totalPriceInput" ValidationExpression="^\d+([.,]\d+)?$"
                    ErrorMessage="Invalid price format." ForeColor="Red" />
            </div>

            <div class="field">
                <label for="saleDateInput"><strong>Sale Date *</strong></label>
                <asp:TextBox ID="saleDateInput" runat="server" TextMode="Date" />
                <asp:RequiredFieldValidator ID="reqDate" runat="server"
                    ControlToValidate="saleDateInput" ErrorMessage="Sale date is required." ForeColor="Red" />
            </div>
        </div>

        <div style="margin-top:10px">
            <asp:Button ID="btnInsertSale" runat="server" CssClass="btn btn-primary"
                Text="Submit sale" OnClick="btnInsertSale_Click" />
            <asp:Label ID="Msg" runat="server" CssClass="msg" Visible="false" />
        </div>

    </div>
</asp:Content>
