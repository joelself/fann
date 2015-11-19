using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FANNCSharp
{
    interface IAccessor<T> : IEnumerable<T>, IEnumerable
    {
        int Count
        {
            get;
        }

        T this[int index]
        {
            get;
        }
    };
}
