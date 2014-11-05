using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static partial class PersonDomain
    {
        public class Person
        {
            protected internal  int     PersonId   { get; set; }
            public              string  PersonName { get; set; }
        }
    }
}
