/*SwigFannFloat.i*/
%module fannfloat
%include "SwigFann.i"
%{
#include "parallel_fann.hpp"
%}
%ignore descale_output;
%ignore descale_input;
%include "std_vector.i"
%apply float INPUT[]  { float* input }
%apply float INPUT[]  { float* output }
%apply float INPUT[]  { float* desired_output }
%apply float INPUT[]  { float* cascade_activation_steepnesses }
%apply float INOUT[]  { float* descale_vector }
%include "fann_data.h"
%include "fann_training_data_cpp.h"
%include "fann_data_cpp.h"
%include "fann_cpp.h"
%include "parallel_fann.hpp"
%extend FANN::neural_net {
    void descale_output_(fann_type *descale_vector) {
        $self->descale_output(descale_vector);
    }
    void descale_input_(fann_type * descale_vector) {
        $self->descale_input(descale_vector);
    }
};
namespace std {
    %template(floatVectorVector) vector<vector<float>>;
	%template(floatVector) vector<float>;
}
%inline %{
	typedef float* float_ptr;
    typedef float fann_type;
%}
%typemap(ctype) void (*)(unsigned int, unsigned int, unsigned int, float *, float *) "void *" 
%typemap(in) void (*)(unsigned int, unsigned int, unsigned int, float *, float *) %{ $1 = (void (*)(unsigned int, unsigned int, unsigned int, float *, float *))$input;%} 
%typemap(imtype, out="global::System.IntPtr") void (*)(unsigned int, unsigned int, unsigned int, float *, float *) "global::System.IntPtr" 
%typemap(cstype, out="global::System.IntPtr") void (*)(unsigned int, unsigned int, unsigned int, float *, float *) "global::System.IntPtr"
%typemap(csin) void (*)(unsigned int, unsigned int, unsigned int, float *, float *) "$csinput"
%array_class(float, floatArray);
%array_class(float_ptr, floatArrayArray);
%array_accessor(float, FloatArrayAccessor,FloatAccessor)
%arrayarray_accessor(float, FloatAccessor, FloatArrayAccessor);



