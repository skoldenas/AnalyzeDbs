using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnalyzeDbs.Klasser.Facades;
using AnalyzeDbs.Klasser.Base;
using System.Data;
using System.Drawing;

namespace AnalyzeDbs
{
	public partial class _Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				FillDropDownSort();
		}

		protected void ButtonAnalysera_Click(object sender, EventArgs e)
		{
			string server = TextBoxServer.Text;
			UserInfo userInfo = new UserInfo(1, server, "MASTER", "alwar", "alwar", server);
			FoAnalyseDatabasesOnServer foAnalyseDatabasesOnServer = new FoAnalyseDatabasesOnServer(userInfo);
			AnalyzeDbs.Klasser.Facades.FoAnalyseDatabasesOnServer.enumOmrade omrade = FoAnalyseDatabasesOnServer.enumOmrade.omrLon;
			switch (Convert.ToInt32(DropDownListOmrade.SelectedValue))
			{
				case 0: omrade = FoAnalyseDatabasesOnServer.enumOmrade.omrGemensamt;
					break;
				case 1: omrade = FoAnalyseDatabasesOnServer.enumOmrade.omrLon;
					break;
				case 2: omrade = FoAnalyseDatabasesOnServer.enumOmrade.omrTid;
					break;
				case 3: omrade = FoAnalyseDatabasesOnServer.enumOmrade.omrResor;
					break;
				case 4: omrade = FoAnalyseDatabasesOnServer.enumOmrade.omrHRM;
					break;
				case 5: omrade = FoAnalyseDatabasesOnServer.enumOmrade.omrHist;
					break;
				case 6: omrade = FoAnalyseDatabasesOnServer.enumOmrade.omrVersion;
					break;
				case 7: omrade = FoAnalyseDatabasesOnServer.enumOmrade.omrACL;
					break;
				default: omrade = FoAnalyseDatabasesOnServer.enumOmrade.omrLon;
					break;
			}
			
			DataTable dt = foAnalyseDatabasesOnServer.GetDbsStats(omrade);
			DataView dv = new DataView(dt);
			if (DropDownListSort.SelectedValue == "NAMN")
				dv.Sort = DropDownListSort.SelectedValue + " ASC";
			else
				dv.Sort = DropDownListSort.SelectedValue + " DESC";
			GridViewDatabasInfo.DataSource = dv;
			GridViewDatabasInfo.DataBind();
		}

		protected void DropDownListOmrade_SelectedIndexChanged(object sender, EventArgs e)
		{
			FillDropDownSort();
		}

		private void FillDropDownSort()
		{
			//foreach(Columns GridViewDatabasInfo.Columns
			GridViewDatabasInfo.Columns.Clear();
			DropDownListSort.Items.Clear();
			InsertField("Namn", "NAMN", true, true);
			InsertField("Användare", "ANTAL_ANVANDARE", false, true);
			InsertField("Anställda", "ANTAL_ANSTALLDA", false, true);
			switch (Convert.ToInt32(DropDownListOmrade.SelectedValue))
			{
				case 0:
					InsertField("Företag", "ANTAL_FORETAG");
					InsertField("Användar-profiler", "ANTAL_ANVANDARPROFILER");
					InsertField("Person-profiler", "ANTAL_PERSONPROFILER");
					InsertField("Program-profiler", "ANTAL_PROGRAMPROFILER");
					InsertField("Rapporter", "ANTAL_RAPPORTER");
					InsertField("Distributioner", "ANTAL_DISTRIBUTIONER");
					InsertField("Noteringar", "ANTAL_NOTERINGAR");
					InsertField("Bevakningar", "ANTAL_BEVAKNINGAR");
					break;
				case 1:
					InsertField("Löner", "ANTAL_LONER");
					InsertField("Projekt", "ANTAL_PROJEKT");
					InsertField("Lärlingar", "ANTAL_LARLINGAR");
					InsertField("Konteringsbegrepp", "ANTAL_KONTBGR");
					InsertField("Revisionstillfällen", "ANTAL_REVISIONSTILLFALLEN");
					InsertField("Avvikelser", "ANTAL_AVVIKELSER");
					break;
				case 2:
					InsertField("Stämplingar tidreg", "ANTAL_STAMPLINGAR_TIDREG");
					InsertField("Stämplingar förtroendetid", "ANTAL_STAMPLINGAR_FORTROENDE");
					InsertField("Offline-stämplingar", "ANTAL_OFFLINESTAMPLINGAR");
					InsertField("Tidterminaler", "ANTAL_TIDTERMINALER");
					break;
				case 3:
					InsertField("Resor", "ANTAL_RESOR");
					InsertField("Kreditkort", "ANTAL_KREDITKORT");
					break;
				case 4:
					InsertField("Medarbetarsamtal", "ANTAL_MEDARB_SAMT");
					InsertField("Rekryteringar", "ANTAL_REKRYT");
					InsertField("Kompetenser", "ANTAL_KOMPETENSER");
					InsertField("Utbildningar", "ANTAL_UTBILDNINGAR");
					break;
				case 5:
					InsertField("Historik arbetstid", "ANTAL_HIST_ARBTID");
					InsertField("Historik fack", "ANTAL_HIST_FACK");
					InsertField("Historik kontering", "ANTAL_HIST_KONTERING");
					InsertField("Historik lön", "ANTAL_HIST_LON");
					InsertField("Historik organisation", "ANTAL_HIST_ORGANISATION");
					InsertField("Historik semester", "ANTAL_HIST_SEMESTER");
					InsertField("Historik skatt", "ANTAL_HIST_SKATT");
					InsertField("Historik statistik", "ANTAL_HIST_STATISTIK");
					break;
				case 6:
					InsertField("Version", "VERSION_TXT");
					InsertField("Scriptnr", "SCRIPT_NR");
					break;
				case 7:
					InsertField("ACL", "ANTAL_ACL");
					InsertField("ACL beskrivning", "ANTAL_ACL_BESTALLNING");
					InsertField("ACL historik", "ANTAL_ACL_HISTORIK");
					InsertField("ACL hist detalj", "ANTAL_ACL_HISTORIK_DETALJER");
					break;

			}
		}

		private void InsertField(string headerText, string dataField)
		{
			InsertField(headerText, dataField, false, false);
		}
		private void InsertField(string headerText, string dataField, bool alignLeft, bool whiteBackground)
		{
			BoundField b = new BoundField();
			b.DataField = dataField;
			b.HeaderText = headerText;
			b.HeaderStyle.CssClass = "gridrowleft";
			if (alignLeft)
				b.ItemStyle.CssClass = "gridrowleft";
			else
			{
				if (whiteBackground)
					b.ItemStyle.CssClass = "gridrowright";
				else
				{
					b.ItemStyle.BackColor = Color.Cornsilk;
					b.ItemStyle.CssClass = "gridrowright";
				}
			}
			GridViewDatabasInfo.Columns.Add(b);
			DropDownListSort.Items.Add(new ListItem(headerText, dataField));

		}
	}
}
