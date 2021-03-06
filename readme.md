# AttributeBasedBinding
This repository provides examples of using attribute based bindings. Rather than having to specify our IoC bindings in separate binding module classes or startup classes, 
the binding can live with the class definition. This is an improvement on the developer experience because its easier to define the binding and also easier to determine
what the binding is when examining the class definition.

## Usage
There are a number of common binding types supported.

### Transient Binding (Default)
This binding type causes a new instance of the service to be created and is the default binding type.
```
[Bind]
public class MessageProvider : IMessageProvider
{
}
```

### Transient Binding To Self
This binding type causes a new instance of the service to be created and is the default binding type.
```
[BindToSelf]
public class MessageProvider
{
}
```


## ASP.NET Framework Example with Ninject IoC Container
There is an example project using ASP.NET Web API 2 and Ninject as the IoC Container.