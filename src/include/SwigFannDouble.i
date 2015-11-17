/*SwigFannDouble.i*/
%module fanndouble
%include "doublefann.h"
%include "SwigFann.i"
%{
#include "parallel_fann.hpp"
%}
%include "std_vector.i"
%array_class(double, doubleArray);
CSHARP_ARRAYS( double*, double[] )
CSHARP_ARRAYS( fann_connection, ConnectionDouble )
%apply double INPUT[]  { double* input }
%apply double INPUT[]  { double* output }
%apply double INPUT[]  { double* desired_output }
%apply double INPUT[]  { double* cascade_activation_steepnesses }
%apply double INPUT[]  { double* input_vector }
%apply double INPUT[]  { double* output_vector }
%apply double* INPUT[]  { double** input }
%apply double* INPUT[]  { double** output }
%apply double OUTPUT[] { double* test }
%apply double OUTPUT[] { double* get_cascade_activation_steepnesses }
%apply double OUTPUT[] { double* run }
%apply double OUTPUT[] { double* get_train_output }
%apply double OUTPUT[] { double* get_train_input }
%apply double* OUTPUT[]  { double** get_input }
%apply double* OUTPUT[]  { double** get_output }
%apply fann_connection INOUT[] { fann_connection* connections}
%typemap(out) double * %{ $result = $1; %}
%typemap(csout, excode=SWIGEXCODE) double *, double [] {
    double[] ret = $imcall;$excode
    return ret;
}
%typemap(out) double ** %{ $result = $1; %}
%typemap(csout, excode=SWIGEXCODE2) double **, double [][] %{ {
      return $imcall;
}
%}
%rename(ConnectionDouble) fann_connection;
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
    typedef double fann_type;
%}


