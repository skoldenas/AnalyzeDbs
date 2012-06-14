using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalyzeDbs.Klasser.Base
{
	public class FoBase
	{
		private DataSQLQuery m_SQLManager = null;
		private UserInfo m_UserInfo = null;

		public DataSQLQuery SQLManager
		{
			get { return m_SQLManager; }
			set { m_SQLManager = value; }
		}

		public FoBase(UserInfo userInfo)
		{
			if (userInfo == null)
				userInfo = new UserInfo(1, "ALAX64\\SQL2005", "MASTER", "alwar", "alwar", "ALAX64\\SQL2005");
			m_SQLManager = new DataSQLQuery(userInfo.ServerName, userInfo.Database, userInfo.UserName, userInfo.Password);
			m_UserInfo = userInfo;

		}
	}
}
