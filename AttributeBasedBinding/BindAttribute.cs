using System;

namespace AttributeBasedBinding
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class AutoBindAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class AutoBindSelfAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class AutoBindSelfAsSingletonAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class AutoBindAsPerRequestAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class AutoBindAsSingletonAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class BindAttribute : Attribute
    {
        public BindAttribute(BindingType bindingType)
        {
            BindingType = bindingType;
        }

        public BindingType BindingType { get; set; }
    }

    public enum BindingType
    {
        Transient,
        Singleton
    }
}
