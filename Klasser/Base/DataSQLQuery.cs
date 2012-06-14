using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace AnalyzeDbs.Klasser.Base
{
	public class DataSQLQuery
	{
		#region Lokala variabler
		private static string m_Anvandare;
		static private string m_Losen;
		static private string m_ServerName;
		static private string m_DatabaseName;
		#endregion

		#region properties
		static public string ServerName
		{
			get	{	return m_ServerName;	}
			set {	m_ServerName = value;	}
		}

		static public string DatabaseName
		{
			get	{	return m_DatabaseName;	}
			set {	m_DatabaseName = value;	}
		}

		static public string Anvandare
		{
			get
			{
				if (m_Anvandare == string.Empty)
					m_Anvandare = "ALWAR";
				return m_Anvandare;
			}
			set	{	m_Anvandare = value;	}
		}

		static public string Losen
		{
			get
			{
				if (m_Losen == string.Empty)
					m_Losen = "ALWAR";
				return m_Losen;
			}
			set	{	m_Losen = value; }
		}
		#endregion

		#region Constructor
		public DataSQLQuery(string serverName, string database, string anvandare, string losen)
		{
			m_Anvandare = anvandare;
			m_Losen = losen;
			m_DatabaseName = database;
			m_ServerName = serverName;
		}
		#endregion

		#region Funktioner
		public static DataSet ExecuteSQL(string SQLstring, ArrayList SQLParams)
		{
			return ExecuteSQL(SQLstring, SQLParams, false);
		}
        public static DataSet ExecuteSQL(string SQLstring, bool Felhantering)
        {
            return ExecuteSQL(SQLstring, new ArrayList(), Felhantering);
        }


		public static DataSet ExecuteSQL(string SQLstring, ArrayList SQLParams, bool Felhantering)
		{
			string ConnStr = "Data Source=" + ServerName;
			ConnStr = ConnStr + ";Initial Catalog=" + DatabaseName;
			ConnStr = ConnStr + ";Persist Security Info=True;";
			ConnStr = ConnStr + "User ID=" + Anvandare;
			ConnStr = ConnStr + "; pwd=" + Losen;

			SqlConnection Conn = new SqlConnection(ConnStr);
			DataSet DS = new DataSet();
			try
			{
				Conn.Open();
				SqlDataAdapter Adp = new SqlDataAdapter();
				Adp.SelectCommand = new SqlCommand(SQLstring, Conn);
				foreach (SqlParameter Param in SQLParams)
				{
					Adp.SelectCommand.Parameters.Add(Param);
				}
				Adp.Fill(DS);
				Conn.Close();
			}

			catch (Exception e)
			{
				if (Felhantering  && !e.Message.Contains("Login failed for user " + Anvandare)) 
					throw e;
//				string s = e.Message;
			}
			return DS;
		}

		public static DataSet ExecuteSQL(string SQLstring)
		{
			return ExecuteSQL(SQLstring, new ArrayList());
		}

		public static DataSet ExecuteSQL(string SQLstring, string sDBS, bool Felhantering )
		{
			string m_DBS = DatabaseName;
			DatabaseName = sDBS;
			DataSet DS = ExecuteSQL(SQLstring, new ArrayList(), Felhantering);
			DatabaseName = m_DBS;
			return DS;
		}

		public static bool InsertValue(string SQLstring, ArrayList SQLParams)
		{
			try
			{
				string ConnStr = "Data Source=" + ServerName;
				ConnStr = ConnStr + ";Initial Catalog=" + DatabaseName;
				ConnStr = ConnStr + ";Persist Security Info=True;";
				ConnStr = ConnStr + "User ID=" + Anvandare;
				ConnStr = ConnStr + "; pwd=" + Losen;
				SqlConnection Conn = new SqlConnection(ConnStr);
				Conn.Open();
				SqlDataAdapter Adp = new SqlDataAdapter();
				Adp.SelectCommand = new SqlCommand(SQLstring, Conn);
				foreach (SqlParameter Param in SQLParams)
				{
					Adp.SelectCommand.Parameters.Add(Param);
				}
				DataSet DS = new DataSet();
				Adp.Fill(DS);
				Conn.Close();
				return true;
			}
			catch //(Exception e)
			{
				return false;
			}
		}

		public static bool InsertValue(string SQLstring)
		{
			return InsertValue(SQLstring, new ArrayList());
		}

		public static bool DeleteValue(string SQLstring, ArrayList SQLParams)
		{
			return InsertValue(SQLstring, SQLParams);
		}

		public static bool DeleteValue(string SQLstring)
		{
			return InsertValue(SQLstring);
		}

		private static bool UpdateSQL(string SQLstring, ArrayList SQLParams)
		{
			bool result = true;
			string ConnStr = "Data Source=" + ServerName;
			ConnStr = ConnStr + ";Initial Catalog=" + DatabaseName;
			ConnStr = ConnStr + ";Persist Security Info=True;";
			ConnStr = ConnStr + "User ID=" + Anvandare;
			ConnStr = ConnStr + "; pwd=" + Losen;

			SqlConnection Conn = new SqlConnection(ConnStr);
			DataSet DS = new DataSet();
			try
			{
				Conn.Open();
				SqlDataAdapter Adp = new SqlDataAdapter();
				Adp.SelectCommand = new SqlCommand(SQLstring, Conn);
				foreach (SqlParameter Param in SQLParams)
				{
					Adp.SelectCommand.Parameters.Add(Param);
				}
				Adp.Fill(DS);
			}
			catch (Exception e)
			{
				string s = e.Message;
				result = false;
			}
			return result;
		}

		public static bool UpdateValueWithRollback(string SQLstring, ArrayList SQLParams)
		{
			string s = "BEGIN TRAN" + Environment.NewLine;
			s = s + "" + Environment.NewLine;
			SQLstring = s + SQLstring + Environment.NewLine;
			s = "ROLLBACK";
			SQLstring = SQLstring + s;
			return UpdateSQL(SQLstring, SQLParams);
		}
		#endregion
	}
}
