using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Conventions.Instances;

namespace TestBussiness.Convention
{
    internal class PostConvention : FluentNHibernate.Conventions.IClassConvention
    {
        public void Apply(IClassInstance instance)
        {
            throw new NotImplementedException();
        }
    }
}
