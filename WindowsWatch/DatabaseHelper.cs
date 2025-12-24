using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace WindowsWatch
{
    public static class DatabaseHelper
    {
        
        private static string connectionString = "Host=localhost;Port=5432;Database=WatchShopDB;Username=postgres;Password=1234567890";

        public static DataTable ExecuteQuery(string query, NpgsqlParameter[] parameters = null)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    DataTable dt = new DataTable();
                    try
                    {
                        con.Open();
                        NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка БД: " + ex.Message);
                        return null;
                    }
                    return dt;
                }
            }
        }

        public static void ExecuteNonQuery(string query, NpgsqlParameter[] parameters = null)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка выполнения: " + ex.Message);
                    }
                }
            }
        }
    }
}