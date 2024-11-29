using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_FinalProject.HelperClasses
{
    public class DBConnection
    {
        static string connectionString = "server=localhost;user=root;password=Rotter;database=wpf_finalproject";

        private static MySqlConnection? connection;

        public MySqlConnection? Connection
        {
            get
            {
                if (connection is null)
                {
                    return DBConnent();
                }
                else
                {
                    return connection;
                }
            }
            private set => connection = value;
        }
        public MySqlCommand Command { get; set; }

        public DBConnection()
        {
            if (connection is null)
            {
                DBConnent();
            }
        }

        private MySqlConnection? DBConnent()
        {
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
                return Connection;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к БД:\n {ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public bool DBCloseConnection()
        {
            try
            {
                connection?.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка закрытия подключения к БД:\n {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        public bool DisposeConnection() 
        {
            try
            {
                connection?.Close();
                connection?.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка закрытия подключения к БД:\n {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
