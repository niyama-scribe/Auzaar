using System;
using AutoFixture;
using AutoFixture.Xunit2;

namespace Auzaar.AutoFixture
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AutoTestParamsAttribute : AutoDataAttribute
    {
        public AutoTestParamsAttribute() 
            : base(() =>
            {
                var fixture = new Fixture()
                    .Customize(FixtureCustomizationsScanner.Compose());
                return fixture;
            })
        {
        }
    }
}
