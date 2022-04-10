using System.Collections.Generic;
using System.Data.SqlClient;

namespace AdoDotNetExample
{
    class DataUtility
    {
        private readonly string _connectionString;
        public DataUtility(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Sql Insert, Update, Delete Operations
        // Without Parameter 
        public void ExecuteCommand(string query)
        {
            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = query;

            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            command.ExecuteNonQuery();
        }

        // Sql Insert, Update, Delete Operations
        // With Parameter
        public void ExecuteCommand(string query, List<(string key, object value)> parameters)
        {
            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = query;

            if (parameters != null && parameters.Count > 0)
            {
                foreach (var (key, value) in parameters)
                {
                    command.Parameters.Add(new SqlParameter(key, value));
                }
            }

            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            command.ExecuteNonQuery();
        }

        // Sql Select Operations
        // Without Parameter
        public IList<Dictionary<string, object>> GetDatas(string query)
        {
            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = query;

            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            List<Dictionary<string, object>> columns = new();

            while (reader.Read())
            {
                //int id = (int)reader[0];
                //int id = (int)reader["name"];
                //var columns = reader.GetColumnSchema();
                //string column1 = columns[0].ColumnName;

                Dictionary<string, object> rows = new();
                foreach (var column in reader.GetColumnSchema())
                {
                    rows.Add(column.ColumnName, reader[column.ColumnName]);
                }
                columns.Add(rows);
            }

            return columns;
        }

        // Sql Select Operations
        // With Parameter
        public IList<Dictionary<string, object>> GetDatas(string query, List<(string, object)> parameters)
        {
            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = query;

            if (parameters != null && parameters.Count > 0)
            {
                foreach (var (key, value) in parameters)
                {
                    command.Parameters.Add(new SqlParameter(key, value));
                }
            }

            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            List<Dictionary<string, object>> columns = new();

            while (reader.Read())
            {
                Dictionary<string, object> rows = new();
                foreach (var column in reader.GetColumnSchema())
                {
                    rows.Add(column.ColumnName, reader[column.ColumnName]);
                }
                columns.Add(rows);
            }

            return columns;
        }
    }
}
