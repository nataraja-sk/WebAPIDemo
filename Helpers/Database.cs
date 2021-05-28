using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIDemo.DataBase
{
    public class Database
    {
        private static String connString = "Data Source=NSK-DESKTOP\\SQLEXPRESS;Initial Catalog=StudentDDetails;Integrated Security=True;";
        /// <summary>
        /// Get query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataTable GetData(string query)
        {
            DataTable result = new DataTable();
            SqlDataReader reader;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    reader = command.ExecuteReader();
                    result.Load(reader);
                    reader.Close();
                    conn.Close();
                }
            }
            return result;
        }
        /// <summary>
        /// Update operations
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public int UpdateData(string query)
        {
            int rows = -1;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    rows = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return rows;
        }
    }
}
