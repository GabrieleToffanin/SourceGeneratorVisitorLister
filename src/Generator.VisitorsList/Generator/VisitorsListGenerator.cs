using Generator.VisitorsList.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Text;

namespace Generator.VisitorsList.Generator
{
    [Generator(LanguageNames.CSharp)]
    public class VisitorsListGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var classesTypes = context.SyntaxProvider
                .CreateSyntaxProvider(CouldBeVisitorAsync,
                    GetClassTypeOrNull)
                .Where(type => type is not null)
                .Collect();

            context.RegisterSourceOutput(classesTypes, GenerateCode);
        }

        private void GenerateCode(SourceProductionContext context, ImmutableArray<ITypeSymbol> classes)
        {
            if (classes.IsDefaultOrEmpty)
                return;

            var sb = new StringBuilder();
            sb.Append(@"
                namespace Generator.Runner
                {
                    public static class VisitorsLister
                        {
                            public static List<object> Visitors = new List<object>();
                                
                            static VisitorsLister()
                            {");
            foreach (var name in classes)
            {
                sb.AppendLine($"Visitors.Add(new {name}());");
            }
            sb.Append(@"}
                public static IEnumerable<object> GetAvailableVisitors()
                        {
                            foreach(var visitor in Visitors)
                                {
                                    yield return visitor;
                                }
                                
                        }
                    }
                }");

            context.AddSource($"{classes[0].ContainingNamespace}VisitorsLister.g.cs", sb.ToString());
        }

        private bool CouldBeVisitorAsync(SyntaxNode node, CancellationToken token)
        {
            if (node is not AttributeSyntax attribute)
                return false;

            var name = ExtractName(attribute.Name);

            return name is "TSQLVisitor" or "TSQLVisitorAttribute";
        }


        // Attributo
        //     |
        // class dhjawodhaowdih
        private static ITypeSymbol? GetClassTypeOrNull(GeneratorSyntaxContext ctx, CancellationToken token) 
        {
            var attributeSyntax = (AttributeSyntax)ctx.Node;

            if (attributeSyntax.Parent?.Parent is not ClassDeclarationSyntax classDeclaration)
                return null;

            var type = ctx.SemanticModel.GetDeclaredSymbol(classDeclaration) as ITypeSymbol;
            var current = type is null || !IsVisitor(type) ? null : type;


            return current;
        } 

        private static bool IsVisitor(ISymbol type)
        {
            var current = type.GetAttributes().Any(a => a.AttributeClass?.Name == "TSQLVisitorAttribute");

            return current;
        }

        private static string? ExtractName(NameSyntax name)
        {
            while (name != null)
            {
                switch (name)
                {
                    case IdentifierNameSyntax ins:
                        return ins.Identifier.Text;
                    case QualifiedNameSyntax qns:
                        name = qns.Right;
                        break;
                    default: return null;

                }
            }

            return null;
        }

        
    }
}
