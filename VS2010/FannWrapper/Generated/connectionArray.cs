//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.7
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace FannWrap {

public class connectionArray : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal connectionArray(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(connectionArray obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~connectionArray() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          SwigFannPINVOKE.delete_connectionArray(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public connectionArray(int nelements) : this(SwigFannPINVOKE.new_connectionArray(nelements), true) {
  }

  public fann_connection getitem(int index) {
    fann_connection ret = new fann_connection(SwigFannPINVOKE.connectionArray_getitem(swigCPtr, index), true);
    return ret;
  }

  public void setitem(int index, fann_connection value) {
    SwigFannPINVOKE.connectionArray_setitem(swigCPtr, index, fann_connection.getCPtr(value));
    if (SwigFannPINVOKE.SWIGPendingException.Pending) throw SwigFannPINVOKE.SWIGPendingException.Retrieve();
  }

  public fann_connection cast() {
    global::System.IntPtr cPtr = SwigFannPINVOKE.connectionArray_cast(swigCPtr);
    fann_connection ret = (cPtr == global::System.IntPtr.Zero) ? null : new fann_connection(cPtr, false);
    return ret;
  }

  public static connectionArray frompointer(fann_connection t) {
    global::System.IntPtr cPtr = SwigFannPINVOKE.connectionArray_frompointer(fann_connection.getCPtr(t));
    connectionArray ret = (cPtr == global::System.IntPtr.Zero) ? null : new connectionArray(cPtr, false);
    return ret;
  }

}

}
