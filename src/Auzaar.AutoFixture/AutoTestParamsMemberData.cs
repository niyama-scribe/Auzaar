using System;
using AutoFixture.Xunit2;

namespace Auzaar.AutoFixture
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class AutoTestParamsMemberData : MemberAutoDataAttribute
    {
        public AutoTestParamsMemberData(string memberName, params object[] parameters) 
            : base(new AutoTestParamsAttribute(), memberName, parameters)
        {
        }
    }
}
