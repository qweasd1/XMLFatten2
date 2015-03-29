using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;

namespace CodeDomFluentHelper
{
    public static class stat
    {
        public static CodeMethodReturnStatement Return(CodeExpression statment)
        {
            return new CodeMethodReturnStatement(statment);
        }

        public static CodeMethodReturnStatement Return()
        {
            return new CodeMethodReturnStatement();
        }

        public static CodeAssignStatement Assign(CodeExpression left, CodeExpression right)
        {
            return new CodeAssignStatement(left, right);
        }
    }
}
