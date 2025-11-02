<%@ Page Title="Guitar Centre | Home" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="GuitarCentre.HomePage" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="head" runat="server">
    <style>
        .hero{
            background:#111; color:#fff; border-radius:16px; padding:24px; 
            box-shadow:0 12px 30px rgba(0,0,0,.18); margin:16px 0;
        }
        .grid{
            display:grid; grid-template-columns:repeat(auto-fit, minmax(220px, 1fr)); gap:12px;
        }
        .card{
            background:#fff; border-radius:14px; padding:16px; border:1px solid #eee;
            box-shadow:0 6px 18px rgba(0,0,0,.06)
        }
        .btns a{
            display:inline-block; padding:10px 14px; border-radius:10px; 
            border:1px solid #ddd; text-decoration:none; margin-right:8px; margin-top:8px;
        }
        .btn-primary{ background:#1a73e8; color:#fff; border-color:#1a73e8; }
        .muted{ color:#666; font-size:13px }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="hero">
        <h1 style="margin:0 0 8px">Guitar Centre — Home</h1>
        <div class="muted">
            <asp:Label ID="NowDate" runat="server" /> · 
            <asp:Label ID="NowTime" runat="server" />
        </div>
        <div class="btns">
            <a href="Products.aspx" class="btn-primary">Products</a>
            <a href="Manufactor.aspx" class="btns">Manufacturers</a>
            <a href="Clients.aspx" class="btns">Clients</a>
            <a href="Employees.aspx" class="btns">Employees</a>
            <a href="Sales.aspx" class="btns">Sales</a>
            <a href="SalesAssistant.aspx" class="btns">🤖 Sales Assistant</a>
        </div>
    </div>

    <div class="grid">
        <div class="card">
            <h3 style="margin-top:0">Quick Tips</h3>
            <ul style="margin:8px 0 0 18px">
                <li>Use <code>&lt;%$ ConnectionStrings:MyDb %></code> — no AttachDbFilename.</li>
                <li>Keep table names consistent (e.g. <code>tblClients</code> everywhere).</li>
                <li>Prefer <code>decimal</code> for prices/salary.</li>
            </ul>
        </div>
        <div class="card">
            <h3 style="margin-top:0">Shortcuts</h3>
            <div class="btns">
                <a href="Clients.aspx">Add Client</a>
                <a href="Employees.aspx">Add Employee</a>
                <a href="Sales.aspx">New Sale</a>
            </div>
        </div>
        <div class="card">
            <h3 style="margin-top:0">Assistant</h3>
            <p class="muted">Ask the AI to suggest guitars by style, budget, or brand.</p>
            <a href="SalesAssistant.aspx" class="btn-primary">Open Assistant</a>
        </div>
    </div>
</asp:Content>
