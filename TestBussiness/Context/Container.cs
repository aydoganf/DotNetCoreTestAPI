using System;
using System.Collections.Generic;
using System.Text;

namespace TestBussiness.Context
{
    [Obsolete]
    public class Container : IContainer
    {
        public object GetInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}
