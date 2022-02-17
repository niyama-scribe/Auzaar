using System;
using System.Collections.Generic;
using AutoFixture;

namespace Auzaar.AutoFixture
{
    /// <summary>
    /// This is the only interface that the consumer of the library has to implement.
    /// This allows you to specify the Autofixture customizations to be used for your tests.
    /// </summary>
    public interface IAutofixtureCustomizationsSpecifier
    {
        IEnumerable<ICustomization> SpecifyCustomizations();
    }

    internal class DefaultAutofixtureCustomizationsSpecifier : IAutofixtureCustomizationsSpecifier
    {
        public IEnumerable<ICustomization> SpecifyCustomizations()
        {
            return new List<ICustomization>()
            {
            };
        }
    }
}
