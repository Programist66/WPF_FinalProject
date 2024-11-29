using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_FinalProject.model;

namespace WPF_FinalProject.HelperClasses
{
    public class DBWorker
    {
        private static DBConnection? _connection;

        public DBWorker() 
        {
            if (_connection is null)
            {
                _connection = new DBConnection();
            }            
        }
        #region "Users Table"
        public User? GetUserById(int id)
        {
            string query = "SELECT * FROM users WHERE id = @id";
            _connection!.DisposeConnection();
            _connection!.Command = new MySqlCommand(query, _connection.Connection);
            _connection.Command.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader = _connection.Command.ExecuteReader();

            if (!reader.HasRows)
            {
                return null;
            }
            reader.Read();
            User user = new User(reader.GetInt16("id")) 
            {
                Surname = reader.GetString("Surname"),
                Name = reader.GetString("Name"),
                Patronymic = reader.GetString("Patronymic")
            };
            int department_id = reader.GetInt16("Department_id");
            int position_id = reader.GetInt16("Position_id");
            int office_id = reader.GetInt16("Office_id");
            reader.Close();
            user.Department = GetDepartmentById(department_id)!;
            user.Position = GetPositionById(position_id)!;
            user.Office = GetOfficeById(office_id)!;            
            return user;
        }

        public IReadOnlyList<User>? GetUsers() 
        {
            List<User> users = new();
            _connection!.DisposeConnection();
            string query = "SELECT * FROM users";
            _connection!.Command = new MySqlCommand(query, _connection.Connection);
            MySqlDataReader reader = _connection.Command.ExecuteReader();

            if (!reader.HasRows)
            {
                return users;
            }
            while (reader.Read())
            {
                users.Add(new User(reader.GetInt16("id")));
            }
            reader.Close();
            for (int i = 0; i < users.Count; i++)
            {
                users[i] = GetUserById(users[i].Id)!;
            }
            return users;
        }

        public void UpdateUser(User user) 
        {
            string query = "UPDATE users SET " +
                "Surname = @Surname, " +
                "Name = @Name, " +
                "Patronymic = @Patronymic, " +
                "Department_id = @Department_id, " +
                "Position_id = @Position_id, " +
                "Office_id = @Office_id " +
                "WHERE (id = @id);";
            _connection!.DisposeConnection();
            _connection.Command = new MySqlCommand(query, _connection.Connection);
            _connection.Command.Parameters.AddWithValue("@Surname", user.Surname);
            _connection.Command.Parameters.AddWithValue("@Name", user.Name);
            _connection.Command.Parameters.AddWithValue("@Patronymic", user.Patronymic);
            _connection.Command.Parameters.AddWithValue("@Department_id", user.Department.Id);
            _connection.Command.Parameters.AddWithValue("@Position_id", user.Position.Id);
            _connection.Command.Parameters.AddWithValue("@Office_id", user.Office.Id);
            _connection.Command.Parameters.AddWithValue("@id", user.Id);
            _connection.Command.ExecuteNonQuery();            
        }

        public void AddUser(User user) 
        {
            string query = "INSERT INTO users " +
                "(Surname, Name, Patronymic, Department_id, Position_id, Office_id)" +
                "VALUES(@Surname, @Name, @Patronymic, @Department_id, @Position_id, @Office_id)";
            _connection!.DisposeConnection();
            _connection.Command = new MySqlCommand(query, _connection.Connection);
            _connection.Command.Parameters.AddWithValue("@Surname", user.Surname);
            _connection.Command.Parameters.AddWithValue("@Name", user.Name);
            _connection.Command.Parameters.AddWithValue("@Patronymic", user.Patronymic);
            _connection.Command.Parameters.AddWithValue("@Department_id", user.Department.Id);
            _connection.Command.Parameters.AddWithValue("@Position_id", user.Position.Id);
            _connection.Command.Parameters.AddWithValue("@Office_id", user.Office.Id);
            _connection.Command.ExecuteNonQuery();            
        }

        public void DeleteUserById(int id) 
        {
            string query = "DELETE FROM users WHERE id = @id;";
            _connection!.DisposeConnection();
            _connection.Command = new MySqlCommand(query, _connection.Connection);
            _connection.Command.Parameters.AddWithValue("@id", id);
            _connection.Command.ExecuteNonQuery();
        }
        #endregion
        #region "Departments Table"
        public Department? GetDepartmentById(int id)
        {
            string query = "SELECT * FROM departments WHERE id = @id";
            _connection!.DisposeConnection();
            _connection!.Command = new MySqlCommand(query, _connection.Connection);
            _connection.Command.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader = _connection.Command.ExecuteReader();

            if (!reader.HasRows)
            {
                return null;
            }
            reader.Read();
            Department department = new Department(reader.GetInt16("id"), reader.GetString("Name"));
            reader.Close();
            return department;
        }

        public IReadOnlyList<Department>? GetDepartments() 
        {
            List<Department> departments = new();
            _connection!.DisposeConnection();
            string query = "SELECT * FROM departments";
            _connection!.Command = new MySqlCommand(query, _connection.Connection);
            MySqlDataReader reader = _connection.Command.ExecuteReader();

            if (!reader.HasRows)
            {
                return departments;
            }
            while (reader.Read()) 
            {
                departments.Add(new Department(reader.GetInt16("id"), reader.GetString("Name")));
            }
            reader.Close();
            return departments;
        }

        public void UpdateDepartment(Department department)
        {
            string query = "UPDATE departments SET Name = Name WHERE (id = @id);";
            _connection!.DisposeConnection();
            _connection.Command = new MySqlCommand(query, _connection.Connection);
            _connection.Command.Parameters.AddWithValue("@Name", department.Name);
            _connection.Command.Parameters.AddWithValue("@id", department.Id);
            _connection.Command.ExecuteNonQuery();
        }

        public void AddDepartment(Department department)
        {
            string query = "INSERT INTO departments (Name) VALUES(@Name)";
            _connection!.DisposeConnection();
            _connection.Command = new MySqlCommand(query, _connection.Connection);
            _connection.Command.Parameters.AddWithValue("@Name", department.Name);
            _connection.Command.ExecuteNonQuery();
        }

        public void DeleteDepartmentById(int id)
        {
            string query = "DELETE FROM departments WHERE id = @id;";
            _connection!.DisposeConnection();
            _connection.Command = new MySqlCommand(query, _connection.Connection);
            _connection.Command.Parameters.AddWithValue("@id", id);
            _connection.Command.ExecuteNonQuery();
        }

        #endregion
        #region "Positions Table"
        public Position? GetPositionById(int id)
        {
            _connection!.DisposeConnection();
            string query = "SELECT * FROM positions WHERE id = @id";
            _connection!.Command = new MySqlCommand(query, _connection.Connection);
            _connection.Command.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader = _connection.Command.ExecuteReader();

            if (!reader.HasRows)
            {
                return null;
            }
            reader.Read();
            Position position = new Position(reader.GetInt16("id"), reader.GetString("Name"), reader.GetDecimal("Salary"));
            reader.Close();
            return position;
        }

        public IReadOnlyList<Position>? GetPositions() 
        {
            List<Position> positions = new();
            _connection!.DisposeConnection();
            string query = "SELECT * FROM positions";
            _connection!.Command = new MySqlCommand(query, _connection.Connection);
            MySqlDataReader reader = _connection.Command.ExecuteReader();

            if (!reader.HasRows)
            {
                return positions;
            }
            while (reader.Read())
            {
                positions.Add(new Position(reader.GetInt16("id")));
            }
            reader.Close();
            for (int i = 0; i < positions.Count; i++)
            {
                positions[i] = GetPositionById(positions[i].Id)!;
            }
            return positions;
        }

        public void UpdatePosition(Position position)
        {
            string query = "UPDATE positions SET Name = @Name, Salary = @Salary WHERE (id = @id);";
            _connection!.DisposeConnection();
            _connection.Command = new MySqlCommand(query, _connection.Connection);
            _connection.Command.Parameters.AddWithValue("@Name", position.Name);
            _connection.Command.Parameters.AddWithValue("@Salary", position.Salary);
            _connection.Command.Parameters.AddWithValue("@id", position.Id);
            _connection.Command.ExecuteNonQuery();
        }

        public void AddPosition(Position position)
        {
            string query = "INSERT INTO positions (Name, Salary) VALUES(@Name, @Salary)";
            _connection!.DisposeConnection();
            _connection.Command = new MySqlCommand(query, _connection.Connection);
            _connection.Command.Parameters.AddWithValue("@Name", position.Name);
            _connection.Command.Parameters.AddWithValue("@Salary", position.Salary);
            _connection.Command.ExecuteNonQuery();
        }

        public void DeletePositionById(int id)
        {
            string query = "DELETE FROM positions WHERE id = @id;";
            _connection!.DisposeConnection();
            _connection.Command = new MySqlCommand(query, _connection.Connection);
            _connection.Command.Parameters.AddWithValue("@id", id);
            _connection.Command.ExecuteNonQuery();
        }

        #endregion
        #region "Offices Table"
        public Office? GetOfficeById(int id)
        {
            string query = "SELECT * FROM offices WHERE id = @id";
            _connection!.DisposeConnection();
            _connection!.Command = new MySqlCommand(query, _connection.Connection);
            _connection.Command.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader = _connection.Command.ExecuteReader();

            if (!reader.HasRows)
            {
                return null;
            }
            reader.Read();
            Office office = new Office(reader.GetInt16("id"))
            {
                Name = reader.GetString("Name")
            };
            int department_id = reader.GetInt16("Department_id");
            reader.Close();
            office.Department = GetDepartmentById(department_id)!;            
            return office;
        }

        public IReadOnlyList<Office>? GetOffices() 
        {
            List<Office> offices = new();
            _connection!.DisposeConnection();
            string query = "SELECT * FROM offices";
            _connection!.Command = new MySqlCommand(query, _connection.Connection);
            MySqlDataReader reader = _connection.Command.ExecuteReader();

            if (!reader.HasRows)
            {
                return offices;
            }
            while (reader.Read())
            {
                offices.Add(new Office(reader.GetInt16("id")));
            }
            reader.Close();
            for (int i = 0; i < offices.Count; i++)
            {
                offices[i] = GetOfficeById(offices[i].Id)!;
            }
            return offices;
        }

        public void UpdateOffice(Office office)
        {
            string query = "UPDATE offices SET Name = @Name, Department_id = @Department_id WHERE (id = @id);";
            _connection!.DisposeConnection();
            _connection.Command = new MySqlCommand(query, _connection.Connection);
            _connection.Command.Parameters.AddWithValue("@Name", office.Name);
            _connection.Command.Parameters.AddWithValue("@Department_id", office.Department.Id);
            _connection.Command.Parameters.AddWithValue("@id", office.Id);
            _connection.Command.ExecuteNonQuery();
        }

        public void AddOffice(Office office)
        {
            string query = "INSERT INTO offices (Name, Department_id) VALUES(@Name, @Department_id)";
            _connection!.DisposeConnection();
            _connection.Command = new MySqlCommand(query, _connection.Connection);
            _connection.Command.Parameters.AddWithValue("@Name", office.Name);
            _connection.Command.Parameters.AddWithValue("@Department_id", office.Department.Id);
            _connection.Command.ExecuteNonQuery();
        }

        public void DeleteOfficeById(int id)
        {
            string query = "DELETE FROM offices WHERE id = @id;";
            _connection!.DisposeConnection();
            _connection.Command = new MySqlCommand(query, _connection.Connection);
            _connection.Command.Parameters.AddWithValue("@id", id);
            _connection.Command.ExecuteNonQuery();
        }

        #endregion
    }
}
