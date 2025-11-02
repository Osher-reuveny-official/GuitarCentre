<%@ Page Title="Guitar Centre | Home" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="GuitarCentre.HomePage" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="hero-card hero-enter">
        <div class="hero-copy">
            <p class="eyebrow">Welcome to Guitar Centre</p>
            <h1 class="hero-title">Find the perfect sound for every stage.</h1>
            <p class="hero-meta">
                <asp:Label ID="NowDate" runat="server" CssClass="timestamp" />
                <span aria-hidden="true">•</span>
                <asp:Label ID="NowTime" runat="server" CssClass="timestamp" />
            </p>
            <p class="muted">Discover pro-level gear, manage your inventory, and keep your sales pipeline in tune.</p>
            <div class="button-row">
                <a href="Products.aspx" class="button primary">Browse Products</a>
                <a href="Manufactor.aspx" class="button secondary">Meet Manufacturers</a>
                <a href="Clients.aspx" class="button ghost">Clients Hub</a>
            </div>
        </div>
        <div class="hero-visual">
            <p>“Instruments that inspire every rehearsal, session, and spotlight moment.”</p>
        </div>
    </section>

    <section class="card-grid fade-up">
        <article class="info-card hover-lift">
            <h3>Quick Tips</h3>
            <p class="muted">Keep data tidy to make reporting and maintenance effortless.</p>
            <ul>
                <li>Use <code>&lt;%$ ConnectionStrings:MyDb %></code> without <code>AttachDbFileName</code>.</li>
                <li>Keep table prefixes consistent (e.g., <code>tblClients</code>).</li>
                <li>Prefer <code>decimal</code> for prices and salaries.</li>
            </ul>
        </article>
        <article class="info-card hover-lift">
            <h3>Shortcuts</h3>
            <p class="muted">Jump straight to the tools your team uses most.</p>
            <div class="button-row">
                <a href="Clients.aspx" class="button ghost">Add Client</a>
                <a href="Employees.aspx" class="button ghost">Add Employee</a>
                <a href="Sales.aspx" class="button ghost">Log Sale</a>
            </div>
        </article>
        <article class="info-card hover-lift">
            <h3>Popular Searches</h3>
            <p class="muted">See what other managers are exploring today.</p>
            <ul class="pill-list">
                <li>Hollow Body</li>
                <li>Tube Amps</li>
                <li>Pedalboards</li>
                <li>Studio Bundles</li>
                <li>Beginner Kits</li>
            </ul>
            <div class="button-row">
                <a href="Products.aspx" class="button secondary">Explore Gear</a>
                <a href="SalesAssistant.aspx" class="button ghost">Sales Assistant</a>
            </div>
        </article>
    </section>
</asp:Content>
