/*SwigFannDouble.i*/
%module fanndouble
%include "doublefann.h"
%include "SwigFann.i"
%{
#include "parallel_fann.hpp"
%}
%include "std_vector.i"
%include "parallel_fann.hpp"
namespace std {
    %template(doubleVectorVector) vector<vector<double>>;
	%template(doubleVector) vector<double>;
};
%inline %{
	typedef double* double_ptr;
    typedef double fann_type;
%}
%array_class(double_ptr, doubleArrayArray);
%array_class(double, doubleArray);


