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
%array_class(float_ptr, floatArrayArray);
%array_class(float, floatArray);


