using System;
using System.Collections.Generic;
using System.Text;

namespace TestBussiness.Context
{
    [Obsolete]
    public interface IContext
    {
        T New<T>();
    }
}
