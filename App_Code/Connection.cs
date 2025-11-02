using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace GuitarCentre.App_Code
{
    public class Connection : IDisposable
    {
        private readonly string _connectionString;
        private SqlConnection _conn;

        public Connection()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString;
            _conn = new SqlConnection(_connectionString);
        }

        public void OpenConnection()
        {
            if (_conn.State != ConnectionState.Open)
                _conn.Open();
        }

        public void CloseConnection()
        {
            if (_conn.State != ConnectionState.Closed)
                _conn.Close();
        }

        public void ExecuteQuery(string query)
        {
            using (SqlCommand cmd = new SqlCommand(query, _conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public SqlDataReader ExecuteReader(string query)
        {
            SqlCommand cmd = new SqlCommand(query, _conn);
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public DataTable GetDataTable(string query)
        {
            using (SqlDataAdapter da = new SqlDataAdapter(query, _conn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public SqlCommand Command(string query)
        {
            return new SqlCommand(query, _conn);
        }

        public void Dispose()
        {
            if (_conn != null)
            {
                if (_conn.State != ConnectionState.Closed)
                    _conn.Close();

                _conn.Dispose();
                _conn = null;
            }
        }
    }
}
