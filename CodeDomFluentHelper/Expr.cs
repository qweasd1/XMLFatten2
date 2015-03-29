using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;

namespace CodeDomFluentHelper
{
    public static class expr
    {
        public static CodePrimitiveExpression Const(object value)
        {
            return new CodePrimitiveExpression(value);
        }
        public static CodeBinaryOperatorExpression Binary(CodeExpression left, CodeBinaryOperatorType op, CodeExpression right)
        {
            return new CodeBinaryOperatorExpression(left, op, right);
        }
    }
}
