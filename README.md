# Auzaar

![Nuget](https://img.shields.io/nuget/v/Auzaar.AutoFixture?color=blue&label=Auzaar.AutoFixture&logo=Nuget&style=plastic)
![GitHub Workflow Status](https://img.shields.io/github/workflow/status/niyama-scribe/SchemaTypist/Build%20&%20Test?style=plastic)


Auzaar is a collection of CSharp tools that help you ramp up your productivity in various aspects of programming.

The first tool in this collection is **Auzaar.AutoFixture**.

AutoFixture is a fantastic test data generator for your unit tests.  Combining AutoFixture with Moq
and XUnit2, you can certainly reduce the friction in test-driven development.  

That is, of course, until you have to customize your test data generator.  And if you're like me, you do tend to customize a fair bit.

Auzaar.AutoFixture is a set of utility classes and interfaces that helps you customize AutoFixture easily and ramp up your productivity again.

## How does it work?

If you wish to use AutoFixture with Moq, you're best advised to install the AutoFixture.AutoMoq nuget package.
You are then required to create custom attributes that extend AutoDataAttribute to add the AutoMoqCustomization.


Instead, just reference Auzaar.AutoFixture.  It creates all the basic attributes for you.  It also provides base classes implementing ISpecimenBuilder that you can use.

Focus on your AutoFixture customizations.  Use Auzaar.AutoFixture for the rest. 

## Where do I start?

 - Add the Auzaar.Autofixture nuget package to your project
   ```commandline
     dotnet add package Auzaar.AutoFixture
   ```
 
  - Implement `IAutoFixtureCustomizationsSpecifier`, like so:
    ```csharp
    internal class FixtureCustomizer : IAutofixtureCustomizationsSpecifier
    {
        public IEnumerable<ICustomization> SpecifyCustomizations()
        {
            //These can be any implementation of ICustomization or ISpecimenBuilder
            //Choose to extend CustomTypeSpecimenBuilderBase<T> to make it even easier!
            var l = new List<ICustomization>()
            {
                new FakesCustomization(),
                new FileSystemWrapperSpecimenBuilder().ToCustomization(),
                new AutoMoqCustomization()
            };
            return l;
        }
    }
    ```
  
  - Almost there!  Now go ahead and use the method attributes available to you, when writing your tests.
    ```csharp
    [Theory]
    [AutoTestParams]
    internal void GetSqlVendor_WithOneRegisteredImpl_ThenUseIt(
            [Frozen] Mock<IPluginLoader> pluginLoader, 
            [Frozen] Mock<ISqlVendor> sqlVendor, 
            SqlVendorPluginLoader sut)
    {
        //Arrange
        sqlVendor.SetupGet(sv => sv.VendorType).Returns(SqlVendorType.PostgreSql);
        pluginLoader.Setup(pl => pl.FindPlugins<ISqlVendor>(It.IsAny<string>(), typeof(SqlVendorDefinition))).Returns(new [] {sqlVendor.Object});
            
        //Act
        sut.LoadRegisteredVendors();
        var sv = sut.GetSqlVendor(SqlVendorType.MicrosoftSqlServer);

        //Assert    
        sv.Should().Be(sqlVendor.Object);
        sv.VendorType.Should().Be(SqlVendorType.PostgreSql);
    }
   ```

