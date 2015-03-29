using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using System.Linq.Expressions;

namespace CodeDomFluentHelper
{
    public static class Class
    {
        public static CodeTypeDeclaration AddClass(this CodeNamespace nspace,string name)
        {
            var codeType = new CodeTypeDeclaration(name);
            nspace.Types.Add(codeType);
            return codeType;
        }

        public static CodeTypeDeclaration AddClass(this CodeNamespace nspace, string name, params Type[] baseTypes)
        {
            var codeType = new CodeTypeDeclaration(name);
            codeType.BaseTypes.AddRange(baseTypes.Select(x => new CodeTypeReference(x)).ToArray());
            nspace.Types.Add(codeType);
            return codeType;
        }

        public static CodeTypeDeclaration Implements(this CodeTypeDeclaration codeType, params Type[] baseTypes)
        {
            codeType.BaseTypes.AddRange(baseTypes.Select(x => new CodeTypeReference(x)).ToArray());
            return codeType;
        }

        //TODO:: use reflection to found all members who are abstract and use them to determine our MemberAttributes
        public static void Implements<TBase>(this CodeTypeDeclaration codeType, bool UseShortName = false)
        {
            var baseType = typeof(TBase);
            codeType.BaseTypes.Add(new CodeTypeReference(baseType.Name));

            var methodsNeedToImplement = baseType.GetMethods().Where(mInfo => mInfo.IsAbstract == true && mInfo.IsSpecialName == false);
            foreach (var mInfo in methodsNeedToImplement)
            {
                CodeDomFluentHelper.Method.AddMethod(codeType, mInfo, UseShortName);
            }

        }

        //TODO: have bug, if the method have the same name but diff params, it will throw exception
        public static CodeMemberMethod Method(this CodeTypeDeclaration codeType, string name)
        {
            return codeType.Members.OfType<CodeMemberMethod>().Where(x => x.Name == name).Single();
        }
    }
}
