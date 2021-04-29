using System;

namespace ExplicitExpansionCompanion.Attributes
{
  [AttributeUsage(AttributeTargets.Property)]
  public class ConditionalInclude : Attribute
  {
    public ConditionalInclude()
    {

    }
  }
}
