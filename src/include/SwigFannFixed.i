/*SwigFannFixed.i*/
%module fannfixed
%include "fixedfann.h"
%include "SwigFann.i"
%array_class(int, intArray);
CSHARP_ARRAYS( int*, int[] )
CSHARP_ARRAYS( fann_connection, ConnectionFixed )
%apply int INPUT[]  { int* input }
%apply int INPUT[]  { int* output }
%apply int* INPUT[]  { int** input }
%apply int* INPUT[]  { int** output }
%apply int OUTPUT[] { int* test }
%apply int OUTPUT[] { int* run }
%apply int OUTPUT[] { int* get_train_output }
%apply int OUTPUT[] { int* get_train_input }
%apply int* OUTPUT[]  { int** get_input }
%apply int* OUTPUT[]  { int** get_output }
%apply fann_connection INOUT[] { fann_connection* connections}
%typemap(out) int * %{ $result = $1; %}
%typemap(csout, excode=SWIGEXCODE) int *, int [] {
    int[] ret = $imcall;$excode
    return ret;
}
%typemap(out) int ** %{ $result = $1; %}
%typemap(csout, excode=SWIGEXCODE2) int **, int [][] %{ {
      return $imcall;
}
%}
%rename(ConnectionFixed) fann_connection;
%include "fann_data.h"
%include "fann_training_data_cpp.h"
%include "fann_data_cpp.h"
%include "fann_cpp.h"
%inline %{
    typedef int fann_type;
%}


