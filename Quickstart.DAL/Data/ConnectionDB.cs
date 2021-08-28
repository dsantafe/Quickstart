using System.Configuration;
using System.Data.SqlClient;

namespace Quickstart.DAL.Data
{
    public class ConnectionDB
    {
        private static SqlConnection connection;

        public static SqlConnection GetInstance()
        {
            if (connection == null)
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Quickstart"].ToString());
                connection.Open();
            }

            return connection;
        }
    }
}
