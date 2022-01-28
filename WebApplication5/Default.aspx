<%@ Page Title="Default" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication5.Default" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>&nbsp;CEP
            <asp:TextBox ID="txtCEP" runat="server" Width="310px"></asp:TextBox>
        </h1>
        <h1>&nbsp;
            <asp:Button ID="btn_gravar" runat="server" Text="Gravar" OnClick="btn_gravar_Click" Width="231px" />
        </h1>
        <p>&nbsp;</p>
        <p>Consultar Estado
            <asp:TextBox ID="txtEstado" runat="server" Width="57px"></asp:TextBox>
            &nbsp;&nbsp;
            <asp:Button ID="btn_pesquisar" runat="server" OnClick="btn_pesquisar_Click" Text="Pesquisar" Width="155px" />
        </p>
        <p>
            <asp:Label ID="lblMensagem" runat="server"></asp:Label>
        </p>
        <p>
            &nbsp;</p>
        <p>
            <asp:GridView ID="GrdCEP" runat="server" AutoGenerateColumns="False"  Width="699px">
                <Columns>
                    <asp:BoundField DataField="logradouro" HeaderText="logradouro" SortExpression="logradouro" />
                    <asp:BoundField DataField="cep" HeaderText="cep" SortExpression="cep" />
                    <asp:BoundField DataField="bairro" HeaderText="bairro" SortExpression="bairro" />
                    <asp:BoundField DataField="uf" HeaderText="uf" SortExpression="uf" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [logradouro], [cep], [bairro], [uf] FROM [CEP]"></asp:SqlDataSource>
        </p>

        <br />
    </div>

</asp:Content>

