using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_FinalProject.model;

namespace WPF_FinalProject
{
    public class User
    {
        public int Id { get; private set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public Department Department { get; set; }
        public Position Position { get; set; }
        public Office Office { get; set; }

        
        public User(int id) 
        {
            Id = id;
        }

        public User(int id, string surname, string name, string patronymic, Department department, Position position, Office office)
        {
            Id = id;
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Department = department;
            Position = position;
            Office = office;
        }

        public override string ToString()
        {
            return $"{Surname} {Name} {Patronymic} {Department} {Position} {Office}";
        }
    }
}
