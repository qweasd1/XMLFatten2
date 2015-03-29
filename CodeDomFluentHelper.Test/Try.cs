using System;
using System.CodeDom;
using System.Reflection;
using CodeDomFluentHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeDomFluentHelper.Test
{
    [TestClass]
    public class Try
    {
        [TestMethod]
        public void TestCreateClassImplementInterface()
        {
            var nspace = Namespace.New("TestNamespace");
            nspace.Using("System");

            var @class = nspace.AddClass("testClass"); 
            @class.BaseTypes.Add(new CodeTypeReference(typeof(IDisposable)));
            var code = nspace.ToCode();


            var methodRef = new CodeMethodReferenceExpression(null, "test");
            var methodInvoke = new CodeMethodInvokeExpression(methodRef, expr.Const("test"));

            
        }
    }
}
