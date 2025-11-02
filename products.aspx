<%@ Page Title="Guitar Centre | Products" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="products.aspx.cs" Inherits="GuitarCentre.products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .wrap{max-width:1100px;margin:0 auto}
        .toolbar{display:flex;gap:8px;align-items:center;margin:12px 0}
        .toolbar input[type=text]{padding:8px 10px;border:1px solid #ddd;border-radius:8px;min-width:260px}
        .btn{padding:8px 12px;border:1px solid #ddd;border-radius:8px;background:#fff;cursor:pointer}
        .btn-primary{background:#1a73e8;color:#fff;border-color:#1a73e8}
        .form-grid{display:grid;grid-template-columns:repeat(auto-fit,minmax(220px,1fr));gap:12px;margin-top:16px}
        .form-grid .field{display:flex;flex-direction:column;gap:6px}
        .msg{margin-top:10px;font-size:14px}
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrap">

        <h1 style="margin:10px 0">Products</h1>

        <!-- חיפוש -->
        <div class="toolbar">
            <asp:TextBox ID="txtSearch" runat="server" placeholder="Search by name or description…" AutoPostBack="true" OnTextChanged="Search_Refresh" />
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="Search_Refresh" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn" OnClick="Clear_Search" />
        </div>

        <!-- מקור נתונים: תמיד דרך MyDb מה-web.config -->
        <asp:SqlDataSource ID="ProductsViewDB" runat="server"
            ConnectionString="<%$ ConnectionStrings:MyDb %>"
            ProviderName="System.Data.SqlClient"
            SelectCommand="
                SELECT productID, productName, description, price
                FROM tblProducts
                WHERE (@q IS NULL OR @q='' OR
                       productName LIKE '%' + @q + '%' OR
                       description LIKE '%' + @q + '%')
                ORDER BY productID">
            <SelectParameters>
                <asp:ControlParameter Name="q" ControlID="txtSearch" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>

        <!-- טבלה -->
        <asp:GridView ID="GridView1" runat="server"
            AutoGenerateColumns="False"
            DataKeyNames="productID"
            DataSourceID="ProductsViewDB"
            AllowPaging="True" PageSize="10"
            AllowSorting="True"
            BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px"
            CellPadding="4" GridLines="Horizontal"
            Width="100%">
            <Columns>
                <asp:BoundField DataField="productID" HeaderText="ID" ReadOnly="True" SortExpression="productID" />
                <asp:BoundField DataField="productName" HeaderText="Product" SortExpression="productName" />
                <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                <asp:BoundField DataField="price" HeaderText="Price" DataFormatString="{0:C}" HtmlEncode="false" SortExpression="price" />
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

        <hr style="margin:18px 0" />

        <!-- הכנסה לטבלה -->
        <h3 style="margin:8px 0">Insert new product</h3>
        <div class="form-grid">
            <div class="field">
                <label for="productIDinput"><strong>Product I.D (optional if identity)</strong></label>
                <asp:TextBox ID="productIDinput" runat="server" />
                <asp:RegularExpressionValidator ID="valProdId" runat="server"
                    ControlToValidate="productIDinput" ValidationExpression="^\d*$"
                    ErrorMessage="Product ID must be numeric." ForeColor="Red" />
            </div>

            <div class="field">
                <label for="manufactorInput"><strong>Manufactor (optional)</strong></label>
                <asp:TextBox ID="manufactorInput" runat="server" />
            </div>

            <div class="field">
                <label for="productnameInput"><strong>Product name *</strong></label>
                <asp:TextBox ID="productnameInput" runat="server" />
                <asp:RequiredFieldValidator ID="reqName" runat="server"
                    ControlToValidate="productnameInput" ErrorMessage="Product name is required."
                    ForeColor="Red" />
            </div>

            <div class="field">
                <label for="descriptionInput"><strong>Description</strong></label>
                <asp:TextBox ID="descriptionInput" runat="server" />
            </div>

            <div class="field">
                <label for="priceInput"><strong>Price *</strong></label>
                <asp:TextBox ID="priceInput" runat="server" />
                <asp:RequiredFieldValidator ID="reqPrice" runat="server"
                    ControlToValidate="priceInput" ErrorMessage="Price is required."
                    ForeColor="Red" />
                <asp:RegularExpressionValidator ID="valPrice" runat="server"
                    ControlToValidate="priceInput" ValidationExpression="^\d+([.,]\d+)?$"
                    ErrorMessage="Invalid price format." ForeColor="Red" />
            </div>

            <div class="field">
                <label for="instockDescription"><strong>Stock qty (optional)</strong></label>
                <asp:TextBox ID="instockDescription" runat="server" />
                <asp:RegularExpressionValidator ID="valStock" runat="server"
                    ControlToValidate="instockDescription" ValidationExpression="^\d*$"
                    ErrorMessage="Stock must be an integer." ForeColor="Red" />
            </div>
        </div>

        <div style="margin-top:10px">
            <asp:Button ID="tableInsertButton" runat="server" CssClass="btn btn-primary"
                Text="Submit to table" OnClick="tableInsertButton_Click" />
            <asp:Label ID="Msg" runat="server" CssClass="msg" Visible="false" />
        </div>

    </div>
</asp:Content>
