using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Class1
    {
        Sample c;

        public Class1()
        {
            c += () => Console.WriteLine("Hello");
        }
    }

    delegate void Sample();
}
