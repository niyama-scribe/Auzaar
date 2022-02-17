using System;
using AutoFixture.Xunit2;

namespace Auzaar.AutoFixture;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class AutoTestParamsInlineData : InlineAutoDataAttribute
{
    public AutoTestParamsInlineData(params object[] values) 
        : base(new AutoTestParamsAttribute(), values)
    {
    }
}