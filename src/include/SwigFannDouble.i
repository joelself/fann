/*SwigFannDouble.i*/
%module fanndouble
%include "doublefann.h"
%include "SwigFann.i"
%{
#include "parallel_fann.hpp"
%}
%include "std_vector.i"
%include "parallel_fann.hpp"
%apply double INPUT[]  { double* input }
%apply double INPUT[]  { double* output }
%apply double INPUT[]  { double* desired_output }
%apply double INPUT[]  { double* cascade_activation_steepnesses }
%apply double INPUT[]  { double* input_vector }
%apply double INPUT[]  { double* output_vector }
%include "fann_data.h"
%include "fann_training_data_cpp.h"
%include "fann_data_cpp.h"
%include "fann_cpp.h"
%include "parallel_fann.hpp"
namespace std {
    %template(doubleVectorVector) vector<vector<double>>;
	%template(doubleVector) vector<double>;
};
%inline %{
	typedef double* double_ptr;
    typedef double fann_type;
%}
%array_class(double, doubleArray);
%array_class(double_ptr, doubleArrayArray);
%array_accessor(double, DoubleArrayAccessor,DoubleAccessor)
%arrayarray_accessor(double, DoubleAccessor, DoubleArrayAccessor);


