using Generator.Runner.Attributes;
using Generator.Runner.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Runner.Visitors
{
    [TSQLVisitor]
    public class SomethingVisitor : IVisitor
    {
        public void Accept(object tree)
        {
            throw new NotImplementedException();
        }

        public void Visit(object something)
        {
            Console.WriteLine("Ciao da something");
        }
    }
}
