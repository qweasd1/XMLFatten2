using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using System.Reflection;

namespace CodeDomFluentHelper
{
    public static class Method
    {
        public static CodeMemberMethod AddMethod(this CodeTypeDeclaration codeType, string name)
        {
            var codeMethod = new CodeMemberMethod();
            codeMethod.Name = name;
            codeMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            codeType.Members.Add(codeMethod);
            return codeMethod;
        }
        
        
       

        //TODO: Auto Add Namespace
        public static CodeMemberMethod AddMethod(this CodeTypeDeclaration codeType, MethodInfo methodInfo, bool UseShortTypeName = false)
        {
            var codeMethod = new CodeMemberMethod();
            codeMethod.Name = methodInfo.Name;
            codeMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;

            //add Parameter
            foreach (var para in methodInfo.GetParameters())
            {
                if (UseShortTypeName)
                {
                    //use short name
                    codeMethod.Parameters.Add(new CodeParameterDeclarationExpression(para.ParameterType.Name, para.Name));
                }
                else
                {
                    // use long name
                    codeMethod.Parameters.Add(new CodeParameterDeclarationExpression(para.ParameterType, para.Name));
                }
                
            }
            //add return type
            if (UseShortTypeName)
            {
                codeMethod.ReturnType = new CodeTypeReference(methodInfo.ReturnType.Name);
            }
            else
            {
                codeMethod.ReturnType = new CodeTypeReference(methodInfo.ReturnType);
            }
           
            

            codeType.Members.Add(codeMethod);
            return codeMethod;
        }
    
        public static CodeMemberMethod DefParam(this CodeMemberMethod codeMethod, string name, Type type)
        {
            var parameter = new CodeParameterDeclarationExpression(type, name);
            codeMethod.Parameters.Add(parameter);
            return codeMethod;
        }

        public static CodeMemberMethod Return(this CodeMemberMethod codeMethod, Type type)
        {
            codeMethod.ReturnType = new CodeTypeReference(type);
            return codeMethod;
        }

        public static CodeMemberMethod Write(this CodeMemberMethod codeMethod, params CodeStatement[] statements)
        {
            codeMethod.Statements.AddRange(statements);
            return codeMethod;
        }

        public static CodeArgumentReferenceExpression Param(this CodeMemberMethod codeMethod,string name)
        {
            foreach (CodeParameterDeclarationExpression para in codeMethod.Parameters)
            {
                if (para.Name == name)
                {
                    return new CodeArgumentReferenceExpression(name);
                }
            }
            throw new ArgumentException(string.Format("method: {0} doesn't contain parameter: {1}", codeMethod.Name, name));
        }

        public static CodeVariableReferenceExpression DeclVar(this CodeMemberMethod codeMethod, string name, Type type)
        {
            var VarDecl = new CodeVariableDeclarationStatement(type, name);
            codeMethod.Statements.Add(VarDecl);
            return new CodeVariableReferenceExpression(name);
             
        }

        public static CodeVariableReferenceExpression DeclVar(this CodeMemberMethod codeMethod, string name, Type type, CodeExpression initExpression)
        {
            var VarDecl = new CodeVariableDeclarationStatement(type, name,initExpression);
            codeMethod.Statements.Add(VarDecl);
            return new CodeVariableReferenceExpression(name);

        }

    }
}
