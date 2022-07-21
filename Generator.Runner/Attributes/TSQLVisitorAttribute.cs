using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Runner.Attributes
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TSQLVisitorAttribute : Attribute
    {
    }

}
