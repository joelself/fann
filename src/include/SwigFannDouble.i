/*SwigFannDouble.i*/
%module fanndouble
%include "doublefann.h"
%include "SwigFann.i"
%{
#include "parallel_fann.hpp"
%}
%ignore descale_output;
%ignore descale_input;
%include "std_vector.i"
%include "parallel_fann.hpp"
%apply double INPUT[]  { double* input }
%apply double INPUT[]  { double* output }
%apply double INPUT[]  { double* desired_output }
%apply double INPUT[]  { double* cascade_activation_steepnesses }
%apply double INOUT[]  { double* descale_vector }
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


