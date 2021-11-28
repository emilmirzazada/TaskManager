using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Entities
{
    public class Organization
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phonenumber { get; set; }
        public string Address { get; set; }
        public IEnumerable<Issue> Issues { get; private set; } = new HashSet<Issue>();
        public IEnumerable<Employee> Employees { get; private set; } = new HashSet<Employee>();
    }
}
