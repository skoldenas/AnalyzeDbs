<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="AnalyzeDbs._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
		<h2>
        Databas-statistik</h2>
    <p>
        &nbsp;
				<asp:Label ID="Label1" runat="server" Text="Databasserver: "></asp:Label>
				<asp:TextBox ID="TextBoxServer" runat="server" Width="269px"></asp:TextBox>
				<asp:Label ID="Label3" runat="server" Text="Område"></asp:Label>
				<asp:DropDownList ID="DropDownListOmrade" runat="server" AutoPostBack="True" onselectedindexchanged="DropDownListOmrade_SelectedIndexChanged">
					<asp:ListItem Value="0">Gemensamt</asp:ListItem>
					<asp:ListItem Value="1">Lön</asp:ListItem>
					<asp:ListItem Value="2">Tid</asp:ListItem>
					<asp:ListItem Value="3">Resor</asp:ListItem>
					<asp:ListItem Value="4">HRM</asp:ListItem>
					<asp:ListItem Value="5">Historik</asp:ListItem>
					<asp:ListItem Value="6">Version</asp:ListItem>
					<asp:ListItem Value="7">ACL</asp:ListItem>
				</asp:DropDownList>
				<asp:Label ID="Label2" runat="server" Text="Sortering:"></asp:Label>
				<asp:DropDownList ID="DropDownListSort" runat="server">
					<asp:ListItem Value="NAMN">Namn</asp:ListItem>
					<asp:ListItem Value="ANTAL_ANVANDARE">Användare</asp:ListItem>
					<asp:ListItem Value="ANTAL_ANSTALLDA">Anställda</asp:ListItem>
					<asp:ListItem Value="ANTAL_STAMPLINGAR">Stämplingar</asp:ListItem>
					<asp:ListItem Value="ANTAL_PROJEKT">Projekt</asp:ListItem>
					<asp:ListItem Value="ANTAL_MEDARB_SAMT">Medarbetarsamtal</asp:ListItem>
					<asp:ListItem Value="ANTAL_KONTBGR">Konteringsbegrepp</asp:ListItem>
					<asp:ListItem Value="ANTAL_LONER">Löner</asp:ListItem>
					<asp:ListItem Value="ANTAL_REVISIONSTILLFALLEN">Revisionstillfällen</asp:ListItem>
					<asp:ListItem Value="ANTAL_RESOR">Resor</asp:ListItem>
					<asp:ListItem Value="ANTAL_REKRYT">Rekryteringar</asp:ListItem>
				</asp:DropDownList>
				<asp:Button ID="ButtonAnalysera" runat="server" onclick="ButtonAnalysera_Click" Text="Analysera server" />
		</p>
    <p>
        <asp:GridView ID="GridViewDatabasInfo" runat="server" AutoGenerateColumns="False" CssClass="grid">
					<Columns>
						<asp:BoundField DataField="NAMN" HeaderText="Namn" >
						<HeaderStyle CssClass="gridrowleft" />
						<ItemStyle HorizontalAlign="Left" CssClass="gridrowleft"/>
						</asp:BoundField>
						
						<asp:BoundField DataField="ANTAL_ANVANDARE" HeaderText="Användare" >
						<HeaderStyle CssClass="gridrowright" />
						<ItemStyle HorizontalAlign="Right" CssClass="gridrowright"/>
						</asp:BoundField>
						
						<asp:BoundField DataField="ANTAL_ANSTALLDA" HeaderText="Anställda" >
						<HeaderStyle CssClass="gridrowright" />
						<ItemStyle HorizontalAlign="Right" CssClass="gridrowright"/>
						</asp:BoundField>
						
						<asp:BoundField DataField="ANTAL_STAMPLINGAR" HeaderText="Stämplingar" >
						<HeaderStyle CssClass="gridrowright" />
						<ItemStyle HorizontalAlign="Right" CssClass="gridrowright"/>
						</asp:BoundField>
						<asp:BoundField DataField="ANTAL_PROJEKT" HeaderText="Projekt" >
						<HeaderStyle CssClass="gridrowright" />
						<ItemStyle HorizontalAlign="Right" CssClass="gridrowright"/>
						</asp:BoundField>
						<asp:BoundField DataField="ANTAL_MEDARB_SAMT" HeaderText="Medarb.samt.">
						<HeaderStyle CssClass="gridrowright" />
						<ItemStyle HorizontalAlign="Right" CssClass="gridrowright"/>
						</asp:BoundField>
						<asp:BoundField DataField="ANTAL_KONTBGR" HeaderText="Kont.begr." >
						<HeaderStyle CssClass="gridrowright" />
						<ItemStyle HorizontalAlign="Right" CssClass="gridrowright"/>
						</asp:BoundField>
						<asp:BoundField DataField="ANTAL_LONER" HeaderText="Löner">
						<HeaderStyle CssClass="gridrowright" />
						<ItemStyle HorizontalAlign="Right" CssClass="gridrowright"/>
						</asp:BoundField>
						<asp:BoundField DataField="ANTAL_REVISIONSTILLFALLEN" HeaderText="Rev.tillf.">
						<HeaderStyle CssClass="gridrowright" />
						<ItemStyle HorizontalAlign="Right" CssClass="gridrowright"/>
						</asp:BoundField>
						<asp:BoundField DataField="ANTAL_RESOR" HeaderText="Resor">
						<HeaderStyle CssClass="gridrowright" />
						<ItemStyle HorizontalAlign="Right" CssClass="gridrowright"/>
						</asp:BoundField>
						<asp:BoundField DataField="ANTAL_REKRYT" HeaderText="Rekryteringar" >
						<HeaderStyle CssClass="gridrowright" />
						<ItemStyle HorizontalAlign="Right" CssClass="gridrowright"/>
						</asp:BoundField>
					</Columns>
				</asp:GridView>
    </p>

</asp:Content>
