using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_FinalProject.model
{
    public class Position
    {
        public int Id {  get; private set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }

        public Position(int id)
        {
            Id = id;
        }

        public Position(int id, string name, decimal salary)
        {
            Id = id;
            Name = name;
            Salary = salary;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
