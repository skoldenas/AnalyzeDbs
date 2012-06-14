using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AnalyzeDbs.Klasser.Base;
using System.Text;
using System.Data;

namespace AnalyzeDbs.Klasser.Facades
{
	public class FoAnalyseDatabasesOnServer : FoBase
	{

		public enum enumOmrade
		{
			omrGemensamt,
			omrLon,
			omrTid,
			omrResor,
			omrHRM,
			omrHist,
			omrVersion,
			omrACL
		}

		public FoAnalyseDatabasesOnServer(UserInfo userInfo)
			: base(userInfo)
		{
			//GetDbsStats();
		}

		public DataTable GetDbsStats(enumOmrade omrade)
		{
			DataTable dtDbsStats = InitDataNew();
			DataTable dtDatabases = GetDatabases();
			foreach (DataRow dr in dtDatabases.Rows)
			{
				AddStatistics(dtDbsStats, dr["NAME"].ToString(), omrade);
			}
			dtDbsStats.AcceptChanges();
			return dtDbsStats;
		}

		private void AddStatistics(DataTable dtDbsStats, string DatabaseName, enumOmrade omrade)
		{
			DataRow dr = dtDbsStats.NewRow();
			try
			{
				//Databasens namn
				dr["NAMN"] = DatabaseName;
				//Antal användare
				AddStatisticFromDbs(dr, DatabaseName, "BA_ANV", "ANTAL_ANVANDARE");
				//Antal anställda
				AddStatisticFromDbs(dr, DatabaseName, "BA_ANSTALLD_GRUND", "ANTAL_ANSTALLDA");

				if (omrade == enumOmrade.omrGemensamt)
				{
					//Antal företag
					AddStatisticFromDbs(dr, DatabaseName, "BA_FTG", "ANTAL_FORETAG");
					//Antal användarprofiler
					AddStatisticFromDbs(dr, DatabaseName, "BA_ANVGRP", "ANTAL_ANVANDARPROFILER");
					//Antal personprofiler
					AddStatisticFromDbs(dr, DatabaseName, "BA_PERSONPROFIL", "ANTAL_PERSONPROFILER");
					//Antal programprofiler
					AddStatisticFromDbs(dr, DatabaseName, "BA_PROGRAMPROFIL", "ANTAL_PROGRAMPROFILER");
					//Antal rapporter
					AddStatisticFromDbs(dr, DatabaseName, "UD_FORMAT", "ANTAL_RAPPORTER");
					//Antal distributioner
					AddStatisticFromDbs(dr, DatabaseName, "UD_DISTRIBUTION", "ANTAL_DISTRIBUTIONER");
					//Antal noteringar
					AddStatisticFromDbs(dr, DatabaseName, "BA_ANTECKNING", "ANTAL_NOTERINGAR");
					//Antal bevakningar
					AddStatisticFromDbs(dr, DatabaseName, "BA_BEVAKNING", "ANTAL_BEVAKNINGAR");
				}
				if (omrade == enumOmrade.omrLon)
				{
					//Antal löner
					AddStatisticFromDbs(dr, DatabaseName, "BA_LONPER_PERS", "ANTAL_LONER");
					//Antal projekt
					AddStatisticFromDbs(dr, DatabaseName, "PR_PROJEKT", "ANTAL_PROJEKT");
					//Antal lärlingar
					AddStatisticFromDbs(dr, DatabaseName, "PR_AKTIVT_FTAL", "ANTAL_LARLINGAR");
					//Antal konteringsbegrepp
					AddStatisticFromDbs(dr, DatabaseName, "BA_KONTBGR", "ANTAL_KONTBGR");
					//Antal revisionstillfällen
					AddStatisticFromDbs(dr, DatabaseName, "LR_REVTILLF", "ANTAL_REVISIONSTILLFALLEN");
					//Antal avvikelser
					AddStatisticFromDbs(dr, DatabaseName, "BA_AVVIK_PERS", "ANTAL_AVVIKELSER");
				}
				if (omrade == enumOmrade.omrTid)
				{
					//Antal stämplingar tidreg
					AddStamplingStatisticFromDbs(dr, DatabaseName, "TI_STAMPLINGAR", "ANTAL_STAMPLINGAR_TIDREG", true);
					//Antal stämplingar förtroende
					AddStamplingStatisticFromDbs(dr, DatabaseName, "TI_STAMPLINGAR", "ANTAL_STAMPLINGAR_FORTROENDE", false);
					//Antal Offline-stämplingar
					AddStatisticFromDbs(dr, DatabaseName, "TI_OFFLINESTAMPLINGAR", "ANTAL_OFFLINESTAMPLINGAR");
					//Antal tidterminaler
					AddStatisticFromDbs(dr, DatabaseName, "TI_TIDTERMINALER", "ANTAL_TIDTERMINALER");
				}
				if (omrade == enumOmrade.omrResor)
				{
					//Antal resor
					AddStatisticFromDbs(dr, DatabaseName, "RE_RESOR", "ANTAL_RESOR");
					AddStatisticFromDbs(dr, DatabaseName, "RE_PERS_KREDITK", "ANTAL_KREDITKORT");
				}
				if (omrade == enumOmrade.omrHRM)
				{
					//Antal medarbetarsamtal
					AddStatisticFromDbs(dr, DatabaseName, "PA_MEDARB_SAMT", "ANTAL_MEDARB_SAMT");
					//Antal rekryteringsärenden
					AddStatisticFromDbs(dr, DatabaseName, "PA_JOBB", "ANTAL_REKRYT");
					//Antal kompetenser
					AddStatisticFromDbs(dr, DatabaseName, "PA_KOMP", "ANTAL_KOMPETENSER");
					//Antal utbildningar
					AddStatisticFromDbs(dr, DatabaseName, "PA_KURS", "ANTAL_UTBILDNINGAR");
				}
				if (omrade == enumOmrade.omrHist)
				{
					//Antal arbetstid
					AddPersHistStatisticFromDbs(dr, DatabaseName, "BA_ANSTALLD_ARBTID", "ANTAL_HIST_ARBTID");
					//Antal fack
					AddPersHistStatisticFromDbs(dr, DatabaseName, "BA_ANSTALLD_FACK", "ANTAL_HIST_FACK");
					//Antal kontering
					AddPersHistStatisticFromDbs(dr, DatabaseName, "BA_ANSTALLD_KONTERING", "ANTAL_HIST_KONTERING");
					//Antal lön
					AddPersHistStatisticFromDbs(dr, DatabaseName, "BA_ANSTALLD_LON", "ANTAL_HIST_LON");
					//Antal organisation
					AddPersHistStatisticFromDbs(dr, DatabaseName, "BA_ANSTALLD_ORGANISATION", "ANTAL_HIST_ORGANISATION");
					//Antal semester
					AddPersHistStatisticFromDbs(dr, DatabaseName, "BA_ANSTALLD_SEMESTER", "ANTAL_HIST_SEMESTER");
					//Antal skatt
					AddPersHistStatisticFromDbs(dr, DatabaseName, "BA_ANSTALLD_SKATT", "ANTAL_HIST_SKATT");
					//Antal statistik
					AddPersHistStatisticFromDbs(dr, DatabaseName, "BA_ANSTALLD_STATISTIK", "ANTAL_HIST_STATISTIK");
				}
				if (omrade == enumOmrade.omrVersion)
				{
					//Versionsinfo
					AddVersionsinfoFromDbs(dr, DatabaseName, "BA_DB_VERSION", "VERSION_TXT", "SCRIPT_NR");
				}
				if (omrade == enumOmrade.omrACL)
				{
					//Antal ACL
					AddStatisticFromDbs(dr, DatabaseName, "ACL", "ANTAL_ACL");
					//Antal ACL beskrivning
					AddStatisticFromDbs(dr, DatabaseName, "ACL_BESTALLNING", "ANTAL_ACL_BESTALLNING");
					//Antal ACL historik
					AddStatisticFromDbs(dr, DatabaseName, "ACL_HISTORIK", "ANTAL_ACL_HISTORIK");
					//Antal ACL historik detaljer
					AddStatisticFromDbs(dr, DatabaseName, "ACL_HISTORIK_DETALJER", "ANTAL_ACL_HISTORIK_DETALJER");
					//Detta är bara en testrad
					AddStatisticFromDbs(dr, DatabaseName, "ACL_HISTORIK_DETALJER", "ANTAL_ACL_HISTORIK_DETALJER");
					
				}
				dtDbsStats.Rows.Add(dr);
				dr.AcceptChanges();
			}
			catch
			{
				dr.RejectChanges();
			}
		}

		private void AddStatisticFromDbs(DataRow dr, string databaseName, string tableName, string fieldName)
		{
			StringBuilder sb = new StringBuilder();
			int Antal = 0;
			sb.Clear();
			sb.AppendLine("USE [" + databaseName + "]");
			sb.AppendLine("SELECT COUNT(*) ANTAL FROM " + tableName);
			Antal = (int)DataSQLQuery.ExecuteSQL(sb.ToString(), true).Tables[0].Rows[0]["ANTAL"];
			dr[fieldName] = Antal;
		}

		private void AddVersionsinfoFromDbs(DataRow dr, string databaseName, string tableName, string fieldName1, string fieldName2)
		{
			StringBuilder sb = new StringBuilder();
			sb.Clear();
			sb.AppendLine("USE [" + databaseName + "]");
			sb.AppendLine("SELECT TOP 1 LOP_NR_VERSION, LOP_NR_VER FROM " + tableName);
			sb.AppendLine("ORDER BY LOP_NR_VERSION DESC");
			DataTable dt = DataSQLQuery.ExecuteSQL(sb.ToString(), true).Tables[0];
			string tmp = dt.Rows[0]["LOP_NR_VERSION"].ToString();
			dr[fieldName1] = tmp.Replace(",", ".");
			tmp = dt.Rows[0]["LOP_NR_VER"].ToString();
			dr[fieldName2] = tmp.Substring(0, tmp.Length - 2);
		}

		private void AddStamplingStatisticFromDbs(DataRow dr, string databaseName, string tableName, string fieldName, bool isTidreg)
		{
			StringBuilder sb = new StringBuilder();
			int Antal = 0;
			sb.Clear();
			sb.AppendLine("USE [" + databaseName + "]");
			sb.AppendLine("SELECT COUNT(*) ANTAL FROM " + tableName);
			if (isTidreg)
				sb.AppendLine("WHERE STATUS_TID2 IS NOT NULL");
			else
				sb.AppendLine("WHERE STATUS_TID2 IS NULL");
			Antal = (int)DataSQLQuery.ExecuteSQL(sb.ToString(), true).Tables[0].Rows[0]["ANTAL"];
			dr[fieldName] = Antal;
		}


		private void AddPersHistStatisticFromDbs(DataRow dr, string databaseName, string tableName, string fieldName)
		{
			StringBuilder sb = new StringBuilder();
			int Antal = 0;
			sb.Clear();
			sb.AppendLine("USE [" + databaseName + "]");
			sb.AppendLine("SELECT COUNT(*) - (SELECT COUNT(*) FROM BA_ANSTALLD_GRUND) ANTAL FROM " + tableName);
			Antal = (int)DataSQLQuery.ExecuteSQL(sb.ToString(), true).Tables[0].Rows[0]["ANTAL"];
			dr[fieldName] = Antal;
		}

		private DataTable InitDataNew()
		{
			StringBuilder sb = new StringBuilder();
			DataTable dt = new DataTable();
			dt.Columns.Add("NAMN", typeof(string));
			dt.Columns.Add("VERSION_TXT", typeof(string));
			dt.Columns.Add("SCRIPT_NR", typeof(string));
			dt.Columns.Add("ANTAL_ANVANDARE", typeof(int));
			dt.Columns.Add("ANTAL_ANSTALLDA", typeof(int));
			dt.Columns.Add("ANTAL_FORETAG", typeof(int));
			dt.Columns.Add("ANTAL_ANVANDARPROFILER", typeof(int));
			dt.Columns.Add("ANTAL_PERSONPROFILER", typeof(int));
			dt.Columns.Add("ANTAL_PROGRAMPROFILER", typeof(int));
			dt.Columns.Add("ANTAL_RAPPORTER", typeof(int));
			dt.Columns.Add("ANTAL_DISTRIBUTIONER", typeof(int));
			dt.Columns.Add("ANTAL_NOTERINGAR", typeof(int));
			dt.Columns.Add("ANTAL_BEVAKNINGAR", typeof(int));
			dt.Columns.Add("ANTAL_STAMPLINGAR_TIDREG", typeof(int));
			dt.Columns.Add("ANTAL_STAMPLINGAR_FORTROENDE", typeof(int));
			dt.Columns.Add("ANTAL_HIST_ARBTID", typeof(int));
			dt.Columns.Add("ANTAL_HIST_FACK", typeof(int));
			dt.Columns.Add("ANTAL_HIST_KONTERING", typeof(int));
			dt.Columns.Add("ANTAL_HIST_LON", typeof(int));
			dt.Columns.Add("ANTAL_HIST_ORGANISATION", typeof(int));
			dt.Columns.Add("ANTAL_HIST_SEMESTER", typeof(int));
			dt.Columns.Add("ANTAL_HIST_SKATT", typeof(int));
			dt.Columns.Add("ANTAL_HIST_STATISTIK", typeof(int));
			dt.Columns.Add("ANTAL_OFFLINESTAMPLINGAR", typeof(int));
			dt.Columns.Add("ANTAL_TIDTERMINALER", typeof(int));
			dt.Columns.Add("ANTAL_PROJEKT", typeof(int));
			dt.Columns.Add("ANTAL_LARLINGAR", typeof(int));
			dt.Columns.Add("ANTAL_MEDARB_SAMT", typeof(int));
			dt.Columns.Add("ANTAL_KONTBGR", typeof(int));
			dt.Columns.Add("ANTAL_LONER", typeof(int));
			dt.Columns.Add("ANTAL_REVISIONSTILLFALLEN", typeof(int));
			dt.Columns.Add("ANTAL_RESOR", typeof(int));
			dt.Columns.Add("ANTAL_KREDITKORT", typeof(int));
			dt.Columns.Add("ANTAL_REKRYT", typeof(int));
			dt.Columns.Add("ANTAL_KOMPETENSER", typeof(int));
			dt.Columns.Add("ANTAL_UTBILDNINGAR", typeof(int));
			dt.Columns.Add("ANTAL_ACL", typeof(int));
			dt.Columns.Add("ANTAL_ACL_BESTALLNING", typeof(int));
			dt.Columns.Add("ANTAL_ACL_HISTORIK", typeof(int));
			dt.Columns.Add("ANTAL_ACL_HISTORIK_DETALJER", typeof(int));
			dt.Columns.Add("ANTAL_AVVIKELSER", typeof(int));
			return dt;
		}

		private DataTable GetDatabases()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("USE Master");
			sb.AppendLine("SELECT NAME FROM master.dbo.sysdatabases");
			sb.AppendLine("	ORDER BY NAME");
			return DataSQLQuery.ExecuteSQL(sb.ToString(), true).Tables[0];
		}

		public DataTable GetDbsStatsFromProcedure()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("DECLARE @DbsName sysname");
			sb.AppendLine("DECLARE @SQL_ANTALANVANDARE nvarchar(4000)");
			sb.AppendLine("DECLARE @AntalAnvandare int");
			sb.AppendLine("DECLARE @AntalAnstallda int");
			sb.AppendLine("DECLARE @AntalStamplingar int ");
			sb.AppendLine("DECLARE @AntalProjekt int");
			sb.AppendLine("DECLARE @AntalMedarbSamt int");
			sb.AppendLine("DECLARE @AntalKonteringsbegrepp int");
			sb.AppendLine("DECLARE @AntalLoner int");
			sb.AppendLine("DECLARE @AntalRevisionstillfallen int");
			sb.AppendLine("DECLARE @AntalResor int");
			sb.AppendLine("DECLARE @AntalRekryt int");

			sb.AppendLine("USE Master");

			sb.AppendLine("IF OBJECT_ID('tempdb..#TMP_DATABASER') IS NOT NULL");
			sb.AppendLine("DROP TABLE #TMP_DATABASER");

			sb.AppendLine("CREATE TABLE #TMP_DATABASER");
			sb.AppendLine("(");
			sb.AppendLine("  NAME VARCHAR(50),");
			sb.AppendLine("  ANTAL_ANVANDARE INT,");
			sb.AppendLine("  ANTAL_ANSTALLDA INT,");
			sb.AppendLine("  ANTAL_STAMPLINGAR INT,");
			sb.AppendLine("  ANTAL_PROJEKT INT,");
 			sb.AppendLine("		ANTAL_MEDARB_SAMT INT,");
			sb.AppendLine("  ANTAL_KONTBGR INT,");
			sb.AppendLine("  ANTAL_LONER INT,");
			sb.AppendLine("  ANTAL_REVISIONSTILLFALLEN INT,");
			sb.AppendLine("  ANTAL_RESOR INT,");
			sb.AppendLine("  ANTAL_REKRYT INT");
			sb.AppendLine(")");

			sb.AppendLine("DECLARE TEST_CURSOR CURSOR FOR	");
				sb.AppendLine("SELECT NAME FROM master.dbo.sysdatabases");
			sb.AppendLine("	ORDER BY 1");
			sb.AppendLine("OPEN TEST_CURSOR");
			sb.AppendLine("FETCH NEXT FROM TEST_CURSOR ");
			sb.AppendLine("	INTO @DbsName");
			sb.AppendLine("WHILE @@FETCH_STATUS = 0");
			sb.AppendLine("BEGIN");
			sb.AppendLine("    BEGIN TRY");
			sb.AppendLine("		--Antal användare");
			sb.AppendLine("		SET @SQL_ANTALANVANDARE = 'USE ' + @DbsName + ' SELECT @AntalAnvandare = COUNT(*) FROM BA_ANV'");
			sb.AppendLine("		EXEC sp_executesql @SQL_ANTALANVANDARE,N'@AntalAnvandare int output', @AntalAnvandare output");
			sb.AppendLine("		--Antal anställda");
			sb.AppendLine("		SET @SQL_ANTALANVANDARE = 'USE ' + @DbsName + ' SELECT @AntalAnstallda = COUNT(*) FROM BA_ANSTALLD_GRUND'");
			sb.AppendLine("		EXEC sp_executesql @SQL_ANTALANVANDARE,N'@AntalAnstallda int output', @AntalAnstallda output");
			sb.AppendLine("		--Antal stämplingar");
			sb.AppendLine("		SET @SQL_ANTALANVANDARE = 'USE ' + @DbsName + ' SELECT @AntalStamplingar = COUNT(*) FROM TI_STAMPLINGAR'");
					sb.AppendLine("EXEC sp_executesql @SQL_ANTALANVANDARE,N'@AntalStamplingar int output', @AntalStamplingar output");
			sb.AppendLine("		--Antal projekt");
			sb.AppendLine("		SET @SQL_ANTALANVANDARE = 'USE ' + @DbsName + ' SELECT @AntalProjekt = COUNT(*) FROM PR_PROJEKT'");
			sb.AppendLine("		EXEC sp_executesql @SQL_ANTALANVANDARE,N'@AntalProjekt int output', @AntalProjekt output");
			sb.AppendLine("		--Antal medarbetarsamtal");
			sb.AppendLine("		SET @SQL_ANTALANVANDARE = 'USE ' + @DbsName + ' SELECT @AntalMedarbSamt = COUNT(*) FROM PA_MEDARB_SAMT'");
			sb.AppendLine("		EXEC sp_executesql @SQL_ANTALANVANDARE,N'@AntalMedarbSamt int output', @AntalMedarbSamt output");
			sb.AppendLine("		--Antal konteringsbegrepp");
			sb.AppendLine("		SET @SQL_ANTALANVANDARE = 'USE ' + @DbsName + ' SELECT @AntalKonteringsbegrepp = COUNT(*) FROM BA_KONTBGR'");
			sb.AppendLine("		EXEC sp_executesql @SQL_ANTALANVANDARE,N'@AntalKonteringsbegrepp int output', @AntalKonteringsbegrepp output");
			sb.AppendLine("		--Antal löner");
			sb.AppendLine("		SET @SQL_ANTALANVANDARE = 'USE ' + @DbsName + ' SELECT @AntalLoner = COUNT(*) FROM BA_LONPER_PERS'");
			sb.AppendLine("		EXEC sp_executesql @SQL_ANTALANVANDARE,N'@AntalLoner int output', @AntalLoner output");
			sb.AppendLine("		--Antal revisionstillfällen");
			sb.AppendLine("		SET @SQL_ANTALANVANDARE = 'USE ' + @DbsName + ' SELECT @AntalRevisionstillfallen = COUNT(*) FROM LR_REVTILLF'");
			sb.AppendLine("		EXEC sp_executesql @SQL_ANTALANVANDARE,N'@AntalRevisionstillfallen int output', @AntalRevisionstillfallen output");
			sb.AppendLine("		--Antal resor");
			sb.AppendLine("		SET @SQL_ANTALANVANDARE = 'USE ' + @DbsName + ' SELECT @AntalResor = COUNT(*) FROM RE_RESOR'");
			sb.AppendLine("		EXEC sp_executesql @SQL_ANTALANVANDARE,N'@AntalResor int output', @AntalResor output");
			sb.AppendLine("		--Antal rekryteringar");
			sb.AppendLine("		SET @SQL_ANTALANVANDARE = 'USE ' + @DbsName + ' SELECT @AntalRekryt = COUNT(*) FROM PA_JOBB'");
					sb.AppendLine("EXEC sp_executesql @SQL_ANTALANVANDARE,N'@AntalRekryt int output', @AntalRekryt output");
		
		
			sb.AppendLine("		INSERT INTO #TMP_DATABASER (NAME, ANTAL_ANVANDARE, ANTAL_ANSTALLDA, ANTAL_STAMPLINGAR, ANTAL_PROJEKT, ANTAL_MEDARB_SAMT, ANTAL_KONTBGR,	");
			sb.AppendLine("					ANTAL_LONER, ANTAL_REVISIONSTILLFALLEN, ANTAL_RESOR, ANTAL_REKRYT)");
			sb.AppendLine("			VALUES (@DbsName, @AntalAnvandare, @AntalAnstallda, @AntalStamplingar, @AntalProjekt, @AntalMedarbSamt, @AntalKonteringsbegrepp,");
			sb.AppendLine("					@AntalLoner, @AntalRevisionstillfallen, @AntalResor, @AntalRekryt)");
	
			sb.AppendLine("	END TRY	");
			sb.AppendLine("	BEGIN CATCH");
			sb.AppendLine("	--Empty catch");
			sb.AppendLine("	END CATCH	");
			sb.AppendLine("	FETCH NEXT FROM TEST_CURSOR ");
			sb.AppendLine("		INTO @DbsName");
			sb.AppendLine("END	");
			sb.AppendLine("CLOSE TEST_CURSOR");
			sb.AppendLine("DEALLOCATE TEST_CURSOR");
			sb.AppendLine("SELECT * FROM #TMP_DATABASER	");
	
			
			return DataSQLQuery.ExecuteSQL(sb.ToString(), true).Tables[0];
		}

	}
}