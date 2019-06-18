using System;
using System.Collections.Generic;
using System.Text;

namespace TestBussiness.Context
{
    public interface IContext
    {
        T New<T>();
    }
}
