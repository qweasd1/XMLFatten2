using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;

namespace CodeDomFluentHelper
{
    public static class Namespace
    {
        public static CodeNamespace New(string name)
        {
            return new CodeNamespace(name);
        }

        public static void Using(this CodeNamespace nspace,params string[] namespaces)
        {            
            nspace.Imports.AddRange(namespaces.Select(x=>new CodeNamespaceImport(x)).ToArray());
        }
    }
}
