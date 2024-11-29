using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_FinalProject.model
{
    public class Office
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public Department Department { get; set; }

        public Office (int id) 
        {
            Id = id;
        }

        public Office(int id, string name, Department department)
        {
            Id = id;
            Name = name;
            Department = department;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
