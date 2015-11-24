//------------------------------------------------------------------------------
/*
 * Title: FANN C# AccessorEnumerator<T>
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace FANNCSharp
{
    /* Class: AccessorEnumerator<T>
         An Enumerator for the <IAccessor<T>>
         interface
    */
    public class AccessorEnumerator<T> : IEnumerator<T>
    {
        internal AccessorEnumerator(IAccessor<T> accessor)
        {
            CurrentIndex = 0;
        }

        /* Property: Current
           
           Returns the item in the collection currently being
           referenced by the internal reference
        */
        public T Current
        {
            get
            {
                return Accessor[CurrentIndex];
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Accessor[CurrentIndex];
            }
        }

        /* Method: Dispose
        
           Disposes the enumerator
        */
        public void Dispose()
        {
            return;
        }

        /* Method: MoveNext
        
           Moves the internal reference to the next item
           in the collection
        */
        public bool MoveNext()
        {
            CurrentIndex++;
            return CurrentIndex < Accessor.Count;
        }

        /* Method: Reset
        
           Resets the internal reference to the first item in
           the collection
        */
        public void Reset()
        {
            CurrentIndex = 0;
        }
        private IAccessor<T> Accessor { get; set; }
        private int CurrentIndex { get; set; }
    }
}
