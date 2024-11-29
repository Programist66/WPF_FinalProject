using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_FinalProject.HelperClasses
{
    public class Authorization
    {
        private static DBConnection db;
        public Authorization()
        {
            if (db is null)
            {
                db = new DBConnection();
            }
        }

        public User? GetUser(string login, string password)
        {
            string query = "SELECT Password, User_id FROM users_login WHERE login = @login";
            db.Command = new MySqlCommand(query, db.Connection);
            db.Command.Parameters.AddWithValue("@login", login);
            MySqlDataReader reader = db.Command.ExecuteReader();

            if (!reader.HasRows)
            {
                reader.Close();
                return null;
            }
            reader.Read();
            string hashedPassword = reader.GetString("Password");
            if (!PasswordHasher.VerifyPassword(password, hashedPassword))
            {
                reader.Close();
                return null;
            }
            int id = reader.GetInt16("User_id");
            reader.Close();
            return new DBWorker().GetUserById(id);
        }        
    }
}
