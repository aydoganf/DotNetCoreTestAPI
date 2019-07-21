using System;
using System.Collections.Generic;
using System.Text;

namespace TestBussiness.Context
{
    [Obsolete]
    public interface IContainer
    {
        object GetInstance(Type type);
    }
}
