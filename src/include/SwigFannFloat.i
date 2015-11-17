/*SwigFannFloat.i*/
%module fannfloat
%include "SwigFann.i"
%{
#include "parallel_fann.hpp"
%}
%include "std_vector.i"
%typemap(ctype)   fann_connection "connection"
%array_class(fann_connection, connectionArray);
CSHARP_ARRAYS( float*, float[] )
%apply float INPUT[]  { float* input }
%apply float INPUT[]  { float* output }
%apply float INPUT[]  { float* desired_output }
%apply float INPUT[]  { float* cascade_activation_steepnesses }
%apply float INPUT[]  { float* input_vector }
%apply float INPUT[]  { float* output_vector }
%apply float* INPUT[]  { float** input }
%apply float* INPUT[]  { float** output }
%apply float OUTPUT[] { float* test }
%apply float OUTPUT[] { float* get_cascade_activation_steepnesses }
%apply float OUTPUT[] { float* run }
%apply float OUTPUT[] { float* get_train_output }
%apply float OUTPUT[] { float* get_train_input }
%apply float OUTPUT[] { float* floatArray_cast }
%apply float* OUTPUT[]  { float** get_input }
%apply float* OUTPUT[]  { float** get_output }
%typemap(out) float * %{ $result = $1; %}
%typemap(csout, excode=SWIGEXCODE) float *, float [] {
    float[] ret = $imcall;$excode
    return ret;
}
%typemap(out) float ** %{ $result = $1; %}
%typemap(csout, excode=SWIGEXCODE2) float **, float [][] %{ {
      return $imcall;
}
%}
%include "fann_data.h"
%include "fann_training_data_cpp.h"
%include "fann_data_cpp.h"
%include "fann_cpp.h"
%include "parallel_fann.hpp"
namespace std {
    %template(floatVectorVector) vector<vector<float>>;
	%template(floatVector) vector<float>;
}
%inline %{
    typedef float fann_type;
    typedef fann_connection connection;
%}
%array_class(float, floatArray);


