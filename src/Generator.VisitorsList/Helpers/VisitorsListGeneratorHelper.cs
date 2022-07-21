using System;
using System.Collections.Generic;
using System.Text;

namespace Generator.VisitorsList.Helpers
{
    public static class VisitorsListGeneratorHelper
    {
        public const string Attribute = @"
                namespace Generator.VisitorsList{
                    [System.AttributeUsage(System.AttributeTargets.Class)]
                    public class TSQLVisitorAttribute : System.Attribute
                    {
                    }
                }";
    }
}
