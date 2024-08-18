# Unity Extension

## Dependency injection 
Implemented dependency injection using C# attributes and reflections. By using [Inject] Attributes we can inject dependency to a method as well as properties. The class that provides the dependency should have IDependencyProvider inheritence and use [Provide] attribute

## Service Locator 
Implemented service locator pattern to provide the dependency. You can register services and get the required services as you need. 
