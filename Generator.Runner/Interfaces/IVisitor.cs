using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Runner.Interfaces
{
    public interface IVisitor
    {
        public void Accept(object tree);
        
        public void Visit(object something);
    }
}
