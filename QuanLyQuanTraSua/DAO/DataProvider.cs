using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanTraSua.DAO
{
    public class DataProvider
    {
        private static string connectionStr = @"Data Source=LAPTOPCUAQUYEN\LEQUYEN;Initial Catalog=QLTiemtrasuatakeaway;Integrated Security=True;";
        public DataTable ExecuteQuery(string query)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
                connection.Close();
            }
            return data;

        }
        public object ExecuteScalar(string query)
        {
            object result = null;

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    result = command.ExecuteScalar();
                }
            }

            return result;
        }
        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    return command.ExecuteNonQuery();
                }
            }
        }

        private static DataProvider instance;
        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return instance; }
            private set { instance = value; }
        }
   

    }
}
