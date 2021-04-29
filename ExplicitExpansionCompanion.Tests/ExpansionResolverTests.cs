using System;
using System.Collections.Generic;
using ExplicitExpansionCompanion.Attributes;
using ExplicitExpansionCompanion.Models;
using Microsoft.Extensions.Options;
using Xunit;

namespace ExplicitExpansionCompanion.Tests
{
  public class ExpansionResolverTests
  {
    [Fact]
    public void CanFindAttribute()
    {
      //Given
      var includes = new ExpansionResolver<MockDtoIncludes>(new GenericExpansionResolverOptions());
      var toFind = new string[] { nameof(MockDtoIncludes.ToFind) };
      //When
      var result = includes.Resolve(toFind);
      //Then
      Assert.NotEmpty(result);
    }

    [Fact]
    public void ResolverReturnsEmptyArrayWhenNotFound()
    {
      //Given
      var includes = new ExpansionResolver<MockDtoIncludes>(new GenericExpansionResolverOptions());
      var toFind = new string[] { nameof(MockDtoIncludes.Name) };
      //When
      var result = includes.Resolve(toFind);
      //Then
      Assert.Empty(result);
    }

    [Fact]
    public void ResolverReturnsEmptyArrayWhenNotFoundEmptyList()
    {
      //Given
      var includes = new ExpansionResolver<MockDtoIncludes>(new GenericExpansionResolverOptions());
      var toFind = new string[] { };
      //When
      var result = includes.Resolve(toFind);
      //Then
      Assert.Empty(result);
    }

    [Fact]
    public void ResolverReturnsEmptyArrayWhenNotFoundNull()
    {
      //Given
      var includes = new ExpansionResolver<MockDtoIncludes>(new GenericExpansionResolverOptions());
      //When
      //Then
      Assert.Throws<NullReferenceException>(() => includes.Resolve(null));
    }

    [Fact]
    public void ResolverNoAttributes()
    {
      //Given
      var includes = new ExpansionResolver<MockDtoNoIncludes>(new GenericExpansionResolverOptions());
      var toFind = new string[] { };
      //When
      var result = includes.Resolve(toFind);
      //Then
      Assert.Empty(result);
    }

    [Fact]
    public void ResolverIgnoresCaseByDefault()
    {
      //Given
      var includes = new ExpansionResolver<MockDtoIncludes>(new GenericExpansionResolverOptions());
      var toFind = new string[] { "tofind" };
      //When
      var result = includes.Resolve(toFind);
      //Then
      Assert.NotEmpty(result);
    }

    [Fact]
    public void ResolverEvaluatesCaseWhenConfigured()
    {
      //Given
      var includes = new ExpansionResolver<MockDtoIncludes>(new GenericExpansionResolverOptions(caseSensitive: true));
      var toFind = new string[] { "tofind" };
      //When
      var result = includes.Resolve(toFind);
      //Then
      Assert.Empty(result);
    }

    [Fact]
    public void ResolverThrowsErrorsWhenConfigured()
    {
      //Given
      var includes = new ExpansionResolver<MockDtoIncludes>(new GenericExpansionResolverOptions(throwErrors: true));
      var toFind = new string[] { nameof(MockDtoIncludes.Name) };
      //When
      //Then
      Assert.Throws<KeyNotFoundException>(() => includes.Resolve(toFind));
    }
  }

  public class MockDtoNoIncludes
  {
    public string Name { get; set; }
  }

  public class MockDtoIncludes
  {
    public string Name { get; set; }
    [ConditionalInclude]
    public string ToFind { get; set; }
  }

  public class GenericExpansionResolverOptions : IOptions<ExpansionResolverOptions>
  {
    public ExpansionResolverOptions Value { get; }
    public GenericExpansionResolverOptions(bool caseSensitive = false, bool throwErrors = false)
    {
      Value = new ExpansionResolverOptions()
      {
        CaseSensitive = caseSensitive,
        ThrowExceptions = throwErrors
      };
    }
  }
}