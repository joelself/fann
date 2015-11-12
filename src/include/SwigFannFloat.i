/*SwigFannFloat.i*/
%module fannfloat
%include "SwigFann.i"
%{
#include "parallel_fann.hpp"
%}
%include "std_vector.i"
%include "parallel_fann.hpp"
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

%array_class(float_ptr, floatArrayArray);
%array_class(float, floatArray);


