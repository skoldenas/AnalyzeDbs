using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalyzeDbs.Klasser.Base
{
	public class UserInfo
	{
		string m_ServerName = string.Empty;
		string m_Database = string.Empty;
		string m_UserName = string.Empty;
		string m_Password = string.Empty;
		string m_Description = string.Empty;
		int m_Id = 0;

		public int Id
		{
			get { return m_Id; }
			set { m_Id = value; }
		}

		public string ServerName
		{
			get { return m_ServerName; }
			set { m_ServerName = value; }
		}
		public string Database
		{
			get { return m_Database; }
			set { m_Database = value; }
		}
		public string UserName
		{
			get { return m_UserName; }
			set { m_UserName = value; }
		}
		public string Password
		{
			get { return m_Password; }
			set { m_Password = value; }
		}
		public string Description
		{
			get { return m_Description; }
			set { m_Description = value; }
		}


		//public UserInfo()
		//  : this("ALASQL2KUTV", "AgdaTest_20122_Projekt", "web", "web", "Projekt 2012.2")
		//{
		//}

		public UserInfo(int id, string serverName, string database, string userName, string password, string description)
		{
			Id = id;
			ServerName = serverName;
			Database = database;
			UserName = userName;
			Password = password;
			Description = description;
		}
	}

	public class UserInfoCollection : List<UserInfo>
	{
		public UserInfoCollection()
		{
			if (this.Count == 0)
				InitColl();
		}
		private void InitColl()
		{
			//this.Add(new UserInfo(1, "ALASQL2KUTV", "AgdaTest_20122_Projekt", "web", "web", "Projekt 2012.2"));
			this.Add(new UserInfo(2, "ALASQL2KUTV", "AgdaTest_20122_Flodestest", "web", "web", "Flödestest 2012.2"));
			this.Add(new UserInfo(3, "ALASQL2KUTV", "AgdaTest_20132_Projekt", "web", "web", "Projekt 2013.2"));
		}

		public UserInfo FindById(int id)
		{
			foreach (UserInfo UI in this)
			{
				if (UI.Id == id)
					return UI;
			}
			throw new Exception("Testprogrammet kunde inte återfinnas.");
		}
	}
}
