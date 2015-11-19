//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.7
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------
/*
 * Title: FANN C# DataAccessor int
 */
using FannWrapperFixed;
using System.Collections.Generic;
using FANNCSharp;
namespace FANNCSharp.Fixed
{
    /* Class: DataAccessor
       
       Provides fast access to an array of ints
    */
    public class DataAccessor : IReadOnlyList<int>, global::System.IDisposable
    {
        private global::System.Runtime.InteropServices.HandleRef swigCPtr;
        protected bool swigCMemOwn;

        internal DataAccessor(global::System.IntPtr cPtr, bool cMemoryOwn)
        {
            swigCMemOwn = cMemoryOwn;
            swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
        }

        internal static global::System.Runtime.InteropServices.HandleRef getCPtr(DataAccessor obj)
        {
            return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
        }
        internal void SetLength(int length)
        {
            ArrayCount = length;
        }

        ~DataAccessor()
        {
            Dispose();
        }
        /* Method: Dispose
        
            Destructs the accessor. Must be called manually.
        */
        public virtual void Dispose()
        {
            lock (this)
            {
                if (swigCPtr.Handle != global::System.IntPtr.Zero)
                {
                    if (swigCMemOwn)
                    {
                        swigCMemOwn = false;
                        fannfixedPINVOKE.delete_IntAccessor(swigCPtr);
                    }
                    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
                }
                global::System.GC.SuppressFinalize(this);
            }
        }
        /* Property: Item
           Parameters:
                      index - The index of the element to return
   
            Return:
                 A int at index
        */
        public int this[int index]
        {
            get
            {
                int ret = fannfixedPINVOKE.IntAccessor_Get(swigCPtr, index);
                return ret;
            }
        }

        /*  Method: GetEnumerator
            Returns an enumerator that can enumerate over the collection of ints
         */
        public IEnumerator<int> GetEnumerator()
        {
            return (IEnumerator<int>)new AccessorEnumerator<int>(this);
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new AccessorEnumerator<int>(this);
        }

        internal static DataAccessor FromPointer(SWIGTYPE_p_int t)
        {
            global::System.IntPtr cPtr = fannfixedPINVOKE.IntAccessor_FromPointer(SWIGTYPE_p_int.getCPtr(t));
            DataAccessor ret = (cPtr == global::System.IntPtr.Zero) ? null : new DataAccessor(cPtr, false);
            return ret;
        }

        /*  Property: Count
            The number of ints this object holds 
         */
        public int Count
        {
            get
            {
                return ArrayCount;
            }
        }
        internal int ArrayCount { get; set; }
    }
}
