namespace Microsoft.Extensions.DependencyInjection
{
  using ExplicitExpansionCompanion;

  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddExplicitExpansionCompanion(this IServiceCollection services)
      => services.AddSingleton(typeof(IExpansionResolver<>), typeof(ExpansionResolver<>));
  }
}