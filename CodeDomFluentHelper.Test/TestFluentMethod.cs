using System;
using CodeDomFluentHelper;
using System.CodeDom;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeDomFluentHelper.Test
{
    [TestClass]
    public class TestFluentMethod
    {
        [TestMethod]
        public void TestAddMethod()
        {
            var codeType = new CodeTypeDeclaration("testClass");
            codeType.Implements(typeof(IDisposable));
            var codeMethod = codeType.AddMethod("testMethod");
            codeMethod.DefParam("x", typeof(int)).DefParam("y", typeof(int)).Return(typeof(int));
            var x = codeMethod.Param("x");
            var y = codeMethod.Param("y");
            var t1 = codeMethod.DeclVar("t1",typeof(string));
            codeMethod.Write(
                stat.Assign(t1,expr.Const("test")),
                stat.Return(expr.Binary(x,CodeBinaryOperatorType.Add,y))
                );
            
           

            var code = codeType.ToCode();
        }
    }
}
