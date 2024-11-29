using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_FinalProject.model
{
    public class Department
    {
        public int Id { get; private set; }
        public string Name { get; set; }

        public Department (int id) 
        {
            Id = id;
        }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString() 
        {
            return Name;
        }
    }
}
