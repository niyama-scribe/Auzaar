using System;
using AutoFixture.Kernel;

namespace Auzaar.AutoFixture
{
    public abstract class CustomTypeSpecimenBuilderBase<T> : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is not Type type || type != typeof(T)) return new NoSpecimen();
            return BuildSpecimen();
        }

        protected abstract T BuildSpecimen();
    }
}
