# AttributeBasedBinding
This repository provides examples of using attribute based bindings. Rather than having to specify our IoC bindings in separate binding module classes or startup classes, 
the binding can live with the class definition. This is an improvement on the developer experience because its easier to define the binding and also easier to determine
what the binding is when examining the class definition.

## Usage
There are a number of common binding types supported.

### Supported Binding Types
#### Transient Binding (Default)
This binding type creates a new instance of the service for every consumer of the service. It is the default binding type.
```
[Bind]
public class MessageProvider : IMessageProvider
{
}
```

#### Transient Binding To Self
This binding type causes a new instance of the service to be created for every consumer of the service.
```
[BindToSelf]
public class MessageProvider
{
}
```

#### Singleton Binding
This binding type causes a single instance of the service to be created for the lifetime of the application. This instance is then shared by all consumers of the service.
```
[BindAsSingleton]
public class MessageProvider : IMessageProvider
{
}
```

#### Singleton Binding To Self
This binding type causes a single instance of the service to be created for the lifetime of the application. This instance is then shared by all consumers of the service.
```
[BindToSelfAsSingleton]
public class MessageProvider
{
}
```

#### Per Request Binding
This binding type causes a new instance of the service to be created for each request scope (in ASP.NET). This instance is then shared by all consumers of the service within the request scope.
```
[BindPerRequest]
public class MessageProvider : IMessageProvider
{
}
```

#### Per Request Binding To Self
This binding type causes a new instance of the service to be created for each request scope (in ASP.NET). This instance is then shared by all consumers of the service within the request scope.
```
[BindToSelfPerRequest]
public class MessageProvider
{
}
```

### Usage with .NET Core
You need the attributes defined in [BindAttribute.cs](https://github.com/jameschristou/AttributeBasedBinding.Net/blob/master/AttributeBasedBinding/BindAttribute.cs) and the `IServiceCollection` extension method defined in [ServiceCollectionExtensions.cs](https://github.com/jameschristou/AttributeBasedBinding.Net/blob/master/AttributeBasedBinding.NetCore/ServiceCollectionExtensions.cs). Just call `UseAttributeBasedBindings` from where you configure your services at startup and you should be good to go.

### Usage with .NET Framework & Ninject
You need the attributes defined in [BindAttribute.cs](https://github.com/jameschristou/AttributeBasedBinding.Net/blob/master/AttributeBasedBinding/BindAttribute.cs) and the `IKernel` extension method defined in [NinjectKernelExtensions.cs](https://github.com/jameschristou/AttributeBasedBinding.Net/blob/master/AttributeBasedBinding.NinjectIoc/NinjectKernelExtensions.cs). Just call `UseAttributeBasedBindings` from where you configure your services at startup and you should be good to go.

## Examples
### ASP.NET Core Example using Microsoft.Extensions.DependencyInjection
[This](https://github.com/jameschristou/AttributeBasedBinding.Net/tree/master/AttributeBasedBinding.Examples.AspNetCore) is an example project using ASP.NET Core.

### ASP.NET Framework Example with Ninject IoC Container
[This](https://github.com/jameschristou/AttributeBasedBinding.Net/tree/master/AttributeBasedBinding.Examples.AspNetNinject) is an example project using ASP.NET Web API 2 and Ninject as the IoC Container.