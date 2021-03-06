using System;

namespace AttributeBasedBinding
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class BindAttribute : Attribute
    {
        public BindAttribute()
        {
            BindingType = BindingType.AsTransient;
        }

        public BindAttribute(BindingType bindingType)
        {
            BindingType = bindingType;
        }

        public BindingType BindingType { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class BindToSelfAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class BindToSelfAsSingletonAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class BindAsSingletonAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class BindToSelfPerRequestAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class BindPerRequestAttribute : Attribute
    {
    }

    public enum BindingType
    {
        AsTransient,
        ToSelfAsTransient,
        AsSingleton,
        ToSelfAsSingleton,
    }
}
