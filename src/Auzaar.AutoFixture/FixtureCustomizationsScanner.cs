using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoFixture;

namespace Auzaar.AutoFixture
{
    /// <summary>
    /// This class serves as the registry for autofixture customizations.
    /// It will be used by 
    /// </summary>
    public class FixtureCustomizationsScanner
    {
        private static readonly Lazy<List<ICustomization>> Registry = new(DiscoverCustomizations);

        private static List<ICustomization> DiscoverCustomizations()
        {
            var matchingTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes()
                    .Where(t => typeof(IAutofixtureCustomizationsSpecifier).IsAssignableFrom(t)));
            var type = matchingTypes.First() ?? typeof(DefaultAutofixtureCustomizationsSpecifier);
            if (Activator.CreateInstance(type) is IAutofixtureCustomizationsSpecifier customizationsProvider)
                return customizationsProvider.SpecifyCustomizations().ToList();
            return new List<ICustomization>();
        }

        public static CompositeCustomization Compose()
        {
            //If this is the first attempt
            return new CompositeCustomization(Registry.Value);
        }


    }
}
