using System;
using System.Reflection;

namespace AttributeBasedBinding.Examples.AspNetNinject.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}