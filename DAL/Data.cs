namespace LibraryCenter.DAL
{
	public class Data
	{

		string ConnectionString = "" +
		   "server=ULIK-DESKTOP1\\SQLEXPRESS;" +
		   "initial catalog=LibraryCenter;" +
		   "user id=sa;" +
		   "password=1234;" +
		   "TrustServerCertificate=Yes";

		private Data()
		{
			Layer = new DataLayer(ConnectionString);

		}
		//משתנה סטטי לשמירת מופע יחיד של המחלקה
		static Data? GetData;

		
		public static DataLayer Get
		{
			get

			{
				if (GetData == null)
				{
					GetData = new Data();

				}
				return GetData.Layer;

			}
		}

		public DataLayer Layer { get; set; }
	}

}
