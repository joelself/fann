using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FANNCSharp
{   /* Interface: IAccessor<T>
         An enumerable interface that provides a count of
         elements in the accessor and random, read-only
         access to its elements.
    */
    interface IAccessor<T> : IEnumerable<T>, IEnumerable
    {
        /* Property: Count
             The number of elements in the accessor.
        */
        int Count
        {
            get;
        }
        /* Property: Item
             Provides access to the element at index

             Parameters:
               index - the index of the item to retrieve
             
        */
        T this[int index]
        {
            get;
        }
    };
}
