// See https://aka.ms/new-console-template for more information
using Generator.Runner;
using Generator.Runner.Interfaces;
using System.Runtime.CompilerServices;

Console.WriteLine("Hello, World!");


foreach(var visitor in VisitorsLister.GetAvailableVisitors())
{
    (visitor as IVisitor).Visit(null!);
}