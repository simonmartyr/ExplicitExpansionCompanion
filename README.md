
# ExplicitExpansionCompanion
ExplicitExpansionCompanion is generic resolver for data transfer objects that
have explicit expansions. This libary is a perfect addition to an application that 
is using [Automapper](https://github.com/AutoMapper/AutoMapper) and wish to design an API
where the user can specify in their requests which expansions they require.

## What is ExplicitExpansion?

Sometimes a resource maybe linked to another resource, for example a `Person` and their `Pet`.
When querying information on a `Person` it might be nessary to pull information on the `Pet`.
ExplicitExpansion allows you to control this by stating which nested objects should be included.

Get Person Example (without expanding pet)
```json
{
    "Id": "1",
    "Name": "Simon",
    "Pet": Null
}
```

Get Person Example (with expanding pet)
```json
{
    "Id": "1",
    "Name": "Simon",
    "Pet": {
        "Name" : "Fluffy"
    }
}
```
This can be used with AutoMapper's Queryable Extensions to ensure your query does not pull 
too much data.  

[AutoMapperDocs](https://docs.automapper.org/en/stable/Queryable-Extensions.html#explicit-expansion)




## Installation 

Install with NuGet

```bash 
  dotnet add package ExplicitExpansionCompanion
```

In your startup.
    
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddExplicitExpansionCompanion();
}
```
## Usage/Examples

Whenever you need the companion, just inject into your class and use the `Resolve` method.

```csharp
private readonly IExpansionResolver<YourDto> _resolver;
public MyClass(IExpansionResolver<YourDto> resolver)
{
    _resolver = resolver;
}

public void MyMethod(string[] listOfTerms)
{
    var includes = _resolver.Resolve(listOfTerms);
}
```

Automapper Example

```csharp

private readonly IExpansionResolver<YourDto> _resolver;
private readonly IMapper _mapper;
public MyClass(IExpansionResolver<YourDto> resolver, IMapper mapper)
{
    _resolver = resolver;
    _mapper = mapper;
}

public void MyMethod(string[] listOfTerms)
{
    var includes = _resolver.Resolve(listOfTerms);

    var resultOfQuery = Query
    .ProjectTo<YourDto>(_mapper.ConfigurationProvider, null, includes.ToArray());
}
```
## Authors

- [@simonmartyr](https://github.com/simonmartyr)

  
## Demo

[Demo Project](https://github.com/simonmartyr/throwaway-conditional-associations)

  
## Feedback

If you have any feedback, please reach out to us at 

- [@vintage_si](https://twitter.com/vintage_si)

  