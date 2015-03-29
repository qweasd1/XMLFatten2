using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.IO;

namespace CodeDomFluentHelper
{
    public static class Generator
    {
        private static CodeDomProvider codeProvider;
        private static CodeGeneratorOptions codeGeneratorOptions;

        static Generator()
        {
            codeProvider = CodeDomProvider.CreateProvider("c#");
            codeGeneratorOptions = new CodeGeneratorOptions()
            {
                BlankLinesBetweenMembers = true,
                BracingStyle = "c",
                ElseOnClosing = false,
                IndentString = "  "
            };           
        }

        public static string ToCode(this CodeTypeDeclaration codeType)
        {
            var generatedCode = new StringBuilder();
            using (var writer = new StringWriter(generatedCode))
            {
                codeProvider.GenerateCodeFromType(codeType, writer, codeGeneratorOptions);
            }
            return generatedCode.ToString();
        }

        public static string ToCode(this CodeNamespace codeNamespace)
        {
            var generatedCode = new StringBuilder();
            using (var writer = new StringWriter(generatedCode))
            {
                codeProvider.GenerateCodeFromNamespace(codeNamespace, writer, codeGeneratorOptions);
            }
            return generatedCode.ToString();
        }
    }
}
