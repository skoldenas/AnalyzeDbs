using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using TestInfo.Klasser.Base;

namespace TestInfo2.Klasser.Facades.Sokning
{
	public class FoSokning : FoBase
	{
		public FoSokning(UserInfo userInfo)
			: base(userInfo)
		{
		}

		public DataTable Search(string Id, string AnmaltAv, string Anmalan, string UtvecklatAv, string UtvecklarKommentar, string TestatAv, string TestNotering, string ProjektId)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("EXEC SP_FIND_TESTITEM '" + Id + "','" + Anmalan + "','" + UtvecklarKommentar + "','" + TestNotering + "','" + AnmaltAv + "','" + UtvecklatAv + "','" + TestatAv + "','" + ProjektId + "'");
			return DataSQLQuery.ExecuteSQL(sb.ToString(), true).Tables[0];
		}

		public DataTable SorteradProjektLista()
		{
			StringBuilder sb3 = new StringBuilder();
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("SELECT LOP_NR_PROJEKT ID , PROJEKT_ID + ' - ' + PROJEKTNAMN PROJEKT FROM BA_PROJEKT");
			sb.AppendLine("WHERE AKTIV = 'J'");
			sb.AppendLine("AND GETDATE() BETWEEN ISNULL(DATUM_FOM, '1900-01-01') AND ISNULL(DATUM_TOM,'2099-12-31')  ");
			sb.AppendLine("ORDER BY PROJEKT_ID, PROJEKTNAMN");
			DataTable dt = DataSQLQuery.ExecuteSQL(sb.ToString(), true).Tables[0];
			DataRow dr =  dt.NewRow();
			dr["ID"] = -1;
			dr["PROJEKT"] = "--Inget värde--";
			dt.Rows.InsertAt(dr,0);
			return dt;
		}

	}
}